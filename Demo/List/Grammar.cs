using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

namespace Demo.List
{
    public static class Grammar
    {
        public static ParserFn<object> Top()
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
