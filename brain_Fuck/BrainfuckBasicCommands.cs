using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => {  write((char)b.Memory[b.MemoryPointer]); });
			vm.RegisterCommand('+', b => { unchecked { b.Memory[b.MemoryPointer]++; } });
			vm.RegisterCommand('-', b => { unchecked { b.Memory[b.MemoryPointer]--; } });
			vm.RegisterCommand(',', b => 
			{
				var count = read();
				if (b.Memory[b.MemoryPointer] != 0)
					b.MemoryPointer++;
				for (byte i = 0; i < count; i++)
					b.Memory[b.MemoryPointer]++;
			});
			vm.RegisterCommand('>', b =>
			{
				b.MemoryPointer++;
				if (b.MemoryPointer >= b.Memory.Length)
					b.MemoryPointer = 0;
			});
			vm.RegisterCommand('<', b => 
			{ 
				b.MemoryPointer--;
				if (b.MemoryPointer < 0)
					b.MemoryPointer = b.Memory.Length - 1;
			});
			var massiveCommands = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
			foreach (var c in massiveCommands)
				vm.RegisterCommand(c, b => 
				{
					var count = (int)c;
					if (b.Memory[b.MemoryPointer] != 0)
						b.MemoryPointer++;
					for (byte i = 0; i < count; i++)
						b.Memory[b.MemoryPointer]++;
				});
		}
	}
}