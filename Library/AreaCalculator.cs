using System;
using Figures;


namespace AreaCalculator
{
    public class AreaCalculator<T>
    {

        public static double Calculate(T figure)
        {
            switch(figure)
            {
                case Circle c:
                    return c.Area();
                case Triangle t:
                    return t.Area();
                default:
                    throw new Exception("Данной фигуры в калькуляторе нету");
            }
        }
    }
}   