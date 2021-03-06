using System;

namespace ParsecSharp.Internal.Parsers
{
    internal sealed class Fail<TToken, T> : PrimitiveParser<TToken, T>
    {
        protected sealed override Result<TToken, T> Run<TState>(TState state)
            => Result.Failure<TToken, TState, T>(state);
    }

    internal sealed class FailWithMessage<TToken, T> : PrimitiveParser<TToken, T>
    {
        private readonly string _message;

        public FailWithMessage(string message)
        {
            this._message = message;
        }

        protected sealed override Result<TToken, T> Run<TState>(TState state)
            => Result.Failure<TToken, TState, T>(this._message, state);
    }

    internal sealed class FailWithMessageDelayed<TToken, T> : PrimitiveParser<TToken, T>
    {
        private readonly Func<IParsecState<TToken>, string> _message;

        public FailWithMessageDelayed(Func<IParsecState<TToken>, string> message)
        {
            this._message = message;
        }

        protected sealed override Result<TToken, T> Run<TState>(TState state)
            => Result.Failure<TToken, TState, T>(this._message(state), state);
    }
}
