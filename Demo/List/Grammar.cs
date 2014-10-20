using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Combinator;
using Combinator.Common;
using Combinator.Generators;
using Combinator.Helpers;
using Combinator.Infrastructure;

namespace Demo.List
{
    public class Grammar
    {
        public string Test()
        {
            Parser rule = top;
            var state = new State("* 111\n* 222\n - 2.1\n - 2.2\n* 333");
            StringBuilder result = new StringBuilder();

            ParseResult parseResult = state.Apply(rule);
            result.AppendLine(parseResult.Result.ToString());
            result.AppendLine("\r\n==============================================\r\n");
            result.AppendLine(state.debugInfo.ToString());

            return result.ToString();
        }

        private Rule top;
        private Rule list;
        private Rule item;
        private Rule indent;
        private Rule bullet;
        private Rule content;
        
        public Grammar()
        {
            top = new Rule("top");
            list = new Rule("list");
            item = new Rule("item", (ParserGenerator)itemFn);
            bullet = new Rule("bullet");
            content = new Rule("content");

            top.Expr = list.Arg(0);

            list.Expr = item
                .AtLeastOnce()
                .Select((List<object> x) => new List(x.Select(i => (TreeItem<string>) i)));

            item.Expr = Parsers.Generate(itemFn);

            bullet.Expr = Parsers.Char('-') | Parsers.Char('+') | Parsers.Char('*');

            content.Expr = Parsers.RegEx(@".+$", RegexOptions.Multiline);
        }

        private Parser indentFn(IArgumentsProvider args)
        {
            int level = (int)args.Peek();
            return Parsers.Char(' ').RepeatExactly(level).Join();
        }

        private Parser itemFn(IArgumentsProvider args)
        {
            args.debugInfo.ParentLast().CustomInfo = args.Peek().ToString();

            int level = (int)args.Peek();

            return (StateIndicators.isBol()
                + Parsers.Generate(indentFn)
                + bullet
                + Parsers.Char(' ')
                + content
                + StateIndicators.isEol()
                + list.Arg(level + 1).Optional())
                .Select((List<object> i) =>
                {
                    TreeItem<string> treeItem = new TreeItem<string>();
                    treeItem.Item = i[4].ToString();
                    treeItem.SubTree = (Tree<string>) i[6] ?? new Tree<string>();
                    return treeItem;
                });
        }
    }
}
