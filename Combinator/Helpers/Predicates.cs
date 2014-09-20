using System;
using System.Collections.Generic;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Predicates
    {
        public static ParserFn Where<T>(this ParserFn parser, Predicate<T> predicate, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    ParseResult presult = state.Apply(parser);
                    if (presult.IsSuccess && predicate((T)presult.Result))
                        return ParseResult.Success(presult.Result);
                    return ParseResult.Failed();
                }
            };
        }
    }
}
