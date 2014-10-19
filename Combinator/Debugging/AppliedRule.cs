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

        public IParserInfo ParserInfo { get; private set; }

        /// <summary>
        /// Результат применения правила
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Позиция до применения правила
        /// </summary>
        public int PosBefore { get; private set; }

        /// <summary>
        /// Позиция после применения правила
        /// </summary>
        public int PosAfter { get; private set; }

        public string Excerpt { get; private set; }

        public void SetResult(bool isSuccess, int posBefore, int posAfter, string input)
        {
            IsSuccess = isSuccess;
            PosBefore = posBefore;
            PosAfter = posAfter;
            setExcerpt(input);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} '{2}' {3}", IsSuccess, PosAfter, Excerpt, ParserInfo);
        }

        private void setExcerpt(string input)
        {
            Excerpt = input.Substring(PosBefore, PosAfter-PosBefore);
            if (Excerpt.Length > 20)
                Excerpt = Excerpt.Substring(0, 10) + " ... " + Excerpt.Substring(Excerpt.Length - 10);
        }
    }
}
