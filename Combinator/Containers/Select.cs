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
    public class Select<T, TR>: ContainerParser
    {
        private Func<T, TR> selector { get; set; }

        public Select(Parser parser, Func<T, TR> selector): base(parser)
        {
            Name = GetType().Name;
            this.selector = selector;
        }

        protected override ParseResult ParseFn(State state)
        {
            ParseResult result = state.Apply(Expr);
            if (result.IsSuccess)
                return ParseResult.Success(selector((T)result.Result));
            return ParseResult.Failed();
        }
    }
}
