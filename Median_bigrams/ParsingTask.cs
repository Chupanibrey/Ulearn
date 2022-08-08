using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines
				.Skip(1)
				.SelectMany(str => ToSlideRecord(str))
				.ToDictionary(k => k.Key, v => v.Value);
		}

		private static Dictionary<int, SlideRecord> ToSlideRecord(string line)
		{
			var dictionary = new Dictionary<int, SlideRecord>();
			var p = line.Split(';');

			if(p.Length == 3
					&& int.TryParse(p[0], out int idS)
					&& Enum.TryParse(p[1], true, out SlideType typeS))
				dictionary.Add(idS, new SlideRecord(idS, typeS, p[2]));

			return dictionary;
		}

        public static IEnumerable<VisitRecord> ParseVisitRecords(
		IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
				.Skip(1)
				.Select(str => ToVisitRecord(slides, str));
		}

		private static VisitRecord ToVisitRecord(IDictionary<int, SlideRecord> slides, string line)
		{
			var p = line.Split(';');
			if (p.Length == 4 
					&& int.TryParse(p[0], out int idU)
					&& int.TryParse(p[1], out int idS)
					&& slides.ContainsKey(idS)
					&& DateTime.TryParse(p[2] + 'T' + p[3], out DateTime res))
				return new VisitRecord(idU, idS, res, slides[idS].SlideType);
			else
				throw new FormatException(string.Format("Wrong line [{0}]", line));
		}
	}
}