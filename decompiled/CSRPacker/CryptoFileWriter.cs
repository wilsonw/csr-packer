// Decompiled with JetBrains decompiler
// Type: CSRPacker.CryptoFileWriter
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using System.IO;
using System.IO.Compression;
using System.Text;

namespace CSRPacker
{
  internal class CryptoFileWriter
  {
    public static void Write(string path, string data, bool overwrite, CryptoProvider provider)
    {
      FileMode mode = overwrite ? FileMode.Create : FileMode.CreateNew;
      string signature = provider.GetSignature(data, false);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(signature);
      stringBuilder.Append('\n');
      stringBuilder.Append(data);
      byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
      using (FileStream fileStream = File.Open(path, mode))
      {
        using (GZipStream gzipStream = new GZipStream((Stream) fileStream, CompressionMode.Compress, false))
          gzipStream.Write(bytes, 0, bytes.Length);
      }
    }
  }
}
