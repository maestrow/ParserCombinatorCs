namespace Combinator
{
    public interface IParseResult<out T>
    {
        bool IsSuccess { get; }

        T Result { get; }

        int Increment { get; }
    }

    public class ParseResult<T>: IParseResult<T>
    {
        #region Static

        public static ParseResult<T> Success(T result, int increment)
        {
            return new ParseResult<T>(true, result, increment);
        }

        public static ParseResult<T> Success(T result)
        {
            return new ParseResult<T>(true, result, 0);
        }

        public static ParseResult<T> Success()
        {
            return new ParseResult<T>(true);
        }

        public static ParseResult<T> Failed()
        {
            return new ParseResult<T>();
        }

        #endregion



        #region Constructors

        private ParseResult()
        {
        }

        public ParseResult(bool success, T result, int increment)
        {
            IsSuccess = success;
            Result = result;
            Increment = increment;
        }

        public ParseResult(bool success, int increment): this(success, default(T), increment)
        {
        }

        public ParseResult(bool success): this(success, default(T), 0)
        {
        }

        #endregion



        #region Properties

        public bool IsSuccess { get; private set; }

        public T Result { get; private set; }

        public int Increment { get; private set; }

        #endregion
    }

}
