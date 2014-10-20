using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Combinator.Containers;
using Combinator.Debugging;
using Combinator.Helpers;

namespace Combinator
{
    public delegate ParseResult ParserDelegate(State state);

    public class Parser : IParserInfo
    {
        public Parser()
        {
            Parameters = new Dictionary<string, object>();
        }

        public ParserDelegate Fn;

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public override string ToString()
        {
            string result = Name;

            var p = Parameters
                .Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))
                .DefaultIfEmpty()
                .Aggregate((a, b) => string.Format("{0},{1}"));
            
            if (!string.IsNullOrEmpty(p))
                result += string.Format("({0})", p);
            
            //if (!string.IsNullOrEmpty(Description)) 
            //    result += ". " + Description;

            return result;
        }

        #region Operators

        public static And operator +(Parser op1, Parser op2)
        {
            return new And(new [] {op1, op2});
        }

        public static And operator +(And op1, Parser op2)
        {
            op1.Container.Add(op2);
            return op1;
        }

        public static Or operator |(Parser op1, Parser op2)
        {
            return new Or(new [] {op1, op2});
        }

        public static Or operator |(Or op1, Parser op2)
        {
            op1.Container.Add(op2);
            return op1;
        }

        public static Parser operator !(Parser parser)
        {
            return new Not(parser);
        }

        #endregion
    }
    
}
