using System.Collections.Generic;
using System.Linq;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Quantifiers
    {
        public static ParserFn AtLeastOnce(this ParserFn parser, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    List<object> listResult = many(parser, state);

                    if (listResult.Any())
                        return ParseResult.Success(listResult);
                    return ParseResult.Failed();
                }
            };
        }

        public static ParserFn Any(this ParserFn parser, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    return ParseResult.Success(many(parser, state));
                }
            };
        }

        public static ParserFn Optional(this ParserFn parser, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    var presult = state.Apply(parser);
                    return ParseResult.Success(presult.Result);
                }
            };
        }

        public static ParserFn RepeatExactly(this ParserFn parser, int count, string ruleName = null)
        {
            return new ParserFn()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    var listResult = new List<object>();
                    ParseResult presult;
                    do
                    {
                        presult = state.Apply(parser);
                        listResult.Add(presult.Result);
                    } while (presult.IsSuccess && count-- > 0);

                    if (count == 0)
                        return ParseResult.Success(listResult);
                    return ParseResult.Failed();
                }
            };
        }

        private static List<object> many(ParserFn parser, State state)
        {
            var listResult = new List<object>();
            ParseResult presult;
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
