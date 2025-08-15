using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Experiment.CSharp.Library
{
    public class ExperimentAboutVector
    {
#if false // SumUInt32() の逆アセンブル結果
00007FF89D165080  mov         rax,15C033C6F40h  
00007FF89D16508A  vxorps      ymm0,ymm0,ymm0  
00007FF89D16508E  xor         ecx,ecx  
00007FF89D165090  mov         edx,80h  
00007FF89D165095  nop         word ptr [rax+rax]  
00007FF89D1650A0  vpaddd      ymm0,ymm0,ymmword ptr [rax+rcx]  
00007FF89D1650A5  vpaddd      ymm0,ymm0,ymmword ptr [rax+rcx+20h]  
00007FF89D1650AB  add         rcx,40h  
00007FF89D1650AF  add         rdx,0FFFFFFFFFFFFFFC0h  
00007FF89D1650B3  cmp         rdx,40h  
00007FF89D1650B7  jae         Experiment.CSharp.Library.ExperimentAboutVector.SumUInt32()+020h (07FF89D1650A0h)  
00007FF89D1650B9  cmp         rdx,20h  
00007FF89D1650BD  jb          Experiment.CSharp.Library.ExperimentAboutVector.SumUInt32()+04Ch (07FF89D1650CCh)  
00007FF89D1650BF  vpaddd      ymm0,ymm0,ymmword ptr [rax+rcx]  
00007FF89D1650C4  add         rcx,20h  
00007FF89D1650C8  add         rdx,0FFFFFFFFFFFFFFE0h  
00007FF89D1650CC  vmovaps     ymm1,ymm0  
00007FF89D1650D0  vextracti128 xmm0,ymm0,1  
00007FF89D1650D6  vpaddd      xmm0,xmm0,xmm1  
00007FF89D1650DA  vpsrldq     xmm1,xmm0,8  
00007FF89D1650DF  vpaddd      xmm0,xmm1,xmm0  
00007FF89D1650E3  vpsrldq     xmm1,xmm0,4  
00007FF89D1650E8  vpaddd      xmm0,xmm1,xmm0  
00007FF89D1650EC  vmovd       r8d,xmm0  
00007FF89D1650F1  test        rdx,rdx  
00007FF89D1650F4  je          Experiment.CSharp.Library.ExperimentAboutVector.SumUInt32()+08Eh (07FF89D16510Eh)  
00007FF89D1650F6  nop         word ptr [rax+rax]  
00007FF89D165100  add         r8d,dword ptr [rax+rcx]  
00007FF89D165104  add         rcx,4  
00007FF89D165108  add         rdx,0FFFFFFFFFFFFFFFCh  
00007FF89D16510C  jne         Experiment.CSharp.Library.ExperimentAboutVector.SumUInt32()+080h (07FF89D165100h)  
00007FF89D16510E  mov         eax,r8d  
00007FF89D165111  vzeroupper  
00007FF89D165114  ret  
#endif

#if false // SumDouble() の逆アセンブル結果
00007FF89D155120  mov         rax,26F344F6E40h  
00007FF89D15512A  vxorps      ymm0,ymm0,ymm0  
00007FF89D15512E  xor         ecx,ecx  
00007FF89D155130  mov         edx,100h  
00007FF89D155135  nop         word ptr [rax+rax]  
00007FF89D155140  vaddpd      ymm0,ymm0,ymmword ptr [rax+rcx]  
00007FF89D155145  vaddpd      ymm0,ymm0,ymmword ptr [rax+rcx+20h]  
00007FF89D15514B  add         rcx,40h  
00007FF89D15514F  add         rdx,0FFFFFFFFFFFFFFC0h  
00007FF89D155153  cmp         rdx,40h  
00007FF89D155157  jae         Experiment.CSharp.Library.ExperimentAboutVector.SumDouble()+020h (07FF89D155140h)  
00007FF89D155159  cmp         rdx,20h  
00007FF89D15515D  jb          Experiment.CSharp.Library.ExperimentAboutVector.SumDouble()+04Ch (07FF89D15516Ch)  
00007FF89D15515F  vaddpd      ymm0,ymm0,ymmword ptr [rax+rcx]  
00007FF89D155164  add         rcx,20h  
00007FF89D155168  add         rdx,0FFFFFFFFFFFFFFE0h  
00007FF89D15516C  vmovaps     ymm1,ymm0  
00007FF89D155170  vpermilpd   xmm2,xmm1,1  
00007FF89D155176  vaddpd      xmm1,xmm2,xmm1  
00007FF89D15517A  vextractf128 xmm0,ymm0,1  
00007FF89D155180  vpermilpd   xmm2,xmm0,1  
00007FF89D155186  vaddpd      xmm0,xmm2,xmm0  
00007FF89D15518A  vaddsd      xmm0,xmm1,xmm0  
00007FF89D15518E  test        rdx,rdx  
00007FF89D155191  je          Experiment.CSharp.Library.ExperimentAboutVector.SumDouble()+08Fh (07FF89D1551AFh)  
00007FF89D155193  nop         dword ptr [rax+rax]  
00007FF89D155198  nop         dword ptr [rax+rax]  
00007FF89D1551A0  vaddsd      xmm0,xmm0,mmword ptr [rax+rcx]  
00007FF89D1551A5  add         rcx,8  
00007FF89D1551A9  add         rdx,0FFFFFFFFFFFFFFF8h  
00007FF89D1551AD  jne         Experiment.CSharp.Library.ExperimentAboutVector.SumDouble()+080h (07FF89D1551A0h)  
00007FF89D1551AF  vzeroupper  
00007FF89D1551B2  ret  
#endif

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 SumUInt32() => Sum([0u, 1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u, 11u, 12u, 13u, 14u, 15u, 16u, 17u, 18u, 19u, 20u, 21u, 22u, 23u, 24u, 25u, 26u, 27u, 28u, 29u, 30u, 31u]);

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double SumDouble() => Sum([0d, 1d, 2d, 3d, 4d, 5d, 6d, 7d, 8d, 9d, 10d, 11d, 12d, 13d, 14d, 15d, 16d, 17d, 18d, 19d, 20d, 21d, 22d, 23d, 24d, 25d, 26d, 27d, 28d, 29d, 30d, 31d]);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T Sum<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> value)
            where ELEMENT_T : INumberBase<ELEMENT_T>
            => Sum(ref Unsafe.AsRef(in MemoryMarshal.GetReference(value)), (UInt32)value.Length * (UInt32)Unsafe.SizeOf<ELEMENT_T>());

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T Sum<ELEMENT_T>(ref ELEMENT_T array, UIntPtr byteLength)
            where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            if (Vector256.IsHardwareAccelerated == false)
                throw new NotSupportedException();
            if (Vector256<ELEMENT_T>.IsSupported == false)
                throw new NotSupportedException();
            var sumv = Vector256.Create(ELEMENT_T.Zero);
            var byteOffset = UIntPtr.Zero;
            var byteCount = byteLength;
            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)Vector256<ELEMENT_T>.Count * 2)
            {
                var sumv2 = sumv + Vector256.LoadUnsafe(ref Unsafe.AddByteOffset(ref array, byteOffset));
                sumv = sumv2 + Vector256.LoadUnsafe(ref Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)Vector256<ELEMENT_T>.Count));
                byteOffset += (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)Vector256<ELEMENT_T>.Count * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)Vector256<ELEMENT_T>.Count * 2u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)Vector256<ELEMENT_T>.Count)
            {
                sumv += Vector256.LoadUnsafe(ref Unsafe.AddByteOffset(ref array, byteOffset));
                byteOffset += (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)Vector256<ELEMENT_T>.Count;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)Vector256<ELEMENT_T>.Count;
            }

            var sum = Vector256.Sum(sumv);
            while (byteCount > 0)
            {
                sum += Unsafe.AddByteOffset(ref array, byteOffset);
                byteOffset += (UInt32)Unsafe.SizeOf<ELEMENT_T>();
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            }

            return sum;
        }
    }
}
