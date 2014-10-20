using Combinator.Common;

namespace Combinator.Containers.Abstract
{
    public abstract class ContainerParser: Parser, IContainerParser
    {
        public ContainerParser(): this(null)
        {
        }

        public ContainerParser(Parser parser): this("", parser)
        {
        }

        public ContainerParser(string name, Parser parser, string description = ""): base(name, description)
        {
            Fn = ParseFn;
            Expr = parser;
        }

        public Parser Expr { get; set; }

        protected abstract ParseResult ParseFn(State state);
    }
}
