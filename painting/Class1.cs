using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector b)
        {
            return Geometry.Add(this, b);
        }

        public bool Belongs(Segment b)
        {
            return Geometry.IsVectorInSegment(this, b);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static double GetLength(Segment a)
        {
            double x = (a.End.X - a.Begin.X) * (a.End.X - a.Begin.X);
            double y = (a.End.Y - a.Begin.Y) * (a.End.Y - a.Begin.Y);
            return Math.Sqrt(x + y);
        }

        public static double GetLength(Vector a, Vector b)
        {
            double x = (b.X - a.X) * (b.X - a.X);
            double y = (b.Y - a.Y) * (b.Y - a.Y);
            return Math.Sqrt(x + y);
        }

        public static bool IsVectorInSegment(Vector vector, Segment a)
        {
            double ab = GetLength(a.Begin, a.End);
            double ax = GetLength(a.Begin, vector);
            double xb = GetLength(vector, a.End);
            return (ax + xb) == ab ? true : false;
        }

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector { X = a.X + b.X, Y = a.Y + b.Y };
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector b)
        {
            return Geometry.IsVectorInSegment(b, this);
        }
    }
}