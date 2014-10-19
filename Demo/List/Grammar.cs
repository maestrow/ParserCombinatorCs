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
        //public Parser Root()
        //{
        //    return List(0);
        //}

        //Parser List(int level)
        //{
        //    return Item(level).AtLeastOnce().Select(objects => objects.Last());
        //}

        //Parser Item(int level)
        //{
        //    Parser bullet = Parser.Char('-').Or(Parser.Char('+')).Or(Parser.Char('*'));
        //    Parser content =  new[] { StateIndicators.isEol().Not(), Parser.Char().Select(a => (object)a) }
        //        .And().AtLeastOnce().And(StateIndicators.isEol());

        //    Parser item = StateIndicators.Begin<char>()
        //        .And(Parser.Char(' ').RepeatExactly(level).Join())
        //        .And(bullet)
        //        .And(Parser.Char(' '))
        //        .And(content)
        //        .And(List(level + 1).Optional());
        //    return item;
        //}
    }
}
