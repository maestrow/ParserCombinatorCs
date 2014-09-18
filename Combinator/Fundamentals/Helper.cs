using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Combinator.Fundamentals
{
    public static class Helper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
    }
}
