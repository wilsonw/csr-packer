// Decompiled with JetBrains decompiler
// Type: CSRPacker.CryptoResult
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

namespace CSRPacker
{
  public class CryptoResult
  {
    public CryptoResult(string result, bool isValid)
    {
      this.Result = result;
      this.IsSignatureValid = isValid;
    }

    public string Result { get; }

    public bool IsSignatureValid { get; }
  }
}
