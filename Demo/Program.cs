using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

namespace ParserCombinatorCs
{
    class Program
    {
        static void Main(string[] args)
        {
            var state = new State("ababab");
            var p = new List<ParserFn<char>> { Parser.Char('a'), Parser.Char('b') }.And().Join().AtLeastOnce().Join();
            ParseResult<string> result = state.Apply(p);

            Console.WriteLine(String.Concat(result.Result));
            Console.ReadLine();
        }

        static void List()
        {
            //ParserFn<IEnumerable<object>> list = null;

            //var bullet = Parser.Char('-').Or(Parser.Char('+')).Or(Parser.Char('*'));
            //var content = StateIndicators.isEol().Not().And(Parser.Char()).AtLeastOnce().And(StateIndicators.isEol());
            //ParserFn<IEnumerable<object>> item = StateIndicators.Begin<char>()
            //    .And(bullet)
            //    .And(Parser.Char(' '))
            //    .And(content)
            //    .And(list.Optional());
            //list = item.AtLeastOnce();
        }
    }
}
