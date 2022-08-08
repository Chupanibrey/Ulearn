using System;
using System.Linq;

namespace Figures
{
    public class Circle
    {
        private double radius;

        public double Radius
        {
            get { return radius; }
            set
            {
                radius = value > 0 ? value : -value;
            }
        }

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public double Area()
        {
            double area = 0;
            area = Math.PI * (Radius * Radius);

            return area;
        }
    }

    public class Triangle
    {
        private double a;
        private double b;
        private double c;

        public double A
        {
            get { return a; }
            set
            {
                a = value > 0 ? value : -value;
            }
        }

        public double B
        {
            get { return b; }
            set
            {
                b = value > 0 ? value : -value;
            }
        }

        public double C
        {
            get { return c; }
            set
            {
                c = value > 0 ? value : -value;
            }
        }

        public Triangle(double a, double b, double c)
        {
            if (a + b > c && a + c > b && b + c > a)
            {
                this.A = a;
                this.B = b;
                this.C = c;
            }
            else
                throw new ArgumentOutOfRangeException("Это не треугольник");
        }

        public double Area()
        {
            double area = 0;

            double halfPerimeter = (A + B + C) / 2;
            area = Math.Sqrt(halfPerimeter * 
                (halfPerimeter - A) * (halfPerimeter - B) * (halfPerimeter - C));

            return area;
        }

        public bool CheckingRightTriangle()
        {
            bool result = false;

            double[] sides = { A, B, C };

            int longestSideIndex = Array.IndexOf(sides, sides.Max());

            double longSide = 0;
            double smallSides = 0;

            for (int i = 0; i < sides.Length; i++)
            {
                if (i != longestSideIndex)
                    smallSides += Math.Pow(sides[i], 2);
                else
                    longSide = Math.Pow(sides[i], 2);
            }

            if (longSide == smallSides)
                result = true;

            return (result);
        }
    }
}