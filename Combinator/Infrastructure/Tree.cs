using System;
using System.Collections.Generic;
using System.Linq;

namespace Combinator.Infrastructure
{
    public class Tree<T>: List<TreeItem<T>> where T : class
    {
        public Tree()
        {
        }

        public Tree(IEnumerable<T> list)
        {
            if (list != null)
            {
                foreach (T item in list)
                {
                    Add(new TreeItem<T>(item));
                }
            }
        }

        public Tree(IEnumerable<TreeItem<T>> list)
        {
            if (list != null)
            {
                foreach (TreeItem<T> treeItem in list)
                {
                    Add(treeItem);
                }
            }
        }

        /// <summary>
        /// Рекурсивный обход дерева
        /// </summary>
        /// <typeparam name="TResult">Тип результата обхода (например, строка), того же типа должно быть начальное значение.</typeparam>
        /// <param name="action">Функция, которая будет вызвана для каждого элемента дерева. Аргументы:
        /// 1. Элемент дерева
        /// 2. Номер элемента в дереве - это список целых чисел. Каждое число - порядковый номер узла для каждого уровня иерархии. Например, 2.3.1.
        /// 3. Промежуточный результат
        /// </param>
        /// <param name="initial">Начальное значение (например, пустая строка)</param>
        /// <returns></returns>
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
                count[count.Count-1]++;
            }
            count.RemoveAt(count.Count-1);
            return result;
        }
    }
}
