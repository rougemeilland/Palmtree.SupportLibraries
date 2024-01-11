using System;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal class ProgressCounterUint64Uint64
    {
        private static readonly TimeSpan _DEFAULT_MINIMUM_STEP_TIME = TimeSpan.FromMilliseconds(100);

        private readonly IProgress<(UInt64 value1, UInt64 value2)>? _progress;
        private readonly TimeSpan _minimumStepTime;

        private DateTime _nextTimeToReport;

        public ProgressCounterUint64Uint64(IProgress<(UInt64 value1, UInt64 value2)>? progress)
            : this(progress, _DEFAULT_MINIMUM_STEP_TIME)
        {
        }

        public ProgressCounterUint64Uint64(IProgress<(UInt64 value1, UInt64 value2)>? progress, TimeSpan minimumStepTime)
        {
            if (minimumStepTime < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(minimumStepTime));

            _progress = progress;
            _minimumStepTime = minimumStepTime;
            _nextTimeToReport = DateTime.UtcNow;
            Value1 = 0;
            Value2 = 0;
        }

        public UInt64 Value1 { get; private set; }
        public UInt64 Value2 { get; private set; }

        public void AddValue1(UInt64 value)
        {
            var needToReport = false;
            lock (this)
            {
                checked
                {
                    Value1 += value;
                }

                needToReport = CheckIfNeedToReport();
            }

            if (needToReport)
                Report();
        }

        public void AddValue2(UInt64 value)
        {
            var needToReport = false;

            lock (this)
            {
                checked
                {
                    Value2 += value;
                }

                needToReport = CheckIfNeedToReport();
            }

            if (needToReport)
                Report();
        }

        public void SetValue1(UInt64 value)
        {
            var needToReport = false;
            lock (this)
            {
                if (value < Value1)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"A value smaller than the previous value was specified.: currentValue1=0x{Value1:x16}, newValue1=0x{value:x16}");
#endif
                }
                else
                {
                    Value1 = value;
                    needToReport = CheckIfNeedToReport();
                }
            }

            if (needToReport)
                Report();
        }

        public void SetValue2(UInt64 value)
        {
            var needToReport = false;
            lock (this)
            {
                if (value < Value2)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"A value smaller than the previous value was specified.: currentValue2=0x{Value2:x16}, newValue2=0x{value:x16}");
#endif
                }
                else
                {
                    Value2 = value;
                    needToReport = CheckIfNeedToReport();
                }
            }

            if (needToReport)
                Report();
        }

        public void Report()
        {
            InternalReport();

            lock (this)
            {
                _nextTimeToReport = DateTime.UtcNow + _minimumStepTime;
            }
        }

        private void InternalReport()
        {
            if (_progress is not null)
            {
                try
                {
                    _progress.Report((Value1, Value2));
                }
                catch (Exception)
                {
                }
            }
        }

        private Boolean CheckIfNeedToReport()
        {
            var now = DateTime.UtcNow;
            if (now < _nextTimeToReport)
                return false;

            _nextTimeToReport = now + _minimumStepTime;
            return true;
        }
    }
}
