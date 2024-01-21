using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    /// <summary>
    /// A class that holds progress values.
    /// </summary>
    /// <typeparam name="VALUE_T">
    /// The type of progress value.
    /// </typeparam>
    public class ProgressValueHolder<VALUE_T>
    {

        private static readonly TimeSpan _minimumValueOfInterval = TimeSpan.Zero;
        private static readonly TimeSpan _maximumValueOfInterval = TimeSpan.FromMilliseconds(Int32.MaxValue);

        private readonly Action<VALUE_T>? _action;
        private readonly Int32 _minimumStepTimeMilliSeconds;

        private VALUE_T _value;
        private Int32 _previousReportedTime;
        private Boolean _isReported;

        /// <summary>
        /// This is an <see cref="Action{T}"/> object that indicates how progress is to be reported, and a constructor that specifies the initial value of the progress value.
        /// </summary>
        /// <param name="progress">
        /// An <see cref="IProgress{T}"/> object that indicates where progress is reported.
        /// If it is null, progress will not be reported.
        /// </param>
        /// <param name="initialCounterValue">
        /// This is the initial value of the progress value.
        /// </param>
        /// <remarks>
        /// The minimum interval between progress reports is 100ms.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The value of <paramref name="action"/> is null.
        /// </exception>
        public ProgressValueHolder(Action<VALUE_T> action, VALUE_T initialCounterValue)
            : this(action, initialCounterValue, TimeSpan.FromMilliseconds(100))
        {
        }

        /// <summary>
        /// This is a constructor that specifies the <see cref="IProgress{T}"/> object to which progress is reported and the initial value of the progress value.
        /// </summary>
        /// <param name="progress">
        /// An <see cref="IProgress{T}"/> object that indicates where progress is reported.
        /// If it is null, progress will not be reported.
        /// </param>
        /// <param name="initialCounterValue">
        /// This is the initial value of the progress value.
        /// </param>
        /// <remarks>
        /// The minimum interval between progress reports is 100ms.
        /// </remarks>
        public ProgressValueHolder(IProgress<VALUE_T>? progress, VALUE_T initialCounterValue)
            : this(progress, initialCounterValue, TimeSpan.FromMilliseconds(100))
        {
        }

        /// <summary>
        /// This is a constructor that specifies an <see cref="Action{T}"/> object that indicates how progress is to be reported, an initial value for the progress value, and the minimum time between progress reports.
        /// </summary>
        /// <param name="action">
        /// An <see cref="Action{T}"/> delegate that indicates how to report progress.
        /// </param>
        /// <param name="initialCounterValue">
        /// This is the initial value of the progress value.
        /// </param>
        /// <param name="minimumIntervalTime">
        /// The minimum time between progress reports.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The value of <paramref name="action"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="minimumIntervalTime"/> is a negative value.
        /// </exception>
        public ProgressValueHolder(Action<VALUE_T> action, VALUE_T initialCounterValue, TimeSpan minimumIntervalTime)
            : this(new SimpleProgress<VALUE_T>(action), initialCounterValue, minimumIntervalTime)
        {
            if (!minimumIntervalTime.IsBetween(_minimumValueOfInterval, _maximumValueOfInterval))
                throw new ArgumentOutOfRangeException(nameof(minimumIntervalTime));

            _action = action ?? throw new ArgumentNullException(nameof(action));
            _minimumStepTimeMilliSeconds = checked((Int32)minimumIntervalTime.TotalMilliseconds);
            _value = initialCounterValue;
            _previousReportedTime = Environment.TickCount;
            _isReported = false;
        }

        /// <summary>
        /// This is a constructor that specifies the progress report destination <see cref="IProgress{T}"/> object, the initial value of the progress value, and the minimum interval time for progress reports.
        /// </summary>
        /// <param name="progress">
        /// An <see cref="IProgress{T}"/> object that indicates where progress is reported.
        /// If it is null, progress will not be reported.
        /// </param>
        /// <param name="initialCounterValue">
        /// This is the initial value of the progress value.
        /// </param>
        /// <param name="minimumIntervalTime">
        /// The minimum time between progress reports.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="minimumIntervalTime"/> is a negative value.
        /// </exception>
        public ProgressValueHolder(IProgress<VALUE_T>? progress, VALUE_T initialCounterValue, TimeSpan minimumIntervalTime)
        {
            if (!minimumIntervalTime.IsBetween(_minimumValueOfInterval, _maximumValueOfInterval))
                throw new ArgumentOutOfRangeException(nameof(minimumIntervalTime));

            _action = value => progress?.Report(value);
            _minimumStepTimeMilliSeconds = checked((Int32)minimumIntervalTime.TotalMilliseconds);
            _value = initialCounterValue;
            _previousReportedTime = Environment.TickCount;
            _isReported = false;
        }

        /// <summary>
        /// Gets or sets the progress value.
        /// </summary>
        /// <value>
        /// A <typeparamref name="VALUE_T"/> object that indicates a progress value.
        /// </value>
        /// <remarks>
        /// Progress reporting may occur when setting progress values.
        /// </remarks>
        public VALUE_T Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                lock (this)
                {
                    return _value;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => UpdateValue(_ => value);
        }

        /// <summary>
        /// Set the progress value.
        /// </summary>
        /// <param name="value">
        /// A <typeparamref name="VALUE_T"/> object that is the progress value to set.
        /// </param>
        /// <remarks>
        /// This method behaves the same as setting the <see cref="Value"/> property.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(VALUE_T value) => UpdateValue(_ => value);

        /// <summary>
        /// Forces a progress report only if no progress report has been made yet.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReportIfInitial()
        {
            if (CheckIfNeedToReport(Environment.TickCount))
                InternalReport();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            Boolean CheckIfNeedToReport(Int32 now)
            {
                lock (this)
                {
                    if (_isReported)
                        return false;
                    _previousReportedTime = now;
                    _isReported = true;
                    return true;
                }
            }
        }

        /// <summary>
        /// Force progress reports.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Report()
        {
            InternalReport();
            lock (this)
            {
                _previousReportedTime = Environment.TickCount;
                _isReported = true;
            }
        }

        /// <summary>
        /// Update the progress value.
        /// </summary>
        /// <param name="valueUpdater">
        /// A delegate that updates progress values.
        /// The delegate's argument is a <typeparamref name="VALUE_T"/> object representing the current progress value, and the return value is a <typeparamref name="VALUE_T"/> object representing the updated progress value.
        /// </param>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Use this method when progress values should be updated thread-safely.</item>
        /// <item>Calling this method may result in progress reporting.</item>
        /// </list>
        /// </remarks>
        protected void UpdateValue(Func<VALUE_T, VALUE_T> valueUpdater)
        {
            if (CheckIfNeedToReport(Environment.TickCount, valueUpdater))
                InternalReport();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            Boolean CheckIfNeedToReport(Int32 now, Func<VALUE_T, VALUE_T> valueUpdater)
            {
                lock (this)
                {
                    _value = valueUpdater(_value);
                    if (unchecked(now - _previousReportedTime) < _minimumStepTimeMilliSeconds)
                        return false;
                    _previousReportedTime = now;
                    return true;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InternalReport()
        {
            if (_action is not null)
            {
                try
                {
                    _action(_value);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
