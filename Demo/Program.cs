using System;
using System.Linq;
using Combinator;
using Combinator.Helpers;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "";
            //result = SampleGrammar.Test();
            List.Grammar.Top();

            Console.WriteLine(result);
            Console.ReadLine();
        }


    }
}
