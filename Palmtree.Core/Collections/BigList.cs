using System;
using System.Collections;
using System.Collections.Generic;

namespace Palmtree.Collections
{
    public class BigList<ELEMENT_T>
        : IReadOnlyIndexer<UInt32, ELEMENT_T>, IEnumerable<ELEMENT_T>
    {
        private readonly BigArray<ELEMENT_T> _array;

        public BigList()
        {
            _array = new BigArray<ELEMENT_T>();
            Count = 0;
        }

        public ELEMENT_T this[UInt32 index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();

                Shrink();
                return _array[index];
            }
        }

        public UInt32 Count { get; private set; }

        public void Add(ELEMENT_T value)
        {
            if (Count >= _array.Length)
            {
                if (_array.Length >= UInt32.MaxValue)
                    throw new OutOfMemoryException();

                var newSize = (UInt32)(_array.Length * 2UL).Minimum(UInt32.MaxValue);
                _array.Resize(newSize);
            }

            _array[Count] = value;
            ++Count;
        }

        public IEnumerator<ELEMENT_T> GetEnumerator()
        {
            Shrink();
            for (var index = 0U; index < Count; ++index)
                yield return _array[index];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Shrink()
        {
            if (_array.Length > Count)
                _array.Resize(Count);
        }
    }
}
