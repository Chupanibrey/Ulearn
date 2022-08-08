using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using static System.Math;


namespace RefactorMe
{
    class Drawling
    {
        static float x, y;
        static Graphics graphic;

        public static void GraphInitialization ( Graphics newGraphic )
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0; 
            y = y0;
        }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Cos(angle));
            var y1 = (float)(y + length * Sin(angle));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void MoveCursor(double length, double angle)
        {
            x = (float)(x + length * Cos(angle)); 
            y = (float)(y + length * Sin(angle));
        }

        public static void DrawSquare(int sz)
        {
            Drawling.DrawSide(sz, 
                0, 
                PI / 4, 
                PI, 
                PI / 2, 
                -PI, 
                3 * PI / 4);

            Drawling.DrawSide(sz, 
                -PI / 2, 
                -PI / 2 + PI / 4, 
                -PI / 2 + PI, 
                -PI / 2 + PI / 2, 
                -PI / 2 - PI, 
                -PI / 2 + 3 * PI / 4);

            Drawling.DrawSide(sz, 
                PI, 
                PI + PI / 4,
                PI + PI, 
                PI + PI / 2,
                PI - PI, 
                PI + 3 * PI / 4);

            Drawling.DrawSide(sz, 
                PI / 2, 
                PI / 2 + PI / 4,
                PI / 2 + PI, 
                PI / 2 + PI / 2, 
                PI / 2 - PI, 
                PI / 2 + 3 * PI / 4);
        }

        public static void DrawSide(int sz, double y0, double y1, double y2, double y3, double move_y0, double move_y1)
        {
            Drawling.DrawLine(Pens.Yellow, sz * 0.375f, y0);
            Drawling.DrawLine(Pens.Yellow, sz * 0.04f * Sqrt(2), y1);
            Drawling.DrawLine(Pens.Yellow, sz * 0.375f, y2);
            Drawling.DrawLine(Pens.Yellow, sz * 0.375f - sz * 0.04f, y3);

            Drawling.MoveCursor(sz * 0.04f, move_y0);
            Drawling.MoveCursor(sz * 0.04f * Sqrt(2), move_y1);
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double angleOfRotation, Graphics graphic)
        {
            Drawling.GraphInitialization(graphic);

            var sz = Min(width, height);

            var diagonal = Sqrt(2) * (sz * 0.375f + sz * 0.04f) / 2;
            var x0 = (float)(diagonal * Cos(PI / 4 + PI)) + width / 2f;
            var y0 = (float)(diagonal * Sin(PI / 4 + PI)) + height / 2f;

            Drawling.SetPosition(x0, y0);

            Drawling.DrawSquare(sz); 
        }
    }
}