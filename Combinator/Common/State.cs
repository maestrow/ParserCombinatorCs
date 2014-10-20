using System.Collections;
using System.Collections.Generic;
using Combinator.Common;
using Combinator.Containers.Abstract;
using Combinator.Debugging;

namespace Combinator
{
    public class State: IArgumentsProvider
    {
        private Stack<object> buffer = new Stack<object>();

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

        /// <summary>
        /// Применяет парсер. В случае успеха <see cref="CurrentPosition"/> изменяется (обычное поведение).
        /// Возвращает результат применения парсера, но с Increment = 0.
        /// Данный метод полезен в <see cref="ContainerParser">парсерах-контейнерах</see>, когда требуется применить парсер, находящийся внутри контейнера.
        /// </summary>
        /// <remarks>
        /// Предположим, что метод парсера-контейнера состоит только из инструкции: return state.Apply(Expr).
        /// Тогда при применении парсера-контейнера произойдет следующее:
        /// - сначала применится содержимое контейнера (Expr), 
        /// - а затем сам койтейнер. 
        /// Смещение <see cref="CurrentPosition">текущей позиции</see> произойдет дважды.
        /// Чтобы этого не происходило, контейнеры, которые сами не перемещают позицию, должны использовать данный метод для применения содержимого.
        /// </remarks>
        /// <param name="parser"></param>
        /// <returns></returns>
        public ParseResult Apply0(Parser parser)
        {
            var result = Apply(parser);
            return new ParseResult(result.IsSuccess, result.Result, 0);
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

        public object Pop()
        {
            return buffer.Pop();
        }

        public void Push(object obj)
        {
            buffer.Push(obj);
        }

        public object Peek()
        {
            return buffer.Peek();
        }
    }
}
