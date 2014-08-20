using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Debugging
{
    public interface IParserInfo
    {
        string Name { get; set; }
        Dictionary<string, string> CtorParams { get; set; }
        string ToString();
    }
}
