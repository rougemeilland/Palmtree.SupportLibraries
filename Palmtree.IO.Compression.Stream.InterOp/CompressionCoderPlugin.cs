using System;
using System.Collections.Generic;

namespace Palmtree.IO.Compression.Stream
{
    public static class CompressionCoderPlugin
    {
        private enum CoderType
        {
            Unknown = 0,
            Decoder,
            Encoder
        }

        private static readonly IDictionary<(CompressionMethodId, CoderType), ICompressionCoder> _indexedCoders;
        private static readonly ICollection<ICompressionCoder> _coders;

        static CompressionCoderPlugin()
        {
            _indexedCoders = new Dictionary<(CompressionMethodId, CoderType), ICompressionCoder>();
            _coders = new List<ICompressionCoder>();
        }

        public static void Register(ICompressionCoder coder)
        {
            var added = false;
            if (coder is ICompressionDecoder or ICompressionHierarchicalDecoder)
            {
                var key = (coder.CompressionMethodId, CoderType.Decoder);
                if (_indexedCoders.TryAdd(key, coder))
                {
                    added = true;
                }
                else
                {
                    var currentPlugin = _indexedCoders[key];
                    if (currentPlugin.GetType() != coder.GetType())
                        throw new InvalidOperationException($"Duplicate plugins have been registered.: plugin1={currentPlugin.GetType().FullName}, plugin2={coder.GetType().FullName}");
                }
            }

            if (coder is ICompressionEncoder or ICompressionHierarchicalEncoder)
            {
                var key = (coder.CompressionMethodId, CoderType.Encoder);
                if (_indexedCoders.TryAdd(key, coder))
                {
                    added = true;
                }
                else
                {
                    var currentPlugin = _indexedCoders[key];
                    if (currentPlugin.GetType() != coder.GetType())
                        throw new InvalidOperationException($"Duplicate plugins have been registered.: plugin1={currentPlugin.GetType().FullName}, plugin2={coder.GetType().FullName}");
                }
            }

            if (added)
                _coders.Add(coder);
        }

        public static IEnumerable<ICompressionCoder> EnumeratePlugins() => _coders;
    }
}
