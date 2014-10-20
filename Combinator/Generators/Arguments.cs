using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;
using Combinator.Containers;
using Combinator.Containers.Abstract;

namespace Combinator.Generators
{
    public static class Arguments
    {
        public static Parser Arg<T>(this Parser parser, T arg)
        {
            return new Arg<T>(parser, arg);
        }
    }
}
