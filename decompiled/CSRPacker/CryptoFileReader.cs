// Decompiled with JetBrains decompiler
// Type: CSRPacker.CryptoFileReader
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CSRPacker
{
  public static class CryptoFileReader
  {
    public static CryptoResult ReadFile(string path, CryptoProvider provider)
    {
      byte[] buffer;
      using (FileStream fileStream = File.OpenRead(path))
      {
        using (GZipStream gzipStream = new GZipStream((Stream) fileStream, CompressionMode.Decompress, false))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            gzipStream.CopyTo((Stream) memoryStream);
            buffer = memoryStream.GetBuffer();
          }
        }
      }
      string str1 = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
      int length = str1.IndexOf("\n", StringComparison.Ordinal);
      string str2 = str1.Substring(length + 1);
      if (length == -1)
        return new CryptoResult(str2, false);
      string strB = str1.Substring(0, length);
      bool isValid = string.Compare(provider.GetSignature(str2, false), strB, StringComparison.Ordinal) == 0;
      return new CryptoResult(str2, isValid);
    }
  }
}
