using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;
public class LeftBorderTask
{
    public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        if (left == right - 1) return left;
        var center = left + (right - left) / 2;
        return string.Compare(prefix,phrases[center],  StringComparison.OrdinalIgnoreCase) <= 0
            ? GetLeftBorderIndex(phrases, prefix, left, center) 
            : GetLeftBorderIndex(phrases, prefix, center, right);
    }
}