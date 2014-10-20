using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;

namespace Combinator
{
    /// <summary>
    /// </summary>
    public interface IListContainerParser
    {
        IList<Parser> Container { get; set; }
    }
}
