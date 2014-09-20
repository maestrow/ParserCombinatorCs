using System;
using System.Collections.Generic;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Transformers
    {
        public static ParserFn Select<T1, T2>(this ParserFn parser, Func<T1, T2> selector, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    ParseResult result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult.Success(selector((T1)result.Result), result.Increment);
                    return ParseResult.Failed();
                }
            };
        }

        public static ParserFn Join(this ParserFn parser, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    ParseResult result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult.Success(String.Concat(result.Result), result.Increment);
                    return ParseResult.Failed();
                }
            };
        }

    }
}
