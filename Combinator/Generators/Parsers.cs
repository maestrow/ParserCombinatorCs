using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Combinator.Common;
using Combinator.Helpers;
using Combinator.Infrastructure;

namespace Combinator
{
    public static class Parsers
    {
        public static Parser Char()
        {
            return new Parser
            {
                Name = Helper.GetCurrentMethod(),
                Description = "Any char",
                Fn = state =>
                {
                    if (!state.Eof())
                    {
                        return ParseResult.Success(state.Input[state.CurrentPosition], 1);
                    }
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser Char(char ch)
        {
            return new Parser
            {
                Name = Helper.GetCurrentMethod(),
                Description = "Specified char",
                Parameters = new Dictionary<string, object>() {{"ch", ch}},
                Fn = state =>
                {
                    if (!state.Eof() && ch == state.Input[state.CurrentPosition])
                    {
                        return ParseResult.Success(ch, 1);
                    }
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser String(string substring)
        {
            return new Parser()
            {
                Name  = Helper.GetCurrentMethod(),
                Parameters = new Dictionary<string, object>() {{"substring", substring}},
                Fn = state =>
                {
                    if (checkString(substring, state))
                        return ParseResult.Success(substring, substring.Length);
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser Strings(List<string> substrings)
        {
            return new Parser()
            {
                Name  = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    foreach (string substring in substrings)
                    {
                        if (checkString(substring, state))
                            return ParseResult.Success(substring, substring.Length);
                    }
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser RegEx(string pattern, RegexOptions options = RegexOptions.None)
        {
            return new Parser()
            {
                Name  = Helper.GetCurrentMethod(),
                Parameters = new Dictionary<string, object>() {{"pattern", pattern}},
                Fn = state =>
                {
                    if (!pattern.StartsWith("^"))
                        pattern = "^" + pattern;
                    string rest = state.Input.Substring(state.CurrentPosition);

                    var regex = new Regex(pattern, options);
                    Match match = regex.Match(rest);

                    if (match.Success)
                        return ParseResult.Success(match.Value, match.Value.Length);
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser Generate(ParserGenerator generator)
        {
            return new Parser(state =>
            {
                return state.Apply0(generator(state));
            });
        }

        private static bool checkString(string substring, State state)
        {
            return state.CurrentPosition < state.Input.Length - substring.Length
                   && state.Input.Substring(state.CurrentPosition, substring.Length) == substring;
        }

    }
}
