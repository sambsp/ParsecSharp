# ParsecSharp
Monadic parser combinator library for C#

## What's this
This library provides most useful Text Parsers, Stream Parsers, and Parser Combinators.
All APIs are pure, immutable, can combine any others.
Designed to utilize JIT Compilers optimization, realizes completely immutable, and can parse infinitely recursive data structures.

This project is affected by [parsec](https://hackage.haskell.org/package/parsec), monadic parser library for Haskell.


## Concept
* Monad on C#
* Pure/Immutable/Functional programming on C#
* Pursues possibility of .NET JIT Compiler


## How to install

### from NuGet
NuGet Package Manager Console:

```powershell
Install-Package ParsecSharp
```


## Supported platform
**Caution** This project is verrry experimental, now targets only x64, with RyuJit (Next-Gen .NET JIT Compiler).

* netstandard1.0 (compatible with net45 or later, netcoreapp, uap, xamarin, and more)


## Get started
1. Add package reference to your project.
2. Add using directive: `using static Parsec.Parser;` and `using static Parsec.Text;` to your code.
3. Parse your all.


## How to use
Here is examples:

* [JsonParser implementation](https://github.com/acple/ParsecSharp/blob/master/ParsecSharpExamples/JsonParser.cs)
* [CsvParser implementation](https://github.com/acple/ParsecSharp/blob/master/ParsecSharpExamples/CsvParser.cs)

Documents are included in [UnitTest code](https://github.com/acple/ParsecSharp/blob/master/ParsecSharpTest/ParserTest.cs) (jp)

If you want more information, read [APIs source code](https://github.com/acple/ParsecSharp/tree/master/ParsecSharp/Parser), all is there.


## License
This software is released under the MIT License, see [LICENSE](https://github.com/acple/ParsecSharp/blob/master/LICENSE).
