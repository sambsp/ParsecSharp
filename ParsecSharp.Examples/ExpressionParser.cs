using System;
using System.Linq.Expressions;
using static ParsecSharp.Parser;
using static ParsecSharp.Text;

namespace ParsecSharp.Examples
{
    public interface INumber<TNumber>
        where TNumber : INumber<TNumber>
    {
        TNumber Add(TNumber value);

        TNumber Sub(TNumber value);

        TNumber Mul(TNumber value);

        TNumber Div(TNumber value);
    }

    public class ExpressionParser<TNumber>
        where TNumber : INumber<TNumber>
    {
        private readonly Parser<char, TNumber> _expr;

        public Parser<char, TNumber> Parser { get; }

        public ExpressionParser(Parser<char, TNumber> number)
        {
            var addsub = Op('+', (x, y) => x.Add(y)) | Op('-', (x, y) => x.Sub(y));
            var muldiv = Op('*', (x, y) => x.Mul(y)) | Op('/', (x, y) => x.Div(y));

            var open = Char('(').Between(Spaces());
            var close = Char(')').Between(Spaces());

            var factor = number | Delay(() => this._expr).Between(open, close);
            var term = factor.ChainLeft(muldiv);
            this._expr = term.ChainLeft(addsub);

            this.Parser = this._expr.Between(Spaces()).End();
        }

        private static Parser<char, Func<TNumber, TNumber, TNumber>> Op(char symbol, Func<TNumber, TNumber, TNumber> function)
            => Char(symbol).Between(Spaces()).Map(_ => function);

        public Result<char, TNumber> Parse(string source)
            => this.Parser.Parse(source);
    }

    public class Integer : INumber<Integer>
    {
        private static readonly Parser<char, Integer> number =
            Many1(DecDigit()).ToInt().Map(x => new Integer(x));

        public static ExpressionParser<Integer> Parser { get; } =
            new ExpressionParser<Integer>(number);

        public int Value { get; }

        private Integer(int value)
        {
            this.Value = value;
        }

        public Integer Add(Integer value)
            => new Integer(this.Value + value.Value);

        public Integer Sub(Integer value)
            => new Integer(this.Value - value.Value);

        public Integer Mul(Integer value)
            => new Integer(this.Value * value.Value);

        public Integer Div(Integer value)
            => new Integer(this.Value / value.Value);
    }

    public class Double : INumber<Double>
    {
        private static readonly Parser<char, Double> number =
            Many1(DecDigit()).Append(Optional(Char('.').Append(Many1(DecDigit())), ".0"))
                .ToDouble().Map(x => new Double(x));

        public static ExpressionParser<Double> Parser { get; } =
            new ExpressionParser<Double>(number);

        public double Value { get; }

        private Double(double value)
        {
            this.Value = value;
        }

        public Double Add(Double value)
            => new Double(this.Value + value.Value);

        public Double Sub(Double value)
            => new Double(this.Value - value.Value);

        public Double Mul(Double value)
            => new Double(this.Value * value.Value);

        public Double Div(Double value)
            => new Double(this.Value / value.Value);
    }

    public class IntegerExpression : INumber<IntegerExpression>
    {
        private static readonly Parser<char, IntegerExpression> number =
            Many1(DecDigit()).ToInt().Map(x => new IntegerExpression(Expression.Constant(x)));

        public static ExpressionParser<IntegerExpression> Parser { get; } =
            new ExpressionParser<IntegerExpression>(number);

        private readonly Expression _value;

        public Expression<Func<int>> Lambda => Expression.Lambda<Func<int>>(this._value);

        private IntegerExpression(Expression value)
        {
            this._value = value;
        }

        public IntegerExpression Add(IntegerExpression value)
            => new IntegerExpression(Expression.Add(this._value, value._value));

        public IntegerExpression Sub(IntegerExpression value)
            => new IntegerExpression(Expression.Subtract(this._value, value._value));

        public IntegerExpression Mul(IntegerExpression value)
            => new IntegerExpression(Expression.Multiply(this._value, value._value));

        public IntegerExpression Div(IntegerExpression value)
            => new IntegerExpression(Expression.Divide(this._value, value._value));
    }
}