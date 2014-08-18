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

        static ParserFn<object> Top()
        {
            return List(0);
        }

        static ParserFn<object> List(int level)
        {
            return Item(level).AtLeastOnce().Select(objects => objects.Last());
        }

        static ParserFn<object> Item(int level)
        {
            ParserFn<char> bullet = Parser.Char('-').Or(Parser.Char('+')).Or(Parser.Char('*'));
            ParserFn<object> content = StateIndicators.isEol().Not().And(Parser.Char()).AtLeastOnce().And(StateIndicators.isEol());
            ParserFn<object> item = StateIndicators.Begin<char>()
                .And(Parser.Char(' ').RepeatExactly(level).Join())
                .And(bullet)
                .And(Parser.Char(' '))
                .And(content)
                .And(List(level + 1).Optional());
            return item;
        }
    }
}
