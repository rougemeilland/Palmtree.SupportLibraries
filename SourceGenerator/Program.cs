﻿using System;
using System.IO;
using System.Text;

namespace SourceGenerator
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        static void Main(string[] args)
        {
            var baseDirectoryPath = new FileInfo(typeof(Program).Assembly.Location).Directory?.Parent?.Parent?.Parent?.Parent ?? throw new Exception();
            if (baseDirectoryPath.Name != "Palmtree.SupportLibraries")
                throw new Exception();

            var coreProjectPath = Path.Combine(baseDirectoryPath.FullName, "Palmtree.Core");

            GenerateArrayExtensions(coreProjectPath);
        }

        private static void GenerateArrayExtensions(string coreProjectPath)
        {
            var outputPath = Path.Combine(coreProjectPath, "ArrayExtensions.AutoGenerated.cs");
            if (File.Exists(outputPath))
            {
                var outputFileAttribute = File.GetAttributes(outputPath);
                File.SetAttributes(outputPath, outputFileAttribute & ~FileAttributes.ReadOnly);
            }

            try
            {
                var unmanagedTypes = new[]
                {
                    "Boolean",
                    "Char",
                    "SByte",
                    "Byte",
                    "Int16",
                    "UInt16",
                    "Int32",
                    "UInt32",
                    "Int64",
                    "UInt64",
                    "Single",
                    "Double",
                    "Decimal",
                };
                using var sourceWriter = new StreamWriter(outputPath, false, Encoding.UTF8);
                sourceWriter.WriteLine("using System;");
                sourceWriter.WriteLine("using System.Collections.Generic;");
                sourceWriter.WriteLine("using System.Runtime.CompilerServices;");
                sourceWriter.WriteLine();
                sourceWriter.WriteLine("namespace Palmtree");
                sourceWriter.WriteLine("{");
                sourceWriter.WriteLine("    partial class ArrayExtensions");
                sourceWriter.WriteLine("    {");
                sourceWriter.WriteLine("        #region InternalQuickSort");
                #region void InternalQuickSort<ELEMENT_T>(ELEMENT_T[] source, Int32 offset, Int32 count)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T>(ELEMENT_T[] source, Int32 offset, Int32 count)");
                sourceWriter.WriteLine("            where ELEMENT_T : IComparable<ELEMENT_T>");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source[offset]), count);");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, offset, offset + count - 1);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine();
                #region void InternalQuickSort<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelector)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelector)");
                sourceWriter.WriteLine("            where KEY_T : IComparable<KEY_T>");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<{unmanagedType}, KEY_T>>(ref keySelector));");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, offset, offset + count - 1, keySelector);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine();
                #region void InternalQuickSort<ELEMENT_T>(ELEMENT_T[] source, Int32 offset, Int32 count, IComparer<ELEMENT_T> comparer)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T>(ELEMENT_T[] source, Int32 offset, Int32 count, IComparer<ELEMENT_T> comparer)");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<{unmanagedType}>>(ref comparer));");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, offset, offset + count - 1, comparer);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine();
                #region void InternalQuickSort<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<{unmanagedType}, KEY_T>>(ref keySelector), keyComparer);");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, offset, offset + count - 1, keySelector, keyComparer);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine();
                #region void InternalQuickSort<ELEMENT_T>(Span<ELEMENT_T> source)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T>(Span<ELEMENT_T> source)");
                sourceWriter.WriteLine("            where ELEMENT_T : IComparable<ELEMENT_T>");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source.GetPinnableReference()), source.Length);");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, 0, source.Length - 1);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine();
                #region void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter)");
                sourceWriter.WriteLine("            where KEY_T : IComparable<KEY_T>");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<{unmanagedType}, KEY_T>>(ref keySekecter));");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, 0, source.Length - 1, keySekecter);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine();
                #region void InternalQuickSort<ELEMENT_T>(Span<ELEMENT_T> source, IComparer<ELEMENT_T> comparer)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T>(Span<ELEMENT_T> source, IComparer<ELEMENT_T> comparer)");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<{unmanagedType}>>(ref comparer));");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, 0, source.Length - 1, comparer);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine();
                #region void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
                sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sourceWriter.WriteLine("        private static void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)");
                sourceWriter.WriteLine("        {");
                sourceWriter.WriteLine("            if (source.Length < 2)");
                sourceWriter.WriteLine("                return;");
                foreach (var unmanagedType in unmanagedTypes)
                {
                    sourceWriter.WriteLine($"            else if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                    sourceWriter.WriteLine($"                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<{unmanagedType}, KEY_T>>(ref keySekecter), keyComparer);");
                }

                sourceWriter.WriteLine("            else");
                sourceWriter.WriteLine("                InternalQuickSortManaged(source, 0, source.Length - 1, keySekecter, keyComparer);");
                sourceWriter.WriteLine("        }");
                #endregion
                sourceWriter.WriteLine("        #endregion");
                sourceWriter.WriteLine();
                sourceWriter.WriteLine("        #region InternalSequenceEqual");
                #region Boolean InternalSequenceEqual<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Boolean InternalSequenceEqual<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)");
                    sourceWriter.WriteLine("            where ELEMENT_T : IEquatable<ELEMENT_T>");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array2[array2Offset]), array2Length);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceEqualManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region Boolean InternalSequenceEqual<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IEqualityComparer<ELEMENT_T> equalityComparer)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Boolean InternalSequenceEqual<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IEqualityComparer<ELEMENT_T> equalityComparer)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<{unmanagedType}>>(ref equalityComparer));");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceEqualManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length, equalityComparer);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)");
                    sourceWriter.WriteLine("            where ELEMENT_T : IEquatable<ELEMENT_T>");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceEqualManaged(array1, array2);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<{unmanagedType}>>(ref equalityComparer));");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceEqualManaged(array1, array2, equalityComparer);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine("        #endregion");
                sourceWriter.WriteLine();
                sourceWriter.WriteLine("        #region InternalSequenceCompare");
                #region Int32 InternalSequenceCompare<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Int32 InternalSequenceCompare<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)");
                    sourceWriter.WriteLine("            where ELEMENT_T : IComparable<ELEMENT_T>");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array2[array2Offset]), array2Length);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceCompareManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region Int32 InternalSequenceCompare<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IComparer<ELEMENT_T> comparer)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Int32 InternalSequenceCompare<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IComparer<ELEMENT_T> comparer)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<{unmanagedType}>>(ref comparer));");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceCompareManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length, comparer);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region Int32 InternalSequenceCompare<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Int32 InternalSequenceCompare<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)");
                    sourceWriter.WriteLine("            where ELEMENT_T : IComparable<ELEMENT_T>");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceCompareManaged(array1, array2);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region Int32 Int32 InternalSequenceCompare<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static Int32 InternalSequenceCompare<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<{unmanagedType}>>(ref comparer));");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                return InternalSequenceCompareManaged(array1, array2, comparer);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine("        #endregion");
                sourceWriter.WriteLine();
                sourceWriter.WriteLine("        #region InternalCopyMemory");
                #region void InternalCopyMemory<ELEMENT_T>(ELEMENT_T[] sourceArray, Int32 sourceArrayOffset, ELEMENT_T[] destinationArray, Int32 destinationArrayOffset, Int32 count)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static void InternalCopyMemory<ELEMENT_T>(ELEMENT_T[] sourceArray, Int32 sourceArrayOffset, ELEMENT_T[] destinationArray, Int32 destinationArrayOffset, Int32 count)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref destinationArray[destinationArrayOffset]), count);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                InternalCopyMemoryManaged(sourceArray, ref sourceArrayOffset, destinationArray, ref destinationArrayOffset, count);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region void InternalCopyMemory<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> sourceArray, Span<ELEMENT_T> destinationArray)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static void InternalCopyMemory<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> sourceArray, Span<ELEMENT_T> destinationArray)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref destinationArray.GetPinnableReference()), sourceArray.Length);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                InternalCopyMemoryManaged(sourceArray, destinationArray);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine("        #endregion");
                sourceWriter.WriteLine();
                sourceWriter.WriteLine("        #region InternalReverseArray");
                #region void InternalReverseArray<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset, Int32 count)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static void InternalReverseArray<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset, Int32 count)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source[offset]), count);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                InternalReverseArrayManaged(source, offset, count);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine();
                #region void InternalReverseArray<ELEMENT_T>(Span<ELEMENT_T> source)
                {
                    sourceWriter.WriteLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                    sourceWriter.WriteLine("        private static void InternalReverseArray<ELEMENT_T>(Span<ELEMENT_T> source)");
                    sourceWriter.WriteLine("        {");
                    var firstElement = true;
                    foreach (var unmanagedType in unmanagedTypes)
                    {
                        sourceWriter.WriteLine($"            {(firstElement ? "" : "else ")}if (typeof(ELEMENT_T) == typeof({unmanagedType}))");
                        sourceWriter.WriteLine($"                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, {unmanagedType}>(ref source.GetPinnableReference()), source.Length);");
                        firstElement = false;
                    }

                    sourceWriter.WriteLine("            else");
                    sourceWriter.WriteLine("                InternalReverseArrayManaged(source);");
                    sourceWriter.WriteLine("        }");
                }
                #endregion
                sourceWriter.WriteLine("        #endregion");
                sourceWriter.WriteLine("    }");
                sourceWriter.WriteLine("}");
            }
            finally
            {
                var outputFileAttribute = File.GetAttributes(outputPath);
                File.SetAttributes(outputPath, outputFileAttribute | FileAttributes.ReadOnly);
            }
        }
    }
}