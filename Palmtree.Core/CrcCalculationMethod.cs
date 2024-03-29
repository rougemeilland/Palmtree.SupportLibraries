﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree
{
    internal abstract class CrcCalculationMethod<CRC_VALUE_T>
           where CRC_VALUE_T : struct
    {
        #region private class

        private class CrcCalculationSession
            : ICrcCalculationState<CRC_VALUE_T>
        {
            private readonly CrcCalculationMethod<CRC_VALUE_T> _calculator;
            private CRC_VALUE_T _state;
            private UInt64 _length;

            public CrcCalculationSession(CrcCalculationMethod<CRC_VALUE_T> calculator)
            {
                _calculator = calculator;
                _state = calculator.InitialValue;
                _length = 0;
            }

            public void Put(Byte data)
            {
                _state = _calculator.Update(_state, data);
                checked
                {
                    ++_length;
                }
            }

            public void Put(Byte[] data, Int32 offset, Int32 count)
            {
                if (data is null)
                    throw new ArgumentNullException(nameof(data));
                if (offset < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset));
                if (count < 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (checked(offset + count) > data.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(data)}.");

                for (var index = 0; index < count; ++index)
                    _state = _calculator.Update(_state, data[offset + index]);
                checked
                {
                    _length += (UInt64)count;
                }
            }

            public void Put(ReadOnlySpan<Byte> data)
            {
                for (var index = 0; index < data.Length; ++index)
                    _state = _calculator.Update(_state, data[index]);
                checked
                {
                    _length += (UInt64)data.Length;
                }
            }

            public void Put(IEnumerable<Byte> data)
            {
                foreach (var byteData in data)
                {
                    _state = _calculator.Update(_state, byteData);
                    checked
                    {
                        ++_length;
                    }
                }
            }

            public void Reset()
            {
                _state = _calculator.InitialValue;
                _length = 0;
            }

            public (CRC_VALUE_T, UInt64) GetResultValue()
                => (_calculator.Finalize(_state), _length);
        }

        #endregion

        public ICrcCalculationState<CRC_VALUE_T> CreateSession() => new CrcCalculationSession(this);

        public (CRC_VALUE_T Crc, UInt64 Length) Calculate(IEnumerable<Byte> byteSequence, IProgress<UInt64>? progress = null)
        {
            if (byteSequence is null)
                throw new ArgumentNullException(nameof(byteSequence));

            var progressCounter = new ProgressCounterUInt64(progress);
            progressCounter.Report();
            var crc = InitialValue;
            foreach (var data in byteSequence)
            {
                crc = Update(crc, data);
                progressCounter.Increment();
            }

            progressCounter.Report();
            return (Finalize(crc), progressCounter.Value);
        }

        public async Task<(CRC_VALUE_T Crc, UInt64 Length)> CalculateAsync(IAsyncEnumerable<Byte> byteSequence, IProgress<UInt64>? progress = null, CancellationToken cancellationToken = default)
        {
            if (byteSequence is null)
                throw new ArgumentNullException(nameof(byteSequence));

            var progressCounter = new ProgressCounterUInt64(progress);
            progressCounter.Report();
            var crc = InitialValue;
            var enumerator = byteSequence.GetAsyncEnumerator(cancellationToken);
            await using (enumerator.ConfigureAwait(false))
            {
                while (await enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    crc = Update(crc, enumerator.Current);
                    progressCounter.Increment();
                }
            }

            progressCounter.Report();
            return (Finalize(crc), progressCounter.Value);
        }

        public IEnumerable<Byte> GetSequenceWithCrc(IEnumerable<Byte> source, ValueHolder<(CRC_VALUE_T Crc, UInt64 Length)> result, IProgress<UInt64>? progress = null)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            var progressCounter = new ProgressCounterUInt64(progress);
            progressCounter.Report();
            var session = CreateSession();
            foreach (var data in source)
            {
                session.Put(data);
                progressCounter.Increment();
                yield return data;
            }

            result.Value = session.GetResultValue();
            progressCounter.Report();
        }

        public async IAsyncEnumerable<Byte> GetAsyncSequenceWithCrc(IAsyncEnumerable<Byte> source, ValueHolder<(CRC_VALUE_T Crc, UInt64 Length)> result, IProgress<UInt64>? progress = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            var progressCounter = new ProgressCounterUInt64(progress);
            progressCounter.Report();
            var session = CreateSession();
            var enumerator = source.GetAsyncEnumerator(cancellationToken);
            await using (enumerator.ConfigureAwait(false))
            {
                while (!await enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var data = enumerator.Current;
                    session.Put(data);
                    progressCounter.Increment();
                    yield return data;
                }

                result.Value = session.GetResultValue();
                progressCounter.Report();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(Byte[] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            return Calculate(array, 0, array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(Byte[] array, Int32 offset)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (!offset.IsBetween(0, array.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return Calculate(array, offset, array.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(Byte[] array, UInt32 offset)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (offset > array.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return Calculate(array, (Int32)offset, array.Length - (Int32)offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(Byte[] array, Range range)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            var (offset, count) = array.GetOffsetAndLength(range, nameof(range));
            return Calculate(array, offset, count);
        }

        public CRC_VALUE_T Calculate(Byte[] array, Int32 offset, Int32 count)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > array.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(array)}.");

            var crc = InitialValue;
            for (var index = 0; index < count; ++index)
                crc = Update(crc, array[offset + index]);
            return Finalize(crc);
        }

        public CRC_VALUE_T Calculate(Byte[] array, UInt32 offset, UInt32 count)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (checked(offset + count) > array.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(array)}.");

            var crc = InitialValue;
            for (var index = 0U; index < count; ++index)
                crc = Update(crc, array[offset + index]);
            return Finalize(crc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(ReadOnlyMemory<Byte> array)
            => Calculate(array.Span);

        public CRC_VALUE_T Calculate(ReadOnlySpan<Byte> array)
        {
            var crc = InitialValue;
            var count = array.Length;
            for (var index = 0; index < count; ++index)
                crc = Update(crc, array[index]);
            return Finalize(crc);
        }

        protected abstract CRC_VALUE_T InitialValue { get; }
        protected abstract CRC_VALUE_T Update(CRC_VALUE_T crc, Byte data);
        protected abstract CRC_VALUE_T Finalize(CRC_VALUE_T crc);
    }
}
