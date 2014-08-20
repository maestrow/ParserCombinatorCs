using System;
using System.Collections.Generic;
using System.Linq;
using Combinator.Debugging;

namespace Combinator
{
    public delegate ParseResult<T> ParserDelegate<T>(State state);

    public class ParserFn<T> : IParserInfo
    {
        
        public ParserDelegate<T> Fn;

        public string Name { get; set; }
        public Dictionary<string, string> CtorParams { get; set; }


        public override string ToString()
        {
            return string.Format("{0}: {1}", 
                Name, 
                CtorParams
                    .Select(KeyValueSelector)
                    .Aggregate((a, b) => string.Format("{0}, {1}", a, b))
            );
        }

        private Func<KeyValuePair<string, string>, string> KeyValueSelector
        {
            get
            {
                return kvp => string.Format("{0} = {1}", kvp.Key, kvp.Value);
            }
        }
    }

    
}
