using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.Interprocess
{
    public interface IChildProcessInputRedirectable
    {
        Task RedirectInputAsync(StreamWriter writer, CancellationToken cancellationToken);
    }
}
