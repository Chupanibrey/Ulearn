using System.Collections.Generic;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] image, double whitePixelsFraction)
		{
			var lengthX = image.GetLength(0);
			var lengthY = image.GetLength(1);
			var whiteCount = (int)(whitePixelsFraction * image.Length);
			var listPixel = new List<double>();
			var blackAndWhiteImage = new double[lengthX, lengthY];
			var threshold = 0.0;

			foreach (var pixel in image)
				listPixel.Add(pixel);

			listPixel.Sort();
			
			if (whiteCount == image.Length) threshold = double.MinValue;
			else if (whiteCount == 0) threshold = double.MaxValue;
			else threshold = listPixel[image.Length - whiteCount];

			for(int x = 0; x < lengthX; x++)
				for (int y = 0; y < lengthY; y++)
					blackAndWhiteImage[x, y] = image[x, y] >= threshold ? 1.0 : 0.0;

			return blackAndWhiteImage;
		}
	}
}