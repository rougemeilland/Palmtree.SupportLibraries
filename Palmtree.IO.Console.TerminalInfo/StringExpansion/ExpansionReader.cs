using System;

namespace Palmtree.IO.Console.StringExpansion
{
    internal class ExpansionReader
        : IPrefetchableTextReader
    {
        private readonly String _source;
        private Int32 _currentIndex;

        public ExpansionReader(String source)
        {
            _source = source;
            _currentIndex = 0;
        }

        public Char? Read()
            => _currentIndex < _source.Length
                ? _source[_currentIndex++]
                : null;

        public Boolean StartsWith(Char c)
            => _currentIndex < _source.Length && _source[_currentIndex] == c;

        public Boolean StartsWith(String s)
        {
            if (_currentIndex + s.Length > _source.Length)
                return false;

            for (var count = 0; count < s.Length; ++count)
            {
                if (_source[_currentIndex + count] != s[count])
                    return false;
            }

            return true;
        }
    }
}
