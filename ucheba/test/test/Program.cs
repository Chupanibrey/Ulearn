using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 101; i++)
            {
                if ((i % 3) == 0 || (i % 3) == 0)
                {
                    Console.WriteLine("Программирование — это прекрасно!");
                }
                else
                    Console.WriteLine(i);
            }
        }
    }
}
