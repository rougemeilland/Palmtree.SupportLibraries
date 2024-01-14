using System;

namespace Palmtree
{
    /// <summary>
    /// This is a class that holds <see cref="UInt64"/> type progress values.
    /// </summary>
    public class ProgressCounterUInt64
        : ProgressCounter<UInt64>
    {
        /// <summary>
        /// Constructor that specifies an <see cref="Action{T}"/> object that indicates how to report progress.
        /// </summary>
        /// <param name="action">
        /// A delegate that indicates how to report progress.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is null.
        /// </exception>
        public ProgressCounterUInt64(Action<UInt64> action)
            : base(action, 0, TimeSpan.Zero)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
        }

        /// <summary>
        /// A constructor that specifies the <see cref="IProgress{T}"/> object that indicates the progress report destination.
        /// </summary>
        /// <param name="progress">
        /// An <see cref="IProgress{T}"/> object that indicates where progress is reported. Null if progress is not reported.
        /// </param>
        public ProgressCounterUInt64(IProgress<UInt64>? progress)
            : base(progress, 0, TimeSpan.Zero)
        {
        }

        /// <summary>
        /// A constructor that specifies an Action object that indicates how to report progress and the minimum time between progress reports.
        /// </summary>
        /// <param name="action">
        /// A delegate that indicates how to report progress.
        /// </param>
        /// <param name="minimumIntervalTime">
        /// The minimum time between progress reports.
        /// Unless you force a progress report, no new progress reports will occur before this time has passed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="action"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="minimumIntervalTime"/> is negative or too long.
        /// </exception>
        public ProgressCounterUInt64(Action<UInt64> action, TimeSpan minimumIntervalTime)
            : base(action, 0, minimumIntervalTime)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            if (minimumIntervalTime < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(minimumIntervalTime));
        }

        /// <summary>
        /// A constructor that specifies the <see cref="IProgress{T}"/> object that indicates the progress report destination and the minimum interval time between progress reports.
        /// </summary>
        /// <param name="progress">
        /// An <see cref="IProgress{T}"/> object that indicates where progress is reported. Null if progress is not reported.
        /// </param>
        /// <param name="minimumIntervalTime">
        /// The minimum time between progress reports.
        /// Unless you force a progress report, no new progress reports will occur before this time has passed.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value of <paramref name="minimumIntervalTime"/> is negative or too long.
        /// </exception>
        public ProgressCounterUInt64(IProgress<UInt64>? progress, TimeSpan minimumIntervalTime)
            : base(progress, 0, minimumIntervalTime)
        {
            if (minimumIntervalTime < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(minimumIntervalTime));
        }

        /// <summary>
        /// Increment the progress value.
        /// </summary>
        public void Increment() => UpdateValue(value => checked(value + 1));
    }
}
