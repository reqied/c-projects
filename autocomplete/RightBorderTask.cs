using System;
using System.Collections.Generic;

namespace Autocomplete;

public static class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            while (left < right - 1)
            {
                var center = left + (right - left) / 2;
                if (string.Compare(prefix,phrases[center],  StringComparison.OrdinalIgnoreCase) >= 0 
                    || phrases[center].StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) 
                {
                    left = center;
                }
                else
                {
                    right = center;
                }
            }
            return right;
        }
    }