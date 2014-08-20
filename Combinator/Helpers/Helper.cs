using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Combinator.Helpers
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
