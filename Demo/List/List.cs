using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Infrastructure;

namespace Demo.List
{
    public class List: Tree<string>
    {
        public List(): this(null)
        {
        }

        public List(IEnumerable<TreeItem<string>> list): base(list)
        {
        }

        public bool IsOrdered { get; set; }

        public char Bullet { get; set; }

        public override string ToString()
        {
            return Recursive(ItemToString, string.Empty);
        }

        private string ItemToString(string item, List<int> numbers, string result)
        {
            result += indent(numbers.Count) + item + "\n";
            return result;
        }

        private string indent(int level)
        {
            return Enumerable.Repeat("  ", level).DefaultIfEmpty().Aggregate((a, b) => a + b);
        }
    }
}
