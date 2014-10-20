using System.Collections.Generic;

namespace Combinator
{
    public interface IParserInfo
    {
        string Name { get; set; }

        string Description { get; set; }

        Dictionary<string, object> Parameters { get; set; }

        string ToString();
    }
}
