using System;

using System.Diagnostics.CodeAnalysis;

namespace Palmtree.IO.Console.StringExpansion
{
    internal interface IArgumentIndexer<INDEX_T, VALUE_T>
        : IIndexer<INDEX_T, VALUE_T>
    {
        Boolean TryGet(INDEX_T index, [MaybeNullWhen(false)] out VALUE_T value);
    }
}
