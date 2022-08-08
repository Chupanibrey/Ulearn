using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			double s = 0.0;
			int counter = 0;

			foreach (var point in data)
			{
				if (counter++ == 0)
					s = point.OriginalY;

				s = alpha * point.OriginalY + (1 - alpha) * s;
				yield return point.WithExpSmoothedY(s);
			}
		}
	}
}