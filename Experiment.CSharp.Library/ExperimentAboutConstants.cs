using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Experiment.CSharp.Library
{
    public static class ExperimentAboutConstants
    {
        #region 基本演算編

        // 0 を足す
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 PlusZero(UInt32 value) => value + 0;

        // 0 を引く
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 MinusZero(UInt32 value) => value - 0;

        // 0 をかける
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 MultiplyZero(UInt32 value) => value * 0;

        // 1 をかける
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 MultiplyOne(UInt32 value) => value * 1;

        // 1 で割る
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 DivideOne(UInt32 value) => value / 1;

        // 0 ビットだけ右シフトする
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 ShiftRightZero(UInt32 value) => value >> 0;

        // 0 ビットだけ左シフトする
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 ShiftLeftZero(UInt32 value) => value << 0;

        // 0 とビット単位の OR をとる
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 BitwiseOrAllZero(UInt32 value) => value | 0;

        // 0xffffffff とビット単位の OR をとる
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 BitwiseOrAllOne(UInt32 value) => value | UInt32.MaxValue;

        // 0 とビット単位の AND をとる
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 BitwiseAndAllZero(UInt32 value) => value & 0;

        // 0xffffffff とビット単位の AND をとる
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 BitwiseAndAllOne(UInt32 value) => value & UInt32.MaxValue;

        #endregion

        #region sizeof() 関連

        // sizeof(Int32) は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetSizeOfInt32() => sizeof(Int32);

        // sizeof(Int32) * 8 は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetBitCountOfInt32() => sizeof(Int32) * 8;

        // Unsafe.SizeOf<Int32>() は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetUnsafeSizeOfInt32() => Unsafe.SizeOf<Int32>();

        // Unsafe.SizeOf<Int32>() * 8 は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetUnsafeBitCountOfInt32() => Unsafe.SizeOf<Int32>() * 8;

        // Unsafe.SizeOf<UIntPtr>() は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetUnsafeSizeOfUIntPtr() => Unsafe.SizeOf<UIntPtr>();

        // Unsafe.SizeOf<UIntPtr>() * 8 は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetUnsafeBitCountOfUIntPtr() => Unsafe.SizeOf<UIntPtr>() * 8;

        // Unsafe.SizeOf<NFloat>() は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetUnsafeSizeOfNFloat() => Unsafe.SizeOf<NFloat>();

        // Unsafe.SizeOf<NFloat>() * 8 は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetUnsafeBitCountOfNFloat() => Unsafe.SizeOf<NFloat>() * 8;

        #endregion

        #region 実行環境編

        // OperatingSystem.IsWindows() は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsWindows() => OperatingSystem.IsWindows();

        // OperatingSystem.IsLinux() は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsLinux() => OperatingSystem.IsLinux();

        // Environment.Is64BitProcess は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean Is64BitProcess() => Environment.Is64BitProcess;

        #endregion

        #region Vector 編

        // VectorXXX.IsHardwareAccelerated は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean GetVector512IsHardwareAccelerated() => Vector512.IsHardwareAccelerated;

        // VectorXXX.IsHardwareAccelerated は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean GetVector256IsHardwareAccelerated() => Vector256.IsHardwareAccelerated;

        // Vector.IsHardwareAccelerated は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean GetVectorIsHardwareAccelerated() => Vector.IsHardwareAccelerated;

        // VectorXXX<ELEMENT_T>.IsSupported は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean GetVector256IsSupported<ELEMENT_T>() => Vector256<ELEMENT_T>.IsSupported;

        // Vector<ELEMENT_T>.IsSupported は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean GetVectorIsSupported<ELEMENT_T>() => Vector<ELEMENT_T>.IsSupported;

        // Vector256<ELEMENT_T>.Count は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetVector256Count<ELEMENT_T>() => Vector256<ELEMENT_T>.Count;

        // Vector<ELEMENT_T>.Count は実行時に定数扱いされるかどうか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Int32 GetVectorCount<ELEMENT_T>() => Vector<ELEMENT_T>.Count;

        #endregion

        #region 応用編

        // 4つの Byte 値から Uint32 値を組み立てる。
        // 冗長な演算は省略されるか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UInt32 MakeUInt32(Byte b0, Byte b1, Byte b2, Byte b3)
            => ((UInt32)b0 << (0 * 8))
                | ((UInt32)b1 << (1 * 8))
                | ((UInt32)b2 << (2 * 8))
                | ((UInt32)b3 << (3 * 8));

        // Windows かつ 64bit かどうかを調べる。
        // 復帰値はどこまで最適化されるか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsWindows64bit() => OperatingSystem.IsWindows() && Environment.Is64BitProcess;

        // Vector256<ELEMENT_T> を積極的に使用すべきかどうかを調べる。
        // 復帰値はどこまで最適化されるか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsBetterToUseVector256<ELEMENT_T>() => Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && Vector256<ELEMENT_T>.Count >= 8;

        // Vector256<ELEMENT_T> を積極的に使用すべきかどうかを調べる。
        // 復帰値はどこまで最適化されるか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsBetterToUseVector256<ELEMENT_T>(Int32 minimumCount) => Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && Vector256<ELEMENT_T>.Count >= minimumCount;

        // Vector<ELEMENT_T> を積極的に使用すべきかどうかを調べる。
        // 復帰値はどこまで最適化されるか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsBetterToUseVector<ELEMENT_T>() => Vector256.IsHardwareAccelerated && Vector<ELEMENT_T>.IsSupported && Vector<ELEMENT_T>.Count >= 8;

        // Vector<ELEMENT_T> を積極的に使用すべきかどうかを調べる。
        // 復帰値はどこまで最適化されるか ?
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsBetterToUseVector<ELEMENT_T>(Int32 minimumCount) => Vector256.IsHardwareAccelerated && Vector<ELEMENT_T>.IsSupported && Vector<ELEMENT_T>.Count >= minimumCount;

        #endregion
    }
}

