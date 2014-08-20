using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinator.Debugging
{
    public class AppliedRule
    {
        public AppliedRule(IParserInfo parserInfo)
        {
            ParserInfo = parserInfo;
        }

        public IParserInfo ParserInfo { get; set; }

        /// <summary>
        /// Результат применения правила
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Позиция после применения правила
        /// </summary>
        public int CurrentPosition { get; private set; }

        public string Excerpt { get; private set; }

        public void SetExcerpt(int currentPosition, string input)
        {
            CurrentPosition = currentPosition;
            int cnt = Math.Min(input.Length - currentPosition, 10);
            Excerpt = input.Substring(currentPosition, cnt);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} '{2}' {3}", IsSuccess, CurrentPosition, Excerpt, ParserInfo);
        }
    }
}
