namespace Combinator.Containers.Abstract
{
    public abstract class ContainerParser: Parser, IContainerParser
    {
        public ContainerParser(): this(null)
        {
        }

        public ContainerParser(Parser parser)
        {
            Fn = ParseFn;
            Expr = parser;
        }

        public Parser Expr { get; set; }

        protected abstract ParseResult ParseFn(State state);
    }
}
