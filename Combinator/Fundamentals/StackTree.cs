using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Fundamentals
{
    public class StackTree<T>: Tree<T>
    {
        public StackTree()
        {
            pointer = this;
            parents = new Stack<Tree<T>>();
        }

        private Tree<T> pointer;

        private Stack<Tree<T>> parents;

        public void LevelUp()
        {
            parents.Push(pointer);
            pointer = pointer.Last().SubTree;
        }

        public void LevelDown()
        {
            pointer = parents.Pop();
        }

        public void Push(T obj)
        {
            pointer.Add(new TreeItem<T>() { Item = obj });
        }

        public T Last()
        {
            return pointer.Last().Item;
        }
    }
}
