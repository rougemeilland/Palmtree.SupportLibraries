using System.Collections.Generic;

namespace Palmtree
{
    public interface IArray<ELEMENT_T>
        : IEnumerable<ELEMENT_T>, IIndexer<int, ELEMENT_T>
    {
        int Length { get; }
    }
}
