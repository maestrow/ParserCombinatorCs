using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Combinator
{
    public static class Parser
    {
        public static ParserFn<char> Char()
        {
            return state =>
            {
                if (!state.Eof())
                {
                    return ParseResult<char>.Success(state.Input[state.CurrentPosition], 1);
                }
                return ParseResult<char>.Failed();
            };
        }

        public static ParserFn<char> Char(char ch)
        {
            return state =>
            {
                if (!state.Eof() && ch == state.Input[state.CurrentPosition])
                {
                    return ParseResult<char>.Success(ch, 1);
                }
                return ParseResult<char>.Failed();
            };
        }

        public static ParserFn<string> String(string substring)
        {
            return state =>
            {
                if (checkString(substring, state))
                    return ParseResult<string>.Success(substring, substring.Length);
                return ParseResult<string>.Failed();
            };
        }

        public static ParserFn<string> Strings(List<string> substrings)
        {
            return state =>
            {
                foreach (string substring in substrings)
                {
                    if (checkString(substring, state))
                        return ParseResult<string>.Success(substring, substring.Length);
                }
                return ParseResult<string>.Failed();
            };
        }

        public static ParserFn<string> RegEx(string pattern)
        {
            return state =>
            {
                if (!pattern.StartsWith("^"))
                    pattern = "^" + pattern;
                string rest = state.Input.Substring(state.CurrentPosition);

                var regex = new Regex(pattern);
                Match match = regex.Match(rest);

                if (match.Success)
                    return ParseResult<string>.Success(match.Value, match.Value.Length);
                return ParseResult<string>.Failed();
            };
        }

        private static bool checkString(string substring, State state)
        {
            return state.CurrentPosition < state.Input.Length - substring.Length
                   && state.Input.Substring(state.CurrentPosition, substring.Length) == substring;
        }

    }
}
