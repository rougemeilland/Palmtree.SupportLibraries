using System;

namespace Palmtree
{
    public class SimpleProgress<VALUE_T>
        : IProgress<VALUE_T>
    {
        private readonly Action<VALUE_T> _action;

        public SimpleProgress(Action<VALUE_T> action)
        {
            _action = action;
        }

        public void Report(VALUE_T value)
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
