using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;
using Combinator.Containers.Abstract;

namespace Combinator
{
    public class Rule: ContainerParser
    {
        public Rule(string name, string description = ""): base(name, null, description)
        {
        }

        public Rule(string name, ParserDelegate fn, string description = "")
            : base(name, new Parser(fn), description)
        {
        }

        public Rule(string name, ParserGenerator generator, string description = "")
            : base(name, Parsers.Generate(generator), description )
        {
        }

        public Rule(string name, Parser parser, string description = "")
            : base(name, parser, description)
        {
        }

        protected override ParseResult ParseFn(State state)
        {
            return state.Apply0(Expr);
        }
    }
}
