using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var minDay = int.MaxValue;
            var maxDay = int.MinValue;
            foreach (var n in names)
            {
                minDay = Math.Min(minDay, n.BirthDate.Day);
                maxDay = Math.Max(maxDay, n.BirthDate.Day);
            }
            var days = new string[maxDay - minDay + 1];
            for (var y = 0; y < days.Length; y++)
                days[y] = (y + minDay).ToString();
            var birthsCounts = new double[maxDay - minDay + 1];
            foreach (var n in names)
                if(n.Name == name && (n.BirthDate.Day - minDay != 0))
                    birthsCounts[n.BirthDate.Day - minDay]++;

            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name), days, birthsCounts);
        }
    }
}