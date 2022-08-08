using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BinarySearchApp
{
	public struct S { int A; }

	public class Program
	{
		static void Main()
		{
			object[] s = new object[2];
			s[0] = new S();
			s[1] = s[0];
			Console.WriteLine(s[0] == s[1]);
		}
	}
}