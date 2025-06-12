using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Palmtree.IO.Console
{
    internal sealed class ReadOnlyArray<ELEMENT_T>
        : IReadOnlyArray<ELEMENT_T>
    {
        private readonly ELEMENT_T[] _array;

        public ReadOnlyArray(ELEMENT_T[] array)
        {
            _array = array;
        }

        ELEMENT_T IReadOnlyIndexer<Int32, ELEMENT_T>.this[Int32 index] => _array[index];

        Int32 IReadOnlyArray<ELEMENT_T>.Length => _array.Length;

        IEnumerator<ELEMENT_T> IEnumerable<ELEMENT_T>.GetEnumerator() => _array.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _array.GetEnumerator();
    }
}
