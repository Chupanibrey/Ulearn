using System;
using System.Globalization;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Calculate(Console.ReadLine()));
        }
        public static double Calculate(string userInput)
        {
            string[] data = userInput.Split(' ');
            double amountMoney = double.Parse(data[0], CultureInfo.InvariantCulture);
            double percentRate = double.Parse(data[1], CultureInfo.InvariantCulture);
            double month = double.Parse(data[2], CultureInfo.InvariantCulture);
            return amountMoney * Math.Pow((percentRate / 100 / 12 + 1), month);
        }
    }
}
