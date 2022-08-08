using System;

namespace ConsoleApp1
{
    class Program
    {
        static int[,] prices =
        {
            { 0, 2, 4, 7 },
            { 2, 0, 3, 1 },
            { 4, 2, 0, 1 },
            { 3, 5, 2, 0 }
        };

        static void Evaluate(int[] permutation)
        {
            int price = 0;
            for (int i = 0; i < permutation.Length; i++)
                price += prices[permutation[i], permutation[(i + 1) % permutation.Length]];
            foreach (var e in permutation)
                Console.Write(e + " ");
            Console.Write(price);
            Console.WriteLine();
        }

        static void MakePermutations(int[] permutation, int position)
        {
            if (position == permutation.Length)
            {
                Evaluate(permutation);
                return;
            }

            for (int i = 0; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 0, position);
                if (index != -1)
                    continue;
                permutation[position] = i;
                MakePermutations(permutation, position + 1);
            }
        }

        public static void Main()
        {
            MakePermutations(new int[4], 0);
        }
    }
}
