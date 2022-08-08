using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right - left <= 1) return left;
            var m = left + (right - left) / 2;
            if (string.Compare(prefix, phrases[m], StringComparison.OrdinalIgnoreCase) > 0)
                return GetLeftBorderIndex(phrases, prefix, m, right);
            else
                return GetLeftBorderIndex(phrases, prefix, left, m);
        }
    }
}