using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		public static double Median(this IEnumerable<double> items)
		{
			var copyItems = items.OrderBy(i => i).ToArray();
			var lengthArr = copyItems.Length;

			if (lengthArr == 0)
				throw new InvalidOperationException();
			else if (lengthArr % 2 == 0)
				return (copyItems[lengthArr / 2 - 1] + copyItems[lengthArr / 2]) / 2;
			else
				return copyItems[lengthArr / 2];
		}

		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			int count = 0;
			T past = default;
			foreach(var i in items)
            {
				if (count == 0)
				{
					past = i;
					count++;
					continue;
				}

				yield return Tuple.Create(past, i);

				past = i;
			}
		}
	}
}