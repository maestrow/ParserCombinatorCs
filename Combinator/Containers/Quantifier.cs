using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combinator.Common;
using Combinator.Containers.Abstract;

namespace Combinator.Containers
{
    public enum QuantifierType
    {
        AtLeastOnce = 1,
        Any = 2,
        Optional = 3,
        RepeatExactly = 4
    }

    public class Quantifier: ContainerParser
    {
        public int RepeatCount { get; set; }

        public Quantifier(Parser parser, QuantifierType type, int count = 0)
        {
            Name = GetType().Name;
            Parameters = new Dictionary<string, object>() {{"type", typeToString(type, count)}};
            Expr = parser;
            Type = type;
            RepeatCount = count;
        }

        public QuantifierType Type { get; private set; }

        protected override ParseResult ParseFn(State state)
        {
            switch (Type)
            {
                case QuantifierType.AtLeastOnce:
                    List<object> listResult = many(state);

                    if (listResult.Any())
                        return ParseResult.Success(listResult);
                    return ParseResult.Failed();
                case QuantifierType.Any:
                    return ParseResult.Success(many(state));
                case QuantifierType.Optional:
                    var presult = state.Apply(Expr);
                    return ParseResult.Success(presult.Result);
                case QuantifierType.RepeatExactly:
                    return repeatExactly(state);

            }

            throw new ArgumentException("Type");
        }

        private ParseResult repeatExactly(State state)
        {
            var listResult = new List<object>();
            ParseResult presult;
            int count = RepeatCount;
            do
            {
                presult = state.Apply(Expr);
                listResult.Add(presult.Result);
            } while (presult.IsSuccess && count-- > 0);

            if (count == 0)
                return ParseResult.Success(listResult);
            return ParseResult.Failed();
        }

        private List<object> many(State state)
        {
            var listResult = new List<object>();
            ParseResult presult;
            do
            {
                presult = state.Apply(Expr);
                if (presult.IsSuccess)
                    listResult.Add(presult.Result);
            } while (presult.IsSuccess);

            return listResult;
        }

        private string typeToString(QuantifierType type, int count)
        {
            switch (type)
            {
                case QuantifierType.Any:
                    return "*";
                case QuantifierType.AtLeastOnce:
                    return "+";
                case QuantifierType.Optional:
                    return "?";
                case QuantifierType.RepeatExactly:
                    return count.ToString();
            }

            throw new ArgumentException("type");
        }
    }

    public static class QuantifierGen
    {
        public static Quantifier AtLeastOnce(this Parser parser)
        {
            return new Quantifier(parser, QuantifierType.AtLeastOnce);
        }

        public static Quantifier Any(this Parser parser)
        {
            return new Quantifier(parser, QuantifierType.Any);
        }

        public static Quantifier Optional(this Parser parser)
        {
            return new Quantifier(parser, QuantifierType.Optional);
        }

        public static Quantifier RepeatExactly(this Parser parser, int count)
        {
            return new Quantifier(parser, QuantifierType.RepeatExactly, count);
        }
    }
}
