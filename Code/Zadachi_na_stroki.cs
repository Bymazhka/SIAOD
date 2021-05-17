using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_number
{
    class Zadachi_na_stroki
    {
        public static void Start_stroki()
        {
            Console.WriteLine("Задача 1. " + CheckIfCanBreak("abc", "xya"));
            Console.WriteLine("Задача 1. " + CheckIfCanBreak("abe", "acd"));
            Console.WriteLine("Задача 2. " + LongestPalindrome("babad"));
            Console.WriteLine("Задача 2. " + LongestPalindrome("вNSDJKBАргентинаманитнегрАJHVJKXJKLC"));
            Console.WriteLine("Задача 3. " + ConcatSum("abcabcabc"));
        }
        static public bool CheckIfCanBreak(string s1, string s2)
        {
            char[] a1 = s1.ToCharArray();
            char[] a2 = s2.ToCharArray();
            Array.Sort(a1);
            Array.Sort(a2);

            bool result = true;
            for (int i = 0; i < s1.Length; i++)
            {
                if (a1[i] > a2[i])
                {
                    result = false;
                    break;
                }
            }
            if (result == true)
                return true;

            result = true;
            for (int i = 0; i < s1.Length; i++)
            {
                if (a1[i] < a2[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        static public string LongestPalindrome(string s)
        {
            if (s == null || s.Length == 0) return "";

            int maxStart = 0, maxEnd = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int start = i, end = i;

                while ((start > 0 && end < s.Length - 1 && s[start - 1] == s[end + 1])) { start--; end++; }

                if (end - start > maxEnd - maxStart) { maxStart = start; maxEnd = end; }

                if (i < s.Length - 1 && s[i] == s[i + 1])
                {
                    start = i; end = i + 1;

                    while ((start > 0 && end < s.Length - 1 && s[start - 1] == s[end + 1])) { start--; end++; }

                    if (end - start > maxEnd - maxStart) { maxStart = start; maxEnd = end; }
                }
            }

            return s.Substring(maxStart, maxEnd - maxStart + 1);
        }
        static int ConcatSum(string str)
        {
            int counter = 0;
            var subs = new List<string>();
            for (int i = 0; i < str.Length - 1; i++)
            {
                int j = i + 1;
                while (j <= str.Length)
                {
                    string left = str.Substring(i, j - i);
                    string right;
                    if (left.Length > (str.Length - j))
                    {
                        right = str.Substring(j);
                    }
                    else
                    {
                        right = str.Substring(j, left.Length);
                    }
                    if (left.Equals(right) && !subs.Contains(left))
                    {
                        subs.Add(left);
                        counter++;
                    }
                    j++;
                }
            }
            return counter;
        }
    }
}
