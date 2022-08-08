using System;

namespace Obrazovanie_chernovik
{
    class Program
    {
        public static void Main()
        {
            Print(GetSquare(42));
        }

        private static void Print(int number)
        {
            Console.WriteLine(number);
        }
        private static int GetSquare(int number)
        {

            return number*number;
        }
    }
}
