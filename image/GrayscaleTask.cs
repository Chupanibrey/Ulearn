namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var lengthX = original.GetLength(0);
			var lengthY = original.GetLength(1);
			var grayscale = new double[lengthX, lengthY];
			for (int x = 0; x < lengthX; x++)
				for (int y = 0; y < lengthY; y++)
					grayscale[x, y] = (0.299 * original[x, y].R + 0.587 * original[x, y].G + 0.114 * original[x, y].B) / 255;
			return grayscale;
		}
	}
}