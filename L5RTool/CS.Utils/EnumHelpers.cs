using System;
using System.Collections.Generic;
using System.Linq;

namespace CS.Utils
{
    public static class EnumHelpers
    {
        public static IEnumerable<T> GetValues<T>() where T: Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
