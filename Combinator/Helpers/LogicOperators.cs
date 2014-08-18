using System;
using System.Collections.Generic;

namespace Combinator.Helpers
{
    public static class LogicOperators
    {
        public static ParserFn<T2> And<T1,T2>(this ParserFn<T1> p1, ParserFn<T2> p2)
        {
            return And(p1, p2, (t1, t2) => t2);
        }

        public static ParserFn<TR> And<T1,T2,TR>(this ParserFn<T1> p1, ParserFn<T2> p2, Func<T1, T2, TR> selector)
        {
            return state =>
            {
                var fail = ParseResult<TR>.Failed();
                var r1 = state.Apply(p1);
                if (!r1.IsSuccess)
                    return fail;
                var r2 = state.Apply(p2);
                if (!r2.IsSuccess)
                    return fail;
                return ParseResult<TR>.Success(selector(r1.Result, r2.Result));
            };
        }

        public static ParserFn<IEnumerable<T>> And<T>(this IEnumerable<ParserFn<T>> parsers)
        {
            return state =>
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
            };
        }

        public static ParserFn<T> Or<T>(this ParserFn<T> p1, ParserFn<T> p2)
        {
            return Or(new List<ParserFn<T>> {p1, p2});
        }

        public static ParserFn<T> Or<T>(this IEnumerable<ParserFn<T>> parsers)
        {
            return state =>
            {
                foreach (ParserFn<T> parser in parsers)
                {
                    ParseResult<T> presult = state.Apply(parser);
                    if (presult.IsSuccess)
                        return ParseResult<T>.Success(presult.Result);
                }
                return ParseResult<T>.Failed();
            };
        }

        public static ParserFn<T> Not<T>(this ParserFn<T> parser)
        {
            return state => new ParseResult<T>(!state.Apply(parser).IsSuccess);
        }
    }
}
