using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public class AndParser: ParserFn
    {
        public IList<ParserFn> Container { get; set; }

        public AndParser(string ruleName = null): this(new List<ParserFn>(), ruleName)
        {
        }

        public AndParser(IEnumerable<ParserFn> parsers, string ruleName = null)
        {
            Container = parsers.ToList();
            Name = ruleName;
            this.Fn = fn;
        }

        private IParseResult<object> fn(State state) 
        {
            var lisT = new List<object>();
            IParseResult<object> presult;
            foreach (ParserFn parser in Container)
            {
                presult = state.Apply(parser);
                if (!presult.IsSuccess)
                    return ParseResult<object>.Failed();
                lisT.Add(presult.Result);
            }
            return ParseResult<object>.Success(lisT);
        }

        
    }
}
