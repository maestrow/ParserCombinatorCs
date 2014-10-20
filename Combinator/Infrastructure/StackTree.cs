using System.Collections.Generic;
using System.Linq;

namespace Combinator.Infrastructure
{
    public class StackTree<T>: Tree<T>
    {
        public StackTree()
        {
            pointer = this;
            parents = new Stack<Tree<T>>();
        }

        /// <summary>
        /// Указатель на поддерево конкретного узла
        /// </summary>
        private Tree<T> pointer;

        /// <summary>
        /// Цепочка (стэк) родительских указателей данного указателя.
        /// </summary>
        private Stack<Tree<T>> parents;

        public void LevelDown()
        {
            parents.Push(pointer);
            pointer = pointer.Last().SubTree;
        }

        public void LevelUp()
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

        public T ParentLast()
        {
            return parents.Peek().Last().Item;
        }
    }
}
