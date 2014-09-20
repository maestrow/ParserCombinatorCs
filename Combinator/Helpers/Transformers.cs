using System;
using System.Collections.Generic;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Transformers
    {
        public static ParserFn ToObj<T>(this ParserFn<T> parser)
        {
            return new ParserFn()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    IParseResult<T> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<object>.Success(result.Result);
                    return ParseResult<object>.Failed();
                }
            };
        }

        public static ParserFn<TR> Select<T1, TR>(this ParserFn parser, Func<T1, TR> selector, string ruleName = null)
        {
            return new ParserFn<TR>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    IParseResult<object> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<TR>.Success(selector((T1)result.Result));
                    return ParseResult<TR>.Failed();
                }
            };
        }

        public static ParserFn<TR> Select<T1, TR>(this ParserFn<T1> parser, Func<T1, TR> selector, string ruleName = null)
        {
            return new ParserFn<TR>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    IParseResult<T1> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<TR>.Success(selector(result.Result));
                    return ParseResult<TR>.Failed();
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
                    IParseResult<IEnumerable<char>> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<string>.Success(String.Concat(result.Result));
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
                    IParseResult<IEnumerable<string>> result = state.Apply(parser);
                    if (result.IsSuccess)
                        return ParseResult<string>.Success(String.Concat(result.Result));
                    return ParseResult<string>.Failed();
                }
            };
        }

    }
}
