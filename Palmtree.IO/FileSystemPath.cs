using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Palmtree.IO
{
    public abstract class FileSystemPath
    {
        private readonly FileSystemInfo _path;

        protected FileSystemPath(FileSystemInfo path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
            Extension = _path.Extension;
            FullName = _path.FullName;
            Name = _path.Name;
            NameWithoutExtension = Path.GetFileNameWithoutExtension(_path.Name);
        }

        public DateTime CreationTimeUtc
        {
            get => InternalCreationTimeUtc;

            set
            {
                if (value.Kind == DateTimeKind.Unspecified)
                    throw new ArgumentException("Do not set a DateTime value whose Kind property value is 'DateTimeKind.Unspecified'.", nameof(value));

                InternalCreationTimeUtc = value;
            }
        }

        public DateTimeOffset CreationTimeOffsetUtc
        {
            get => InternalCreationTimeUtc.ToDateTimeOffset();
            set => InternalCreationTimeUtc = value.ToDateTime(DateTimeKind.Utc);
        }

        public Boolean Exists
        {
            get
            {
                _path.Refresh();
                return _path.Exists;
            }
        }

        public String Extension { get; }

        public String FullName { get; }

        public DateTime LastAccessTimeUtc
        {
            get => InternalLastAccessTimeUtc;

            set
            {
                if (value.Kind == DateTimeKind.Unspecified)
                    throw new ArgumentException("Do not set a DateTime value whose Kind property value is 'DateTimeKind.Unspecified'.", nameof(value));

                InternalLastAccessTimeUtc = value;
            }
        }

        public DateTimeOffset LastAccessTimeOffsetUtc
        {
            get => InternalLastAccessTimeUtc.ToDateTimeOffset();
            set => InternalLastAccessTimeUtc = value.ToDateTime(DateTimeKind.Utc);
        }

        public DateTime LastWriteTimeUtc
        {
            get => InternalLastWriteTimeUtc;

            set
            {
                if (value.Kind == DateTimeKind.Unspecified)
                    throw new ArgumentException("Do not set a DateTime value whose Kind property value is 'DateTimeKind.Unspecified'.", nameof(value));

                InternalLastWriteTimeUtc = value;
            }
        }

        public DateTimeOffset LastWriteTimeOffsetUtc
        {
            get => InternalLastWriteTimeUtc.ToDateTimeOffset();
            set => InternalLastWriteTimeUtc = value.ToDateTime(DateTimeKind.Utc);
        }

        public String Name { get; }
        public String NameWithoutExtension { get; }

        public void Delete()
        {
            _path.Refresh();
            try
            {
                _path.Delete();

            }
            finally
            {
                _path.Refresh();
#if DEBUG
                ValidationPath();
#endif
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String ToString() => FullName;

        internal void Refresh() => _path.Refresh();

#if DEBUG
        protected virtual void ValidationPath()
        {
            Validation.Assert(String.Equals(_path.Extension, Extension, StringComparison.OrdinalIgnoreCase), "String.Equals(_path.Extension, Extension, StringComparison.OrdinalIgnoreCase)");
            Validation.Assert(String.Equals(_path.FullName, FullName, StringComparison.OrdinalIgnoreCase), "String.Equals(_path.FullName, FullName, StringComparison.OrdinalIgnoreCase)");
            Validation.Assert(String.Equals(_path.Name, Name, StringComparison.OrdinalIgnoreCase), "String.Equals(_path.Name, Name, StringComparison.OrdinalIgnoreCase)");
            Validation.Assert(String.Equals(_path.GetNameWithoutExtension(), NameWithoutExtension, StringComparison.OrdinalIgnoreCase), "String.Equals(_path.GetNameWithoutExtension(), NameWithoutExtension, StringComparison.OrdinalIgnoreCase)");
        }
#endif

        private DateTime InternalCreationTimeUtc
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                _path.Refresh();
                return _path.CreationTimeUtc;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Palmtree.Validation.Assert(value.Kind is DateTimeKind.Utc or DateTimeKind.Local, "value.Kind is DateTimeKind.Utc or DateTimeKind.Local");
                try
                {
                    _path.CreationTimeUtc = value.ToUniversalTime();
                }
                finally
                {
                    _path.Refresh();
                }
            }
        }

        private DateTime InternalLastAccessTimeUtc
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                _path.Refresh();
                return _path.LastAccessTimeUtc;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Palmtree.Validation.Assert(value.Kind is DateTimeKind.Utc or DateTimeKind.Local, "value.Kind is DateTimeKind.Utc or DateTimeKind.Local");
                try
                {
                    _path.LastAccessTimeUtc = value.ToUniversalTime();
                }
                finally
                {
                    _path.Refresh();
                }
            }
        }

        private DateTime InternalLastWriteTimeUtc
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                _path.Refresh();
                return _path.LastWriteTimeUtc;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Validation.Assert(value.Kind is DateTimeKind.Utc or DateTimeKind.Local, "value.Kind is DateTimeKind.Utc or DateTimeKind.Local");
                try
                {
                    _path.LastWriteTimeUtc = value.ToUniversalTime();
                }
                finally
                {
                    _path.Refresh();
                }
            }
        }
    }
}
