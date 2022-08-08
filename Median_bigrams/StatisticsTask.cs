using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			var arrayVR = visits.ToArray();

			if (arrayVR.Length == 0)
				return 0;

			var arrayTime = arrayVR
			.OrderBy(vr => vr.DateTime)
			.GroupBy(vr => vr.UserId)
			.SelectMany(group => group
				.Bigrams()
				.Where(tuple => tuple.Item1.SlideId != tuple.Item2.SlideId
							&& tuple.Item1.SlideType == slideType))
			.Select(tuple => (tuple.Item2.DateTime - tuple.Item1.DateTime).TotalMinutes)
			.ToArray();

			if (arrayTime.Length == 0)
				return 0;

			return arrayTime
				.Where(min => (min >= 1 && min <= 120))
				.Median();
		}
	}
}