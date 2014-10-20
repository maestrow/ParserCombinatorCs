﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Combinator;
using Combinator.Helpers;
using Combinator.Infrastructure;

namespace Demo.List
{
    public class Grammar
    {
        public string Test()
        {
            Parser rule = List(0);
            var state = new State("* 111\n* 222\n - 2.1\n - 2.2\n* 333");
            StringBuilder result = new StringBuilder();

            ParseResult parseResult = state.Apply(rule);
            result.AppendLine(parseResult.Result.ToString());
            result.AppendLine("\r\n==============================================\r\n");
            result.AppendLine(state.debugInfo.ToString());

            return result.ToString();
        }

        Rule list = new Rule("list");
        Rule bullet = new Rule("bullet");
        Rule content = new Rule("content");
        
        public Grammar()
        {
            list.Expr = Item()
                .AtLeastOnce()
                .Select((List<object> x) => new List(x.Select(i => (TreeItem<string>) i)));
            
            bullet.Expr = Parsers.Char('-') | Parsers.Char('+') | Parsers.Char('*');

            content.Expr = Parsers.RegEx(@".+$", RegexOptions.Multiline);
        }

        private Parser List(int level)
        {
            return new Parser()
            {
                Name = "List",
                Fn = state =>
                {
                    state.Push(level);
                    var result = state.Apply0(list);
                    state.Pop();
                    return result;
                }
            };
        }

        private Parser Item()
        {
            return new Parser()
            {
                Name = "Item",
                Fn = state =>
                {
                    state.debugInfo.ParentLast().CustomInfo = state.Peek().ToString();

                    int level = (int)state.Peek();

                    Rule item = new Rule("item");

                    item.Expr = (StateIndicators.isBol()
                        + Parsers.Char(' ').RepeatExactly(level).Join()
                        + bullet
                        + Parsers.Char(' ')
                        + content
                        + StateIndicators.isEol()
                        + List(level+1).Optional())
                        .Select((List<object> i) =>
                        {
                            TreeItem<string> treeItem = new TreeItem<string>();
                            treeItem.Item = i[4].ToString();
                            treeItem.SubTree = (Tree<string>)i[6] ?? new Tree<string>();
                            return treeItem;
                        });

                    return state.Apply0(item);
                }
            };
        }
    }
}
