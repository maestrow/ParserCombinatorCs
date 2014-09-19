using System;
using System.Collections.Generic;
using System.Linq;
using Combinator.Infrastructure;

namespace Combinator.Debugging
{
    public class DebugInfo: StackTree<AppliedRule>
    {
        public override string ToString()
        {
            return this.Recursive(itemToString, string.Empty);
        }

        private string itemToString(AppliedRule item, List<int> count, string result)
        {
            return result + getIndent(count.Count - 1) + /*getNumber(count) +*/ item + Environment.NewLine;
        }

        private string getNumber(List<int> count)
        {
            return count.Select(a => a.ToString()).Aggregate((a, b) => string.Format("{0}.{1}", a, b)) + ". ";
        }

        private string getIndent(int level)
        {
            return Enumerable.Repeat("  ", level)
                .DefaultIfEmpty()
                .Aggregate((a, b) => string.Format("{0}{1}", a, b));
        }
    }
}
