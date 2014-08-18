using System;

namespace Combinator.Helpers
{
    public static class Predicates
    {
        public static ParserFn<T> Where<T>(this ParserFn<T> parser, Predicate<T> predicate)
        {
            return state =>
            {
                ParseResult<T> presult = state.Apply(parser);
                if (presult.IsSuccess && predicate(presult.Result))
                    return ParseResult<T>.Success(presult.Result);
                return ParseResult<T>.Failed();
            };
        }
    }
}
