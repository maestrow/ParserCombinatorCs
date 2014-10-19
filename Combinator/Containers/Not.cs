using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Containers.Abstract;
using Combinator.Infrastructure;

namespace Combinator.Containers
{
    public class Not: ContainerParser
    {
        public Not(): this(null)
        {
        }

        public Not(Parser parser)
        {
            Name = GetType().Name;
            Expr = parser;
        }

        protected override ParseResult ParseFn(State state)
        {
            return new ParseResult(!state.Apply(Expr).IsSuccess);
        }
    }

}
