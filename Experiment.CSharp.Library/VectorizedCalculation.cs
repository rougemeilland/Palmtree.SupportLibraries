using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Experiment.CSharp.Library
{
    public static partial class VectorizedCalculation
    {
#if false
.NET9.0 Max512<Single>()
00007FFA50076C70  vmovups     ymm0,ymmword ptr [rdx]  
00007FFA50076C74  vmovups     ymm1,ymmword ptr [r8]  
00007FFA50076C79  vcmpeqps    ymm2,ymm0,ymm1  
00007FFA50076C7E  vxorps      ymm3,ymm3,ymm3  
00007FFA50076C82  vpcmpgtd    ymm3,ymm3,ymm1  
00007FFA50076C86  vandps      ymm2,ymm3,ymm2  
00007FFA50076C8A  vcmpneqps   ymm3,ymm0,ymm0  
00007FFA50076C8F  vorps       ymm2,ymm3,ymm2  
00007FFA50076C93  vcmpltps    ymm3,ymm1,ymm0  
00007FFA50076C98  vorps       ymm2,ymm3,ymm2  
00007FFA50076C9C  vblendvps   ymm0,ymm1,ymm0,ymm2  
00007FFA50076CA2  vmovups     ymm1,ymmword ptr [rdx+20h]  
00007FFA50076CA7  vmovups     ymm2,ymmword ptr [r8+20h]  
00007FFA50076CAD  vcmpeqps    ymm3,ymm1,ymm2  
00007FFA50076CB2  vxorps      ymm4,ymm4,ymm4  
00007FFA50076CB6  vpcmpgtd    ymm4,ymm4,ymm2  
00007FFA50076CBA  vandps      ymm3,ymm4,ymm3  
00007FFA50076CBE  vcmpneqps   ymm4,ymm1,ymm1  
00007FFA50076CC3  vorps       ymm3,ymm4,ymm3  
00007FFA50076CC7  vcmpltps    ymm4,ymm2,ymm1  
00007FFA50076CCC  vorps       ymm3,ymm4,ymm3  
00007FFA50076CD0  vblendvps   ymm1,ymm2,ymm1,ymm3  
00007FFA50076CD6  vmovups     ymmword ptr [rcx],ymm0  
00007FFA50076CDA  vmovups     ymmword ptr [rcx+20h],ymm1  
00007FFA50076CDF  mov         rax,rcx  
00007FFA50076CE2  vzeroupper  
00007FFA50076CE5  ret  

.NET9.0 Max256<Single>()
00007FFA50076D70  vmovups     ymm0,ymmword ptr [rdx]  
00007FFA50076D74  vmovups     ymm1,ymmword ptr [r8]  
00007FFA50076D79  vcmpeqps    ymm2,ymm1,ymm0  
00007FFA50076D7E  vxorps      ymm3,ymm3,ymm3  
00007FFA50076D82  vpcmpgtd    ymm3,ymm3,ymm1  
00007FFA50076D86  vandps      ymm2,ymm3,ymm2  
00007FFA50076D8A  vcmpneqps   ymm3,ymm0,ymm0  
00007FFA50076D8F  vorps       ymm2,ymm3,ymm2  
00007FFA50076D93  vcmpltps    ymm3,ymm1,ymm0  
00007FFA50076D98  vorps       ymm2,ymm3,ymm2  
00007FFA50076D9C  vblendvps   ymm0,ymm1,ymm0,ymm2  
00007FFA50076DA2  vmovups     ymmword ptr [rcx],ymm0  
00007FFA50076DA6  mov         rax,rcx  
00007FFA50076DA9  vzeroupper  
00007FFA50076DAC  ret  

.NET9.0 Max128<Single>()
00007FFA50076F20  vmovups     xmm0,xmmword ptr [rdx]  
00007FFA50076F24  vmovups     xmm1,xmmword ptr [r8]  
00007FFA50076F29  vcmpeqps    xmm2,xmm1,xmm0  
00007FFA50076F2E  vxorps      xmm3,xmm3,xmm3  
00007FFA50076F32  vpcmpgtd    xmm3,xmm3,xmm1  
00007FFA50076F36  vandps      xmm2,xmm3,xmm2  
00007FFA50076F3A  vcmpneqps   xmm3,xmm0,xmm0  
00007FFA50076F3F  vorps       xmm2,xmm3,xmm2  
00007FFA50076F43  vcmpltps    xmm3,xmm1,xmm0  
00007FFA50076F48  vorps       xmm2,xmm3,xmm2  
00007FFA50076F4C  vblendvps   xmm0,xmm1,xmm0,xmm2  
00007FFA50076F52  vmovups     xmmword ptr [rcx],xmm0  
00007FFA50076F56  mov         rax,rcx  
00007FFA50076F59  ret  

.NET9.0 Max64<Single>()
00007FFA50077340  sub         rsp,18h  
00007FFA50077344  mov         qword ptr [rsp+8],rcx  
00007FFA50077349  mov         qword ptr [rsp],rdx  
00007FFA5007734D  vmovss      xmm0,dword ptr [rsp+8]  
00007FFA50077353  vmovss      xmm1,dword ptr [rsp]  
00007FFA50077358  vucomiss    xmm0,xmm1  
00007FFA5007735C  jp          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+02Ah (07FFA5007736Ah)  
00007FFA5007735E  jne         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+02Ah (07FFA5007736Ah)  
00007FFA50077360  vmovd       eax,xmm1  
00007FFA50077364  test        eax,eax  
00007FFA50077366  jl          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+036h (07FFA50077376h)  
00007FFA50077368  jmp         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+03Ah (07FFA5007737Ah)  
00007FFA5007736A  vucomiss    xmm0,xmm0  
00007FFA5007736E  jp          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+036h (07FFA50077376h)  
00007FFA50077370  vucomiss    xmm0,xmm1  
00007FFA50077374  jbe         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+028h (07FFA50077368h)  
00007FFA50077376  vmovaps     xmm1,xmm0  
00007FFA5007737A  vmovss      dword ptr [rsp+10h],xmm1  
00007FFA50077380  vmovss      xmm0,dword ptr [rsp+0Ch]  
00007FFA50077386  vmovss      xmm1,dword ptr [rsp+4]  
00007FFA5007738C  vucomiss    xmm0,xmm1  
00007FFA50077390  jp          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+05Eh (07FFA5007739Eh)  
00007FFA50077392  jne         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+05Eh (07FFA5007739Eh)  
00007FFA50077394  vmovd       eax,xmm1  
00007FFA50077398  test        eax,eax  
00007FFA5007739A  jl          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+06Ah (07FFA500773AAh)  
00007FFA5007739C  jmp         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+06Eh (07FFA500773AEh)  
00007FFA5007739E  vucomiss    xmm0,xmm0  
00007FFA500773A2  jp          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+06Ah (07FFA500773AAh)  
00007FFA500773A4  vucomiss    xmm0,xmm1  
00007FFA500773A8  jbe         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+05Ch (07FFA5007739Ch)  
00007FFA500773AA  vmovaps     xmm1,xmm0  
00007FFA500773AE  vmovss      dword ptr [rsp+14h],xmm1  
00007FFA500773B4  mov         rax,qword ptr [rsp+10h]  
00007FFA500773B9  add         rsp,18h  
00007FFA500773BD  ret  

.NET9.0 MaxNumber512<Single>()
00007FFA500780E0  vmovups     ymm0,ymmword ptr [rdx]  
00007FFA500780E4  vmovups     ymm1,ymmword ptr [r8]  
00007FFA500780E9  vcmpltps    ymm2,ymm1,ymm0  
00007FFA500780EE  vcmpneqps   ymm3,ymm1,ymm1  
00007FFA500780F3  vorps       ymm2,ymm3,ymm2  
00007FFA500780F7  vcmpeqps    ymm3,ymm1,ymm0  
00007FFA500780FC  vxorps      ymm4,ymm4,ymm4  
00007FFA50078100  vpcmpgtd    ymm4,ymm4,ymm1  
00007FFA50078104  vandps      ymm3,ymm4,ymm3  
00007FFA50078108  vorps       ymm2,ymm3,ymm2  
00007FFA5007810C  vblendvps   ymm0,ymm1,ymm0,ymm2  
00007FFA50078112  vmovups     ymm1,ymmword ptr [rdx+20h]  
00007FFA50078117  vmovups     ymm2,ymmword ptr [r8+20h]  
00007FFA5007811D  vcmpltps    ymm3,ymm2,ymm1  
00007FFA50078122  vcmpneqps   ymm4,ymm2,ymm2  
00007FFA50078127  vorps       ymm3,ymm4,ymm3  
00007FFA5007812B  vcmpeqps    ymm4,ymm2,ymm1  
00007FFA50078130  vxorps      ymm5,ymm5,ymm5  
00007FFA50078134  vpcmpgtd    ymm5,ymm5,ymm2  
00007FFA50078138  vandps      ymm4,ymm5,ymm4  
00007FFA5007813C  vorps       ymm3,ymm4,ymm3  
00007FFA50078140  vblendvps   ymm1,ymm2,ymm1,ymm3  
00007FFA50078146  vmovups     ymmword ptr [rcx],ymm0  
00007FFA5007814A  vmovups     ymmword ptr [rcx+20h],ymm1  
00007FFA5007814F  mov         rax,rcx  
00007FFA50078152  vzeroupper  
00007FFA50078155  ret  

.NET9.0 MaxNumber256<Single>()
00007FFA500788F0  vmovups     ymm0,ymmword ptr [r8]  
00007FFA500788F5  vmovups     ymm1,ymmword ptr [rdx]  
00007FFA500788F9  vcmpltps    ymm2,ymm0,ymm1  
00007FFA500788FE  vcmpneqps   ymm3,ymm0,ymm0  
00007FFA50078903  vorps       ymm2,ymm3,ymm2  
00007FFA50078907  vcmpeqps    ymm3,ymm0,ymm1  
00007FFA5007890C  vxorps      ymm4,ymm4,ymm4  
00007FFA50078910  vpcmpgtd    ymm4,ymm4,ymm0  
00007FFA50078914  vandps      ymm3,ymm4,ymm3  
00007FFA50078918  vorps       ymm2,ymm3,ymm2  
00007FFA5007891C  vblendvps   ymm0,ymm0,ymm1,ymm2  
00007FFA50078922  vmovups     ymmword ptr [rcx],ymm0  
00007FFA50078926  mov         rax,rcx  
00007FFA50078929  vzeroupper  
00007FFA5007892C  ret  

.NET9.0 MaxNumber128<Single>()
00007FFA50079950  vmovups     xmm0,xmmword ptr [r8]  
00007FFA50079955  vmovups     xmm1,xmmword ptr [rdx]  
00007FFA50079959  vcmpltps    xmm2,xmm0,xmm1  
00007FFA5007995E  vcmpneqps   xmm3,xmm0,xmm0  
00007FFA50079963  vorps       xmm2,xmm3,xmm2  
00007FFA50079967  vcmpeqps    xmm3,xmm0,xmm1  
00007FFA5007996C  vxorps      xmm4,xmm4,xmm4  
00007FFA50079970  vpcmpgtd    xmm4,xmm4,xmm0  
00007FFA50079974  vandps      xmm3,xmm4,xmm3  
00007FFA50079978  vorps       xmm2,xmm3,xmm2  
00007FFA5007997C  vblendvps   xmm0,xmm0,xmm1,xmm2  
00007FFA50079982  vmovups     xmmword ptr [rcx],xmm0  
00007FFA50079986  mov         rax,rcx  
00007FFA50079989  ret  

.NET9.0 MaxNumber64<Single>()
00007FFA5007A180  sub         rsp,38h  
00007FFA5007A184  mov         qword ptr [rsp+28h],rcx  
00007FFA5007A189  mov         qword ptr [rsp+20h],rdx  
00007FFA5007A18E  vmovss      xmm0,dword ptr [rsp+28h]  
00007FFA5007A194  vmovss      xmm1,dword ptr [rsp+20h]  
00007FFA5007A19A  call        qword ptr [CLRStub[MethodDescPrestub]@00007FFA504A4C30 (07FFA504A4C30h)]  
00007FFA5007A1A0  vmovss      dword ptr [rsp+30h],xmm0  
00007FFA5007A1A6  vmovss      xmm0,dword ptr [rsp+2Ch]  
00007FFA5007A1AC  vmovss      xmm1,dword ptr [rsp+24h]  
00007FFA5007A1B2  call        qword ptr [CLRStub[MethodDescPrestub]@00007FFA504A4C30 (07FFA504A4C30h)]  
00007FFA5007A1B8  vmovss      dword ptr [rsp+34h],xmm0  
00007FFA5007A1BE  mov         rax,qword ptr [rsp+30h]  
00007FFA5007A1C3  add         rsp,38h  
00007FFA5007A1C7  ret  


.NET8.0 Max512<Single>()
00007FFA4AF94FB0  vzeroupper  
00007FFA4AF94FB3  vmovups     ymm0,ymmword ptr [rdx]  
00007FFA4AF94FB7  vmaxps      ymm0,ymm0,ymmword ptr [r8]  
00007FFA4AF94FBC  vmovups     ymm1,ymmword ptr [rdx+20h]  
00007FFA4AF94FC1  vmaxps      ymm1,ymm1,ymmword ptr [r8+20h]  
00007FFA4AF94FC7  vmovups     ymmword ptr [rcx],ymm0  
00007FFA4AF94FCB  vmovups     ymmword ptr [rcx+20h],ymm1  
00007FFA4AF94FD0  mov         rax,rcx  
00007FFA4AF94FD3  vzeroupper  
00007FFA4AF94FD6  ret  

.NET8.0 Max256<Single>()
00007FFA4AF952E0  vzeroupper  
00007FFA4AF952E3  vmovups     ymm0,ymmword ptr [rdx]  
00007FFA4AF952E7  vmaxps      ymm0,ymm0,ymmword ptr [r8]  
00007FFA4AF952EC  vmovups     ymmword ptr [rcx],ymm0  
00007FFA4AF952F0  mov         rax,rcx  
00007FFA4AF952F3  vzeroupper  
00007FFA4AF952F6  ret  

.NET8.0 Max128<Single>()
00007FFA4AF95850  vzeroupper  
00007FFA4AF95853  vmovups     xmm0,xmmword ptr [rdx]  
00007FFA4AF95857  vmaxps      xmm0,xmm0,xmmword ptr [r8]  
00007FFA4AF9585C  vmovups     xmmword ptr [rcx],xmm0  
00007FFA4AF95860  mov         rax,rcx  
00007FFA4AF95863  ret  

.NET8.0 Max64<Single>()
00007FFA4AF95C60  sub         rsp,18h  
00007FFA4AF95C64  vzeroupper  
00007FFA4AF95C67  mov         qword ptr [rsp+8],rcx  
00007FFA4AF95C6C  mov         qword ptr [rsp],rdx  
00007FFA4AF95C70  vmovss      xmm0,dword ptr [rsp+8]  
00007FFA4AF95C76  vmovaps     xmm1,xmm0  
00007FFA4AF95C7A  vmovss      xmm2,dword ptr [rsp]  
00007FFA4AF95C7F  vucomiss    xmm1,xmm2  
00007FFA4AF95C83  ja          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+027h (07FFA4AF95C87h)  
00007FFA4AF95C85  jmp         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+02Bh (07FFA4AF95C8Bh)  
00007FFA4AF95C87  vmovaps     xmm2,xmm0  
00007FFA4AF95C8B  vmovss      dword ptr [rsp+10h],xmm2  
00007FFA4AF95C91  vmovss      xmm2,dword ptr [rsp+0Ch]  
00007FFA4AF95C97  vmovaps     xmm1,xmm2  
00007FFA4AF95C9B  vmovss      xmm0,dword ptr [rsp+4]  
00007FFA4AF95CA1  vucomiss    xmm1,xmm0  
00007FFA4AF95CA5  ja          Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+049h (07FFA4AF95CA9h)  
00007FFA4AF95CA7  jmp         Experiment.CSharp.Library.VectorizedCalculation.Max64[[System.Single, System.Private.CoreLib]](System.Runtime.Intrinsics.Vector64`1<Single>, System.Runtime.Intrinsics.Vector64`1<Single>)+04Dh (07FFA4AF95CADh)  
00007FFA4AF95CA9  vmovaps     xmm0,xmm2  
00007FFA4AF95CAD  vmovss      dword ptr [rsp+14h],xmm0  
00007FFA4AF95CB3  mov         rax,qword ptr [rsp+10h]  
00007FFA4AF95CB8  add         rsp,18h  
00007FFA4AF95CBC  ret  
#endif

        // ベクトル化した方がパフォーマンスの向上が見込めるメソッド:
        // - Max(ReadOnlySpan<Byte>)
        // - Max(ReadOnlySpan<Char>)
        // - Max(ReadOnlySpan<Double>)
        // - Max(ReadOnlySpan<Int16>)
        // - Max(ReadOnlySpan<Int32>)
        // - Max(ReadOnlySpan<Int64>)
        // - Max(ReadOnlySpan<SByte>)
        // - Max(ReadOnlySpan<Single>)
        // - Max(ReadOnlySpan<UInt16>)
        // - Max(ReadOnlySpan<UInt32>)
        // - Max(ReadOnlySpan<UInt64>)
        // - Min(ReadOnlySpan<Byte>)
        // - Min(ReadOnlySpan<Char>)
        // - Min(ReadOnlySpan<Double>)
        // - Min(ReadOnlySpan<Int16>)
        // - Min(ReadOnlySpan<Int32>)
        // - Min(ReadOnlySpan<Int64>)
        // - Min(ReadOnlySpan<SByte>)
        // - Min(ReadOnlySpan<Single>)
        // - Min(ReadOnlySpan<UInt16>)
        // - Min(ReadOnlySpan<UInt32>)
        // - MaxNumber(ReadOnlySpan<Byte>)
        // - MaxNumber(ReadOnlySpan<Char>)
        // - MaxNumber(ReadOnlySpan<Double>)
        // - MaxNumber(ReadOnlySpan<Int16>)
        // - MaxNumber(ReadOnlySpan<Int32>)
        // - MaxNumber(ReadOnlySpan<Int64>)
        // - MaxNumber(ReadOnlySpan<SByte>)
        // - MaxNumber(ReadOnlySpan<Single>)
        // - MaxNumber(ReadOnlySpan<Uint16>)
        // - MaxNumber(ReadOnlySpan<Uint32>)
        // - MaxNumber(ReadOnlySpan<Uint64>)
        // - MinNumber(ReadOnlySpan<Byte>)
        // - MinNumber(ReadOnlySpan<Char>)
        // - MinNumber(ReadOnlySpan<Double>)
        // - MinNumber(ReadOnlySpan<Int16>)
        // - MinNumber(ReadOnlySpan<Int32>)
        // - MinNumber(ReadOnlySpan<Int64>)
        // - MinNumber(ReadOnlySpan<SByte>)
        // - MinNumber(ReadOnlySpan<Single>)
        // - MinNumber(ReadOnlySpan<UInt16>)
        // - MinNumber(ReadOnlySpan<UInt32>)
        //
        // ベクトル化してもパフォーマンスの向上があまり見込めないかあるいは劣化が見込めるメソッド
        // - Min(ReadOnlySpan<UInt64>)
        // - MinNumber(ReadOnlySpan<UInt64>)
        //

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<ELEMENT_T> Max256<ELEMENT_T>(Vector256<ELEMENT_T> left, Vector256<ELEMENT_T> right) => Vector256.Max(left, right);

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<ELEMENT_T> MaxNumber256<ELEMENT_T>(Vector256<ELEMENT_T> left, Vector256<ELEMENT_T> right) => Vector256.MaxNumber(left, right);
#endif

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<ELEMENT_T> Min256<ELEMENT_T>(Vector256<ELEMENT_T> left, Vector256<ELEMENT_T> right) => Vector256.Min(left, right);

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<ELEMENT_T> MinNumber256<ELEMENT_T>(Vector256<ELEMENT_T> left, Vector256<ELEMENT_T> right) => Vector256.MinNumber(left, right);
#endif
        #region Max

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T NonVectorizedMax<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new ArgumentException($"\"{nameof(array)}\" must not be empty.", nameof(array));

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMax(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMax(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMax(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMax(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMax(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMax(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            return NonVectorizedMax(ref MemoryMarshal.GetReference(array), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T VectorizedMax<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMaxByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw new Exception();
            }
#endif

            return NonVectorizedMax(array);
        }

        #endregion

        #region Min

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T NonVectorizedMin<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new ArgumentException($"\"{nameof(array)}\" must not be empty.", nameof(array));

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMin(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMin(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMin(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMin(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMin(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMin(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            return NonVectorizedMin(ref MemoryMarshal.GetReference(array), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T VectorizedMin<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMinByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMinByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMinByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw new Exception();
            }
#endif

            return NonVectorizedMin(array);
        }

        #endregion

        #region MaxNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T NonVectorizedMaxNumber<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new ArgumentException($"\"{nameof(array)}\" must not be empty.", nameof(array));

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMaxNumber(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMaxNumber(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMaxNumber(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMaxNumber(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMaxNumber(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMaxNumber(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            return NonVectorizedMaxNumber(ref MemoryMarshal.GetReference(array), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T VectorizedMaxNumber<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMaxNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw new Exception();
            }
#endif

            return NonVectorizedMaxNumber(array);
        }

        #endregion

        #region MinNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T NonVectorizedMinNumber<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new ArgumentException($"\"{nameof(array)}\" must not be empty.", nameof(array));

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMinNumber(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(NonVectorizedMinNumber(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMinNumber(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(NonVectorizedMinNumber(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMinNumber(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(NonVectorizedMinNumber(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length));
            }

            return NonVectorizedMinNumber(ref MemoryMarshal.GetReference(array), (UInt32)Unsafe.SizeOf<ELEMENT_T>() * (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T VectorizedMinNumber<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)CalculateMinNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw new Exception();
            }
#endif
            return NonVectorizedMinNumber(array);
        }

        #endregion

        #region Sum

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T NonVectorizedSum<ELEMENT_T, RESULT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumberBase<ELEMENT_T>
            where RESULT_T : INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                checked
                {
                    sum += RESULT_T.CreateChecked(array[index]);
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T VectorizedSum<ELEMENT_T, RESULT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumberBase<ELEMENT_T>
            where RESULT_T : INumberBase<RESULT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Int32) && typeof(RESULT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForSignedIntegerByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, unchecked((Int32)(1U << 31)));
            if (typeof(ELEMENT_T) == typeof(Int64) && typeof(RESULT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForSignedIntegerByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, unchecked((Int64)(1UL << 63)));
            if (typeof(ELEMENT_T) == typeof(IntPtr) && typeof(RESULT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (RESULT_T)(Object)new IntPtr(CalculateSumForSignedIntegerByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, unchecked((Int32)(1U << 31))));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (RESULT_T)(Object)new IntPtr(CalculateSumForSignedIntegerByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, unchecked((Int64)(1UL << 63))));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(UInt32) && typeof(RESULT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForUnsignedIntegerByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, 1U << 31);
            if (typeof(ELEMENT_T) == typeof(UInt64) && typeof(RESULT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForUnsignedIntegerByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, 1UL << 63);
            if (typeof(ELEMENT_T) == typeof(UIntPtr) && typeof(RESULT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (RESULT_T)(Object)new UIntPtr(CalculateSumForUnsignedIntegerByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, 1U << 31));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (RESULT_T)(Object)new UIntPtr(CalculateSumForUnsignedIntegerByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length, 1UL << 63));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Single) && typeof(RESULT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && typeof(RESULT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat) && typeof(RESULT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (RESULT_T)(Object)(NFloat)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (RESULT_T)(Object)(NFloat)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw new Exception();
            }

            return NonVectorizedSum<ELEMENT_T, RESULT_T>(array);
        }

        #endregion

        #region SumNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T NonVectorizedSumNumber<ELEMENT_T, RESULT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumberBase<ELEMENT_T>
            where RESULT_T : INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var element = array[index];
                if (!ELEMENT_T.IsNaN(element))
                {
                    checked
                    {
                        sum += RESULT_T.CreateChecked(element);
                    }
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T VectorizedSumNumber<ELEMENT_T, RESULT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumberBase<ELEMENT_T>
            where RESULT_T : INumberBase<RESULT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Int32))
                return VectorizedSum<ELEMENT_T, RESULT_T>(array);
            if (typeof(ELEMENT_T) == typeof(Int64))
                return VectorizedSum<ELEMENT_T, RESULT_T>(array);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
                return VectorizedSum<ELEMENT_T, RESULT_T>(array);
            if (typeof(ELEMENT_T) == typeof(UInt32))
                return VectorizedSum<ELEMENT_T, RESULT_T>(array);
            if (typeof(ELEMENT_T) == typeof(UInt64))
                return VectorizedSum<ELEMENT_T, RESULT_T>(array);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
                return VectorizedSum<ELEMENT_T, RESULT_T>(array);

            if (typeof(ELEMENT_T) == typeof(Single) && typeof(RESULT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (RESULT_T)(Object)CalculateSumNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && typeof(RESULT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (RESULT_T)(Object)CalculateSumNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat) && typeof(RESULT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (RESULT_T)(Object)(NFloat)CalculateSumNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (RESULT_T)(Object)(NFloat)CalculateSumNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw new Exception();
            }

            return NonVectorizedSumNumber<ELEMENT_T, RESULT_T>(array);
        }

        #endregion

        #region UncheckedSum

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T NonVectorizedUncheckedSum<ELEMENT_T, RESULT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumberBase<ELEMENT_T>
            where RESULT_T : INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
                sum += RESULT_T.CreateChecked(array[index]);
            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T VectorizedUncheckedSum<ELEMENT_T, RESULT_T>(ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : INumberBase<ELEMENT_T>
            where RESULT_T : INumberBase<RESULT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Int32) && typeof(RESULT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (RESULT_T)(Object)CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && typeof(RESULT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (RESULT_T)(Object)CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr) && typeof(RESULT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (RESULT_T)(Object)new IntPtr(CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (RESULT_T)(Object)new IntPtr(CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(UInt32) && typeof(RESULT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (RESULT_T)(Object)CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && typeof(RESULT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (RESULT_T)(Object)CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr) && typeof(RESULT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (RESULT_T)(Object)new UIntPtr(CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (RESULT_T)(Object)new UIntPtr(CalculateUncheckedSumByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw new Exception();
            }

            if (typeof(ELEMENT_T) == typeof(Single) && typeof(RESULT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && typeof(RESULT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (RESULT_T)(Object)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat) && typeof(RESULT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (RESULT_T)(Object)(NFloat)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (RESULT_T)(Object)(NFloat)CalculateSumForIeee754FloatingNumberByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw new Exception();
            }

            return NonVectorizedUncheckedSum<ELEMENT_T, RESULT_T>(array);
        }

        #endregion

        #region private Max/Min/MaxNumber/MinNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMaxByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMaxByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMaxByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)CalculateMaxByNonVector(ref array, elementLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMinByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMinByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMinByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)CalculateMinByNonVector(ref array, elementLength);
        }

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxNumberByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMaxNumberByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)CalculateMaxNumberByNonVector(ref array, elementLength);
        }
#endif

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinNumberByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
                return (ELEMENT_T)(Object)CalculateMinNumberByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)CalculateMinNumberByNonVector(ref array, elementLength);
        }
#endif

        #endregion

        #region private Sum

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumForSignedIntegerByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
            where ELEMENT_T : ISignedNumber<ELEMENT_T>, IBinaryInteger<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector512<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector512<ELEMENT_T>.Count * (UInt32)Vector512<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfSignedIntegerByVector512(ref array, vectorizedElementLength, overflowTestMask);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return checked(sum1 + sum2);
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector256<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector256<ELEMENT_T>.Count * (UInt32)Vector256<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfSignedIntegerByVector256(ref array, vectorizedElementLength, overflowTestMask);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return checked(sum1 + sum2);
            }
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector128<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector128<ELEMENT_T>.Count * (UInt32)Vector128<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfSignedIntegerByVector128(ref array, vectorizedElementLength, overflowTestMask);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return checked(sum1 + sum2);
            }
            else
            {
                return CalculateSumByNonVector(ref array, UIntPtr.Zero, elementLength);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumForUnsignedIntegerByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
            where ELEMENT_T : IUnsignedNumber<ELEMENT_T>, IBinaryInteger<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector512<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector512<ELEMENT_T>.Count * (UInt32)Vector512<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfUnsignedIntegerByVector512(ref array, vectorizedElementLength, overflowTestMask);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return checked(sum1 + sum2);
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector256<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector256<ELEMENT_T>.Count * (UInt32)Vector256<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfUnsignedIntegerByVector256(ref array, vectorizedElementLength, overflowTestMask);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return checked(sum1 + sum2);
            }
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector128<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector128<ELEMENT_T>.Count * (UInt32)Vector128<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfUnsignedIntegerByVector128(ref array, vectorizedElementLength, overflowTestMask);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return checked(sum1 + sum2);
            }
            else
            {
                return CalculateSumByNonVector(ref array, UIntPtr.Zero, elementLength);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumForIeee754FloatingNumberByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : IFloatingPointIeee754<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector512<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector512<ELEMENT_T>.Count * (UInt32)Vector512<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfIeee754FloatingNumberByVector512(ref array, vectorizedElementLength);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return sum1 + sum2;
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector256<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector256<ELEMENT_T>.Count * (UInt32)Vector256<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfIeee754FloatingNumberByVector256(ref array, vectorizedElementLength);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return sum1 + sum2;
            }
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector128<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector128<ELEMENT_T>.Count * (UInt32)Vector128<ELEMENT_T>.Count);

                var sum1 = CalculateSumOfIeee754FloatingNumberByVector128(ref array, vectorizedElementLength);
                var sum2 = CalculateSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
                return sum1 + sum2;
            }
            else
            {
                return CalculateSumByNonVector(ref array, UIntPtr.Zero, elementLength);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementOffset, UIntPtr elementLength)
            where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            var sum = ELEMENT_T.Zero;
            var byteLength = elementLength * (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            for (var byteOffset = elementLength * (UInt32)Unsafe.SizeOf<ELEMENT_T>(); byteOffset < byteLength; byteOffset += (UInt32)Unsafe.SizeOf<ELEMENT_T>())
            {
                checked
                {
                    sum += Unsafe.AddByteOffset(ref array, byteOffset);
                }
            }

            return sum;
        }

        #endregion

        #region private SumNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumNumberByVector<ELEMENT_T>(ref ELEMENT_T array, UInt32 elementLength)
            where ELEMENT_T : IFloatingPointIeee754<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.Count > 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector<ELEMENT_T>.Count);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector512<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector512<ELEMENT_T>.Count * (UInt32)Vector512<ELEMENT_T>.Count);

                var sum = CalculateSumNumberOfIeee754FloatingNumberByVector512(ref array, vectorizedElementLength);
                return sum + CalculateSumNumberByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector256<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector256<ELEMENT_T>.Count * (UInt32)Vector256<ELEMENT_T>.Count);

                var sum = CalculateSumNumberOfIeee754FloatingNumberByVector256(ref array, vectorizedElementLength);
                return sum + CalculateSumNumberByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
            }
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector128<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector128<ELEMENT_T>.Count * (UInt32)Vector128<ELEMENT_T>.Count);

                var sum = CalculateSumNumberOfIeee754FloatingNumberByVector128(ref array, vectorizedElementLength);
                return sum + CalculateSumNumberByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
            }
            else
            {
                return CalculateSumNumberByNonVector(ref array, UIntPtr.Zero, elementLength);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumNumberByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr offset, UIntPtr elementLength)
            where ELEMENT_T : IFloatingPointIeee754<ELEMENT_T>
        {
            var sum = ELEMENT_T.Zero;
            var byteLength = elementLength * (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            for (var byteOffset = offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>(); byteOffset < byteLength; byteOffset += (UInt32)Unsafe.SizeOf<ELEMENT_T>())
            {
                var element = Unsafe.AddByteOffset(ref array, byteOffset);
                if (!ELEMENT_T.IsNaN(element))
                    sum += element;
            }

            return sum;
        }

        #endregion

        #region private UncheckedSum

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateUncheckedSumByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : IBinaryInteger<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.Count > 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector<ELEMENT_T>.Count);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && (UInt32)Vector512<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector512<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector512<ELEMENT_T>.Count * (UInt32)Vector512<ELEMENT_T>.Count);

                var sum = CalculateUncheckedSumByVector512(ref array, vectorizedElementLength);
                return sum + CalculateUncheckedSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && (UInt32)Vector256<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector256<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector256<ELEMENT_T>.Count * (UInt32)Vector256<ELEMENT_T>.Count);

                var sum = CalculateUncheckedSumByVector256(ref array, vectorizedElementLength);
                return sum + CalculateUncheckedSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
            }
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && (UInt32)Vector128<ELEMENT_T>.Count >= 2u && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                var vectorizedElementLength = elementLength & ~((UIntPtr)Vector128<ELEMENT_T>.Count - 1);
                System.Diagnostics.Debug.Assert(vectorizedElementLength == elementLength / (UInt32)Vector128<ELEMENT_T>.Count * (UInt32)Vector128<ELEMENT_T>.Count);

                var sum = CalculateUncheckedSumByVector128(ref array, vectorizedElementLength);
                return sum + CalculateUncheckedSumByNonVector(ref array, vectorizedElementLength, elementLength - vectorizedElementLength);
            }
            else
            {
                return CalculateUncheckedSumByNonVector(ref array, UIntPtr.Zero, elementLength);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateUncheckedSumByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementOffset, UIntPtr elementLength)
            where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            var sum = ELEMENT_T.Zero;
            var byteLength = elementLength * (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            for (var byteOffset = UIntPtr.Zero; byteOffset < byteLength; byteOffset += (UInt32)Unsafe.SizeOf<ELEMENT_T>())
                sum += Unsafe.AddByteOffset(ref array, byteOffset);

            return sum;
        }

        #endregion
    }
}
