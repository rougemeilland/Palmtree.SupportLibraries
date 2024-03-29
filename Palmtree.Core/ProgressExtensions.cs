﻿using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static class ProgressExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IProgress<TO_VALUE_T> Cast<FROM_VALUE_T, TO_VALUE_T>(this IProgress<FROM_VALUE_T> progress, Func<TO_VALUE_T, FROM_VALUE_T> selector)
            => new SimpleProgress<TO_VALUE_T>(value => progress.Report(selector(value)));
    }
}
