using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Combinator.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TreeTest
    {

        private const string expectedRecursive = @"1. a1
2. a2
 2.1. b1
 2.2. b2
  2.2.1. c1
  2.2.2. c2
  2.2.3. c3
 2.3. b3
3. a3
4. a4
";

        [TestMethod]
        public void Recursive()
        {
            var tree = getTree();
            var result = tree.Recursive(action, "");
            Assert.AreEqual(expectedRecursive, result);
        }

        private Tree<string> getTree()
        {
            var tree = new Tree<string>();
            var a1 = new TreeItem<string>() {Item = "a1"};
            var a2 = new TreeItem<string>() {Item = "a2"};
            var a3 = new TreeItem<string>() {Item = "a3"};
            var a4 = new TreeItem<string>() {Item = "a4"};
            var b1 = new TreeItem<string>() {Item = "b1"};
            var b2 = new TreeItem<string>() {Item = "b2"};
            var b3 = new TreeItem<string>() {Item = "b3"};
            var c1 = new TreeItem<string>() {Item = "c1"};
            var c2 = new TreeItem<string>() {Item = "c2"};
            var c3 = new TreeItem<string>() {Item = "c3"};
            
            a2.SubTree.Add(b1);
            a2.SubTree.Add(b2);
            a2.SubTree.Add(b3);
            
            b2.SubTree.Add(c1);
            b2.SubTree.Add(c2);
            b2.SubTree.Add(c3);

            tree.Add(a1);
            tree.Add(a2);
            tree.Add(a3);
            tree.Add(a4);

            return tree;
        }

        private string action(string item, List<int> count, string result)
        {
            return result + getIndent(count.Count - 1) + getNumber(count) + item + Environment.NewLine;
        }
        
        private string getNumber(List<int> count)
        {
            return count.Select(a => a.ToString()).Aggregate((a, b) => string.Format("{0}.{1}", a, b)) + ". ";
        }

        private string getIndent(int level)
        {
            return Enumerable.Repeat(" ", level).DefaultIfEmpty().Aggregate((a, b) => string.Format("{0}{1}", a, b));
        }
    }
}
