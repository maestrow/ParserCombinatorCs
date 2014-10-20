using System.Collections.Generic;
using System.Linq;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Quantifiers
    {
        public static Parser AtLeastOnce(this Parser parser)
        {
            return new Parser()
            {
                Name  = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    List<object> listResult = many(parser, state);

                    if (listResult.Any())
                        return ParseResult.Success(listResult);
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser Any(this Parser parser)
        {
            return new Parser()
            {
                Name  = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    return ParseResult.Success(many(parser, state));
                }
            };
        }

        public static Parser Optional(this Parser parser)
        {
            return new Parser()
            {
                Name  = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    var presult = state.Apply(parser);
                    return ParseResult.Success(presult.Result);
                }
            };
        }

        public static Parser RepeatExactly(this Parser parser, int count)
        {
            return new Parser()
            {
                Name  = Helper.GetCurrentMethod(),
                Parameters = new Dictionary<string, object>() {{"count", count}},
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

        private static List<object> many(Parser parser, State state)
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
