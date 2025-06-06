using System;
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

        private sealed class CrcCalculationSession
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
                ArgumentNullException.ThrowIfNull(data);
                ArgumentOutOfRangeException.ThrowIfNegative(offset);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
                ArgumentOutOfRangeException.ThrowIfNegative(count);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, data.Length - offset);

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
            ArgumentNullException.ThrowIfNull(byteSequence);

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
            ArgumentNullException.ThrowIfNull(byteSequence);

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
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(result);

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
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(result);

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
            ArgumentNullException.ThrowIfNull(array);

            return Calculate(array, 0, array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(Byte[] array, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);

            return Calculate(array, offset, array.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(Byte[] array, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)array.Length);

            return Calculate(array, (Int32)offset, array.Length - (Int32)offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRC_VALUE_T Calculate(Byte[] array, Range range)
        {
            ArgumentNullException.ThrowIfNull(array);

            var (offset, count) = array.GetOffsetAndLength(range);
            return Calculate(array, offset, count);
        }

        public CRC_VALUE_T Calculate(Byte[] array, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

            var crc = InitialValue;
            for (var index = 0; index < count; ++index)
                crc = Update(crc, array[offset + index]);
            return Finalize(crc);
        }

        public CRC_VALUE_T Calculate(Byte[] array, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)array.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

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
