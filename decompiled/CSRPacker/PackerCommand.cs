// Decompiled with JetBrains decompiler
// Type: CSRPacker.PackerCommand
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using ManyConsole;
using System;

namespace CSRPacker
{
  internal abstract class PackerCommand : ConsoleCommand
  {
    public string InputPath { get; set; }

    public string OutputPath { get; set; }

    public CsrGame Game { get; set; } = CsrGame.Racing2;

    protected PackerCommand()
    {
      this.HasRequiredOption("i|input=", "Specify input file", (Action<string>) (s => this.InputPath = s));
      this.HasOption("o|output=", "Specify output file", (Action<string>) (s => this.OutputPath = s));
      this.HasOption("g|game=", "Specify target game", (Action<string>) (s => this.Game = this.ParseGame(s)));
    }

    private CsrGame ParseGame(string s)
    {
      string lowerInvariant = s.ToLowerInvariant();
      if (lowerInvariant == "csr" || lowerInvariant == "racing")
        return CsrGame.Racing;
      if (lowerInvariant == "classic" || lowerInvariant == "classics")
        return CsrGame.Classics;
      return lowerInvariant == "csr2" || lowerInvariant == "racing2" ? CsrGame.Racing2 : CsrGame.Unknown;
    }

    public override int? OverrideAfterHandlingArgumentsBeforeRun(string[] remainingArguments)
    {
      if (this.Game != CsrGame.Unknown)
        return base.OverrideAfterHandlingArgumentsBeforeRun(remainingArguments);
      Console.WriteLine("Game name not recognized.\nAvailable options: csr, csr2, classic");
      return new int?(2);
    }
  }
}
