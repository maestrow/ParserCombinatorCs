namespace Combinator
{
    public class State
    {
        public State(string input)
        {
            Input = input;
        }
        
        public string Input { get; private set; }

        public int CurrentPosition { get; private set; }

        public ParseResult<T> Apply<T>(ParserFn<T> parser)
        {
            int savedPos = this.CurrentPosition;  // Сохраняем позицию
            ParseResult<T> result = parser(this); // здесь позиция может измениться 
            if (result.IsSuccess)
                this.CurrentPosition += result.Increment;
            else
                this.CurrentPosition = savedPos;  // восстанавливаем позицию
            return result;
        }

        public ParseResult<T> Lookahead<T>(ParserFn<T> parser)
        {
            int savedPos = this.CurrentPosition;  // Сохраняем позицию
            ParseResult<T> result = parser(this); // здесь позиция может измениться 
            this.CurrentPosition = savedPos;      // восстанавливаем позицию
            return result;
        }

        public bool Eof()
        {
            return CurrentPosition >= Input.Length;
        }
    }
}
