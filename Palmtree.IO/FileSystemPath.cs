using System;
using System.IO;

namespace Palmtree.IO
{
    public abstract class FileSystemPath
    {
        protected FileSystemPath(FileSystemInfo path)
        {
            ArgumentNullException.ThrowIfNull(path);

            Extension = path.Extension;
            FullName = path.FullName;
            Name = path.Name;
            NameWithoutExtension = Path.GetFileNameWithoutExtension(path.Name);
        }

        public DateTime CreationTimeUtc
        {
            get => InternalCreationTimeUtc;

            set
            {
                InternalCreationTimeUtc =
                    value.Kind switch
                    {
                        DateTimeKind.Utc => value,
                        DateTimeKind.Local => value.ToUniversalTime(),
                        _ => throw new ArgumentException($"The value of the '{nameof(DateTime.Kind)}' property of the '{nameof(value)}' parameter is not '{nameof(DateTimeKind)}.{nameof(DateTimeKind.Utc)}' or '{nameof(DateTimeKind)}.{nameof(DateTimeKind.Local)}'.: {nameof(value)}.{nameof(DateTime.Kind)}={value.Kind}", nameof(value)),
                    };
            }
        }

        public DateTimeOffset CreationTimeOffsetUtc
        {
            get => InternalCreationTimeUtc.ToDateTimeOffset();
            set => InternalCreationTimeUtc = value.ToDateTime(DateTimeKind.Utc);
        }

        public abstract Boolean Exists { get; }

        public String Extension { get; }

        public String FullName { get; }

        public DateTime LastAccessTimeUtc
        {
            get => InternalLastAccessTimeUtc;

            set
            {
                InternalLastAccessTimeUtc =
                    value.Kind switch
                    {
                        DateTimeKind.Utc => value,
                        DateTimeKind.Local => value.ToUniversalTime(),
                        _ => throw new ArgumentException($"The value of the '{nameof(DateTime.Kind)}' property of the '{nameof(value)}' parameter is not '{nameof(DateTimeKind)}.{nameof(DateTimeKind.Utc)}' or '{nameof(DateTimeKind)}.{nameof(DateTimeKind.Local)}'.: {nameof(value)}.{nameof(DateTime.Kind)}={value.Kind}", nameof(value)),
                    };
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
                InternalLastWriteTimeUtc =
                    value.Kind switch
                    {
                        DateTimeKind.Utc => value,
                        DateTimeKind.Local => value.ToUniversalTime(),
                        _ => throw new ArgumentException($"The value of the '{nameof(DateTime.Kind)}' property of the '{nameof(value)}' parameter is not '{nameof(DateTimeKind)}.{nameof(DateTimeKind.Utc)}' or '{nameof(DateTimeKind)}.{nameof(DateTimeKind.Local)}'.: {nameof(value)}.{nameof(DateTime.Kind)}={value.Kind}", nameof(value)),
                    };
            }
        }

        public DateTimeOffset LastWriteTimeOffsetUtc
        {
            get => InternalLastWriteTimeUtc.ToDateTimeOffset();
            set => InternalLastWriteTimeUtc = value.ToDateTime(DateTimeKind.Utc);
        }

        public String Name { get; }
        public String NameWithoutExtension { get; }

        public override String ToString() => FullName;

        protected abstract DateTime InternalCreationTimeUtc { get; set; }
        protected abstract DateTime InternalLastAccessTimeUtc { get; set; }
        protected abstract DateTime InternalLastWriteTimeUtc { get; set; }
    }
}
