using System;
using System.Collections.Generic;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Transformers
    {
        public static ParserFn<object> ToObj<T>(this ParserFn<T> parser)
        {
            return parser.Select(a => (object)a);
        }
        
        public static ParserFn<T2> Select<T1, T2>(this ParserFn<T1> parser, Func<T1, T2> selector, string ruleName = null)
        {
            return new ParserFn<T2>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    ParseResult<T1> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<T2>.Success(selector(result.Result), result.Increment);
                    return ParseResult<T2>.Failed();
                }
            };
        }

        public static ParserFn<string> Join(this ParserFn<IEnumerable<char>> parser, string ruleName = null)
        {
            return new ParserFn<string>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    ParseResult<IEnumerable<char>> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<string>.Success(String.Concat(result.Result), result.Increment);
                    return ParseResult<string>.Failed();
                }
            };
        }

        public static ParserFn<string> Join(this ParserFn<IEnumerable<string>> parser, string ruleName = null)
        {
            return new ParserFn<string>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    ParseResult<IEnumerable<string>> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<string>.Success(String.Concat(result.Result), result.Increment);
                    return ParseResult<string>.Failed();
                }
            };
        }

    }
}
