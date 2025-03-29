using System.Collections.Generic;

namespace Palmtree
{
    public interface IReadOnlyArray<ELEMENT_T>
        : IEnumerable<ELEMENT_T>, IReadOnlyIndexer<int, ELEMENT_T>
    {
        int Length { get; }
    }
}
