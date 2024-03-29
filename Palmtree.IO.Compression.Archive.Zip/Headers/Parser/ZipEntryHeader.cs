﻿using System;

namespace Palmtree.IO.Compression.Archive.Zip.Headers.Parser
{
    internal class ZipEntryHeader
    {
        public ZipEntryHeader(ZipEntryCentralDirectoryHeader centralDirectoryHeader, ZipEntryLocalHeader localHeader)
        {
            if (centralDirectoryHeader.LocalHeaderPosition != localHeader.LocalHeaderPosition)
                throw new BadZipFileFormatException($"The value of {nameof(centralDirectoryHeader.LocalHeaderPosition)} does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");
            if (!centralDirectoryHeader.FullNameBytes.Span.SequenceEqual(localHeader.FullNameBytes.Span))
                throw new BadZipFileFormatException($"The value of {nameof(centralDirectoryHeader.FullNameBytes)} does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");
            if (centralDirectoryHeader.CompressionMethodId != localHeader.CompressionMethodId)
                throw new BadZipFileFormatException($"The value of {nameof(centralDirectoryHeader.CompressionMethodId)} does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");
            if (centralDirectoryHeader.DosDateTimeOffset != localHeader.DosDateTimeOffset)
                throw new BadZipFileFormatException($"The value of {nameof(centralDirectoryHeader.DosDateTimeOffset)} does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");

            if (centralDirectoryHeader.GeneralPurposeBitFlag.HasFlag(ZipEntryGeneralPurposeBitFlag.HasDataDescriptor)
                != localHeader.GeneralPurposeBitFlag.HasFlag(ZipEntryGeneralPurposeBitFlag.HasDataDescriptor))
            {
                throw new BadZipFileFormatException($"The value of general purpose flag 3bit does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");
            }

            if (centralDirectoryHeader.GeneralPurposeBitFlag.HasFlag(ZipEntryGeneralPurposeBitFlag.UseUnicodeEncodingForNameAndComment)
                != localHeader.GeneralPurposeBitFlag.HasFlag(ZipEntryGeneralPurposeBitFlag.UseUnicodeEncodingForNameAndComment))
            {
                throw new BadZipFileFormatException("The value of general purpose flag bit 11 does not match between local header and central directory header.");
            }

            if (centralDirectoryHeader.Crc != localHeader.Crc)
                throw new BadZipFileFormatException($"The value of {nameof(centralDirectoryHeader.Crc)} does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");
            if (centralDirectoryHeader.PackedSize != localHeader.PackedSize)
                throw new BadZipFileFormatException($"The value of {nameof(centralDirectoryHeader.PackedSize)} does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");
            if (centralDirectoryHeader.Size != localHeader.Size)
                throw new BadZipFileFormatException($"The value of {nameof(centralDirectoryHeader.PackedSize)} does not match between the central directory header and local directory header.: centralDirectory={centralDirectoryHeader.CentralDirectoryHeaderPosition}");

            ID = new ZipEntryId(centralDirectoryHeader.CentralDirectoryHeaderPosition);
            LocationOrder = new ZipEntryLocationOrder(localHeader.LocalHeaderPosition);
            CentralDirectoryHeader = centralDirectoryHeader;
            LocalHeader = localHeader;
        }

        public ZipEntryId ID { get; }
        public ZipEntryLocationOrder LocationOrder { get; }
        public ZipEntryCentralDirectoryHeader CentralDirectoryHeader { get; }
        public ZipEntryLocalHeader LocalHeader { get; }
    }
}
