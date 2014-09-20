using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Optimization
{
    public interface IOptimizer
    {
        ParserFn Optimize(ParserFn parser);
    }
}
