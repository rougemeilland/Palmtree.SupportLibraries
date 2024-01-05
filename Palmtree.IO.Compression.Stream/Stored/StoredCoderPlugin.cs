using System;

namespace Palmtree.IO.Compression.Stream.Stored
{
    public abstract class StoredCoderPlugin
        : ICompressionCoder, ICompressionCoderPlugin
    {
        private class DummyOption
            : ICoderOption
        {
        }

        internal StoredCoderPlugin()
        {
        }

        public CompressionMethodId CompressionMethodId => CompressionMethodId.Stored;

        public ICoderOption DefaultOption => new DummyOption();

        public ICoderOption GetOptionFromGeneralPurposeFlag(Boolean bit1, Boolean bit2) => new DummyOption();

        public static void EnablePlugin()
        {
            CompressionCoderPlugin.Register(new StoredDecoderPlugin());
            CompressionCoderPlugin.Register(new StoredEncoderPlugin());
        }
    }
}
