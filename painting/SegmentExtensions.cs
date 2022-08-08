using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
	public static class SegmentExtensions
	{
		static Dictionary<Segment, Color> dictionary = new Dictionary<Segment, Color>();
		public static Color SetColor(this Segment a, Color b)
		{
			if (!dictionary.ContainsKey(a))
				dictionary.Add(a, b);
			else
				dictionary[a] = b;
			return dictionary[a];
		}

		public static Color GetColor(this Segment a)
		{
			if (dictionary.ContainsKey(a))
				return dictionary[a];
			else
				return Color.Black;
		}
	}
}