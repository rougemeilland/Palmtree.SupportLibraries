using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class LinqExtensions
    {
        #region Max

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Max(this IEnumerable<SByte> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Max(this IEnumerable<SByte> source, IComparer<SByte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte> selector, IComparer<SByte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Max(this IEnumerable<SByte?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Max(this IEnumerable<SByte?> source, IComparer<SByte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte?> selector, IComparer<SByte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Max(this IEnumerable<Byte> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Max(this IEnumerable<Byte> source, IComparer<Byte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte> selector, IComparer<Byte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Max(this IEnumerable<Byte?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Max(this IEnumerable<Byte?> source, IComparer<Byte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte?> selector, IComparer<Byte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Max(this IEnumerable<Int16> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Max(this IEnumerable<Int16> source, IComparer<Int16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16> selector, IComparer<Int16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Max(this IEnumerable<Int16?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Max(this IEnumerable<Int16?> source, IComparer<Int16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16?> selector, IComparer<Int16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Max(this IEnumerable<UInt16> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Max(this IEnumerable<UInt16> source, IComparer<UInt16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16> selector, IComparer<UInt16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Max(this IEnumerable<UInt16?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Max(this IEnumerable<UInt16?> source, IComparer<UInt16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16?> selector, IComparer<UInt16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Max(this IEnumerable<Int32> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Max(this IEnumerable<Int32> source, IComparer<Int32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32> selector, IComparer<Int32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Max(this IEnumerable<Int32?> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32?> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Max(this IEnumerable<Int32?> source, IComparer<Int32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32?> selector, IComparer<Int32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Max(this IEnumerable<UInt32> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Max(this IEnumerable<UInt32> source, IComparer<UInt32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32> selector, IComparer<UInt32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Max(this IEnumerable<UInt32?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Max(this IEnumerable<UInt32?> source, IComparer<UInt32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32?> selector, IComparer<UInt32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Max(this IEnumerable<Int64> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Max(this IEnumerable<Int64> source, IComparer<Int64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64> selector, IComparer<Int64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Max(this IEnumerable<Int64?> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64?> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Max(this IEnumerable<Int64?> source, IComparer<Int64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64?> selector, IComparer<Int64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Max(this IEnumerable<UInt64> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Max(this IEnumerable<UInt64> source, IComparer<UInt64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64> selector, IComparer<UInt64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Max(this IEnumerable<UInt64?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Max(this IEnumerable<UInt64?> source, IComparer<UInt64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64?> selector, IComparer<UInt64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Max(this IEnumerable<Int128> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Max(this IEnumerable<Int128> source, IComparer<Int128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128> selector, IComparer<Int128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Max(this IEnumerable<Int128?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Max(this IEnumerable<Int128?> source, IComparer<Int128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128?> selector, IComparer<Int128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Max(this IEnumerable<UInt128> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Max(this IEnumerable<UInt128> source, IComparer<UInt128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128> selector, IComparer<UInt128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Max(this IEnumerable<UInt128?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Max(this IEnumerable<UInt128?> source, IComparer<UInt128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128?> selector, IComparer<UInt128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Max(this IEnumerable<Half> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Max(this IEnumerable<Half> source, IComparer<Half> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half> selector, IComparer<Half> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Max(this IEnumerable<Half?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Max(this IEnumerable<Half?> source, IComparer<Half?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half?> selector, IComparer<Half?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Max(this IEnumerable<Single> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Max(this IEnumerable<Single> source, IComparer<Single> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single> selector, IComparer<Single> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Max(this IEnumerable<Single?> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single?> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Max(this IEnumerable<Single?> source, IComparer<Single?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single?> selector, IComparer<Single?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Max(this IEnumerable<Double> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Max(this IEnumerable<Double> source, IComparer<Double> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double> selector, IComparer<Double> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Max(this IEnumerable<Double?> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double?> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Max(this IEnumerable<Double?> source, IComparer<Double?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double?> selector, IComparer<Double?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Max(this IEnumerable<NFloat> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Max(this IEnumerable<NFloat> source, IComparer<NFloat> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat> selector, IComparer<NFloat> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Max(this IEnumerable<NFloat?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Max(this IEnumerable<NFloat?> source, IComparer<NFloat?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat?> selector, IComparer<NFloat?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Max(this IEnumerable<Decimal> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Max(this IEnumerable<Decimal> source, IComparer<Decimal> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal> selector, IComparer<Decimal> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Max(this IEnumerable<Decimal?> source)
            => System.Linq.Enumerable.Max(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal?> selector)
            => System.Linq.Enumerable.Max(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Max(this IEnumerable<Decimal?> source, IComparer<Decimal?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal?> selector, IComparer<Decimal?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Max(this IEnumerable<IntPtr> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Max(this IEnumerable<IntPtr> source, IComparer<IntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr> selector, IComparer<IntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Max(this IEnumerable<IntPtr?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Max(this IEnumerable<IntPtr?> source, IComparer<IntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr?> selector, IComparer<IntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Max(this IEnumerable<UIntPtr> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Max(this IEnumerable<UIntPtr> source, IComparer<UIntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr> selector, IComparer<UIntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Max(this IEnumerable<UIntPtr?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Max(this IEnumerable<UIntPtr?> source, IComparer<UIntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr?> selector, IComparer<UIntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Max(this IEnumerable<BigInteger> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Max(this IEnumerable<BigInteger> source, IComparer<BigInteger> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger> selector, IComparer<BigInteger> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Max(this IEnumerable<BigInteger?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Max(this IEnumerable<BigInteger?> source, IComparer<BigInteger?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MaxCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Max<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger?> selector, IComparer<BigInteger?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MaxCore(source, selector, comparer);
        }

        #endregion

        #region Min

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Min(this IEnumerable<SByte> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Min(this IEnumerable<SByte> source, IComparer<SByte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte> selector, IComparer<SByte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Min(this IEnumerable<SByte?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Min(this IEnumerable<SByte?> source, IComparer<SByte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte?> selector, IComparer<SByte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Min(this IEnumerable<Byte> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Min(this IEnumerable<Byte> source, IComparer<Byte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte> selector, IComparer<Byte> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Min(this IEnumerable<Byte?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Min(this IEnumerable<Byte?> source, IComparer<Byte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte?> selector, IComparer<Byte?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Min(this IEnumerable<Int16> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Min(this IEnumerable<Int16> source, IComparer<Int16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16> selector, IComparer<Int16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Min(this IEnumerable<Int16?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Min(this IEnumerable<Int16?> source, IComparer<Int16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16?> selector, IComparer<Int16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Min(this IEnumerable<UInt16> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Min(this IEnumerable<UInt16> source, IComparer<UInt16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16> selector, IComparer<UInt16> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Min(this IEnumerable<UInt16?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Min(this IEnumerable<UInt16?> source, IComparer<UInt16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16?> selector, IComparer<UInt16?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Min(this IEnumerable<Int32> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Min(this IEnumerable<Int32> source, IComparer<Int32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32> selector, IComparer<Int32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Min(this IEnumerable<Int32?> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32?> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Min(this IEnumerable<Int32?> source, IComparer<Int32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32?> selector, IComparer<Int32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Min(this IEnumerable<UInt32> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Min(this IEnumerable<UInt32> source, IComparer<UInt32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32> selector, IComparer<UInt32> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Min(this IEnumerable<UInt32?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Min(this IEnumerable<UInt32?> source, IComparer<UInt32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32?> selector, IComparer<UInt32?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Min(this IEnumerable<Int64> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Min(this IEnumerable<Int64> source, IComparer<Int64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64> selector, IComparer<Int64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Min(this IEnumerable<Int64?> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64?> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Min(this IEnumerable<Int64?> source, IComparer<Int64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64?> selector, IComparer<Int64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Min(this IEnumerable<UInt64> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Min(this IEnumerable<UInt64> source, IComparer<UInt64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64> selector, IComparer<UInt64> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Min(this IEnumerable<UInt64?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Min(this IEnumerable<UInt64?> source, IComparer<UInt64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64?> selector, IComparer<UInt64?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Min(this IEnumerable<Int128> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Min(this IEnumerable<Int128> source, IComparer<Int128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128> selector, IComparer<Int128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Min(this IEnumerable<Int128?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Min(this IEnumerable<Int128?> source, IComparer<Int128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128?> selector, IComparer<Int128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Min(this IEnumerable<UInt128> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Min(this IEnumerable<UInt128> source, IComparer<UInt128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128> selector, IComparer<UInt128> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Min(this IEnumerable<UInt128?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Min(this IEnumerable<UInt128?> source, IComparer<UInt128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128?> selector, IComparer<UInt128?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Min(this IEnumerable<Half> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Min(this IEnumerable<Half> source, IComparer<Half> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half> selector, IComparer<Half> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Min(this IEnumerable<Half?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Min(this IEnumerable<Half?> source, IComparer<Half?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half?> selector, IComparer<Half?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Min(this IEnumerable<Single> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Min(this IEnumerable<Single> source, IComparer<Single> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single> selector, IComparer<Single> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Min(this IEnumerable<Single?> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single?> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Min(this IEnumerable<Single?> source, IComparer<Single?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single?> selector, IComparer<Single?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Min(this IEnumerable<Double> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Min(this IEnumerable<Double> source, IComparer<Double> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double> selector, IComparer<Double> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Min(this IEnumerable<Double?> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double?> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Min(this IEnumerable<Double?> source, IComparer<Double?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double?> selector, IComparer<Double?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Min(this IEnumerable<NFloat> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Min(this IEnumerable<NFloat> source, IComparer<NFloat> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat> selector, IComparer<NFloat> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Min(this IEnumerable<NFloat?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Min(this IEnumerable<NFloat?> source, IComparer<NFloat?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat?> selector, IComparer<NFloat?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Min(this IEnumerable<Decimal> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Min(this IEnumerable<Decimal> source, IComparer<Decimal> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal> selector, IComparer<Decimal> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Min(this IEnumerable<Decimal?> source)
            => System.Linq.Enumerable.Min(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal?> selector)
            => System.Linq.Enumerable.Min(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Min(this IEnumerable<Decimal?> source, IComparer<Decimal?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal?> selector, IComparer<Decimal?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Min(this IEnumerable<IntPtr> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Min(this IEnumerable<IntPtr> source, IComparer<IntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr> selector, IComparer<IntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Min(this IEnumerable<IntPtr?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Min(this IEnumerable<IntPtr?> source, IComparer<IntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr?> selector, IComparer<IntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Min(this IEnumerable<UIntPtr> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Min(this IEnumerable<UIntPtr> source, IComparer<UIntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr> selector, IComparer<UIntPtr> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Min(this IEnumerable<UIntPtr?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Min(this IEnumerable<UIntPtr?> source, IComparer<UIntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr?> selector, IComparer<UIntPtr?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Min(this IEnumerable<BigInteger> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Min(this IEnumerable<BigInteger> source, IComparer<BigInteger> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger> selector, IComparer<BigInteger> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Min(this IEnumerable<BigInteger?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Min(this IEnumerable<BigInteger?> source, IComparer<BigInteger?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.MinCore(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Min<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger?> selector, IComparer<BigInteger?> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);
            ArgumentNullException.ThrowIfNull(comparer);

            return MinCore(source, selector, comparer);
        }

        #endregion

        #region Sum

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Sum(this IEnumerable<SByte> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<SByte, Int32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, SByte, Int32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Sum(this IEnumerable<SByte?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<SByte, Int32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, SByte?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, SByte, Int32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Sum(this IEnumerable<Byte> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Byte, UInt32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Byte, UInt32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Sum(this IEnumerable<Byte?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Byte, UInt32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Byte?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Byte, UInt32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Sum(this IEnumerable<Int16> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Int16, Int32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Int16, Int32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Sum(this IEnumerable<Int16?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Int16, Int32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int16?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Int16, Int32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Sum(this IEnumerable<UInt16> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt16, UInt32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt16, UInt32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Sum(this IEnumerable<UInt16?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt16, UInt32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt16?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt16, UInt32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Sum(this IEnumerable<Int32> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Sum(this IEnumerable<Int32?> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int32?> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Sum(this IEnumerable<UInt32> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt32, UInt32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt32, UInt32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Sum(this IEnumerable<UInt32?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt32, UInt32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt32?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt32, UInt32>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Sum(this IEnumerable<Int64> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Sum(this IEnumerable<Int64?> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int64?> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Sum(this IEnumerable<UInt64> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt64, UInt64>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt64, UInt64>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Sum(this IEnumerable<UInt64?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt64, UInt64>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt64?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt64, UInt64>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Sum(this IEnumerable<Int128> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Int128, Int128>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Int128, Int128>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Sum(this IEnumerable<Int128?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Int128, Int128>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Int128?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Int128, Int128>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Sum(this IEnumerable<UInt128> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt128, UInt128>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt128, UInt128>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Sum(this IEnumerable<UInt128?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UInt128, UInt128>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UInt128?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UInt128, UInt128>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Sum(this IEnumerable<Half> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Half, Half>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Half, Half>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Sum(this IEnumerable<Half?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<Half, Half>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Half?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, Half, Half>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Sum(this IEnumerable<Single> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Sum(this IEnumerable<Single?> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Single?> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Sum(this IEnumerable<Double> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Sum(this IEnumerable<Double?> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Double?> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Sum(this IEnumerable<NFloat> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<NFloat, NFloat>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, NFloat, NFloat>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Sum(this IEnumerable<NFloat?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<NFloat, NFloat>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static NFloat? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, NFloat?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, NFloat, NFloat>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Sum(this IEnumerable<Decimal> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Sum(this IEnumerable<Decimal?> source)
            => System.Linq.Enumerable.Sum(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Decimal?> selector)
            => System.Linq.Enumerable.Sum(source, selector);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Sum(this IEnumerable<IntPtr> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<IntPtr, IntPtr>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, IntPtr, IntPtr>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Sum(this IEnumerable<IntPtr?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<IntPtr, IntPtr>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IntPtr? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, IntPtr?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, IntPtr, IntPtr>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Sum(this IEnumerable<UIntPtr> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UIntPtr, UIntPtr>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UIntPtr, UIntPtr>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Sum(this IEnumerable<UIntPtr?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<UIntPtr, UIntPtr>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UIntPtr? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, UIntPtr?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, UIntPtr, UIntPtr>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Sum(this IEnumerable<BigInteger> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<BigInteger, BigInteger>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, BigInteger, BigInteger>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Sum(this IEnumerable<BigInteger?> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SumCore<BigInteger, BigInteger>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static BigInteger? Sum<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, BigInteger?> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.SumCore<ELEMENT_T, BigInteger, BigInteger>(selector);
        }

        #endregion
    }
}
