using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    /// <summary>
    /// A class that holds numerical progress values.
    /// </summary>
    /// <typeparam name="VALUE_T">
    /// The type of progress value.
    /// This type must implement interface <see cref="IAdditionOperators{TSelf, TOther, TResult}"/>.
    /// </typeparam>
    public class ProgressCounter<VALUE_T>
        : ProgressValueHolder<VALUE_T>
        where VALUE_T : IAdditionOperators<VALUE_T, VALUE_T, VALUE_T>
    {
        /// <inheritdoc/>
        public ProgressCounter(Action<VALUE_T> action, VALUE_T initialCounterValue)
            : base(action, initialCounterValue, TimeSpan.FromMilliseconds(100))
        {
        }

        /// <inheritdoc/>
        public ProgressCounter(IProgress<VALUE_T>? progress, VALUE_T initialCounterValue)
            : base(progress, initialCounterValue, TimeSpan.FromMilliseconds(100))
        {
        }

        /// <inheritdoc/>
        public ProgressCounter(Action<VALUE_T> action, VALUE_T initialCounterValue, TimeSpan minimumIntervalTime)
            : base(action, initialCounterValue, minimumIntervalTime)
        {
        }

        /// <inheritdoc/>
        public ProgressCounter(IProgress<VALUE_T>? progress, VALUE_T initialCounterValue, TimeSpan minimumIntervalTime)
            : base(progress, initialCounterValue, minimumIntervalTime)
        {
        }

        /// <summary>
        /// Add progress value.
        /// </summary>
        /// <param name="value">
        /// This is the value to be added to the current progress value.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddValue(VALUE_T value) => UpdateValue(v => checked(v + value));
    }
}
