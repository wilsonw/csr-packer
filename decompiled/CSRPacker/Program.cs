// Decompiled with JetBrains decompiler
// Type: CSRPacker.Program
// Assembly: CSRPacker, Version=0.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A17A4B6B-F3E5-476C-8BE5-01C84293E76F
// Assembly location: \\VBOXSVR\CSR_Packer\CSR Packer\CSRPacker.exe

using ManyConsole;
using System;
using System.Collections.Generic;

namespace CSRPacker
{
  internal static class Program
  {
    private static int Main(string[] args) => ConsoleCommandDispatcher.DispatchCommand(Program.GetCommands(), args, Console.Out);

    private static IEnumerable<ConsoleCommand> GetCommands() => ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof (Program));
  }
}
