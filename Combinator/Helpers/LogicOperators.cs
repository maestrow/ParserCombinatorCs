using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Combinator.Fundamentals;

namespace Combinator.Helpers
{
    public static class LogicOperators
    {
        public static ParserFn<T2> And<T1, T2>(this ParserFn<T1> p1, ParserFn<T2> p2, string ruleName = null)
        {
            return And(p1, p2, (t1, t2) => t2, ruleName);
        }

        public static ParserFn<TR> And<T1, T2, TR>(this ParserFn<T1> p1, ParserFn<T2> p2, Func<T1, T2, TR> selector, string ruleName = null)
        {
            return new ParserFn<TR>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    var fail = ParseResult<TR>.Failed();
                    var r1 = state.Apply(p1);
                    if (!r1.IsSuccess)
                        return fail;
                    var r2 = state.Apply(p2);
                    if (!r2.IsSuccess)
                        return fail;
                    return ParseResult<TR>.Success(selector(r1.Result, r2.Result));
                }
            };
        }

        public static ParserFn<IEnumerable<T>> And<T>(this IEnumerable<ParserFn<T>> parsers, string ruleName = null)
        {
            return new ParserFn<IEnumerable<T>>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    var lisT = new List<T>();
                    ParseResult<T> presult;
                    foreach (ParserFn<T> parser in parsers)
                    {
                        presult = state.Apply(parser);
                        if (!presult.IsSuccess)
                            return ParseResult<IEnumerable<T>>.Failed();
                        lisT.Add(presult.Result);
                    }
                    return ParseResult<IEnumerable<T>>.Success(lisT);
                }
            };
        }

        public static ParserFn<T> Or<T>(this ParserFn<T> p1, ParserFn<T> p2, string ruleName = null)
        {
            return Or(new List<ParserFn<T>> {p1, p2}, ruleName);
        }

        public static ParserFn<T> Or<T>(this IEnumerable<ParserFn<T>> parsers, string ruleName = null)
        {
            return new ParserFn<T>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    foreach (ParserFn<T> parser in parsers)
                    {
                        ParseResult<T> presult = state.Apply(parser);
                        if (presult.IsSuccess)
                            return ParseResult<T>.Success(presult.Result);
                    }
                    return ParseResult<T>.Failed();
                }
            };
        }

        public static ParserFn<T> Not<T>(this ParserFn<T> parser, string ruleName = null)
        {
            return new ParserFn<T>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state => new ParseResult<T>(!state.Apply(parser).IsSuccess)
            };
        }

        private static string ParsersToString<T>(IEnumerable<ParserFn<T>> parsers)
        {
            return parsers.Select(a => a.Name).Aggregate((a, b) => string.Format("{0}, {1}", a, b));
        }

    }
}
