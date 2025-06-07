using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class TextWriterWithBranch
        : TextWriter
    {
        private readonly TextWriter _baseWriter1;
        private readonly TextWriter _baseWriter2;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public TextWriterWithBranch(TextWriter baseWriter1, TextWriter baseWriter2, Boolean leaveOpen)
        {
            _baseWriter1 = baseWriter1;
            _baseWriter2 = baseWriter2;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        public override Encoding Encoding => throw new NotSupportedException();
        public override IFormatProvider FormatProvider => throw new NotSupportedException();

        [AllowNull]
        public override String NewLine
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override async Task FlushAsync()
        {
            var task1 = _baseWriter1.FlushAsync();
            var task2 = _baseWriter2.FlushAsync();
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            var task1 = _baseWriter1.FlushAsync(cancellationToken);
            var task2 = _baseWriter2.FlushAsync(cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteAsync(Char value)
        {
            var task1 = _baseWriter1.WriteAsync(value);
            var task2 = _baseWriter2.WriteAsync(value);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteAsync(Char[] buffer, Int32 index, Int32 count)
        {
            var task1 = _baseWriter1.WriteAsync(buffer, index, count);
            var task2 = _baseWriter2.WriteAsync(buffer, index, count);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteAsync(ReadOnlyMemory<Char> buffer, CancellationToken cancellationToken = default)
        {
            var task1 = _baseWriter1.WriteAsync(buffer, cancellationToken);
            var task2 = _baseWriter2.WriteAsync(buffer, cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteAsync(String? value)
        {
            var task1 = _baseWriter1.WriteAsync(value);
            var task2 = _baseWriter2.WriteAsync(value);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            var task1 = _baseWriter1.WriteAsync(value, cancellationToken);
            var task2 = _baseWriter2.WriteAsync(value, cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteLineAsync()
        {
            var task1 = _baseWriter1.WriteLineAsync();
            var task2 = _baseWriter2.WriteLineAsync();
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteLineAsync(Char value)
        {
            var task1 = _baseWriter1.WriteLineAsync(value);
            var task2 = _baseWriter2.WriteLineAsync(value);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteLineAsync(Char[] buffer, Int32 index, Int32 count)
        {
            var task1 = _baseWriter1.WriteLineAsync(buffer, index, count);
            var task2 = _baseWriter2.WriteLineAsync(buffer, index, count);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteLineAsync(ReadOnlyMemory<Char> buffer, CancellationToken cancellationToken = default)
        {
            var task1 = _baseWriter1.WriteLineAsync(buffer, cancellationToken);
            var task2 = _baseWriter2.WriteLineAsync(buffer, cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteLineAsync(String? value)
        {
            var task1 = _baseWriter1.WriteLineAsync(value);
            var task2 = _baseWriter2.WriteLineAsync(value);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteLineAsync(StringBuilder? value, CancellationToken cancellationToken = default)
        {
            var task1 = _baseWriter1.WriteLineAsync(value, cancellationToken);
            var task2 = _baseWriter2.WriteLineAsync(value, cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override void Flush()
        {
            _baseWriter1.Flush();
            _baseWriter2.Flush();
        }

        public override void Write(Boolean value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(Char value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(Char[] buffer, Int32 index, Int32 count)
        {
            _baseWriter1.Write(buffer, index, count);
            _baseWriter2.Write(buffer, index, count);
        }

        public override void Write(Decimal value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(Double value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(Int32 value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(Int64 value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(Object? value)
        {
            if (value is null)
                return;
            if (value is IFormattable formattableValue)
            {
                _baseWriter1.Write(formattableValue.ToString(null, _baseWriter1.FormatProvider));
                _baseWriter2.Write(formattableValue.ToString(null, _baseWriter2.FormatProvider));
            }
            else
            {
                _baseWriter1.Write(value.ToString());
                _baseWriter2.Write(value.ToString());
            }
        }

        public override void Write(ReadOnlySpan<Char> buffer)
        {
            _baseWriter1.Write(buffer);
            _baseWriter2.Write(buffer);
        }

        public override void Write(Single value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(String? value)
        {
            if (value == null)
                return;
            var charArray = value.ToCharArray();
            _baseWriter1.Write(charArray);
            _baseWriter2.Write(charArray);
        }

        public override void Write(StringBuilder? value)
        {
            if (value == null)
                return;
            foreach (var chunk in value.GetChunks())
            {
                var chunkSpan = chunk.Span;
                _baseWriter1.Write(chunkSpan);
                _baseWriter2.Write(chunkSpan);
            }
        }

        public override void Write(UInt32 value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write(UInt64 value)
        {
            _baseWriter1.Write(value);
            _baseWriter2.Write(value);
        }

        public override void Write([StringSyntax("CompositeFormat")] String format, Object? arg0)
        {
            _baseWriter1.Write(format, arg0);
            _baseWriter2.Write(format, arg0);
        }

        public override void Write([StringSyntax("CompositeFormat")] String format, Object? arg0, Object? arg1)
        {
            _baseWriter1.Write(format, arg0, arg1);
            _baseWriter2.Write(format, arg0, arg1);
        }

        public override void Write([StringSyntax("CompositeFormat")] String format, Object? arg0, Object? arg1, Object? arg2)
        {
            _baseWriter1.Write(format, arg0, arg1, arg2);
            _baseWriter2.Write(format, arg0, arg1, arg2);
        }

        public override void Write([StringSyntax("CompositeFormat")] String format, params Object?[] arg)
        {
            _baseWriter1.Write(format, arg);
            _baseWriter2.Write(format, arg);
        }

#if NET9_0_OR_GREATER
        public override void Write([StringSyntax("CompositeFormat")] String format, params scoped ReadOnlySpan<Object?> arg)
        {
            _baseWriter1.Write(format, arg);
            _baseWriter2.Write(format, arg);
        }
#endif

        public override void WriteLine()
        {
            _baseWriter1.WriteLine( );
            _baseWriter2.WriteLine( );
        }

        public override void WriteLine(Char value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(Char[] buffer, Int32 index, Int32 count)
        {
            _baseWriter1.WriteLine(buffer, index, count);
            _baseWriter2.WriteLine(buffer, index, count);
        }

        public override void WriteLine(Char[]? buffer)
        {
            _baseWriter1.WriteLine(buffer);
            _baseWriter2.WriteLine(buffer);
        }

        public override void WriteLine(Decimal value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(Double value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(Int32 value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(Int64 value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(Object? value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(ReadOnlySpan<Char> buffer)
        {
            _baseWriter1.WriteLine(buffer);
            _baseWriter2.WriteLine(buffer);
        }

        public override void WriteLine(Single value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(String? value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(StringBuilder? value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(UInt32 value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine(UInt64 value)
        {
            _baseWriter1.WriteLine(value);
            _baseWriter2.WriteLine(value);
        }

        public override void WriteLine([StringSyntax("CompositeFormat")] String format, Object? arg0)
        {
            _baseWriter1.WriteLine(format, arg0);
            _baseWriter2.WriteLine(format, arg0);
        }

        public override void WriteLine([StringSyntax("CompositeFormat")] String format, Object? arg0, Object? arg1)
        {
            _baseWriter1.WriteLine(format, arg0, arg1);
            _baseWriter2.WriteLine(format, arg0, arg1);
        }

        public override void WriteLine([StringSyntax("CompositeFormat")] String format, Object? arg0, Object? arg1, Object? arg2)
        {
            _baseWriter1.WriteLine(format, arg0, arg1, arg2);
            _baseWriter2.WriteLine(format, arg0, arg1, arg2);
        }

        public override void WriteLine([StringSyntax("CompositeFormat")] String format, params Object?[] arg)
        {
            _baseWriter1.WriteLine(format, arg);
            _baseWriter2.WriteLine(format, arg);
        }

#if NET9_0_OR_GREATER
        public override void WriteLine([StringSyntax("CompositeFormat")] String format, params scoped ReadOnlySpan<Object?> arg)
        {
            _baseWriter1.WriteLine(format, arg);
            _baseWriter2.WriteLine(format, arg);
        }
#endif

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                    {
                        _baseWriter1.Dispose();
                        _baseWriter2.Dispose();
                    }
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
