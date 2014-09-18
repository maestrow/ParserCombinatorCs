using Combinator.Fundamentals;

namespace Combinator.Helpers
{
    public static class StateIndicators
    {
        public static ParserFn<object> Begin()
        {
            return Begin<object>();
        }

        public static ParserFn<T> Begin<T>()
        {
            return new ParserFn<T>()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (state.CurrentPosition == 0)
                        return ParseResult<T>.Success();
                    return ParseResult<T>.Failed();
                }
            };
        }

        public static ParserFn<object> End()
        {
            return End<object>();
        }

        public static ParserFn<T> End<T>()
        {
            return new ParserFn<T>()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (state.CurrentPosition >= state.Input.Length)
                        return ParseResult<T>.Success();
                    return ParseResult<T>.Failed();
                }
            };
        }

        public static ParserFn<object> isBol()
        {
            return isBol<object>();
        }

        public static ParserFn<T> isBol<T>()
        {
            return new ParserFn<T>()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (state.CurrentPosition == 0 || (
                        state.CurrentPosition < state.Input.Length
                        && state.Input[state.CurrentPosition - 1] == '\n'
                        && state.Input[state.CurrentPosition] != '\n'
                        && state.Input[state.CurrentPosition] != '\r'
                        ))
                        return ParseResult<T>.Success();
                    return ParseResult<T>.Failed();
                }
            };
        }

        
        public static ParserFn<object> isEol()
        {
            return isEol<object>();
        }

        public static ParserFn<T> isEol<T>()
        {
            return new ParserFn<T>()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    bool isEnd = state.Apply(End()).IsSuccess;
                    if (isEnd || state.Input[state.CurrentPosition] == '\n')
                        return ParseResult<T>.Success(default(T), isEnd ? 0 : 1);
                    return ParseResult<T>.Failed();
                }
            };
        }
    }
}
