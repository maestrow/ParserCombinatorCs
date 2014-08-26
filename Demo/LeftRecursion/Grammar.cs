using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

namespace Demo.LeftRecursion
{
    public static class Grammar
    {
        public static string Test()
        {
            var state = new State("123");

        }

        public static ParserFn<int> Number()
        {
            return Parser.RegEx(@"\d").Select(a => int.Parse(a))
        }
    }
}
