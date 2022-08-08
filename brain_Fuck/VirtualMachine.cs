using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public Dictionary<char, Action<IVirtualMachine>> Command { get; set; }

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = string.Copy(program);
			Memory = new byte[memorySize];
			Command = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			Command.Add(symbol, execute);
		}

		public void Run()
		{
			var length = Instructions.Length;
			for(; InstructionPointer < length; InstructionPointer++)
            {
				if (Command.ContainsKey(Instructions[InstructionPointer]))
					Command[Instructions[InstructionPointer]](this);
			}
		}
	}
}