using System;

namespace Palmtree.IO.Compression.Stream
{
    public class LzmaCompressionOption
        : ICoderOption
    {
        public Boolean UseEndOfStreamMarker { get; set; }
    }
}
