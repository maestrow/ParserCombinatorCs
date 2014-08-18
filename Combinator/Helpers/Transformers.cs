using System;
using System.Collections.Generic;

namespace Combinator.Helpers
{
    public static class Transformers
    {
        public static ParserFn<T2> Select<T1, T2>(this ParserFn<T1> parser, Func<T1, T2> selector)
        {
            return state =>
            {
                ParseResult<T1> result = state.Apply(parser);
                if (result.IsSuccess)
                    return ParseResult<T2>.Success(selector(result.Result), result.Increment);
                return ParseResult<T2>.Failed();
            };
        }

        public static ParserFn<string> Join(this ParserFn<IEnumerable<char>> parser)
        {
            return state =>
            {
                ParseResult<IEnumerable<char>> result = state.Apply(parser);
                if (result.IsSuccess)
                    return ParseResult<string>.Success(String.Concat(result.Result), result.Increment);
                return ParseResult<string>.Failed();
            };
        }

        public static ParserFn<string> Join(this ParserFn<IEnumerable<string>> parser)
        {
            return state =>
            {
                ParseResult<IEnumerable<string>> result = state.Apply(parser);
                if (result.IsSuccess)
                    return ParseResult<string>.Success(String.Concat(result.Result), result.Increment);
                return ParseResult<string>.Failed();
            };
        }
    }
}
