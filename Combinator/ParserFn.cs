using System;
using System.Collections.Generic;
using System.Linq;
using Combinator.Debugging;
using Combinator.Helpers;

namespace Combinator
{
    public delegate ParseResult ParserDelegate(State state);

    public class ParserFn : IParserInfo
    {
        public ParserFn()
        {
        }

        public ParserFn(string name)
        {
            this.Name = name;
        }

        public ParserDelegate Fn;

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static And operator +(ParserFn op1, ParserFn op2)
        {
            return new And(new[] {op1, op2});
        }
    }
    
}
