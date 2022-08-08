using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] image, double[,] sx)
        {
            var sideLength = sx.GetLength(0);
            var sy = new double[sideLength, sideLength];

            for (int x = 0; x < sideLength; x++)
                for (int y = 0; y < sideLength; y++)
                    sy[x, y] = sx[y, x];

            var width = image.GetLength(0);
            var height = image.GetLength(1);
            var shift = sx.GetLength(0) / 2;
            var result = new double[width, height];

            for (int x = shift; x < width - shift; x++)
                for (int y = shift; y < height - shift; y++)
                {
                    var gx = NeighborhoodSumAndMatrix(image, sx, x, y, shift);
                    var gy = NeighborhoodSumAndMatrix(image, sy, x, y, shift);

                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }

        public static double NeighborhoodSumAndMatrix(
            double[,] image, double[,] matrix, int pixelX, int pixelY, int shift)
        {
            var result = 0.0;
            var sideLength = matrix.GetLength(0);

            for (int i = 0; i < sideLength; i++)
                for (int j = 0; j < sideLength; j++)
                    result += image[pixelX - shift + i, pixelY - shift + j] * matrix[i, j];

            return result;
        }
    }
}