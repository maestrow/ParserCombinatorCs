using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public class And: ParserFn
    {
        public IList<ParserFn> Container { get; set; }

        public And(string ruleName = null): this(new List<ParserFn>(), ruleName)
        {
        }

        public And(IEnumerable<ParserFn> parsers, string ruleName = null)
        {
            Container = parsers.ToList();
            Name = ruleName;
            this.Fn = fn;
        }

        public static And operator +(And op1, ParserFn op2)
        {
            op1.Container.Add(op2);
            return op1;
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
