using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Atomics;
using Combinator.Common;

namespace Demo
{
    public static class SampleGrammar
    {
        public static string Test()
        {
            var state = new State("ababab");
            ParseResult result = state.Apply(Top);
            return result.Result.ToString();
        }
        
        public static Parser Top = (Parsers.Char('a') + Parsers.Char('b'))
            .Join()
            .AtLeastOnce()
            .Join();
    }
}
