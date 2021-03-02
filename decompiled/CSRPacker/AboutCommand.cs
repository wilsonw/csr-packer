// Decompiled with JetBrains decompiler
// Type: CSRPacker.AboutCommand
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using ManyConsole;
using System;

namespace CSRPacker
{
  internal class AboutCommand : ConsoleCommand
  {
    public AboutCommand() => this.IsCommand("about", "Author info");

    public override int Run(string[] remainingArguments)
    {
      Console.WriteLine("Created by alexx999 for 4pda.ru users");
      return 0;
    }
  }
}
