using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tubes3Stima
{
    public class Searching
    {
        public static int BmMatch(string text, string pattern)
        {
            int last[] = BuildLast(pattern);
            int n = text.Length;
            int m = pattern.Length;
            int i = m - 1;

            if (n > n-1)
            {
                return -1;
            }
            else
            {
                int j = m - 1;
                do
                {
                    if (pattern.ElementAt(j) == text.ElementAt(i))
                    {
                        if (j == 0)
                        {
                            return i;
                        }
                        else
                        {
                            i--;
                            j--;
                        }
                    }
                    else
                    {
                        int lo = last[text.ElementAt(i)];
                        i = i + m - Math.Min(j, 1 + lo);
                        j = m - 1;
                    }
                } while (i <= n - 1)

                return -1;
            }
        }

        public static int[] BuildLast(string pattern)
        {
            int last[] = new int[128];

            for (int idx = 0; idx < 128; idx++)
            {
                last[idx] = -1;
            }

            for(int idx = 0; idx < pattern.Length; idx++)
            {
                last[pattern.ElementAt(idx)] = idx;
            }

            return last;
        }

        public static int KmpMatch(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;

            int fail[] = computeFail(pattern);

            while (i < n)
            {
                if (pattern.ElementAt(j) == text.ElementAt(i))
                {
                    if (j == m -1)
                    {
                        return i - m + 1;
                    }
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    i++;
                }
            }
            return -1;
        }

        public static int[] computeFail(string pattern)
        {
            int fail[] = new int[pattern.Length];
            fail[0] = 0;

            int m = pattern.Length;
            int j = 0;
            int i = 1;

            while (i < m)
            {
                if (pattern.ElementAt(j) == pattern.ElementAt(i))
                {
                    fail[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    fail[i] = 0;
                    i++;
                }
            }
            return fail;
        }

        public static int RegexSearch(string text, string pattern)
        {

        }
    }
}