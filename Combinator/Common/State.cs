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

        public ParseResult Apply(Parser parser)
        {
            int savedPos = this.CurrentPosition;  // Сохраняем позицию
            debugInfo.Push(new AppliedRule(parser));
            debugInfo.LevelDown();
            ParseResult result = parser.Fn(this); // здесь позиция может измениться 
            debugInfo.LevelUp();
            if (result.IsSuccess)
                this.CurrentPosition += result.Increment;
            else
                this.CurrentPosition = savedPos;  // восстанавливаем позицию
            debugInfo.Last().SetResult(result.IsSuccess, savedPos, CurrentPosition, Input);
            return result;
        }

        public ParseResult Lookahead(Parser parser)
        {
            int savedPos = this.CurrentPosition;  // Сохраняем позицию
            ParseResult result = parser.Fn(this); // здесь позиция может измениться 
            this.CurrentPosition = savedPos;      // восстанавливаем позицию
            return result;
        }

        public bool Eof()
        {
            return CurrentPosition >= Input.Length;
        }
    }
}
