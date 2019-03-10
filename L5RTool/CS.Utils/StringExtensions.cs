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
    }
}
