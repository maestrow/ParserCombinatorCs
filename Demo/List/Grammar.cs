using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

namespace Demo.List
{
    public class Grammar
    {
        //public ParserFn Root()
        //{
        //    return List(0);
        //}

        //ParserFn List(int level)
        //{
        //    return Item(level).AtLeastOnce().Select(objects => objects.Last());
        //}

        //ParserFn Item(int level)
        //{
        //    ParserFn bullet = Parser.Char('-').Or(Parser.Char('+')).Or(Parser.Char('*'));
        //    ParserFn content =  new[] { StateIndicators.isEol().Not(), Parser.Char().Select(a => (object)a) }
        //        .And().AtLeastOnce().And(StateIndicators.isEol());

        //    ParserFn item = StateIndicators.Begin<char>()
        //        .And(Parser.Char(' ').RepeatExactly(level).Join())
        //        .And(bullet)
        //        .And(Parser.Char(' '))
        //        .And(content)
        //        .And(List(level + 1).Optional());
        //    return item;
        //}
    }
}
