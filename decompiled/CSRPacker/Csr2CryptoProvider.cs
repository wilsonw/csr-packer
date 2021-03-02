// Decompiled with JetBrains decompiler
// Type: CSRPacker.Csr2CryptoProvider
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using System.Text;

namespace CSRPacker
{
  public class Csr2CryptoProvider : CryptoProvider
  {
    public override byte[] GetEncryptionKey(bool online) => Encoding.UTF8.GetBytes(online ? "UDMZr24F" : "4cPw3ZyC");
  }
}
