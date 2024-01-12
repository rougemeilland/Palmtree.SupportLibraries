using System;
using System.Numerics;

namespace Palmtree
{
    public class ProgressCounter<VALUE_T>
        where VALUE_T : struct, IComparable<VALUE_T>, IAdditionOperators<VALUE_T, VALUE_T, VALUE_T>
    {
        private readonly IProgress<VALUE_T>? _progress;
        private readonly VALUE_T _initialCounterValue;
        private readonly Int64 _minimumStepTimeMilliSeconds;

        private Int64 _previousReportedTime;

        public ProgressCounter(IProgress<VALUE_T>? progress, VALUE_T initialCounterValue, TimeSpan minimumStepTime)
        {
            if (minimumStepTime < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(minimumStepTime));

            _progress = progress;
            _initialCounterValue = initialCounterValue;
            _minimumStepTimeMilliSeconds = checked((Int64)minimumStepTime.TotalMilliseconds);
            Value = initialCounterValue;
            _previousReportedTime = Environment.TickCount64;
        }

        public VALUE_T Value { get; private set; }

        public void AddValue(VALUE_T value)
        {
            var needToReport = false;
            var now = Environment.TickCount64;

            lock (this)
            {
                checked
                {
                    Value += value;
                }

                if (unchecked(now - _previousReportedTime) >= _minimumStepTimeMilliSeconds)
                {
                    needToReport = true;
                    _previousReportedTime = now;
                }
            }

            if (needToReport)
                InternalReport();
        }

        public void ReportIfInitial()
        {
            if (Value.CompareTo(_initialCounterValue) <= 0)
                Report();
        }

        public void Report()
        {
            InternalReport();

            lock (this)
            {
                _previousReportedTime = Environment.TickCount64;
            }
        }

        private void InternalReport()
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
