using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Fundamentals
{
    public class Tree<T>: List<TreeItem<T>>
    {
        public TResult Recursive<TResult>(Func<T, List<int>, TResult, TResult> action, TResult initial)
        {
            var count = new List<int>();
            return Recursive(action, count, initial);
        }

        private TResult Recursive<TResult>(Func<T, List<int>, TResult, TResult> action, List<int> count, TResult result)
        {
            count.Add(1);
            foreach (TreeItem<T> item in this)
            {
                result = action(item.Item, count, result);
                result = item.SubTree.Recursive(action, count, result);
                count[count.Count - 1]++;
            }
            count.RemoveAt(count.Count-1);
            return result;
        }
    }
}
