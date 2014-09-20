using Combinator.Debugging;

namespace Combinator
{
    public class State
    {
        public DebugInfo debugInfo { get; private set; }

        public State(string input)
        {
            Input = input;
            debugInfo = new DebugInfo();
        }
        
        public string Input { get; private set; }

        public int CurrentPosition { get; private set; }

        public IParseResult<T> Apply<T>(ParserFn<T> parser)
        {
            int savedPos = this.CurrentPosition;  // Сохраняем позицию
            debugInfo.Push(new AppliedRule(parser));
            debugInfo.LevelUp();
            IParseResult<T> result = parser.Fn(this); // здесь позиция может измениться 
            debugInfo.LevelDown();
            if (result.IsSuccess)
                this.CurrentPosition += result.Increment;
            else
                this.CurrentPosition = savedPos;  // восстанавливаем позицию
            debugInfo.Last().IsSuccess = result.IsSuccess;
            debugInfo.Last().SetExcerpt(CurrentPosition, Input);
            return result;
        }

        public IParseResult<T> Lookahead<T>(ParserFn<T> parser)
        {
            int savedPos = this.CurrentPosition;  // Сохраняем позицию
            IParseResult<T> result = parser.Fn(this); // здесь позиция может измениться 
            this.CurrentPosition = savedPos;      // восстанавливаем позицию
            return result;
        }

        public bool Eof()
        {
            return CurrentPosition >= Input.Length;
        }
    }
}
