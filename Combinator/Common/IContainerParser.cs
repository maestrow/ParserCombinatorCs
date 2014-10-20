using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;

namespace Combinator
{
    public interface IContainerParser
    {
        Parser Expr { get; set; }
    }
}
