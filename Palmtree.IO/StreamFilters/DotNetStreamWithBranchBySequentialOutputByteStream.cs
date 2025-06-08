using System;
using System.IO;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamWithBranchBySequentialOutputByteStream
        : DotNetStreamWithBranch
    {
        private readonly ISequentialOutputByteStream _baseStream1;
        private readonly ISequentialOutputByteStream _baseStream2;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public DotNetStreamWithBranchBySequentialOutputByteStream(ISequentialOutputByteStream baseStream1, Stream rawStream1, ISequentialOutputByteStream baseStream2, Stream rawStream2, Boolean leaveOpen)
            : base(rawStream1, rawStream2, leaveOpen)
        {
            Validation.Assert(rawStream1.CanWrite);
            Validation.Assert(rawStream2.CanWrite);
            _baseStream1 = baseStream1;
            _baseStream2 = baseStream2;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                    {
                        _baseStream1.Dispose();
                        _baseStream2.Dispose();
                    }
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
