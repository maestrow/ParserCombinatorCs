using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class LogicOperators
    {
        public static And And(this ParserFn p1, ParserFn p2, string ruleName = null)
        {
            return new And(new [] {p1, p2}, ruleName);
        }

        public static And And(this IEnumerable<ParserFn> parsers, string ruleName = null)
        {
            return new And(parsers, ruleName);
        }

        public static ParserFn Or(this ParserFn p1, ParserFn p2, string ruleName = null)
        {
            return Or(new List<ParserFn> {p1, p2}, ruleName);
        }

        public static ParserFn Or(this IEnumerable<ParserFn> parsers, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    foreach (ParserFn parser in parsers)
                    {
                        ParseResult presult = state.Apply(parser);
                        if (presult.IsSuccess)
                            return ParseResult.Success(presult.Result);
                    }
                    return ParseResult.Failed();
                }
            };
        }

        public static ParserFn Not(this ParserFn parser, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state => new ParseResult(!state.Apply(parser).IsSuccess)
            };
        }

    }
}
