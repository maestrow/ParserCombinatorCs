using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Containers.Abstract;

namespace Combinator
{
    public class Rule: ContainerParser
    {
        public Rule(string name)
        {
            Name = name;
        }

        public Rule(string name, string description): this(name)
        {
            Description = description;
        }

        protected override ParseResult ParseFn(State state)
        {
            var result = state.Apply(Expr);
            return new ParseResult(result.IsSuccess, result.Result, 0);
        }
    }
}
