using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeUIClassLib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the <paramref name="input" /> string list into a string delimiting each entry using the 
        /// <paramref name="delimiter" />. The default delimiter is \r\n 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string ToDelimited(this List<string> input, string delimiter = "\r\n")
        {
            if (null == input)
            {
                return string.Empty;
            }
            var stringBuilder = new StringBuilder();
            foreach (var stringValue in input)
            {
                stringBuilder.AppendFormat("{0}{1}", stringValue, delimiter);
            }
            return stringBuilder.ToString();
        }

        public static List<string> FromDelimited(this string input, string delimiter = "\r\n")
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return new List<string>(input.Split(new string[] {delimiter}, StringSplitOptions.RemoveEmptyEntries));
            }
            return new List<string>();
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}