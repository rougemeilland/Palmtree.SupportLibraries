using System;
using System.Linq;
using System.Threading.Tasks;
using Palmtree.Collections;
using Palmtree.IO;
using Palmtree.IO.StreamFilters;

namespace Palmtree.Debug.IO
{
    public class InputTestDataStream
        : SequentialInputByteStreamFilter
    {
        private const Int32 _MAX_BUFFER_SIZE = 1024 * 1024;

        private InputTestDataStream(ISequentialInputByteStream baseStream)
            : base(baseStream, false)
        {
        }

        public static InputTestDataStream Create(UInt64 length, Func<Byte, Byte>? byteDataFilter = null)
        {
            if (length < (sizeof(UInt64) + sizeof(UInt32)))
                throw new ArgumentOutOfRangeException(nameof(length));

            var pipe = new InProcessPipe();
            _ = Task.Run(() =>
            {
                using var outStream = pipe.OpenOutputStream();
                var contentLength = checked(length - (sizeof(UInt64) + sizeof(UInt32)));
                outStream.WriteUInt64LE(contentLength);

                var crcValueHolder = new ValueHolder<(UInt32 crc, UInt64 length)>();
                using (var contentStream = outStream.WithCrc32Calculation(crcValueHolder, true))
                {
                    var remain = contentLength;
                    while (remain > 0)
                    {
                        var lengthToWrite = checked((Int32)remain.Minimum((UInt64)_MAX_BUFFER_SIZE));
                        var testDataSequence = RandomSequence.GetByteSequence().Take(lengthToWrite);
                        if (byteDataFilter is not null)
                            testDataSequence = testDataSequence.Select(b => byteDataFilter(b));
                        foreach (var btteData in testDataSequence)
                        {
                            contentStream.WriteByte(btteData);
                        }

                        checked
                        {
                            remain -= (UInt64)lengthToWrite;
                        }
                    }
                }

                outStream.WriteUInt32LE(crcValueHolder.Value.crc);
            });
            return new InputTestDataStream(pipe.OpenInputStream());
        }
    }
}
