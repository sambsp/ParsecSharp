﻿using System;
using System.Collections.Generic;

namespace Parsec
{
    public interface IParsecStateStream<out T> : IParsecState<T>, IEnumerable<T>, IDisposable
    {
        IParsecStateStream<T> Next { get; }
    }
}