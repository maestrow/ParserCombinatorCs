using System.Collections.Generic;
using System.Linq;
using Combinator.Common;
using Combinator.Containers.Abstract;

namespace Combinator.Containers
{
    public class Or: ListContainerParser
    {
        public Or(): this(new List<Parser>())
        {
        }

        public Or(IEnumerable<Parser> parsers): base(parsers.ToList())
        {
            Name = GetType().Name;
        }

        protected override ParseResult ParseFn(State state)
        {
            foreach (Parser parser in Container)
            {
                ParseResult presult = state.Apply(parser);
                if (presult.IsSuccess)
                    return ParseResult.Success(presult.Result);
            }
            return ParseResult.Failed();
        }
    }
}
