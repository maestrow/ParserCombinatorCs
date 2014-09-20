using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

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
        
        public static ParserFn Top = new List<ParserFn>
        {
            Parser.Char('a'), 
            Parser.Char('b')
        }
        .And()
        .Join()
        .AtLeastOnce()
        .Join();
    }
}
