using System;
using System.Runtime.CompilerServices;

namespace Experiment.CSharp.Library
{
    public static class ExperimentAboutNan
    {
#if false
IsNaN: ; Double.IsNaN(value)
    vucomisd    xmm0,xmm0  
    setp        al  ; set if value is NaN
    movzx       eax,al  
    ret  

Equals: ; left.Equals(right)
    vucomisd    xmm1,xmm0  
    jp          LABEL_1 ; goto LABEL_1 if if NaN
    je          RETURN_TRUE ; goto RETURN_TRUE if left == right
LABEL_1; ; here NaN or left != right
    vucomisd    xmm1,xmm1  
    jp          LABEL_2 ; goto LABEL_2 if right is NaN
    jne         LABEL_2 ; goto LABEL_2 if if right != right (**This branch instruction may be useless.**)
    ; here NaN and (right is not NaN)
    xor         eax,eax ; return false
    jmp         RETURN
LABEL_2: ; here right is NaN or right != right
    vucomisd    xmm0,xmm0  
    setp        al ; set if left is Nan
    movzx       eax,al  
    jmp         RETURN ; return (left is NaN)
RETURN_TRUE:
    mov         eax,1 ; return true
RETURN:
    ret  

OperatorEqual: left == right
    vucomisd    xmm0,xmm1  
    setnp       al ; set if not NaN
    jp          RETURN ; return false if NaN
    ; here not NaN
    sete        al ; set if left == right
RETURN:
    movzx       eax,al  
    ret  

GreaterThan: left > right
    vucomisd    xmm0,xmm1  
    seta        al ; return (left > right)
    movzx       eax,al  
    ret  

GreaterThanOrNaN: !(left <= right)
    vucomisd    xmm1,xmm0  
    setb        al ; return (left > right or NaN)
    movzx       eax,al  
    ret  


; ** Flags affected by "vucomisd" **
;
; | Comparison results | ZF  | PF  | CF  |
; |:-------------------|:---:|:---:|:---:|
; | UNORDERED (NaN)    |  1  |  1  |  1  |
; | GREATER_THAN       |  0  |  0  |  0  |
; | LESS_THAN          |  0  |  0  |  1  |
; | EQUAL              |  1  |  0  |  0  |
#endif

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsNaN(Double value) => Double.IsNaN(value);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean Equals(Double left, Double right) => left.Equals(right);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean OperatorEqual(Double left, Double right) => left == right;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean GreaterThan(Double left, Double right) => left > right;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean GreaterThanOrNaN(Double left, Double right) => !(left <= right);
    }
}
