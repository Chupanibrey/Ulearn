using System.Collections.Generic;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static List<double> Neighborhood = new List<double>();

		public static double[,] MedianFilter(double[,] image)
		{
			var lengthX = image.GetLength(0);
			var lengthY = image.GetLength(1);
			var noNoiseImage = new double[lengthX, lengthY];

			for (int x = 0; x < lengthX; x++)
				for (int y = 0; y < lengthY; y++)
				{
					noNoiseImage[x, y] = FindMedian(image, x, y, lengthX, lengthY);
					Neighborhood.Clear();
				}

			return noNoiseImage;
		}

		public static double FindMedian(double[,] image, int pixelX, int pixelY, int lengthPixelX, int lengthPixelY)
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					if(PixelInsideImage(pixelX - 1 + i, pixelY - 1 + j, lengthPixelX, lengthPixelY))
						Neighborhood.Add(image[pixelX - 1 + i, pixelY - 1 + j]);

			Neighborhood.Sort();
			int sizeList = Neighborhood.Count;

			if (sizeList % 2 == 0)
				return (Neighborhood[sizeList / 2] + Neighborhood[(sizeList / 2) - 1]) / 2;
			else
				return Neighborhood[sizeList / 2];
		}

		public static bool PixelInsideImage(int pixelX, int pixelY, int lengthPixelX, int lengthPixelY)
        {
			return pixelX > -1 && pixelX < lengthPixelX && pixelY > -1 && pixelY < lengthPixelY;
        }
	}
}