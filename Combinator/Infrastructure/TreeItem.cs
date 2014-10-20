namespace Combinator.Infrastructure
{
    public class TreeItem<T> where T : class
    {
        public TreeItem(): this(null)
        {
        }

        public TreeItem(T item)
        {
            SubTree = new Tree<T>();
            Item = item;
        }
        public T Item { get; set; }

        public Tree<T> SubTree { get; set; }
    }
}
