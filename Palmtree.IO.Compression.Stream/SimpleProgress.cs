using System;

namespace Palmtree.IO.Compression.Stream
{
    internal class SimpleProgress<VALUE_T>
        : IProgress<VALUE_T>
    {
        private readonly Action<VALUE_T> _action;

        public SimpleProgress(Action<VALUE_T> action)
        {
            _action = action;
        }

        void IProgress<VALUE_T>.Report(VALUE_T value)
        {
            try
            {
                _action(value);
            }
            catch (Exception)
            {
            }
        }
    }
}
