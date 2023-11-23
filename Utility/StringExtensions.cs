using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeEon.Utility
{
    public static class StringExtensions
    {
        public static string RemoveWhitespace(this string input)
        {
            StringBuilder sb = new();
            foreach (char c in input)
            {
                if (c == '\n' || c == ' ' || c == '\t')
                {
                    continue;
                }

                sb.Append(c);
            }
            return sb.ToString();
        }

        public static IEnumerable<string> SplitByNewLine(this string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitByEmptyLine(this string input)
        {
            return input.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}