using System.Linq;

namespace CS.Utils
{
    public static class BoolHelpers
    {
        public static int CountTrue(params bool[] bools)
        {
            return bools.Where(b => b).Count();
        }
    }
}
