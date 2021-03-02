// Decompiled with JetBrains decompiler
// Type: CSRPacker.UnpackSaveCommand
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace CSRPacker
{
  internal class UnpackSaveCommand : PackerCommand
  {
    public bool Prettify { get; set; }

    public UnpackSaveCommand()
    {
      this.IsCommand("unpack", "Used to unpack save files");
      this.HasOption("p|prettify", "Prettify resulting JSON file", (Action<string>) (v => this.Prettify = v != null));
    }

    public override int Run(string[] remainingArguments)
    {
      if (this.OutputPath == null)
        this.OutputPath = this.InputPath + ".txt";
      CryptoProvider cryptoProvider = CryptoFactory.GetCryptoProvider(this.Game);
      CryptoResult cryptoResult;
      try
      {
        cryptoResult = CryptoFileReader.ReadFile(this.InputPath, cryptoProvider);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Can't open input file");
        return 1;
      }
      FileStream fileStream;
      try
      {
        fileStream = File.Open(this.OutputPath, FileMode.Create, FileAccess.Write);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Can't create output file");
        return 1;
      }
      if (!cryptoResult.IsSignatureValid)
        Console.WriteLine("Warning: signature is invalid");
      string str = cryptoResult.Result;
      if (this.Prettify)
      {
        try
        {
          str = this.GetPrettyString(str);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Warning: data is not valid JSON");
        }
      }
      byte[] bytes = Encoding.UTF8.GetBytes(str);
      fileStream.Write(bytes, 0, bytes.Length);
      fileStream.Close();
      return 0;
    }

    private string GetPrettyString(string str) => JsonConvert.SerializeObject(JsonConvert.DeserializeObject(str), Formatting.Indented);
  }
}
