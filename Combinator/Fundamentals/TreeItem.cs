using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Fundamentals
{
    public class TreeItem<T>
    {
        public TreeItem()
        {
            SubTree = new Tree<T>();
        }
        public T Item { get; set; }

        public Tree<T> SubTree { get; set; }
    }
}
