namespace Combinator.Infrastructure
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
