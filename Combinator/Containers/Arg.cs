using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;
using Combinator.Containers.Abstract;

namespace Combinator.Containers
{
    public class Arg<T>: ContainerParser
    {
        public Arg(Parser parser, T arg): base(parser)
        {
            Argument = arg;
        }

        public T Argument { get; set; }

        protected override ParseResult ParseFn(State state)
        {
            state.Push(Argument);
            var result = state.Apply0(Expr);
            return result;
        }
    }
}
