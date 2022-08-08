using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var loopBoundaries = new Dictionary<int,int>();
			var start = new Stack<int>();

			for (int i = 0; i < vm.Instructions.Length; i++)
				if (vm.Instructions[i] == '[')
					start.Push(i);
				else if (vm.Instructions[i] == ']')
				{
					loopBoundaries.Add(i, start.Peek());
					loopBoundaries.Add(start.Pop(), i);
				}

			vm.RegisterCommand('[', b => 
			{
				if (vm.Memory[vm.MemoryPointer] == 0)
					vm.InstructionPointer = loopBoundaries[vm.InstructionPointer];
			});
			vm.RegisterCommand(']', b => 
			{
				if (vm.Memory[vm.MemoryPointer] != 0)
					vm.InstructionPointer = loopBoundaries[vm.InstructionPointer];
			});
		}
	}
}