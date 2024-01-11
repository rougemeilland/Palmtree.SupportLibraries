using System;
using System.Numerics;

namespace Palmtree
{
    public class ProgressCounter<VALUE_T>
        where VALUE_T : struct, IComparable<VALUE_T>, IAdditionOperators<VALUE_T, VALUE_T, VALUE_T>
    {
        private readonly IProgress<VALUE_T>? _progress;
        private readonly VALUE_T _initialCounterValue;
        private readonly TimeSpan _minimumStepTime;

        private DateTime _nextTimeToReport;

        public ProgressCounter(IProgress<VALUE_T>? progress, VALUE_T initialCounterValue, TimeSpan minimumStepTime)
        {
            if (minimumStepTime < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(minimumStepTime));

            _progress = progress;
            _initialCounterValue = initialCounterValue;
            _minimumStepTime = minimumStepTime;
            Value = initialCounterValue;
            _nextTimeToReport = DateTime.UtcNow;
        }

        public VALUE_T Value { get; private set; }

        public void AddValue(VALUE_T value)
        {
            var needToReport = false;
            var now = DateTime.UtcNow;

            lock (this)
            {
                checked
                {
                    Value += value;
                }

                if (now >= _nextTimeToReport)
                {
                    needToReport = true;
                    _nextTimeToReport = now + _minimumStepTime;
                }
            }

            if (needToReport)
                Report();
        }

        public void ReportIfInitial()
        {
            if (Value.CompareTo(_initialCounterValue) <= 0)
                Report();
        }

        public void Report()
        {
            if (_progress is not null)
            {
                try
                {
                    _progress.Report(Value);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
