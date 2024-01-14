using System;
using System.Threading;
using System.Threading.Tasks;
using Palmtree.IO;
using Palmtree.IO.StreamFilters;

namespace Palmtree.Debug.IO
{
    public class OutputTestDataStream
        : SequentialOutputByteStreamFilter
    {
        private Exception? _exception;

        private OutputTestDataStream(ISequentialOutputByteStream baseStream)
            : base(baseStream, false)
        {
        }

        public static OutputTestDataStream Create(Action<Exception> exceptionHandler)
        {
            var endOfReading = new ManualResetEventSlim();
            var pipe = new InProcessPipe();
            var instance =
                new OutputTestDataStream(
                    pipe.OpenOutputStream()
                    .WithEndAction(
                        _ =>
                        {
                            endOfReading.Wait();
                            endOfReading.Dispose();
                        }));
            _ = Task.Run(() =>
            {
                try
                {
                    using var inStream = pipe.OpenInputStream();
                    var contentLength = inStream.ReadUInt64LE();
                    var (actualCrc, length) = inStream.WithPartial(contentLength, true).CalculateCrc32();
                    var crc = inStream.ReadUInt32LE();
                    if (crc != actualCrc)
                        throw new Exception("The output test data is incorrect.");
                    if (inStream.ReadBytes(1).Length > 0)
                        throw new Exception("The output test data is incorrect.");
                }
                catch (Exception ex)
                {
                    lock (instance)
                    {
                        instance._exception = ex;
                    }

                    try
                    {
                        exceptionHandler(ex);
                    }
                    catch (Exception)
                    {
                    }
                }
                finally
                {
                    endOfReading.Set();
                }
            });
            return instance;
        }

        protected override Int32 WriteCore(ReadOnlySpan<Byte> buffer)
        {
            ThrowIfError();
            return base.WriteCore(buffer);
        }

        protected override Task<Int32> WriteAsyncCore(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
        {
            ThrowIfError();
            return base.WriteAsyncCore(buffer, cancellationToken);
        }

        protected override void FlushCore()
        {
            ThrowIfError();
            base.FlushCore();
        }

        protected override Task FlushAsyncCore(CancellationToken cancellationToken = default)
        {
            ThrowIfError();
            return base.FlushAsyncCore(cancellationToken);
        }

        private void ThrowIfError()
        {
            lock (this)
            {
                if (_exception is not null)
                {
                    var ex = _exception;
                    _exception = null;
                    throw ex;
                }
            }
        }
    }
}
