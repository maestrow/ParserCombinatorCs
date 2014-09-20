using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;

namespace Demo.List
{
    public class Grammar
    {
        public static string Test()
        {
            ParserFn<IEnumerable<string>> rule = Root();
            var state = new State("- 1111\n- 222\n- rfrfrf");
            IParseResult<IEnumerable<string>> parseResult = state.Apply(rule);

            StringBuilder result = new StringBuilder();
            result.AppendLine(parseResult.Result.Aggregate((a, b) => string.Format("{0}, {1}", a, b)));
            result.AppendLine("\r\n=============================\r\n");
            result.AppendLine(state.debugInfo.ToString());

            return result.ToString();
        }

        public static ParserFn<IEnumerable<string>> Root()
        {
            ParserFn<char> bullet = Parser.Char('-') | Parser.Char('+') | Parser.Char('*');
            ParserFn<string> content = (Parser.RegEx(".+").ToObj() + StateIndicators.isEol().ToObj())
                .Select((List<object> values) => (string)values[0]);

            ParserFn<string> item = (StateIndicators.isBol().ToObj() + bullet.ToObj() + content.ToObj())
                .Select((List<object> values) => (string)values[2]);

            ParserFn<IEnumerable<string>> list = item.AtLeastOnce();

            return list;
        }

        //private ParseResult<ListItem> Item(State state)
        //{
        //}
    }
}
