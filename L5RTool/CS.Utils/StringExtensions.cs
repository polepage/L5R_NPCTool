using System;

namespace CS.Utils
{
    public static class StringExtensions
    {
        public static int IndexOfAny(this string text, params string[] anyOf)
        {
            int result = -1;
            foreach (string s in anyOf)
            {
                int index = text.IndexOf(s);
                if (index > -1 && (index < result || result == -1))
                {
                    result = index;
                }
            }

            return result;
        }

        public static int LastIndexOfBounded(this string text, string value, int upperBound)
        {
            int index = -1;
            int nextIndex = 0;
            while (nextIndex < upperBound)
            {
                nextIndex = text.IndexOf(value, nextIndex);
                if (nextIndex < 0 || nextIndex >= upperBound)
                {
                    break;
                }
                else
                {
                    index = nextIndex;
                    nextIndex += value.Length;
                }
            }

            return index;
        }

        public static string[] Split(this string text, params string[] separator)
        {
            return text.Split(separator, StringSplitOptions.None);
        }
    }
}
