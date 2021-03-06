using System;

namespace ParsecSharp.Internal.Parsers
{
    internal sealed class Next<TToken, TIntermediate, T> : Parser<TToken, T>
    {
        private readonly Parser<TToken, TIntermediate> _parser;

        private readonly Func<TIntermediate, Parser<TToken, T>> _success;

        private readonly Func<Failure<TToken, TIntermediate>, Parser<TToken, T>> _failure;

        public Next(Parser<TToken, TIntermediate> parser, Func<TIntermediate, Parser<TToken, T>> success, Func<Failure<TToken, TIntermediate>, Parser<TToken, T>> failure)
        {
            this._parser = parser;
            this._success = success;
            this._failure = failure;
        }

        internal sealed override Result<TToken, TResult> Run<TState, TResult>(TState state, Func<Result<TToken, T>, Result<TToken, TResult>> cont)
        {
            var success = this._success;
            var failure = this._failure;
            return this._parser.Run(state, result => result.CaseOf(
                result => failure(result).Run(state, cont),
                result => result.Next(success, cont)));
        }
    }
}
