using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Optimization
{
    //public class AndOptimizer : IOptimizer
    //{
    //    private And result { get; set; }

    //    public Parser Optimize(Parser parser)
    //    {
    //        if (!(parser is And))
    //            throw new ArgumentException();
    //        result = new And(parser.Name);
    //        optimize((And)parser);
    //        return result;
    //    }

    //    private void optimize(And parent)
    //    {
    //        foreach (Parser parserFn in parent.Container)
    //        {
    //            if (parserFn is And)
    //                optimize((And)parserFn);
    //            else
    //            {
    //                result.Container.Add(parserFn);
    //            }
    //        }
    //    }
    //}
}
