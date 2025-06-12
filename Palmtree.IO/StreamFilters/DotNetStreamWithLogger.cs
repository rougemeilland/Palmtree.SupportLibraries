using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamWithLogger
        : Stream
    {
        private static UInt64 _serialNumber;
        private readonly Stream _baseStream;
        private readonly IValidationLogger? _validationLogger;
        private readonly UInt64 _handle;
        private Boolean _isDisposed;
        private Boolean _loggedDispose;

        public DotNetStreamWithLogger(Stream baseStream, IValidationLogger? validationLogger)
        {
            ArgumentNullException.ThrowIfNull(baseStream);

            _baseStream = baseStream;
#if DEBUG
            _validationLogger = validationLogger ?? Validation.Debug;
#elif TRACE
            _validationLogger = validationLogger ?? Validation.Trace;
#else
            _validationLogger = validationLogger;
#endif
            _handle = Interlocked.Increment(ref _serialNumber);
            _isDisposed = false;
            _loggedDispose = false;
            _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:.ctor(Stream ({baseStream.GetType().FullName}{(baseStream is FileStream baseFileStream ? $", \"{baseFileStream.Name}\"" : "")}), IValidationLogger?{(validationLogger is null ? " null" : "")})");
        }

        public override Boolean CanRead => _baseStream.CanRead;
        public override Boolean CanSeek => _baseStream.CanSeek;
        public override Boolean CanTimeout => _baseStream.CanTimeout;
        public override Boolean CanWrite => _baseStream.CanWrite;
        public override Int64 Length => _baseStream.Length;

        public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:BeginRead(Byte[], Int32 {offset}, Int32 {count}, AsyncCallback?{(callback is null ? " null" : "")}, Object?{(state is null ? " null" : "")})");
                var ret = _baseStream.BeginRead(buffer, offset, count, callback, state);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:BeginRead(Byte[], Int32, Int32, AsyncCallback?, Object?)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:BeginWrite(Byte[], Int32 {offset}, Int32 {count}, AsyncCallback?{(callback is null ? " null" : "")}, Object?{(state is null ? " null" : "")})");
                var ret = _baseStream.BeginWrite(buffer, offset, count, callback, state);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:BeginWrite(Byte[], Int32, Int32, AsyncCallback?, Object?)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override Int32 EndRead(IAsyncResult asyncResult)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:EndRead(IAsyncResult)");
                var ret = _baseStream.EndRead(asyncResult);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {ret} <= {_handle}:EndRead(IAsyncResult)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Read(Byte[], Int32 {offset}, Int32 {count})");
                var ret = _baseStream.Read(buffer, offset, count);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {ret} <= {_handle}:Read(Byte[], Int32, Int32)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Read(Span<Byte> ({buffer.Length} byte(s)))");
                var ret = _baseStream.Read(buffer);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {ret} <= {_handle}:Read(Span<Byte>)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override Int32 ReadByte()
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:ReadByte()");
                var ret = _baseStream.ReadByte();
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {ret} <= {_handle}:ReadByte()");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override Int64 Position
        {
            get
            {
                try
                {
                    _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Position.get()");
                    var value = _baseStream.Position;
                    _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {value} <= {_handle}:Position.get()");
                    return value;
                }
                catch (Exception ex)
                {
                    _validationLogger?.WriteLog(ex);
                    throw;
                }
            }

            set
            {
                try
                {
                    _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Position.set({value})");
                    _baseStream.Position = value;
                    _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:Position.set()");
                }
                catch (Exception ex)
                {
                    _validationLogger?.WriteLog(ex);
                    throw;
                }
            }
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Seek(Int64 {offset}, SeekOrigin {origin})");
                var ret = _baseStream.Seek(offset, origin);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {ret} <= {_handle}:Seek(Int64, SeekOrigin)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override async Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken cancellationToken)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:CopyToAsync(Stream, Int32 {bufferSize}, CancellationToken{(cancellationToken == default ? " default" : "")})");
                await _baseStream.CopyToAsync(destination, bufferSize, cancellationToken).ConfigureAwait(false);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:CopyToAsync(Stream, Int32, CancellationToken)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:FlushAsync(CancellationToken{(cancellationToken == default ? " default" : "")})");
                await _baseStream.FlushAsync(cancellationToken).ConfigureAwait(false);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:FlushAsync(CancellationToken)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1835:'ReadAsync' および 'WriteAsync' で 'Memory' ベースのオーバーロードを優先的に使用する", Justification = "_baseStream への透過的な呼び出しを優先するため、Memory ベースへの呼び出しには変更しない。")]
        public override async Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:WriteAsync(Byte[], Int32 {offset}, Int32 {count}, CancellationToken{(cancellationToken == default ? " default" : "")})");
                await _baseStream.WriteAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1835:'ReadAsync' および 'WriteAsync' で 'Memory' ベースのオーバーロードを優先的に使用する", Justification = "_baseStream への透過的な呼び出しを優先するため、Memory ベースへの呼び出しには変更しない。")]
        public override async Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:ReadAsync(Byte[], Int32 {offset}, Int32 {count}, CancellationToken{(cancellationToken == default ? " default" : "")})");
                var ret = await _baseStream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {ret} <= {_handle}:ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override async ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:WriteAsync(ReadOnlyMemory<Byte> ({buffer.Length} byte(s)), CancellationToken{(cancellationToken == default ? " default" : "")})");
                await _baseStream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:WriteAsync(ReadOnlyMemory<Byte>, CancellationToken)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override async ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:ReadAsync(Memory<Byte> ({buffer.Length} byte(s)), CancellationToken{(cancellationToken == default ? " default" : "")})");
                var ret = await _baseStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) {ret} <= {_handle}:ReadAsync(Memory<Byte>, CancellationToken)");
                return ret;
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override void CopyTo(Stream destination, Int32 bufferSize)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:CopyTo(Stream, Int32 {bufferSize})");
                _baseStream.CopyTo(destination, bufferSize);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:CopyTo(Stream, Int32)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:EndWrite(IAsyncResult)");
                _baseStream.EndWrite(asyncResult);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:EndWrite(IAsyncResult)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override void Flush()
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Flush()");
                _baseStream.Flush();
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:Flush()");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override void SetLength(Int64 value)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:SetLength(Int64 {value})");
                _baseStream.SetLength(value);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:SetLength(Int64)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Write(Byte[], Int32 {offset}, Int32 {count})");
                _baseStream.Write(buffer, offset, count);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:Write(Byte[], Int32, Int32)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Write(ReadOnlySpan<Byte> ({buffer.Length} byte(s)))");
                _baseStream.Write(buffer);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:Write(ReadOnlySpan<Byte>)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        public override void WriteByte(Byte value)
        {
            try
            {
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:WriteByte(Byte {value}");
                _baseStream.WriteByte(value);
                _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:WriteByte(Byte)");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            try
            {
                if (!_loggedDispose)
                    _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) => {_handle}:Dispose()/Close()");

                if (!_isDisposed)
                {
                    if (disposing)
                    {
                        _baseStream.Dispose();
                    }

                    _isDisposed = true;
                }

                base.Dispose(disposing);

                if (!_loggedDispose)
                    _validationLogger?.WriteLog(LogCategory.Information, $"({Environment.CurrentManagedThreadId}) <= {_handle}:Dispose()/Close()");
            }
            catch (Exception ex)
            {
                _validationLogger?.WriteLog(ex);
                throw;
            }
            finally
            {
                _loggedDispose = true;
            }
        }
    }
}
