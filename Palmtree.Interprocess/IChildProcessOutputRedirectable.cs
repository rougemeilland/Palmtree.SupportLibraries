using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.Interprocess
{
    public interface IChildProcessOutputRedirectable
    {
        Task RedirectOutputAsync(StreamReader reader, CancellationToken cancellationToken);
    }
}
