﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Parsec.Internal;

namespace Parsec
{
    public sealed class TextStream : IParsecStateStream<char>
    {
        private readonly IDisposable disposable;

        private readonly TextPosition _position;

        private readonly Lazy<IParsecStateStream<char>> _next;

        public char Current { get; }

        public bool HasValue { get; }

        public IPosition Position => this._position;

        public IParsecStateStream<char> Next => this._next.Value;

        public TextStream(Stream source) : this(source, Encoding.UTF8)
        { }

        public TextStream(Stream source, Encoding encoding) : this(new StreamReader(source, encoding))
        { }

        public TextStream(TextReader reader) : this(reader, TextPosition.Initial)
        { }

        private TextStream(TextReader reader, TextPosition position)
        {
            this.disposable = reader;
            this._position = position;
            try
            {
                var token = reader.Read();
                this.HasValue = token != -1;
                this.Current = (this.HasValue) ? (char)token : default(char);
                this._next = new Lazy<IParsecStateStream<char>>(() => new TextStream(reader, position.Next(this.Current)), false);
            }
            catch
            {
                this.HasValue = false;
                this.Current = default(char);
                this._next = new Lazy<IParsecStateStream<char>>(() => EmptyStream<char>.Instance, false);
                this.Dispose();
                throw;
            }
        }

        public void Dispose()
            => this.disposable.Dispose();

        public override string ToString()
            => (this.HasValue) ? this.Current.ToString() : string.Empty;

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
            => new ParsecStateStreamEnumerator<char>(this);

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => new ParsecStateStreamEnumerator<char>(this);
    }
}
