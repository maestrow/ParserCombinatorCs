using System;
using System.Collections.Generic;
using System.Linq;
using Combinator.Debugging;
using Combinator.Helpers;

namespace Combinator
{
    public delegate IParseResult<T> ParserDelegate<T>(State state);

    public class ParserFn<T> : IParserInfo
    {
        public ParserFn()
        {
        }

        public ParserFn(string name)
        {
            this.Name = name;
        }

        public ParserDelegate<T> Fn;

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static OrParser<T> operator |(ParserFn<T> op1, ParserFn<T> op2)
        {
            return new OrParser<T>(new []{op1, op2});
        }

        public static OrParser<T> operator |(OrParser<T> op1, ParserFn<T> op2)
        {
            op1.Container.Add(op2);
            return op1;
        }
    }

    public class ParserFn : ParserFn<object>
    {
        public static AndParser operator +(ParserFn op1, ParserFn op2)
        {
            return new AndParser(new [] { op1, op2 });
        }

        public static AndParser operator +(AndParser op1, ParserFn op2)
        {
            op1.Container.Add(op2);
            return op1;
        }
    }
    
}
