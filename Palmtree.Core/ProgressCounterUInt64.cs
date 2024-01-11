using System;

namespace Palmtree
{
    public class ProgressCounterUInt64
        : ProgressCounter<UInt64>
    {
        private static readonly TimeSpan _DEFAULT_MINIMUM_STEP_TIME = TimeSpan.FromMilliseconds(100);

        public ProgressCounterUInt64(IProgress<UInt64>? progress)
            : this(progress, _DEFAULT_MINIMUM_STEP_TIME)
        {
        }

        public ProgressCounterUInt64(IProgress<UInt64>? progress, TimeSpan minimumStepTime)
            : base(progress, 0, minimumStepTime)
        {
            if (minimumStepTime < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(minimumStepTime));
        }

        public void Increment() => AddValue(1);
    }
}
