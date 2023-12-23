using System;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public interface ITrashBox
    {
        Boolean DisposeFile(FilePath file);
        Task<Boolean> DisposeFileAsync(FilePath file);
    }
}
