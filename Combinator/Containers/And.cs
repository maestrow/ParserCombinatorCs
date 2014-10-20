using System.Collections.Generic;
using System.Linq;
using Combinator.Common;
using Combinator.Containers.Abstract;

namespace Combinator.Containers
{
    public class And : ListContainerParser
    {
        public And(): this(new List<Parser>())
        {
        }

        public And(IEnumerable<Parser> parsers): base(parsers.ToList())
        {
            Name = GetType().Name;
        }

        protected override ParseResult ParseFn(State state)
        {
            var lisT = new List<object>();
            ParseResult presult;
            foreach (Parser parser in Container)
            {
                presult = state.Apply(parser);
                if (!presult.IsSuccess)
                    return ParseResult.Failed();
                lisT.Add(presult.Result);
            }
            return ParseResult.Success(lisT);
        }
    }
}
