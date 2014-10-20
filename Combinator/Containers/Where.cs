using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;
using Combinator.Containers.Abstract;
using Combinator.Infrastructure;

namespace Combinator.Containers
{
    public class Where<T>: ContainerParser
    {
        public Where(Parser parser, Predicate<T> predicate): base(parser)
        {
            Name = GetType().Name;
            Predicate = predicate;
        }

        public Predicate<T> Predicate { get; set; }

        protected override ParseResult ParseFn(State state)
        {
            ParseResult presult = state.Apply(Expr);
            if (presult.IsSuccess && Predicate((T)presult.Result))
                return ParseResult.Success(presult.Result);
            return ParseResult.Failed();
        }
    }

    public static class WhereGen
    {
        public static Where<T> Where<T>(this Parser parser, Predicate<T> predicate)
        {
            return new Where<T>(parser, predicate);
        }
    }
}
