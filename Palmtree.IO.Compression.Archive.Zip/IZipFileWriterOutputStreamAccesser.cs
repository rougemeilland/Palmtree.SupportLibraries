namespace Palmtree.IO.Compression.Archive.Zip
{
    internal interface IZipFileWriterOutputStreamAccesser
    {
        IZipOutputStream MainStream { get; }
        ISequentialOutputByteStream StreamForCentralDirectoryHeaders { get; }
        void BeginToWriteContent();
        void EndToWritingContent();
        void SetErrorMark();
        void LockZipStream();
        void UnlockZipStream();
    }
}
