using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;
using Combinator.Containers.Abstract;
using Combinator.Infrastructure;

namespace Combinator.Containers
{
    public class Join: ContainerParser
    {
        public Join(Parser parser): base(parser)
        {
            Name = GetType().Name;
        }

        protected override ParseResult ParseFn(State state)
        {
            ParseResult result = state.Apply(Expr);
            if (result.IsSuccess)
                return ParseResult.Success(String.Concat(result.Result));
            return ParseResult.Failed();
        }
    }
}
