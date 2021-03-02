// Decompiled with JetBrains decompiler
// Type: CSRPacker.PackSaveCommand
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace CSRPacker
{
  internal class PackSaveCommand : PackerCommand
  {
    public bool Minify { get; set; }

    public PackSaveCommand()
    {
      this.IsCommand("pack", "Used to pack save files");
      this.HasOption("m|minify", "Minify resulting JSON file", (Action<string>) (v => this.Minify = v != null));
    }

    public override int Run(string[] remainingArguments)
    {
      if (this.OutputPath == null)
        this.OutputPath = this.InputPath + ".sav";
      FileStream fileStream;
      try
      {
        fileStream = File.OpenRead(this.InputPath);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Can't open input file");
        return 1;
      }
      byte[] numArray = new byte[fileStream.Length];
      fileStream.Read(numArray, 0, (int) fileStream.Length);
      fileStream.Close();
      string minifiedString = Encoding.UTF8.GetString(numArray, 0, numArray.Length);
      if (this.Minify)
      {
        try
        {
          minifiedString = this.GetMinifiedString(minifiedString);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Warning: data is not valid JSON");
        }
      }
      CryptoProvider cryptoProvider = CryptoFactory.GetCryptoProvider(this.Game);
      try
      {
        CryptoFileWriter.Write(this.OutputPath, minifiedString, false, cryptoProvider);
      }
      catch (IOException ex)
      {
        Console.WriteLine("Can't create output file");
        return 1;
      }
      return 0;
    }

    private string GetMinifiedString(string str) => JsonConvert.SerializeObject(JsonConvert.DeserializeObject(str), Formatting.None);
  }
}
