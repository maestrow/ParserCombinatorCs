﻿using System.Collections.Generic;
using System.Linq;
using Combinator.Fundamentals;

namespace Combinator.Helpers
{
    public static class Quantifiers
    {
        public static ParserFn<IEnumerable<T>> AtLeastOnce<T>(this ParserFn<T> parser, string ruleName = null)
        {
            return new ParserFn<IEnumerable<T>>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                CtorParams = new Dictionary<string, string>() {{"parser", parser.Name}},
                Fn = state =>
                {
                    List<T> listResult = many(parser, state);

                    if (listResult.Any())
                        return ParseResult<IEnumerable<T>>.Success(listResult);
                    return ParseResult<IEnumerable<T>>.Failed();
                }
            };
        }

        public static ParserFn<IEnumerable<T>> Any<T>(this ParserFn<T> parser, string ruleName = null)
        {
            return new ParserFn<IEnumerable<T>>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                CtorParams = new Dictionary<string, string>() {{"parser", parser.Name}},
                Fn = state =>
                {
                    return ParseResult<IEnumerable<T>>.Success(many(parser, state));
                }
            };
        }

        public static ParserFn<T> Optional<T>(this ParserFn<T> parser, string ruleName = null)
        {
            return new ParserFn<T>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                CtorParams = new Dictionary<string, string>() {{"parser", parser.Name}},
                Fn = state =>
                {
                    var presult = state.Apply(parser);
                    return ParseResult<T>.Success(presult.Result);
                }
            };
        }

        public static ParserFn<IEnumerable<T>> RepeatExactly<T>(this ParserFn<T> parser, int count, string ruleName = null)
        {
            return new ParserFn<IEnumerable<T>>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                CtorParams = new Dictionary<string, string>()
                {
                    {"parser", parser.Name},
                    {"count", count.ToString()}
                },
                Fn = state =>
                {
                    var listResult = new List<T>();
                    ParseResult<T> presult;
                    do
                    {
                        presult = state.Apply(parser);
                        listResult.Add(presult.Result);
                    } while (presult.IsSuccess && count-- > 0);

                    if (count == 0)
                        return ParseResult<IEnumerable<T>>.Success(listResult);
                    return ParseResult<IEnumerable<T>>.Failed();
                }
            };
        }

        private static List<T> many<T>(ParserFn<T> parser, State state)
        {
            var listResult = new List<T>();
            ParseResult<T> presult;
            do
            {
                presult = state.Apply(parser);
                if (presult.IsSuccess)
                    listResult.Add(presult.Result);
            } while (presult.IsSuccess);

            return listResult;
        }
    }
}
