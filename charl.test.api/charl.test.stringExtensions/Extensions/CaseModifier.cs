using System;
using System.Linq;

namespace charl.test.stringExtensions.Extensions
{
    public static class CaseModifier
    {
        /// <summary>
        /// This is a string extension to convert the string case upper case, I added it just for fun.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetUpper(this string input)
        {
            var lowerChars = GetLowerChars();
            var upperChars = GetUpperChars();

            var returnValue = string.Empty;

            foreach (var c in input)
            {
                if (!char.IsLetter(c))
                {
                    returnValue += c;
                    continue;
                }

                if (!upperChars.Contains(c))
                {
                    var index = Array.IndexOf(lowerChars, c);
                    returnValue += upperChars[index];
                }
                else
                {
                    returnValue += c;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// This is a string extension to convert the string case lower case.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetLower(this string input)
        {
            if (input == null)
                return null;

            var lowerChars = GetLowerChars();
            var upperChars = GetUpperChars();

            var returnValue = string.Empty;

            foreach (var c in input)
            {
                if (!char.IsLetter(c))
                {
                    returnValue += c;
                    continue;
                }

                if (!lowerChars.Contains(c))
                {
                    var index = Array.IndexOf(upperChars, c);
                    returnValue += lowerChars[index];
                }
                else
                {
                    returnValue += c;
                }
            }

            return returnValue;
        }

        //Gets the upper case character range.
        private static char[] GetUpperChars()
        {
            return Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i).ToArray();
        }

        //Gets the upper case character range.
        private static char[] GetLowerChars()
        {
            return Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char)i).ToArray();
        }
    }
}