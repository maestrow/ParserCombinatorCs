using System;
using System.Collections.Generic;
using Combinator.Containers;
using Combinator.Infrastructure;

namespace Combinator.Helpers
{
    public static class Transformers
    {
        public static Where<T> Where<T>(this Parser parser, Predicate<T> predicate)
        {
            return new Where<T>(parser, predicate);
        }

        public static Parser Select<T, TR>(this Parser parser, Func<T, TR> selector)
        {
            return new Select<T, TR>(parser, selector);
        }

        public static Parser Join(this Parser parser)
        {
            return new Join(parser);
        }

    }
}
