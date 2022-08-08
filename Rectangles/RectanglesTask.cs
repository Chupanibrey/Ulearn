using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        static Rectangle Intersect(Rectangle r1, Rectangle r2)
        {
            if (IndexOfInnerRectangle(r1, r2) == 1)
            {
                return r2;
            }
            else if (IndexOfInnerRectangle(r1, r2) == 0)
            {
                return r1;
            }
            else
            {
                var left = Math.Max(r1.Left, r2.Left);
                var top = Math.Max(r1.Top, r2.Top);
                var right = Math.Min(r1.Right, r2.Right);
                var bottom = Math.Min(r1.Bottom, r2.Bottom);

                return new Rectangle(left, top, right - left, bottom - top);
            }
        }

        static bool Equals(Rectangle r1, Rectangle r2)
        {
            return r1 != null && r2 != null && r1.Left == r2.Left && 
                r1.Top == r2.Top && r1.Right == r2.Right && r1.Bottom == r2.Bottom;
        }

        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return Intersect(r1, r2).Width >= 0 && Intersect(r1, r2).Height >= 0;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var intersection = Intersect(r1, r2);

            if (Intersect(r1, r2).Width >= 0 && Intersect(r1, r2).Height >= 0)
            {
                return intersection.Width * intersection.Height;
            }
            else
            {
                return 0;
            }
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if ((r1.Left <= r2.Left) && (r1.Top <= r2.Top) && (r1.Right >= r2.Right) && (r1.Bottom >= r2.Bottom))
            {
                return 1;
            }

            else if ((r2.Left <= r1.Left) && (r2.Top <= r1.Top) && (r2.Right >= r1.Right) && (r2.Bottom >= r1.Bottom))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}