namespace Parsec.Internal
{
    public readonly struct LinearPosition : IPosition
    {
        public static LinearPosition Initial => default;

        public int Line => 0;

        public int Column { get; }

        public LinearPosition(int index)
        {
            this.Column = index;
        }

        public LinearPosition Next()
            => new LinearPosition(this.Column + 1);

        public override string ToString()
            => $"Index: {this.Column.ToString()}";
    }
}
