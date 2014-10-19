using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class StateIndicators
    {

        public static Parser Begin()
        {
            return new Parser()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (state.CurrentPosition == 0)
                        return ParseResult.Success();
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser End()
        {
            return new Parser()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    if (state.CurrentPosition >= state.Input.Length)
                        return ParseResult.Success();
                    return ParseResult.Failed();
                }
            };
        }

        public static Parser isBol()
        {
            return new Parser()
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
                        return ParseResult.Success();
                    return ParseResult.Failed();
                }
            };
        }


        public static Parser isEol()
        {
            return new Parser()
            {
                Name = Helper.GetCurrentMethod(),
                Fn = state =>
                {
                    bool isEnd = state.Apply(End()).IsSuccess;
                    if (isEnd || state.Input[state.CurrentPosition] == '\n')
                        return ParseResult.Success(default(object), isEnd ? 0 : 1);
                    return ParseResult.Failed();
                }
            };
        }
    }
}
