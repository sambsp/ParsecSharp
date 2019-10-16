namespace ParsecSharp.Internal
{
    public class EmptyPosition : IPosition
    {
        public static IPosition Initial { get; } = new EmptyPosition();

        public int Line => 0;

        public int Column => -1;

        private EmptyPosition()
        { }

        public int CompareTo(IPosition other)
            => (this.Equals(other)) ? 0 : -1;

        public bool Equals(IPosition other)
            => ReferenceEquals(this, other);

        public override bool Equals(object? obj)
            => ReferenceEquals(this, obj);

        public override int GetHashCode()
            => 0;

        public override string ToString()
            => "Position: none";
    }
}