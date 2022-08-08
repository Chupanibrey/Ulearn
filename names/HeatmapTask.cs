using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var days = new string[30];
            var month = new string[12];
            for (var x = 0; x < days.Length; x++)
                days[x] = (x + 2).ToString(); // 1 день любого месяца не считается и 0 день не существует
            for (var y = 0; y < month.Length; y++)
                month[y] = (y + 1).ToString(); // 0 месяц не существует
            var birthsCounts = new double[days.Length, month.Length];
            foreach (var n in names)
            {
                for (int y = 0; y < month.Length; y++)
                {
                    for (int x = 0; x < days.Length; x++)
                    {
                        if ((n.BirthDate.Day - 1 == x + 1 && n.BirthDate.Month - 1 == y))
                        {
                            birthsCounts[x, y]++;
                            break;
                        }
                    }
                }
            }

            return new HeatmapData("Карта интенсивности", birthsCounts, days, month);
        }
    }
}