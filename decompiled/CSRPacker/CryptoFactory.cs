// Decompiled with JetBrains decompiler
// Type: CSRPacker.CryptoFactory
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

namespace CSRPacker
{
  public static class CryptoFactory
  {
    public static CryptoProvider GetCryptoProvider(CsrGame game) => (CryptoProvider) new Csr2CryptoProvider();
  }
}
