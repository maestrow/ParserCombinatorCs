using System.Collections.Generic;
using Combinator.Common;

namespace Combinator.Containers.Abstract
{
    public abstract class ListContainerParser: Parser, IListContainerParser
    {
        public ListContainerParser(): this(new List<Parser>())
        {
        }

        public ListContainerParser(IList<Parser> container)
        {
            Fn = ParseFn;
            Container = container;
        }

        public IList<Parser> Container { get; set; }

        protected abstract ParseResult ParseFn(State state);
    }
}
