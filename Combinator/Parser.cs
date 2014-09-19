using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Combinator.Helpers;
using Combinator.Infrastructure;

namespace Combinator
{
    public static class Parser
    {
        public static ParserFn<char> Char(string ruleName = null)
        {
            return new ParserFn<char>
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (!state.Eof())
                    {
                        return ParseResult<char>.Success(state.Input[state.CurrentPosition], 1);
                    }
                    return ParseResult<char>.Failed();
                }
            };
        }

        public static ParserFn<char> Char(char ch, string ruleName = null)
        {
            return new ParserFn<char>
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (!state.Eof() && ch == state.Input[state.CurrentPosition])
                    {
                        return ParseResult<char>.Success(ch, 1);
                    }
                    return ParseResult<char>.Failed();
                }
            };
        }

        public static ParserFn<string> String(string substring, string ruleName = null)
        {
            return new ParserFn<string>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (checkString(substring, state))
                        return ParseResult<string>.Success(substring, substring.Length);
                    return ParseResult<string>.Failed();
                }
            };
        }

        public static ParserFn<string> Strings(List<string> substrings, string ruleName = null)
        {
            return new ParserFn<string>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    foreach (string substring in substrings)
                    {
                        if (checkString(substring, state))
                            return ParseResult<string>.Success(substring, substring.Length);
                    }
                    return ParseResult<string>.Failed();
                }
            };
        }

        public static ParserFn<string> RegEx(string pattern, string ruleName = null)
        {
            return new ParserFn<string>()
            {
                Name = ruleName ?? Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (!pattern.StartsWith("^"))
                        pattern = "^" + pattern;
                    string rest = state.Input.Substring(state.CurrentPosition);

                    var regex = new Regex(pattern);
                    Match match = regex.Match(rest);

                    if (match.Success)
                        return ParseResult<string>.Success(match.Value, match.Value.Length);
                    return ParseResult<string>.Failed();
                }
            };
        }

        private static bool checkString(string substring, State state)
        {
            return state.CurrentPosition < state.Input.Length - substring.Length
                   && state.Input.Substring(state.CurrentPosition, substring.Length) == substring;
        }

    }
}
