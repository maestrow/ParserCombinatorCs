using System;
using System.Collections.Generic;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Predicates
    {
        public static ParserFn<T> Where<T>(this ParserFn<T> parser, Predicate<T> predicate, string ruleName = null)
        {
            return new ParserFn<T>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    ParseResult<T> presult = state.Apply(parser);
                    if (presult.IsSuccess && predicate(presult.Result))
                        return ParseResult<T>.Success(presult.Result);
                    return ParseResult<T>.Failed();
                }
            };
        }
    }
}
