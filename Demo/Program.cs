﻿using System;
using System.Linq;
using Combinator;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "";
            
            //result = Company.Grammar.Test();
            result = new List.Grammar().Test();

            Console.WriteLine(result);
            Console.ReadLine();
        }


    }
}
