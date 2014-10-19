namespace Combinator
{
    public class ParseResult
    {
        #region Static

        public static ParseResult Success(object result, int increment)
        {
            return new ParseResult(true, result, increment);
        }

        public static ParseResult Success(object result)
        {
            return new ParseResult(true, result, 0);
        }

        public static ParseResult Success()
        {
            return new ParseResult(true);
        }

        public static ParseResult Failed()
        {
            return new ParseResult();
        }

        #endregion



        #region Constructors

        private ParseResult()
        {
        }

        public ParseResult(bool success, object result, int increment)
        {
            IsSuccess = success;
            Result = result;
            Increment = increment;
        }

        public ParseResult(bool success, int increment): this(success, default(object), increment)
        {
        }

        public ParseResult(bool success): this(success, default(object), 0)
        {
        }

        #endregion



        #region Properties

        public bool IsSuccess { get; private set; }

        public object Result { get; private set; }

        public int Increment { get; private set; }

        #endregion
    }
}
