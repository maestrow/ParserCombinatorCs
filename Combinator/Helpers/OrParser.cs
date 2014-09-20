using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Helpers
{
    public class OrParser<T>: ParserFn<T>
    {
        public IList<ParserFn<T>> Container { get; set; }

        public OrParser(string ruleName = null): this(new List<ParserFn<T>>(), ruleName)
        {
        }

        public OrParser(IList<ParserFn<T>> parsers, string ruleName = null)
        {
            Container = parsers.ToList();
            Name = ruleName;
            this.Fn = fn;
        }

        private ParseResult<T> fn(State state)
        {
            foreach (ParserFn<T> parser in Container)
            {
                IParseResult<T> presult = state.Apply(parser);
                if (presult.IsSuccess)
                    return ParseResult<T>.Success(presult.Result);
            }
            return ParseResult<T>.Failed();
        }
    }
}
