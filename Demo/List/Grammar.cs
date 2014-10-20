using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Combinator;
using Combinator.Atomics;
using Combinator.Common;
using Combinator.Generators;
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

        private Rule top = new Rule("top");
        private Rule list = new Rule("list");
        private Rule item = new Rule("item");
        private Rule bullet = new Rule("bullet");
        private Rule content = new Rule("content");
        
        public Grammar()
        {
            top.Expr = list.Arg(0);
            list.Expr = Parsers.Generate(listGen);
            item.Expr = Parsers.Generate(itemGen);
            bullet.Expr = Parsers.Char('-') | Parsers.Char('+') | Parsers.Char('*');
            content.Expr = Parsers.RegEx(@".+$", RegexOptions.Multiline);
        }

        private Parser listGen(IArgumentsProvider args)
        {
            int level = (int)args.Pop();
            return item.Arg(level)
                .AtLeastOnce()
                .Select((List<object> x) => new List(x.Select(i => (TreeItem<string>)i)));
        }

        private Parser indentGen(IArgumentsProvider args)
        {
            int level = (int)args.Pop();
            return Parsers.Char(' ').RepeatExactly(level).Join();
        }

        private Parser itemGen(IArgumentsProvider args)
        {
            args.debugInfo.ParentLast().CustomInfo = args.Peek().ToString();

            int level = (int)args.Pop();

            return (StateIndicators.isBol()
                + Parsers.Generate(indentGen).Arg(level)
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
