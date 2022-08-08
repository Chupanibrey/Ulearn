using System;
using System.Collections.Generic;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var queue = new LinkedList<Tuple<double, double>>();
			var queueX = new Queue<double>();

			foreach (var point in data)
			{
				queueX.Enqueue(point.X);

				if (queueX.Count > windowWidth && queue.First.Value.Item1 <= queueX.Dequeue())
					queue.RemoveFirst();

				while(queue.Count != 0 && point.OriginalY > queue.Last.Value.Item2)
					queue.RemoveLast();

				queue.AddLast(new Tuple<double, double>(point.X, point.OriginalY));

				yield return point.WithMaxY(queue.First.Value.Item2);
			}
		}
	}
}