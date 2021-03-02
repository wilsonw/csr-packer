// Decompiled with JetBrains decompiler
// Type: CSRPacker.CryptoProvider
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using System.Security.Cryptography;
using System.Text;

namespace CSRPacker
{
  public abstract class CryptoProvider
  {
    public abstract byte[] GetEncryptionKey(bool online);

    public string GetSignature(string data, bool online)
    {
      using (HMACSHA1 hmacshA1 = new HMACSHA1(this.GetEncryptionKey(online)))
        return CryptoProvider.HexStr(hmacshA1.ComputeHash(Encoding.UTF8.GetBytes(data)));
    }

    private static string HexStr(byte[] p)
    {
      char[] chArray = new char[p.Length * 2];
      int index1 = 0;
      int index2 = 0;
      while (index1 < p.Length)
      {
        byte num1 = (byte) ((uint) p[index1] >> 4);
        chArray[index2] = num1 > (byte) 9 ? (char) ((int) num1 + 87) : (char) ((int) num1 + 48);
        byte num2 = (byte) ((uint) p[index1] & 15U);
        int num3;
        chArray[num3 = index2 + 1] = num2 > (byte) 9 ? (char) ((int) num2 + 87) : (char) ((int) num2 + 48);
        ++index1;
        index2 = num3 + 1;
      }
      return new string(chArray);
    }
  }
}
