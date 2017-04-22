using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Tubes3Stima
{
    public class Searching
    {
        public static bool KmpMatch(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;

            int[] fail = ComputeFail(pattern);

            int i = 0;
            int j = 0;

            while (i < n)
            {
                if (pattern[j] == text[i])
                {
                    if (j == m - 1)
                        return true; // match
                    i++;
                    j++;
                }
                else if (j > 0)
                    j = fail[j - 1];
                else
                    i++;
            }
            return false; // no match
        }

        public static int[] ComputeFail(string pattern)
        {
            int[] fail = new int[pattern.Length];
            fail[0] = 0;

            int m = pattern.Length;
            int j = 0;
            int i = 1;

            while (i < m)
            {
                if (pattern[j] == pattern[i])
                { // j + 1 chars match
                    fail[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0) // j follows matching prefix
                    j = fail[j - 1];
                else
                { // no match
                    fail[i] = 0;
                    i++;
                }
            }
            return fail;
        }

        public static bool BmMatch(string text, string pattern)
        {
            int[] last = BuildLast(pattern);
            int n = text.Length;
            int m = pattern.Length;
            int i = m - 1;

            if (i > n - 1)
                return false;// no match if pattern is longer than text

            int j = m - 1;

            do
            {
                if (pattern[j] == text[i])
                    if (j == 0)
                        return true; // match
                    else
                    {
                        i--;
                        j--;
                    }
                else
                {
                    int lo = last[text[i]];
                    i = i + m - Math.Min(j, 1 + lo);
                    j = m - 1;
                }
            } while (i <= n - 1);
            return false; // no match
        }

        public static int[] BuildLast(string pattern)
        {
            /* Return array storing index of last occurence of each ascii char in pattern */
            int[] last = new int[128]; // ASCII char set

            for (int i = 0; i < 128; i++)
                last[i] = -1;

            for (int i = 0; i < pattern.Length; i++)
                last[pattern[i]] = i;

            return last;
        }

        public static bool RegexMatch(string text, string pattern)
        {
            string patternCek = @"\b(" + pattern + @")\b";
            Match match = Regex.Match(input, pattern);
            return match.Success;
        }
    }
}