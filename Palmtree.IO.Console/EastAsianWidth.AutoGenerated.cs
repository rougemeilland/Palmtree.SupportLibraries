using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Palmtree.IO.Console
{
    internal partial class EastAsianWidth
    {
        private static readonly HashSet<String> _eastAsianCultureNames;

        static EastAsianWidth()
        {
            var eastAsianCultureNames =
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(culture => culture.Name == "ja" || culture.Name.StartsWith("ja-", StringComparison.OrdinalIgnoreCase) || culture.Name == "ko" || culture.Name.StartsWith("ko-", StringComparison.OrdinalIgnoreCase) || culture.Name == "vi" || culture.Name.StartsWith("vi-", StringComparison.OrdinalIgnoreCase) || culture.Name == "zh" || culture.Name.StartsWith("zh-", StringComparison.OrdinalIgnoreCase))
                 .Select(culture => culture.Name);
            _eastAsianCultureNames = [.. eastAsianCultureNames];
#if DEBUG
            DoTest();
#endif
        }

        private static EastAsianWidthType GetWidthType(Int32 codePoint)
        {
            if (codePoint < 0x0003fffe)
            {
                if (codePoint < 0x00020000)
                {
                    if (codePoint < 0x0000fffe)
                    {
                        if (codePoint < 0x0000a48d)
                        {
                            if (codePoint < 0x00003250)
                            {
                                if (codePoint < 0x00002010)
                                {
                                    if (codePoint < 0x00001100)
                                    {
                                        if (codePoint < 0x00000452)
                                        {
                                            if (codePoint < 0x00000251)
                                            {
                                                if (codePoint < 0x00000128)
                                                {
                                                    if (codePoint < 0x000000a1)
                                                    {
                                                        if (codePoint < 0x0000007f)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x00000000 and <= 0x0000007e);
#endif
                                                            return codePoint < 0x00000020 ? EastAsianWidthType.N : EastAsianWidthType.Na;
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0000007f and <= 0x000000a0);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x000000e6)
                                                        {
                                                            if (codePoint < 0x000000c6)
                                                            {
                                                                if (codePoint < 0x000000b5)
                                                                {
                                                                    if (codePoint < 0x000000ac)
                                                                    {
                                                                        if (codePoint < 0x000000a7)
                                                                        {
                                                                            if (codePoint < 0x000000a5)
                                                                            {
                                                                                if (codePoint < 0x000000a4)
                                                                                {
#if DEBUG
                                                                                    Validation.Assert(codePoint is >= 0x000000a1 and <= 0x000000a3);
#endif
                                                                                    return codePoint < 0x000000a2 ? EastAsianWidthType.A : EastAsianWidthType.Na;
                                                                                }
                                                                                else
                                                                                {
#if DEBUG
                                                                                    Validation.Assert(codePoint == 0x000000a4);
#endif
                                                                                    return EastAsianWidthType.A;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x000000a5 or 0x000000a6);
#endif
                                                                                return EastAsianWidthType.Na;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (codePoint < 0x000000aa)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x000000a7 and <= 0x000000a9);
#endif
                                                                                return codePoint < 0x000000a9 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x000000aa or 0x000000ab);
#endif
                                                                                return codePoint < 0x000000ab ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000000b0)
                                                                        {
                                                                            if (codePoint < 0x000000af)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x000000ac and <= 0x000000ae);
#endif
                                                                                return codePoint < 0x000000ad ? EastAsianWidthType.Na : EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x000000af);
#endif
                                                                                return EastAsianWidthType.Na;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000000b0 and <= 0x000000b4);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000000bc)
                                                                    {
                                                                        if (codePoint < 0x000000bb)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000000b5 and <= 0x000000ba);
#endif
                                                                            return codePoint < 0x000000b6 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x000000bb);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000000bc and <= 0x000000c5);
#endif
                                                                        return codePoint < 0x000000c0 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000000d7)
                                                                {
                                                                    if (codePoint < 0x000000d0)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000000c6 and <= 0x000000cf);
#endif
                                                                        return codePoint < 0x000000c7 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000000d0 and <= 0x000000d6);
#endif
                                                                        return codePoint < 0x000000d1 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000000de)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000000d7 and <= 0x000000dd);
#endif
                                                                        return codePoint < 0x000000d9 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000000de and <= 0x000000e5);
#endif
                                                                        return codePoint < 0x000000e2 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00000102)
                                                            {
                                                                if (codePoint < 0x000000f4)
                                                                {
                                                                    if (codePoint < 0x000000ee)
                                                                    {
                                                                        if (codePoint < 0x000000eb)
                                                                        {
                                                                            if (codePoint < 0x000000e8)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x000000e6 or 0x000000e7);
#endif
                                                                                return codePoint < 0x000000e7 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x000000e8 and <= 0x000000ea);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000000eb and <= 0x000000ed);
#endif
                                                                            return codePoint < 0x000000ec ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000000f2)
                                                                        {
                                                                            if (codePoint < 0x000000f1)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x000000ee and <= 0x000000f0);
#endif
                                                                                return codePoint < 0x000000f0 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x000000f1);
#endif
                                                                                return EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000000f2 or 0x000000f3);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000000fc)
                                                                    {
                                                                        if (codePoint < 0x000000f7)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000000f4 and <= 0x000000f6);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000000f7 and <= 0x000000fb);
#endif
                                                                            return codePoint < 0x000000fb ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000000ff)
                                                                        {
                                                                            if (codePoint < 0x000000fe)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x000000fc or 0x000000fd);
#endif
                                                                                return codePoint < 0x000000fd ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x000000fe);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000000ff and <= 0x00000101);
#endif
                                                                            return codePoint < 0x00000101 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x00000114)
                                                                {
                                                                    if (codePoint < 0x00000111)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00000102 and <= 0x00000110);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00000113)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x00000111 or 0x00000112);
#endif
                                                                            return codePoint < 0x00000112 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x00000113);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0000011c)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00000114 and <= 0x0000011b);
#endif
                                                                        return codePoint < 0x0000011b ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000011c and <= 0x00000127);
#endif
                                                                        return codePoint < 0x00000126 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x000001ce)
                                                    {
                                                        if (codePoint < 0x0000016c)
                                                        {
                                                            if (codePoint < 0x0000014c)
                                                            {
                                                                if (codePoint < 0x00000139)
                                                                {
                                                                    if (codePoint < 0x00000131)
                                                                    {
                                                                        if (codePoint < 0x0000012c)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00000128 and <= 0x0000012b);
#endif
                                                                            return codePoint < 0x0000012b ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000012c and <= 0x00000130);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00000134)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00000131 and <= 0x00000133);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00000134 and <= 0x00000138);
#endif
                                                                            return codePoint < 0x00000138 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00000143)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00000139 and <= 0x00000142);
#endif
                                                                        return codePoint < 0x0000013f ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00000148)
                                                                        {
                                                                            if (codePoint < 0x00000145)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00000143 or 0x00000144);
#endif
                                                                                return codePoint < 0x00000144 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00000145 and <= 0x00000147);
#endif
                                                                                return EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00000148 and <= 0x0000014b);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x00000154)
                                                                {
                                                                    if (codePoint < 0x00000152)
                                                                    {
                                                                        if (codePoint < 0x0000014e)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x0000014c or 0x0000014d);
#endif
                                                                            return codePoint < 0x0000014d ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000014e and <= 0x00000151);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is 0x00000152 or 0x00000153);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00000166)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00000154 and <= 0x00000165);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000016b)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00000166 and <= 0x0000016a);
#endif
                                                                            return codePoint < 0x00000168 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x0000016b);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0000016c and <= 0x000001cd);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x000001dd)
                                                        {
                                                            if (codePoint < 0x000001d6)
                                                            {
                                                                if (codePoint < 0x000001d3)
                                                                {
                                                                    if (codePoint < 0x000001d1)
                                                                    {
                                                                        if (codePoint < 0x000001d0)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000001ce or 0x000001cf);
#endif
                                                                            return codePoint < 0x000001cf ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x000001d0);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is 0x000001d1 or 0x000001d2);
#endif
                                                                        return codePoint < 0x000001d2 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000001d5)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is 0x000001d3 or 0x000001d4);
#endif
                                                                        return codePoint < 0x000001d4 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint == 0x000001d5);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000001da)
                                                                {
                                                                    if (codePoint < 0x000001d9)
                                                                    {
                                                                        if (codePoint < 0x000001d8)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000001d6 or 0x000001d7);
#endif
                                                                            return codePoint < 0x000001d7 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x000001d8);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint == 0x000001d9);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000001dc)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is 0x000001da or 0x000001db);
#endif
                                                                        return codePoint < 0x000001db ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint == 0x000001dc);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x000001dd and <= 0x00000250);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x00000370)
                                                {
                                                    if (codePoint < 0x000002e0)
                                                    {
                                                        if (codePoint < 0x000002c4)
                                                        {
                                                            if (codePoint < 0x00000262)
                                                            {
                                                                if (codePoint < 0x00000261)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x00000251 and <= 0x00000260);
#endif
                                                                    return codePoint < 0x00000252 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint == 0x00000261);
#endif
                                                                    return EastAsianWidthType.A;
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00000262 and <= 0x000002c3);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x000002d1)
                                                            {
                                                                if (codePoint < 0x000002cc)
                                                                {
                                                                    if (codePoint < 0x000002c9)
                                                                    {
                                                                        if (codePoint < 0x000002c7)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000002c4 and <= 0x000002c6);
#endif
                                                                            return codePoint < 0x000002c5 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000002c7 or 0x000002c8);
#endif
                                                                            return codePoint < 0x000002c8 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000002c9 and <= 0x000002cb);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000002ce)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is 0x000002cc or 0x000002cd);
#endif
                                                                        return codePoint < 0x000002cd ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000002ce and <= 0x000002d0);
#endif
                                                                        return codePoint < 0x000002d0 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000002d8)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000002d1 and <= 0x000002d7);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000002dd)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000002d8 and <= 0x000002dc);
#endif
                                                                        return codePoint < 0x000002dc ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000002df)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000002dd or 0x000002de);
#endif
                                                                            return codePoint < 0x000002de ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x000002df);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x000002e0 and <= 0x0000036f);
#endif
                                                        return codePoint < 0x00000300 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x000003ca)
                                                    {
                                                        if (codePoint < 0x000003a2)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x00000370 and <= 0x000003a1);
#endif
                                                            return codePoint < 0x00000391 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x000003b1)
                                                            {
                                                                if (codePoint < 0x000003aa)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000003a2 and <= 0x000003a9);
#endif
                                                                    return codePoint < 0x000003a3 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000003aa and <= 0x000003b0);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000003c2)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000003b1 and <= 0x000003c1);
#endif
                                                                    return EastAsianWidthType.A;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000003c2 and <= 0x000003c9);
#endif
                                                                    return codePoint < 0x000003c3 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x00000410)
                                                        {
                                                            if (codePoint < 0x00000401)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x000003ca and <= 0x00000400);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00000401 and <= 0x0000040f);
#endif
                                                                return codePoint < 0x00000402 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00000450)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00000410 and <= 0x0000044f);
#endif
                                                                return EastAsianWidthType.A;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is 0x00000450 or 0x00000451);
#endif
                                                                return codePoint < 0x00000451 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x00000452 and <= 0x000010ff);
#endif
                                            return EastAsianWidthType.N;
                                        }
                                    }
                                    else
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x00001100 and <= 0x0000200f);
#endif
                                        return codePoint < 0x00001160 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                    }
                                }
                                else
                                {
                                    if (codePoint < 0x00002985)
                                    {
                                        if (codePoint < 0x000024ea)
                                        {
                                            if (codePoint < 0x00002282)
                                            {
                                                if (codePoint < 0x00002153)
                                                {
                                                    if (codePoint < 0x000020ad)
                                                    {
                                                        if (codePoint < 0x00002074)
                                                        {
                                                            if (codePoint < 0x0000203f)
                                                            {
                                                                if (codePoint < 0x00002028)
                                                                {
                                                                    if (codePoint < 0x0000201c)
                                                                    {
                                                                        if (codePoint < 0x00002017)
                                                                        {
                                                                            if (codePoint < 0x00002013)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002010 and <= 0x00002012);
#endif
                                                                                return codePoint < 0x00002011 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002013 and <= 0x00002016);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (codePoint < 0x0000201a)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002017 and <= 0x00002019);
#endif
                                                                                return codePoint < 0x00002018 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x0000201a or 0x0000201b);
#endif
                                                                                return EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00002023)
                                                                        {
                                                                            if (codePoint < 0x00002020)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x0000201c and <= 0x0000201f);
#endif
                                                                                return codePoint < 0x0000201e ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002020 and <= 0x00002022);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002023 and <= 0x00002027);
#endif
                                                                            return codePoint < 0x00002024 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002034)
                                                                    {
                                                                        if (codePoint < 0x00002030)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002028 and <= 0x0000202f);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (codePoint < 0x00002032)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002030 or 0x00002031);
#endif
                                                                                return codePoint < 0x00002031 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002032 or 0x00002033);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000203b)
                                                                        {
                                                                            if (codePoint < 0x00002036)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002034 or 0x00002035);
#endif
                                                                                return codePoint < 0x00002035 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002036 and <= 0x0000203a);
#endif
                                                                                return EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (codePoint < 0x0000203e)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x0000203b and <= 0x0000203d);
#endif
                                                                                return codePoint < 0x0000203c ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x0000203e);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000203f and <= 0x00002073);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00002085)
                                                            {
                                                                if (codePoint < 0x0000207f)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x00002074 and <= 0x0000207e);
#endif
                                                                    return codePoint < 0x00002075 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002081)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is 0x0000207f or 0x00002080);
#endif
                                                                        return codePoint < 0x00002080 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002081 and <= 0x00002084);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000020a9)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x00002085 and <= 0x000020a8);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000020ac)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000020a9 and <= 0x000020ab);
#endif
                                                                        return codePoint < 0x000020aa ? EastAsianWidthType.H : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint == 0x000020ac);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x00002103)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x000020ad and <= 0x00002102);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0000212c)
                                                            {
                                                                if (codePoint < 0x00002117)
                                                                {
                                                                    if (codePoint < 0x0000210a)
                                                                    {
                                                                        if (codePoint < 0x00002106)
                                                                        {
                                                                            if (codePoint < 0x00002105)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002103 or 0x00002104);
#endif
                                                                                return codePoint < 0x00002104 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x00002105);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002106 and <= 0x00002109);
#endif
                                                                            return codePoint < 0x00002109 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00002113)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000210a and <= 0x00002112);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (codePoint < 0x00002116)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002113 and <= 0x00002115);
#endif
                                                                                return codePoint < 0x00002114 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x00002116);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002121)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002117 and <= 0x00002120);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00002127)
                                                                        {
                                                                            if (codePoint < 0x00002126)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002121 and <= 0x00002125);
#endif
                                                                                return codePoint < 0x00002123 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x00002126);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002127 and <= 0x0000212b);
#endif
                                                                            return codePoint < 0x0000212b ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000212c and <= 0x00002152);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x000021e8)
                                                    {
                                                        if (codePoint < 0x0000219a)
                                                        {
                                                            if (codePoint < 0x0000217a)
                                                            {
                                                                if (codePoint < 0x0000216c)
                                                                {
                                                                    if (codePoint < 0x00002160)
                                                                    {
                                                                        if (codePoint < 0x0000215b)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002153 and <= 0x0000215a);
#endif
                                                                            return codePoint < 0x00002155 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000215b and <= 0x0000215f);
#endif
                                                                            return codePoint < 0x0000215f ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002160 and <= 0x0000216b);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000216c and <= 0x00002179);
#endif
                                                                    return codePoint < 0x00002170 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0000218a)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000217a and <= 0x00002189);
#endif
                                                                    return codePoint < 0x00002189 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000218a and <= 0x00002199);
#endif
                                                                    return codePoint < 0x00002190 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x000021ba)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000219a and <= 0x000021b9);
#endif
                                                                return codePoint < 0x000021b8 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000021d2)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000021ba and <= 0x000021d1);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000021d5)
                                                                    {
                                                                        if (codePoint < 0x000021d4)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000021d2 or 0x000021d3);
#endif
                                                                            return codePoint < 0x000021d3 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x000021d4);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000021d5 and <= 0x000021e7);
#endif
                                                                        return codePoint < 0x000021e7 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x00002234)
                                                        {
                                                            if (codePoint < 0x0000220f)
                                                            {
                                                                if (codePoint < 0x00002200)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000021e8 and <= 0x000021ff);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002207)
                                                                    {
                                                                        if (codePoint < 0x00002204)
                                                                        {
                                                                            if (codePoint < 0x00002202)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002200 or 0x00002201);
#endif
                                                                                return codePoint < 0x00002201 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002202 or 0x00002203);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002204 and <= 0x00002206);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000220c)
                                                                        {
                                                                            if (codePoint < 0x00002209)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002207 or 0x00002208);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002209 and <= 0x0000220b);
#endif
                                                                                return codePoint < 0x0000220b ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000220c and <= 0x0000220e);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x00002221)
                                                                {
                                                                    if (codePoint < 0x0000221a)
                                                                    {
                                                                        if (codePoint < 0x00002215)
                                                                        {
                                                                            if (codePoint < 0x00002212)
                                                                            {
                                                                                if (codePoint < 0x00002211)
                                                                                {
#if DEBUG
                                                                                    Validation.Assert(codePoint is 0x0000220f or 0x00002210);
#endif
                                                                                    return codePoint < 0x00002210 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                                }
                                                                                else
                                                                                {
#if DEBUG
                                                                                    Validation.Assert(codePoint == 0x00002211);
#endif
                                                                                    return EastAsianWidthType.A;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002212 and <= 0x00002214);
#endif
                                                                                return EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002215 and <= 0x00002219);
#endif
                                                                            return codePoint < 0x00002216 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000221d)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000221a and <= 0x0000221c);
#endif
                                                                            return codePoint < 0x0000221b ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000221d and <= 0x00002220);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0000222d)
                                                                    {
                                                                        if (codePoint < 0x00002227)
                                                                        {
                                                                            if (codePoint < 0x00002225)
                                                                            {
                                                                                if (codePoint < 0x00002224)
                                                                                {
#if DEBUG
                                                                                    Validation.Assert(codePoint is >= 0x00002221 and <= 0x00002223);
#endif
                                                                                    return codePoint < 0x00002223 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                                }
                                                                                else
                                                                                {
#if DEBUG
                                                                                    Validation.Assert(codePoint == 0x00002224);
#endif
                                                                                    return EastAsianWidthType.N;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002225 or 0x00002226);
#endif
                                                                                return codePoint < 0x00002226 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002227 and <= 0x0000222c);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000222f)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x0000222d or 0x0000222e);
#endif
                                                                            return codePoint < 0x0000222e ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000222f and <= 0x00002233);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00002260)
                                                            {
                                                                if (codePoint < 0x0000224c)
                                                                {
                                                                    if (codePoint < 0x0000223e)
                                                                    {
                                                                        if (codePoint < 0x00002238)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002234 and <= 0x00002237);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002238 and <= 0x0000223d);
#endif
                                                                            return codePoint < 0x0000223c ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00002248)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000223e and <= 0x00002247);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002248 and <= 0x0000224b);
#endif
                                                                            return codePoint < 0x00002249 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002253)
                                                                    {
                                                                        if (codePoint < 0x00002252)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000224c and <= 0x00002251);
#endif
                                                                            return codePoint < 0x0000224d ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x00002252);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002253 and <= 0x0000225f);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x00002270)
                                                                {
                                                                    if (codePoint < 0x00002268)
                                                                    {
                                                                        if (codePoint < 0x00002264)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002260 and <= 0x00002263);
#endif
                                                                            return codePoint < 0x00002262 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002264 and <= 0x00002267);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000226c)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002268 and <= 0x0000226b);
#endif
                                                                            return codePoint < 0x0000226a ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000226c and <= 0x0000226f);
#endif
                                                                            return codePoint < 0x0000226e ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x00002270 and <= 0x00002281);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x000023e9)
                                                {
                                                    if (codePoint < 0x0000232b)
                                                    {
                                                        if (codePoint < 0x000022c0)
                                                        {
                                                            if (codePoint < 0x000022a5)
                                                            {
                                                                if (codePoint < 0x00002295)
                                                                {
                                                                    if (codePoint < 0x00002288)
                                                                    {
                                                                        if (codePoint < 0x00002286)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002282 and <= 0x00002285);
#endif
                                                                            return codePoint < 0x00002284 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x00002286 or 0x00002287);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002288 and <= 0x00002294);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0000229a)
                                                                    {
                                                                        if (codePoint < 0x00002299)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002295 and <= 0x00002298);
#endif
                                                                            return codePoint < 0x00002296 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x00002299);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000229a and <= 0x000022a4);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000022bf)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000022a5 and <= 0x000022be);
#endif
                                                                    return codePoint < 0x000022a6 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint == 0x000022bf);
#endif
                                                                    return EastAsianWidthType.A;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00002312)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x000022c0 and <= 0x00002311);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0000231c)
                                                                {
                                                                    if (codePoint < 0x0000231a)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002312 and <= 0x00002319);
#endif
                                                                        return codePoint < 0x00002313 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is 0x0000231a or 0x0000231b);
#endif
                                                                        return EastAsianWidthType.W;
                                                                    }
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000231c and <= 0x0000232a);
#endif
                                                                    return codePoint < 0x00002329 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0000232b and <= 0x000023e8);
#endif
                                                        return EastAsianWidthType.N;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x00002460)
                                                    {
                                                        if (codePoint < 0x000023f4)
                                                        {
                                                            if (codePoint < 0x000023f0)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x000023e9 and <= 0x000023ef);
#endif
                                                                return codePoint < 0x000023ed ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000023f3)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000023f0 and <= 0x000023f2);
#endif
                                                                    return codePoint < 0x000023f1 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint == 0x000023f3);
#endif
                                                                    return EastAsianWidthType.W;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x000023f4 and <= 0x0000245f);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x00002460 and <= 0x000024e9);
#endif
                                                        return EastAsianWidthType.A;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (codePoint < 0x0000273d)
                                            {
                                                if (codePoint < 0x00002614)
                                                {
                                                    if (codePoint < 0x00002580)
                                                    {
                                                        if (codePoint < 0x0000254c)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x000024ea and <= 0x0000254b);
#endif
                                                            return codePoint < 0x000024eb ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00002574)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000254c and <= 0x00002573);
#endif
                                                                return codePoint < 0x00002550 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00002574 and <= 0x0000257f);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x000025cb)
                                                        {
                                                            if (codePoint < 0x000025a3)
                                                            {
                                                                if (codePoint < 0x00002592)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x00002580 and <= 0x00002591);
#endif
                                                                    return codePoint < 0x00002590 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002596)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002592 and <= 0x00002595);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000025a0)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002596 and <= 0x0000259f);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025a0 and <= 0x000025a2);
#endif
                                                                            return codePoint < 0x000025a2 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000025b8)
                                                                {
                                                                    if (codePoint < 0x000025aa)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000025a3 and <= 0x000025a9);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000025b2)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025aa and <= 0x000025b1);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (codePoint < 0x000025b6)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x000025b2 and <= 0x000025b5);
#endif
                                                                                return codePoint < 0x000025b4 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x000025b6 or 0x000025b7);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000025c2)
                                                                    {
                                                                        if (codePoint < 0x000025be)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025b8 and <= 0x000025bd);
#endif
                                                                            return codePoint < 0x000025bc ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025be and <= 0x000025c1);
#endif
                                                                            return codePoint < 0x000025c0 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000025c6)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025c2 and <= 0x000025c5);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025c6 and <= 0x000025ca);
#endif
                                                                            return codePoint < 0x000025c9 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x000025f0)
                                                            {
                                                                if (codePoint < 0x000025e2)
                                                                {
                                                                    if (codePoint < 0x000025d2)
                                                                    {
                                                                        if (codePoint < 0x000025ce)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025cb and <= 0x000025cd);
#endif
                                                                            return codePoint < 0x000025cc ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000025ce and <= 0x000025d1);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000025d2 and <= 0x000025e1);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000025e6)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000025e2 and <= 0x000025e5);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000025e6 and <= 0x000025ef);
#endif
                                                                        return codePoint < 0x000025ef ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x00002605)
                                                                {
                                                                    if (codePoint < 0x000025fd)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000025f0 and <= 0x000025fc);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000025fd and <= 0x00002604);
#endif
                                                                        return codePoint < 0x000025ff ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0000260e)
                                                                    {
                                                                        if (codePoint < 0x0000260a)
                                                                        {
                                                                            if (codePoint < 0x00002607)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x00002605 or 0x00002606);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x00002607 and <= 0x00002609);
#endif
                                                                                return codePoint < 0x00002609 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000260a and <= 0x0000260d);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000260e and <= 0x00002613);
#endif
                                                                        return codePoint < 0x00002610 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x000026aa)
                                                    {
                                                        if (codePoint < 0x00002660)
                                                        {
                                                            if (codePoint < 0x00002638)
                                                            {
                                                                if (codePoint < 0x0000261f)
                                                                {
                                                                    if (codePoint < 0x0000261c)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002614 and <= 0x0000261b);
#endif
                                                                        return codePoint < 0x00002616 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000261e)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x0000261c or 0x0000261d);
#endif
                                                                            return codePoint < 0x0000261d ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x0000261e);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000261f and <= 0x00002637);
#endif
                                                                    return codePoint < 0x00002630 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x00002648)
                                                                {
                                                                    if (codePoint < 0x00002641)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002638 and <= 0x00002640);
#endif
                                                                        return codePoint < 0x00002640 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00002643)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x00002641 or 0x00002642);
#endif
                                                                            return codePoint < 0x00002642 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002643 and <= 0x00002647);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x00002648 and <= 0x0000265f);
#endif
                                                                    return codePoint < 0x00002654 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0000268a)
                                                            {
                                                                if (codePoint < 0x00002670)
                                                                {
                                                                    if (codePoint < 0x00002667)
                                                                    {
                                                                        if (codePoint < 0x00002663)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002660 and <= 0x00002662);
#endif
                                                                            return codePoint < 0x00002662 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002663 and <= 0x00002666);
#endif
                                                                            return codePoint < 0x00002666 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000266c)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002667 and <= 0x0000266b);
#endif
                                                                            return codePoint < 0x0000266b ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (codePoint < 0x0000266f)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x0000266c and <= 0x0000266e);
#endif
                                                                                return codePoint < 0x0000266e ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x0000266f);
#endif
                                                                                return EastAsianWidthType.A;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0000267f)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002670 and <= 0x0000267e);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000267f and <= 0x00002689);
#endif
                                                                        return codePoint < 0x00002680 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0000269e)
                                                                {
                                                                    if (codePoint < 0x00002694)
                                                                    {
                                                                        if (codePoint < 0x00002690)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000268a and <= 0x0000268f);
#endif
                                                                            return EastAsianWidthType.W;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002690 and <= 0x00002693);
#endif
                                                                            return codePoint < 0x00002693 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002694 and <= 0x0000269d);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000026a2)
                                                                    {
                                                                        if (codePoint < 0x000026a1)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000269e and <= 0x000026a0);
#endif
                                                                            return codePoint < 0x000026a0 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint == 0x000026a1);
#endif
                                                                            return EastAsianWidthType.W;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000026a2 and <= 0x000026a9);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x000026f4)
                                                        {
                                                            if (codePoint < 0x000026cf)
                                                            {
                                                                if (codePoint < 0x000026bd)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000026aa and <= 0x000026bc);
#endif
                                                                    return codePoint < 0x000026ac ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000026c6)
                                                                    {
                                                                        if (codePoint < 0x000026c0)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000026bd and <= 0x000026bf);
#endif
                                                                            return codePoint < 0x000026bf ? EastAsianWidthType.W : EastAsianWidthType.A;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000026c0 and <= 0x000026c5);
#endif
                                                                            return codePoint < 0x000026c4 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000026c6 and <= 0x000026ce);
#endif
                                                                        return codePoint < 0x000026ce ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000026e2)
                                                                {
                                                                    if (codePoint < 0x000026d5)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000026cf and <= 0x000026d4);
#endif
                                                                        return codePoint < 0x000026d4 ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000026d5 and <= 0x000026e1);
#endif
                                                                        return EastAsianWidthType.A;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x000026eb)
                                                                    {
                                                                        if (codePoint < 0x000026e8)
                                                                        {
                                                                            if (codePoint < 0x000026e4)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x000026e2 or 0x000026e3);
#endif
                                                                                return codePoint < 0x000026e3 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is >= 0x000026e4 and <= 0x000026e7);
#endif
                                                                                return EastAsianWidthType.N;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000026e8 and <= 0x000026ea);
#endif
                                                                            return codePoint < 0x000026ea ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x000026eb and <= 0x000026f3);
#endif
                                                                        return codePoint < 0x000026f2 ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0000270c)
                                                            {
                                                                if (codePoint < 0x00002700)
                                                                {
                                                                    if (codePoint < 0x000026fb)
                                                                    {
                                                                        if (codePoint < 0x000026f6)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000026f4 or 0x000026f5);
#endif
                                                                            return codePoint < 0x000026f5 ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000026f6 and <= 0x000026fa);
#endif
                                                                            return codePoint < 0x000026fa ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x000026fe)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x000026fb and <= 0x000026fd);
#endif
                                                                            return codePoint < 0x000026fd ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x000026fe or 0x000026ff);
#endif
                                                                            return EastAsianWidthType.A;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002706)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002700 and <= 0x00002705);
#endif
                                                                        return codePoint < 0x00002705 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x00002706 and <= 0x0000270b);
#endif
                                                                        return codePoint < 0x0000270a ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x00002728)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000270c and <= 0x00002727);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x00002728 and <= 0x0000273c);
#endif
                                                                    return codePoint < 0x00002729 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x000027ee)
                                                {
                                                    if (codePoint < 0x00002795)
                                                    {
                                                        if (codePoint < 0x00002776)
                                                        {
                                                            if (codePoint < 0x00002758)
                                                            {
                                                                if (codePoint < 0x0000274c)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000273d and <= 0x0000274b);
#endif
                                                                    return codePoint < 0x0000273e ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x00002753)
                                                                    {
                                                                        if (codePoint < 0x0000274f)
                                                                        {
                                                                            if (codePoint < 0x0000274e)
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint is 0x0000274c or 0x0000274d);
#endif
                                                                                return codePoint < 0x0000274d ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                            }
                                                                            else
                                                                            {
#if DEBUG
                                                                                Validation.Assert(codePoint == 0x0000274e);
#endif
                                                                                return EastAsianWidthType.W;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000274f and <= 0x00002752);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x00002756)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x00002753 and <= 0x00002755);
#endif
                                                                            return EastAsianWidthType.W;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is 0x00002756 or 0x00002757);
#endif
                                                                            return codePoint < 0x00002757 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00002758 and <= 0x00002775);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x00002776 and <= 0x00002794);
#endif
                                                            return codePoint < 0x00002780 ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x000027c0)
                                                        {
                                                            if (codePoint < 0x000027b0)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00002795 and <= 0x000027af);
#endif
                                                                return codePoint < 0x00002798 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x000027bf)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x000027b0 and <= 0x000027be);
#endif
                                                                    return codePoint < 0x000027b1 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint == 0x000027bf);
#endif
                                                                    return EastAsianWidthType.W;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x000027c0 and <= 0x000027ed);
#endif
                                                            return codePoint < 0x000027e6 ? EastAsianWidthType.N : EastAsianWidthType.Na;
                                                        }
                                                    }
                                                }
                                                else
                                                {
#if DEBUG
                                                    Validation.Assert(codePoint is >= 0x000027ee and <= 0x00002984);
#endif
                                                    return EastAsianWidthType.N;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (codePoint < 0x00002e80)
                                        {
                                            if (codePoint < 0x00002b5a)
                                            {
                                                if (codePoint < 0x00002b1b)
                                                {
#if DEBUG
                                                    Validation.Assert(codePoint is >= 0x00002985 and <= 0x00002b1a);
#endif
                                                    return codePoint < 0x00002987 ? EastAsianWidthType.Na : EastAsianWidthType.N;
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x00002b50)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x00002b1b and <= 0x00002b4f);
#endif
                                                        return codePoint < 0x00002b1d ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x00002b56)
                                                        {
                                                            if (codePoint < 0x00002b55)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00002b50 and <= 0x00002b54);
#endif
                                                                return codePoint < 0x00002b51 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint == 0x00002b55);
#endif
                                                                return EastAsianWidthType.W;
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x00002b56 and <= 0x00002b59);
#endif
                                                            return EastAsianWidthType.A;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
#if DEBUG
                                                Validation.Assert(codePoint is >= 0x00002b5a and <= 0x00002e7f);
#endif
                                                return EastAsianWidthType.N;
                                            }
                                        }
                                        else
                                        {
                                            if (codePoint < 0x00003041)
                                            {
                                                if (codePoint < 0x00002f00)
                                                {
                                                    if (codePoint < 0x00002e9b)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x00002e80 and <= 0x00002e9a);
#endif
                                                        return codePoint < 0x00002e9a ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x00002e9b and <= 0x00002eff);
#endif
                                                        return codePoint < 0x00002ef4 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x00002fd6)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x00002f00 and <= 0x00002fd5);
#endif
                                                        return EastAsianWidthType.W;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x00003001)
                                                        {
                                                            if (codePoint < 0x00002ff0)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00002fd6 and <= 0x00002fef);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00002ff0 and <= 0x00003000);
#endif
                                                                return codePoint < 0x00003000 ? EastAsianWidthType.W : EastAsianWidthType.F;
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x00003001 and <= 0x00003040);
#endif
                                                            return codePoint < 0x0000303f ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x00003131)
                                                {
                                                    if (codePoint < 0x00003099)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x00003041 and <= 0x00003098);
#endif
                                                        return codePoint < 0x00003097 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x00003100)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x00003099 and <= 0x000030ff);
#endif
                                                            return EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00003105)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00003100 and <= 0x00003104);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00003105 and <= 0x00003130);
#endif
                                                                return codePoint < 0x00003130 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x000031e6)
                                                    {
                                                        if (codePoint < 0x0000318f)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x00003131 and <= 0x0000318e);
#endif
                                                            return EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0000318f and <= 0x000031e5);
#endif
                                                            return codePoint < 0x00003190 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0000321f)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x000031e6 and <= 0x0000321e);
#endif
                                                            return codePoint < 0x000031ef ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x00003248)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000321f and <= 0x00003247);
#endif
                                                                return codePoint < 0x00003220 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x00003248 and <= 0x0000324f);
#endif
                                                                return EastAsianWidthType.A;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
#if DEBUG
                                Validation.Assert(codePoint is >= 0x00003250 and <= 0x0000a48c);
#endif
                                return EastAsianWidthType.W;
                            }
                        }
                        else
                        {
                            if (codePoint < 0x0000d7a4)
                            {
                                if (codePoint < 0x0000ac00)
                                {
                                    if (codePoint < 0x0000a960)
                                    {
                                        if (codePoint < 0x0000a4c7)
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x0000a48d and <= 0x0000a4c6);
#endif
                                            return codePoint < 0x0000a490 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                        }
                                        else
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x0000a4c7 and <= 0x0000a95f);
#endif
                                            return EastAsianWidthType.N;
                                        }
                                    }
                                    else
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x0000a960 and <= 0x0000abff);
#endif
                                        return codePoint < 0x0000a97d ? EastAsianWidthType.W : EastAsianWidthType.N;
                                    }
                                }
                                else
                                {
#if DEBUG
                                    Validation.Assert(codePoint is >= 0x0000ac00 and <= 0x0000d7a3);
#endif
                                    return EastAsianWidthType.W;
                                }
                            }
                            else
                            {
                                if (codePoint < 0x0000e000)
                                {
#if DEBUG
                                    Validation.Assert(codePoint is >= 0x0000d7a4 and <= 0x0000dfff);
#endif
                                    return EastAsianWidthType.N;
                                }
                                else
                                {
                                    if (codePoint < 0x0000f900)
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x0000e000 and <= 0x0000f8ff);
#endif
                                        return EastAsianWidthType.A;
                                    }
                                    else
                                    {
                                        if (codePoint < 0x0000fb00)
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x0000f900 and <= 0x0000faff);
#endif
                                            return EastAsianWidthType.W;
                                        }
                                        else
                                        {
                                            if (codePoint < 0x0000fe00)
                                            {
#if DEBUG
                                                Validation.Assert(codePoint is >= 0x0000fb00 and <= 0x0000fdff);
#endif
                                                return EastAsianWidthType.N;
                                            }
                                            else
                                            {
                                                if (codePoint < 0x0000ff01)
                                                {
                                                    if (codePoint < 0x0000fe6c)
                                                    {
                                                        if (codePoint < 0x0000fe30)
                                                        {
                                                            if (codePoint < 0x0000fe1a)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000fe00 and <= 0x0000fe19);
#endif
                                                                return codePoint < 0x0000fe10 ? EastAsianWidthType.A : EastAsianWidthType.W;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000fe1a and <= 0x0000fe2f);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0000fe53)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0000fe30 and <= 0x0000fe52);
#endif
                                                                return EastAsianWidthType.W;
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0000fe67)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000fe53 and <= 0x0000fe66);
#endif
                                                                    return codePoint < 0x0000fe54 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000fe67 and <= 0x0000fe6b);
#endif
                                                                    return codePoint < 0x0000fe68 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0000fe6c and <= 0x0000ff00);
#endif
                                                        return EastAsianWidthType.N;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0000ff61)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0000ff01 and <= 0x0000ff60);
#endif
                                                        return EastAsianWidthType.F;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0000ffbf)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0000ff61 and <= 0x0000ffbe);
#endif
                                                            return EastAsianWidthType.H;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0000ffe0)
                                                            {
                                                                if (codePoint < 0x0000ffd0)
                                                                {
                                                                    if (codePoint < 0x0000ffc8)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000ffbf and <= 0x0000ffc7);
#endif
                                                                        return codePoint < 0x0000ffc2 ? EastAsianWidthType.N : EastAsianWidthType.H;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000ffc8 and <= 0x0000ffcf);
#endif
                                                                        return codePoint < 0x0000ffca ? EastAsianWidthType.N : EastAsianWidthType.H;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0000ffd8)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000ffd0 and <= 0x0000ffd7);
#endif
                                                                        return codePoint < 0x0000ffd2 ? EastAsianWidthType.N : EastAsianWidthType.H;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (codePoint < 0x0000ffdd)
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000ffd8 and <= 0x0000ffdc);
#endif
                                                                            return codePoint < 0x0000ffda ? EastAsianWidthType.N : EastAsianWidthType.H;
                                                                        }
                                                                        else
                                                                        {
#if DEBUG
                                                                            Validation.Assert(codePoint is >= 0x0000ffdd and <= 0x0000ffdf);
#endif
                                                                            return EastAsianWidthType.N;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0000ffef)
                                                                {
                                                                    if (codePoint < 0x0000ffe8)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000ffe0 and <= 0x0000ffe7);
#endif
                                                                        return codePoint < 0x0000ffe7 ? EastAsianWidthType.F : EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0000ffe8 and <= 0x0000ffee);
#endif
                                                                        return EastAsianWidthType.H;
                                                                    }
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0000ffef and <= 0x0000fffd);
#endif
                                                                    return codePoint < 0x0000fffd ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (codePoint < 0x000187f8)
                        {
                            if (codePoint < 0x00016fe0)
                            {
#if DEBUG
                                Validation.Assert(codePoint is >= 0x0000fffe and <= 0x00016fdf);
#endif
                                return EastAsianWidthType.N;
                            }
                            else
                            {
                                if (codePoint < 0x00017000)
                                {
                                    if (codePoint < 0x00016ff0)
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x00016fe0 and <= 0x00016fef);
#endif
                                        return codePoint < 0x00016fe5 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                    }
                                    else
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x00016ff0 and <= 0x00016fff);
#endif
                                        return codePoint < 0x00016ff2 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                    }
                                }
                                else
                                {
#if DEBUG
                                    Validation.Assert(codePoint is >= 0x00017000 and <= 0x000187f7);
#endif
                                    return EastAsianWidthType.W;
                                }
                            }
                        }
                        else
                        {
                            if (codePoint < 0x0001d300)
                            {
                                if (codePoint < 0x0001aff0)
                                {
                                    if (codePoint < 0x00018d09)
                                    {
                                        if (codePoint < 0x00018cd6)
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x000187f8 and <= 0x00018cd5);
#endif
                                            return codePoint < 0x00018800 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                        }
                                        else
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x00018cd6 and <= 0x00018d08);
#endif
                                            return codePoint < 0x00018cff ? EastAsianWidthType.N : EastAsianWidthType.W;
                                        }
                                    }
                                    else
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x00018d09 and <= 0x0001afef);
#endif
                                        return EastAsianWidthType.N;
                                    }
                                }
                                else
                                {
                                    if (codePoint < 0x0001b2fc)
                                    {
                                        if (codePoint < 0x0001b170)
                                        {
                                            if (codePoint < 0x0001b123)
                                            {
                                                if (codePoint < 0x0001b000)
                                                {
                                                    if (codePoint < 0x0001affc)
                                                    {
                                                        if (codePoint < 0x0001aff5)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001aff0 and <= 0x0001aff4);
#endif
                                                            return codePoint < 0x0001aff4 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001aff5 and <= 0x0001affb);
#endif
                                                            return EastAsianWidthType.W;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001afff)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001affc and <= 0x0001affe);
#endif
                                                            return codePoint < 0x0001affd ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint == 0x0001afff);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                }
                                                else
                                                {
#if DEBUG
                                                    Validation.Assert(codePoint is >= 0x0001b000 and <= 0x0001b122);
#endif
                                                    return EastAsianWidthType.W;
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x0001b150)
                                                {
                                                    if (codePoint < 0x0001b133)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001b123 and <= 0x0001b132);
#endif
                                                        return codePoint < 0x0001b132 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001b133 and <= 0x0001b14f);
#endif
                                                        return EastAsianWidthType.N;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0001b164)
                                                    {
                                                        if (codePoint < 0x0001b156)
                                                        {
                                                            if (codePoint < 0x0001b153)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001b150 and <= 0x0001b152);
#endif
                                                                return EastAsianWidthType.W;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001b153 and <= 0x0001b155);
#endif
                                                                return codePoint < 0x0001b155 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001b156 and <= 0x0001b163);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001b164 and <= 0x0001b16f);
#endif
                                                        return codePoint < 0x0001b168 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x0001b170 and <= 0x0001b2fb);
#endif
                                            return EastAsianWidthType.W;
                                        }
                                    }
                                    else
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x0001b2fc and <= 0x0001d2ff);
#endif
                                        return EastAsianWidthType.N;
                                    }
                                }
                            }
                            else
                            {
                                if (codePoint < 0x0001f004)
                                {
                                    if (codePoint < 0x0001d377)
                                    {
                                        if (codePoint < 0x0001d357)
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x0001d300 and <= 0x0001d356);
#endif
                                            return EastAsianWidthType.W;
                                        }
                                        else
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x0001d357 and <= 0x0001d376);
#endif
                                            return codePoint < 0x0001d360 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                        }
                                    }
                                    else
                                    {
#if DEBUG
                                        Validation.Assert(codePoint is >= 0x0001d377 and <= 0x0001f003);
#endif
                                        return EastAsianWidthType.N;
                                    }
                                }
                                else
                                {
                                    if (codePoint < 0x0001f7f1)
                                    {
                                        if (codePoint < 0x0001f3f8)
                                        {
                                            if (codePoint < 0x0001f200)
                                            {
                                                if (codePoint < 0x0001f100)
                                                {
                                                    if (codePoint < 0x0001f0cf)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001f004 and <= 0x0001f0ce);
#endif
                                                        return codePoint < 0x0001f005 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001f0cf and <= 0x0001f0ff);
#endif
                                                        return codePoint < 0x0001f0d0 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0001f18e)
                                                    {
                                                        if (codePoint < 0x0001f130)
                                                        {
                                                            if (codePoint < 0x0001f110)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f100 and <= 0x0001f10f);
#endif
                                                                return codePoint < 0x0001f10b ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f110 and <= 0x0001f12f);
#endif
                                                                return codePoint < 0x0001f12e ? EastAsianWidthType.A : EastAsianWidthType.N;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0001f16a)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f130 and <= 0x0001f169);
#endif
                                                                return EastAsianWidthType.A;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f16a and <= 0x0001f18d);
#endif
                                                                return codePoint < 0x0001f170 ? EastAsianWidthType.N : EastAsianWidthType.A;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001f1ad)
                                                        {
                                                            if (codePoint < 0x0001f19b)
                                                            {
                                                                if (codePoint < 0x0001f191)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f18e and <= 0x0001f190);
#endif
                                                                    return codePoint < 0x0001f18f ? EastAsianWidthType.W : EastAsianWidthType.A;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f191 and <= 0x0001f19a);
#endif
                                                                    return EastAsianWidthType.W;
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f19b and <= 0x0001f1ac);
#endif
                                                                return EastAsianWidthType.A;
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f1ad and <= 0x0001f1ff);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x0001f300)
                                                {
                                                    if (codePoint < 0x0001f266)
                                                    {
                                                        if (codePoint < 0x0001f23c)
                                                        {
                                                            if (codePoint < 0x0001f210)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f200 and <= 0x0001f20f);
#endif
                                                                return codePoint < 0x0001f203 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f210 and <= 0x0001f23b);
#endif
                                                                return EastAsianWidthType.W;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0001f252)
                                                            {
                                                                if (codePoint < 0x0001f249)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f23c and <= 0x0001f248);
#endif
                                                                    return codePoint < 0x0001f240 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f249 and <= 0x0001f251);
#endif
                                                                    return codePoint < 0x0001f250 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f252 and <= 0x0001f265);
#endif
                                                                return codePoint < 0x0001f260 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001f266 and <= 0x0001f2ff);
#endif
                                                        return EastAsianWidthType.N;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0001f37d)
                                                    {
                                                        if (codePoint < 0x0001f337)
                                                        {
                                                            if (codePoint < 0x0001f321)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f300 and <= 0x0001f320);
#endif
                                                                return EastAsianWidthType.W;
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0001f32d)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f321 and <= 0x0001f32c);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f32d and <= 0x0001f336);
#endif
                                                                    return codePoint < 0x0001f336 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f337 and <= 0x0001f37c);
#endif
                                                            return EastAsianWidthType.W;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001f3cb)
                                                        {
                                                            if (codePoint < 0x0001f3a0)
                                                            {
                                                                if (codePoint < 0x0001f394)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f37d and <= 0x0001f393);
#endif
                                                                    return codePoint < 0x0001f37e ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f394 and <= 0x0001f39f);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f3a0 and <= 0x0001f3ca);
#endif
                                                                return EastAsianWidthType.W;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0001f3e0)
                                                            {
                                                                if (codePoint < 0x0001f3d4)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f3cb and <= 0x0001f3d3);
#endif
                                                                    return codePoint < 0x0001f3cf ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f3d4 and <= 0x0001f3df);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0001f3f1)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f3e0 and <= 0x0001f3f0);
#endif
                                                                    return EastAsianWidthType.W;
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0001f3f5)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f3f1 and <= 0x0001f3f4);
#endif
                                                                        return codePoint < 0x0001f3f4 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f3f5 and <= 0x0001f3f7);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (codePoint < 0x0001f5fb)
                                            {
                                                if (codePoint < 0x0001f4fd)
                                                {
                                                    if (codePoint < 0x0001f442)
                                                    {
                                                        if (codePoint < 0x0001f43f)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f3f8 and <= 0x0001f43e);
#endif
                                                            return EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0001f441)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is 0x0001f43f or 0x0001f440);
#endif
                                                                return codePoint < 0x0001f440 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint == 0x0001f441);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001f442 and <= 0x0001f4fc);
#endif
                                                        return EastAsianWidthType.W;
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0001f57b)
                                                    {
                                                        if (codePoint < 0x0001f53e)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f4fd and <= 0x0001f53d);
#endif
                                                            return codePoint < 0x0001f4ff ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0001f568)
                                                            {
                                                                if (codePoint < 0x0001f550)
                                                                {
                                                                    if (codePoint < 0x0001f54b)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f53e and <= 0x0001f54a);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f54b and <= 0x0001f54f);
#endif
                                                                        return codePoint < 0x0001f54f ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                    }
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f550 and <= 0x0001f567);
#endif
                                                                    return EastAsianWidthType.W;
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f568 and <= 0x0001f57a);
#endif
                                                                return codePoint < 0x0001f57a ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001f5a5)
                                                        {
                                                            if (codePoint < 0x0001f595)
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001f57b and <= 0x0001f594);
#endif
                                                                return EastAsianWidthType.N;
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0001f5a4)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f595 and <= 0x0001f5a3);
#endif
                                                                    return codePoint < 0x0001f597 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint == 0x0001f5a4);
#endif
                                                                    return EastAsianWidthType.W;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f5a5 and <= 0x0001f5fa);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x0001f6f4)
                                                {
                                                    if (codePoint < 0x0001f680)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001f5fb and <= 0x0001f67f);
#endif
                                                        return codePoint < 0x0001f650 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001f6c6)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f680 and <= 0x0001f6c5);
#endif
                                                            return EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0001f6dc)
                                                            {
                                                                if (codePoint < 0x0001f6d3)
                                                                {
                                                                    if (codePoint < 0x0001f6cd)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f6c6 and <= 0x0001f6cc);
#endif
                                                                        return codePoint < 0x0001f6cc ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f6cd and <= 0x0001f6d2);
#endif
                                                                        return codePoint < 0x0001f6d0 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (codePoint < 0x0001f6d8)
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f6d3 and <= 0x0001f6d7);
#endif
                                                                        return codePoint < 0x0001f6d5 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                    }
                                                                    else
                                                                    {
#if DEBUG
                                                                        Validation.Assert(codePoint is >= 0x0001f6d8 and <= 0x0001f6db);
#endif
                                                                        return EastAsianWidthType.N;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0001f6eb)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f6dc and <= 0x0001f6ea);
#endif
                                                                    return codePoint < 0x0001f6e0 ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001f6eb and <= 0x0001f6f3);
#endif
                                                                    return codePoint < 0x0001f6ed ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0001f7e0)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001f6f4 and <= 0x0001f7df);
#endif
                                                        return codePoint < 0x0001f6fd ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001f7ec)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f7e0 and <= 0x0001f7eb);
#endif
                                                            return EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f7ec and <= 0x0001f7f0);
#endif
                                                            return codePoint < 0x0001f7f0 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (codePoint < 0x0001faf9)
                                        {
                                            if (codePoint < 0x0001f947)
                                            {
                                                if (codePoint < 0x0001f90c)
                                                {
#if DEBUG
                                                    Validation.Assert(codePoint is >= 0x0001f7f1 and <= 0x0001f90b);
#endif
                                                    return EastAsianWidthType.N;
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0001f93b)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001f90c and <= 0x0001f93a);
#endif
                                                        return EastAsianWidthType.W;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001f946)
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint is >= 0x0001f93b and <= 0x0001f945);
#endif
                                                            return codePoint < 0x0001f93c ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                        }
                                                        else
                                                        {
#if DEBUG
                                                            Validation.Assert(codePoint == 0x0001f946);
#endif
                                                            return EastAsianWidthType.N;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (codePoint < 0x0001fa00)
                                                {
#if DEBUG
                                                    Validation.Assert(codePoint is >= 0x0001f947 and <= 0x0001f9ff);
#endif
                                                    return EastAsianWidthType.W;
                                                }
                                                else
                                                {
                                                    if (codePoint < 0x0001fa7d)
                                                    {
#if DEBUG
                                                        Validation.Assert(codePoint is >= 0x0001fa00 and <= 0x0001fa7c);
#endif
                                                        return codePoint < 0x0001fa70 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                    }
                                                    else
                                                    {
                                                        if (codePoint < 0x0001fac7)
                                                        {
                                                            if (codePoint < 0x0001fa8f)
                                                            {
                                                                if (codePoint < 0x0001fa8a)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001fa7d and <= 0x0001fa89);
#endif
                                                                    return codePoint < 0x0001fa80 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001fa8a and <= 0x0001fa8e);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                            }
                                                            else
                                                            {
#if DEBUG
                                                                Validation.Assert(codePoint is >= 0x0001fa8f and <= 0x0001fac6);
#endif
                                                                return EastAsianWidthType.W;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (codePoint < 0x0001fadf)
                                                            {
                                                                if (codePoint < 0x0001face)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001fac7 and <= 0x0001facd);
#endif
                                                                    return EastAsianWidthType.N;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001face and <= 0x0001fade);
#endif
                                                                    return codePoint < 0x0001fadd ? EastAsianWidthType.W : EastAsianWidthType.N;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (codePoint < 0x0001faea)
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001fadf and <= 0x0001fae9);
#endif
                                                                    return EastAsianWidthType.W;
                                                                }
                                                                else
                                                                {
#if DEBUG
                                                                    Validation.Assert(codePoint is >= 0x0001faea and <= 0x0001faf8);
#endif
                                                                    return codePoint < 0x0001faf0 ? EastAsianWidthType.N : EastAsianWidthType.W;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
#if DEBUG
                                            Validation.Assert(codePoint is >= 0x0001faf9 and <= 0x0001ffff);
#endif
                                            return EastAsianWidthType.N;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (codePoint < 0x00030000)
                    {
#if DEBUG
                        Validation.Assert(codePoint is >= 0x00020000 and <= 0x0002ffff);
#endif
                        return codePoint < 0x0002fffe ? EastAsianWidthType.W : EastAsianWidthType.N;
                    }
                    else
                    {
#if DEBUG
                        Validation.Assert(codePoint is >= 0x00030000 and <= 0x0003fffd);
#endif
                        return EastAsianWidthType.W;
                    }
                }
            }
            else
            {
                if (codePoint < 0x000e0100)
                {
#if DEBUG
                    Validation.Assert(codePoint is >= 0x0003fffe and <= 0x000e00ff);
#endif
                    return EastAsianWidthType.N;
                }
                else
                {
                    if (codePoint < 0x000ffffe)
                    {
                        if (codePoint < 0x000f0000)
                        {
#if DEBUG
                            Validation.Assert(codePoint is >= 0x000e0100 and <= 0x000effff);
#endif
                            return codePoint < 0x000e01f0 ? EastAsianWidthType.A : EastAsianWidthType.N;
                        }
                        else
                        {
#if DEBUG
                            Validation.Assert(codePoint is >= 0x000f0000 and <= 0x000ffffd);
#endif
                            return EastAsianWidthType.A;
                        }
                    }
                    else
                    {
                        if (codePoint < 0x0010fffe)
                        {
#if DEBUG
                            Validation.Assert(codePoint is >= 0x000ffffe and <= 0x0010fffd);
#endif
                            return codePoint < 0x00100000 ? EastAsianWidthType.N : EastAsianWidthType.A;
                        }
                        else
                        {
#if DEBUG
                            Validation.Assert(codePoint is 0x0010fffe or 0x0010ffff);
#endif
                            return EastAsianWidthType.N;
                        }
                    }
                }
            }
        }

#if DEBUG
        private static void DoTest()
        {
            for (var codePoint = 0x00000000; codePoint <= 0x0000001f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000020; codePoint <= 0x0000007e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.Na);
            for (var codePoint = 0x0000007f; codePoint <= 0x000000a0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000a1; codePoint <= 0x000000a1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000a2; codePoint <= 0x000000a3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.Na);
            for (var codePoint = 0x000000a4; codePoint <= 0x000000a4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000a5; codePoint <= 0x000000a6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.Na);
            for (var codePoint = 0x000000a7; codePoint <= 0x000000a8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000a9; codePoint <= 0x000000a9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000aa; codePoint <= 0x000000aa; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000ab; codePoint <= 0x000000ab; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000ac; codePoint <= 0x000000ac; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.Na);
            for (var codePoint = 0x000000ad; codePoint <= 0x000000ae; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000af; codePoint <= 0x000000af; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.Na);
            for (var codePoint = 0x000000b0; codePoint <= 0x000000b4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000b5; codePoint <= 0x000000b5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000b6; codePoint <= 0x000000ba; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000bb; codePoint <= 0x000000bb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000bc; codePoint <= 0x000000bf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000c0; codePoint <= 0x000000c5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000c6; codePoint <= 0x000000c6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000c7; codePoint <= 0x000000cf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000d0; codePoint <= 0x000000d0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000d1; codePoint <= 0x000000d6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000d7; codePoint <= 0x000000d8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000d9; codePoint <= 0x000000dd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000de; codePoint <= 0x000000e1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000e2; codePoint <= 0x000000e5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000e6; codePoint <= 0x000000e6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000e7; codePoint <= 0x000000e7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000e8; codePoint <= 0x000000ea; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000eb; codePoint <= 0x000000eb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000ec; codePoint <= 0x000000ed; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000ee; codePoint <= 0x000000ef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000f0; codePoint <= 0x000000f0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000f1; codePoint <= 0x000000f1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000f2; codePoint <= 0x000000f3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000f4; codePoint <= 0x000000f6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000f7; codePoint <= 0x000000fa; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000fb; codePoint <= 0x000000fb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000fc; codePoint <= 0x000000fc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000fd; codePoint <= 0x000000fd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000000fe; codePoint <= 0x000000fe; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000000ff; codePoint <= 0x00000100; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000101; codePoint <= 0x00000101; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000102; codePoint <= 0x00000110; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000111; codePoint <= 0x00000111; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000112; codePoint <= 0x00000112; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000113; codePoint <= 0x00000113; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000114; codePoint <= 0x0000011a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000011b; codePoint <= 0x0000011b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000011c; codePoint <= 0x00000125; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000126; codePoint <= 0x00000127; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000128; codePoint <= 0x0000012a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000012b; codePoint <= 0x0000012b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000012c; codePoint <= 0x00000130; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000131; codePoint <= 0x00000133; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000134; codePoint <= 0x00000137; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000138; codePoint <= 0x00000138; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000139; codePoint <= 0x0000013e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000013f; codePoint <= 0x00000142; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000143; codePoint <= 0x00000143; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000144; codePoint <= 0x00000144; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000145; codePoint <= 0x00000147; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000148; codePoint <= 0x0000014b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000014c; codePoint <= 0x0000014c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000014d; codePoint <= 0x0000014d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000014e; codePoint <= 0x00000151; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000152; codePoint <= 0x00000153; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000154; codePoint <= 0x00000165; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000166; codePoint <= 0x00000167; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000168; codePoint <= 0x0000016a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000016b; codePoint <= 0x0000016b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000016c; codePoint <= 0x000001cd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001ce; codePoint <= 0x000001ce; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001cf; codePoint <= 0x000001cf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001d0; codePoint <= 0x000001d0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001d1; codePoint <= 0x000001d1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001d2; codePoint <= 0x000001d2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001d3; codePoint <= 0x000001d3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001d4; codePoint <= 0x000001d4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001d5; codePoint <= 0x000001d5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001d6; codePoint <= 0x000001d6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001d7; codePoint <= 0x000001d7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001d8; codePoint <= 0x000001d8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001d9; codePoint <= 0x000001d9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001da; codePoint <= 0x000001da; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001db; codePoint <= 0x000001db; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000001dc; codePoint <= 0x000001dc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000001dd; codePoint <= 0x00000250; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000251; codePoint <= 0x00000251; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000252; codePoint <= 0x00000260; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000261; codePoint <= 0x00000261; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000262; codePoint <= 0x000002c3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002c4; codePoint <= 0x000002c4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002c5; codePoint <= 0x000002c6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002c7; codePoint <= 0x000002c7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002c8; codePoint <= 0x000002c8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002c9; codePoint <= 0x000002cb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002cc; codePoint <= 0x000002cc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002cd; codePoint <= 0x000002cd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002ce; codePoint <= 0x000002cf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002d0; codePoint <= 0x000002d0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002d1; codePoint <= 0x000002d7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002d8; codePoint <= 0x000002db; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002dc; codePoint <= 0x000002dc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002dd; codePoint <= 0x000002dd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002de; codePoint <= 0x000002de; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000002df; codePoint <= 0x000002df; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000002e0; codePoint <= 0x000002ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000300; codePoint <= 0x0000036f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000370; codePoint <= 0x00000390; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000391; codePoint <= 0x000003a1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000003a2; codePoint <= 0x000003a2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000003a3; codePoint <= 0x000003a9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000003aa; codePoint <= 0x000003b0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000003b1; codePoint <= 0x000003c1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000003c2; codePoint <= 0x000003c2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000003c3; codePoint <= 0x000003c9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000003ca; codePoint <= 0x00000400; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000401; codePoint <= 0x00000401; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000402; codePoint <= 0x0000040f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000410; codePoint <= 0x0000044f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000450; codePoint <= 0x00000450; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00000451; codePoint <= 0x00000451; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00000452; codePoint <= 0x000010ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00001100; codePoint <= 0x0000115f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00001160; codePoint <= 0x0000200f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002010; codePoint <= 0x00002010; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002011; codePoint <= 0x00002012; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002013; codePoint <= 0x00002016; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002017; codePoint <= 0x00002017; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002018; codePoint <= 0x00002019; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000201a; codePoint <= 0x0000201b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000201c; codePoint <= 0x0000201d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000201e; codePoint <= 0x0000201f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002020; codePoint <= 0x00002022; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002023; codePoint <= 0x00002023; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002024; codePoint <= 0x00002027; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002028; codePoint <= 0x0000202f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002030; codePoint <= 0x00002030; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002031; codePoint <= 0x00002031; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002032; codePoint <= 0x00002033; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002034; codePoint <= 0x00002034; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002035; codePoint <= 0x00002035; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002036; codePoint <= 0x0000203a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000203b; codePoint <= 0x0000203b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000203c; codePoint <= 0x0000203d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000203e; codePoint <= 0x0000203e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000203f; codePoint <= 0x00002073; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002074; codePoint <= 0x00002074; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002075; codePoint <= 0x0000207e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000207f; codePoint <= 0x0000207f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002080; codePoint <= 0x00002080; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002081; codePoint <= 0x00002084; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002085; codePoint <= 0x000020a8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000020a9; codePoint <= 0x000020a9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.H);
            for (var codePoint = 0x000020aa; codePoint <= 0x000020ab; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000020ac; codePoint <= 0x000020ac; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000020ad; codePoint <= 0x00002102; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002103; codePoint <= 0x00002103; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002104; codePoint <= 0x00002104; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002105; codePoint <= 0x00002105; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002106; codePoint <= 0x00002108; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002109; codePoint <= 0x00002109; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000210a; codePoint <= 0x00002112; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002113; codePoint <= 0x00002113; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002114; codePoint <= 0x00002115; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002116; codePoint <= 0x00002116; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002117; codePoint <= 0x00002120; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002121; codePoint <= 0x00002122; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002123; codePoint <= 0x00002125; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002126; codePoint <= 0x00002126; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002127; codePoint <= 0x0000212a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000212b; codePoint <= 0x0000212b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000212c; codePoint <= 0x00002152; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002153; codePoint <= 0x00002154; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002155; codePoint <= 0x0000215a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000215b; codePoint <= 0x0000215e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000215f; codePoint <= 0x0000215f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002160; codePoint <= 0x0000216b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000216c; codePoint <= 0x0000216f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002170; codePoint <= 0x00002179; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000217a; codePoint <= 0x00002188; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002189; codePoint <= 0x00002189; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000218a; codePoint <= 0x0000218f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002190; codePoint <= 0x00002199; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000219a; codePoint <= 0x000021b7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000021b8; codePoint <= 0x000021b9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000021ba; codePoint <= 0x000021d1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000021d2; codePoint <= 0x000021d2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000021d3; codePoint <= 0x000021d3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000021d4; codePoint <= 0x000021d4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000021d5; codePoint <= 0x000021e6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000021e7; codePoint <= 0x000021e7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000021e8; codePoint <= 0x000021ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002200; codePoint <= 0x00002200; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002201; codePoint <= 0x00002201; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002202; codePoint <= 0x00002203; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002204; codePoint <= 0x00002206; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002207; codePoint <= 0x00002208; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002209; codePoint <= 0x0000220a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000220b; codePoint <= 0x0000220b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000220c; codePoint <= 0x0000220e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000220f; codePoint <= 0x0000220f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002210; codePoint <= 0x00002210; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002211; codePoint <= 0x00002211; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002212; codePoint <= 0x00002214; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002215; codePoint <= 0x00002215; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002216; codePoint <= 0x00002219; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000221a; codePoint <= 0x0000221a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000221b; codePoint <= 0x0000221c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000221d; codePoint <= 0x00002220; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002221; codePoint <= 0x00002222; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002223; codePoint <= 0x00002223; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002224; codePoint <= 0x00002224; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002225; codePoint <= 0x00002225; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002226; codePoint <= 0x00002226; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002227; codePoint <= 0x0000222c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000222d; codePoint <= 0x0000222d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000222e; codePoint <= 0x0000222e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000222f; codePoint <= 0x00002233; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002234; codePoint <= 0x00002237; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002238; codePoint <= 0x0000223b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000223c; codePoint <= 0x0000223d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000223e; codePoint <= 0x00002247; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002248; codePoint <= 0x00002248; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002249; codePoint <= 0x0000224b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000224c; codePoint <= 0x0000224c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000224d; codePoint <= 0x00002251; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002252; codePoint <= 0x00002252; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002253; codePoint <= 0x0000225f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002260; codePoint <= 0x00002261; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002262; codePoint <= 0x00002263; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002264; codePoint <= 0x00002267; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002268; codePoint <= 0x00002269; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000226a; codePoint <= 0x0000226b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000226c; codePoint <= 0x0000226d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000226e; codePoint <= 0x0000226f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002270; codePoint <= 0x00002281; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002282; codePoint <= 0x00002283; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002284; codePoint <= 0x00002285; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002286; codePoint <= 0x00002287; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002288; codePoint <= 0x00002294; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002295; codePoint <= 0x00002295; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002296; codePoint <= 0x00002298; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002299; codePoint <= 0x00002299; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000229a; codePoint <= 0x000022a4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000022a5; codePoint <= 0x000022a5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000022a6; codePoint <= 0x000022be; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000022bf; codePoint <= 0x000022bf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000022c0; codePoint <= 0x00002311; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002312; codePoint <= 0x00002312; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002313; codePoint <= 0x00002319; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000231a; codePoint <= 0x0000231b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000231c; codePoint <= 0x00002328; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002329; codePoint <= 0x0000232a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000232b; codePoint <= 0x000023e8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000023e9; codePoint <= 0x000023ec; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000023ed; codePoint <= 0x000023ef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000023f0; codePoint <= 0x000023f0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000023f1; codePoint <= 0x000023f2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000023f3; codePoint <= 0x000023f3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000023f4; codePoint <= 0x0000245f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002460; codePoint <= 0x000024e9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000024ea; codePoint <= 0x000024ea; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000024eb; codePoint <= 0x0000254b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000254c; codePoint <= 0x0000254f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002550; codePoint <= 0x00002573; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002574; codePoint <= 0x0000257f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002580; codePoint <= 0x0000258f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002590; codePoint <= 0x00002591; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002592; codePoint <= 0x00002595; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002596; codePoint <= 0x0000259f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025a0; codePoint <= 0x000025a1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025a2; codePoint <= 0x000025a2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025a3; codePoint <= 0x000025a9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025aa; codePoint <= 0x000025b1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025b2; codePoint <= 0x000025b3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025b4; codePoint <= 0x000025b5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025b6; codePoint <= 0x000025b7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025b8; codePoint <= 0x000025bb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025bc; codePoint <= 0x000025bd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025be; codePoint <= 0x000025bf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025c0; codePoint <= 0x000025c1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025c2; codePoint <= 0x000025c5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025c6; codePoint <= 0x000025c8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025c9; codePoint <= 0x000025ca; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025cb; codePoint <= 0x000025cb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025cc; codePoint <= 0x000025cd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025ce; codePoint <= 0x000025d1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025d2; codePoint <= 0x000025e1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025e2; codePoint <= 0x000025e5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025e6; codePoint <= 0x000025ee; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025ef; codePoint <= 0x000025ef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000025f0; codePoint <= 0x000025fc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000025fd; codePoint <= 0x000025fe; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000025ff; codePoint <= 0x00002604; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002605; codePoint <= 0x00002606; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002607; codePoint <= 0x00002608; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002609; codePoint <= 0x00002609; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000260a; codePoint <= 0x0000260d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000260e; codePoint <= 0x0000260f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002610; codePoint <= 0x00002613; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002614; codePoint <= 0x00002615; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002616; codePoint <= 0x0000261b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000261c; codePoint <= 0x0000261c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000261d; codePoint <= 0x0000261d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000261e; codePoint <= 0x0000261e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000261f; codePoint <= 0x0000262f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002630; codePoint <= 0x00002637; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002638; codePoint <= 0x0000263f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002640; codePoint <= 0x00002640; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002641; codePoint <= 0x00002641; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002642; codePoint <= 0x00002642; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002643; codePoint <= 0x00002647; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002648; codePoint <= 0x00002653; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002654; codePoint <= 0x0000265f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002660; codePoint <= 0x00002661; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002662; codePoint <= 0x00002662; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002663; codePoint <= 0x00002665; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002666; codePoint <= 0x00002666; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002667; codePoint <= 0x0000266a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000266b; codePoint <= 0x0000266b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000266c; codePoint <= 0x0000266d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000266e; codePoint <= 0x0000266e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000266f; codePoint <= 0x0000266f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002670; codePoint <= 0x0000267e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000267f; codePoint <= 0x0000267f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002680; codePoint <= 0x00002689; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000268a; codePoint <= 0x0000268f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002690; codePoint <= 0x00002692; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002693; codePoint <= 0x00002693; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002694; codePoint <= 0x0000269d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000269e; codePoint <= 0x0000269f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026a0; codePoint <= 0x000026a0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000026a1; codePoint <= 0x000026a1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026a2; codePoint <= 0x000026a9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000026aa; codePoint <= 0x000026ab; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026ac; codePoint <= 0x000026bc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000026bd; codePoint <= 0x000026be; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026bf; codePoint <= 0x000026bf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026c0; codePoint <= 0x000026c3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000026c4; codePoint <= 0x000026c5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026c6; codePoint <= 0x000026cd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026ce; codePoint <= 0x000026ce; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026cf; codePoint <= 0x000026d3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026d4; codePoint <= 0x000026d4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026d5; codePoint <= 0x000026e1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026e2; codePoint <= 0x000026e2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000026e3; codePoint <= 0x000026e3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026e4; codePoint <= 0x000026e7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000026e8; codePoint <= 0x000026e9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026ea; codePoint <= 0x000026ea; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026eb; codePoint <= 0x000026f1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026f2; codePoint <= 0x000026f3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026f4; codePoint <= 0x000026f4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026f5; codePoint <= 0x000026f5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026f6; codePoint <= 0x000026f9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026fa; codePoint <= 0x000026fa; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026fb; codePoint <= 0x000026fc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000026fd; codePoint <= 0x000026fd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000026fe; codePoint <= 0x000026ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002700; codePoint <= 0x00002704; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002705; codePoint <= 0x00002705; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002706; codePoint <= 0x00002709; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000270a; codePoint <= 0x0000270b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000270c; codePoint <= 0x00002727; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002728; codePoint <= 0x00002728; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002729; codePoint <= 0x0000273c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000273d; codePoint <= 0x0000273d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000273e; codePoint <= 0x0000274b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000274c; codePoint <= 0x0000274c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000274d; codePoint <= 0x0000274d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000274e; codePoint <= 0x0000274e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000274f; codePoint <= 0x00002752; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002753; codePoint <= 0x00002755; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002756; codePoint <= 0x00002756; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002757; codePoint <= 0x00002757; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002758; codePoint <= 0x00002775; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002776; codePoint <= 0x0000277f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002780; codePoint <= 0x00002794; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002795; codePoint <= 0x00002797; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002798; codePoint <= 0x000027af; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000027b0; codePoint <= 0x000027b0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000027b1; codePoint <= 0x000027be; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000027bf; codePoint <= 0x000027bf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000027c0; codePoint <= 0x000027e5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000027e6; codePoint <= 0x000027ed; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.Na);
            for (var codePoint = 0x000027ee; codePoint <= 0x00002984; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002985; codePoint <= 0x00002986; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.Na);
            for (var codePoint = 0x00002987; codePoint <= 0x00002b1a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002b1b; codePoint <= 0x00002b1c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002b1d; codePoint <= 0x00002b4f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002b50; codePoint <= 0x00002b50; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002b51; codePoint <= 0x00002b54; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002b55; codePoint <= 0x00002b55; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002b56; codePoint <= 0x00002b59; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00002b5a; codePoint <= 0x00002e7f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002e80; codePoint <= 0x00002e99; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002e9a; codePoint <= 0x00002e9a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002e9b; codePoint <= 0x00002ef3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002ef4; codePoint <= 0x00002eff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002f00; codePoint <= 0x00002fd5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00002fd6; codePoint <= 0x00002fef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00002ff0; codePoint <= 0x00002fff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00003000; codePoint <= 0x00003000; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.F);
            for (var codePoint = 0x00003001; codePoint <= 0x0000303e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000303f; codePoint <= 0x00003040; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00003041; codePoint <= 0x00003096; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00003097; codePoint <= 0x00003098; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00003099; codePoint <= 0x000030ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00003100; codePoint <= 0x00003104; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00003105; codePoint <= 0x0000312f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00003130; codePoint <= 0x00003130; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00003131; codePoint <= 0x0000318e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000318f; codePoint <= 0x0000318f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00003190; codePoint <= 0x000031e5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000031e6; codePoint <= 0x000031ee; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000031ef; codePoint <= 0x0000321e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000321f; codePoint <= 0x0000321f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00003220; codePoint <= 0x00003247; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00003248; codePoint <= 0x0000324f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x00003250; codePoint <= 0x0000a48c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000a48d; codePoint <= 0x0000a48f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000a490; codePoint <= 0x0000a4c6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000a4c7; codePoint <= 0x0000a95f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000a960; codePoint <= 0x0000a97c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000a97d; codePoint <= 0x0000abff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ac00; codePoint <= 0x0000d7a3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000d7a4; codePoint <= 0x0000dfff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000e000; codePoint <= 0x0000f8ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000f900; codePoint <= 0x0000faff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000fb00; codePoint <= 0x0000fdff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000fe00; codePoint <= 0x0000fe0f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000fe10; codePoint <= 0x0000fe19; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000fe1a; codePoint <= 0x0000fe2f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000fe30; codePoint <= 0x0000fe52; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000fe53; codePoint <= 0x0000fe53; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000fe54; codePoint <= 0x0000fe66; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000fe67; codePoint <= 0x0000fe67; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000fe68; codePoint <= 0x0000fe6b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0000fe6c; codePoint <= 0x0000ff00; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ff01; codePoint <= 0x0000ff60; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.F);
            for (var codePoint = 0x0000ff61; codePoint <= 0x0000ffbe; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.H);
            for (var codePoint = 0x0000ffbf; codePoint <= 0x0000ffc1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ffc2; codePoint <= 0x0000ffc7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.H);
            for (var codePoint = 0x0000ffc8; codePoint <= 0x0000ffc9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ffca; codePoint <= 0x0000ffcf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.H);
            for (var codePoint = 0x0000ffd0; codePoint <= 0x0000ffd1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ffd2; codePoint <= 0x0000ffd7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.H);
            for (var codePoint = 0x0000ffd8; codePoint <= 0x0000ffd9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ffda; codePoint <= 0x0000ffdc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.H);
            for (var codePoint = 0x0000ffdd; codePoint <= 0x0000ffdf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ffe0; codePoint <= 0x0000ffe6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.F);
            for (var codePoint = 0x0000ffe7; codePoint <= 0x0000ffe7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000ffe8; codePoint <= 0x0000ffee; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.H);
            for (var codePoint = 0x0000ffef; codePoint <= 0x0000fffc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0000fffd; codePoint <= 0x0000fffd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0000fffe; codePoint <= 0x00016fdf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00016fe0; codePoint <= 0x00016fe4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00016fe5; codePoint <= 0x00016fef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00016ff0; codePoint <= 0x00016ff1; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00016ff2; codePoint <= 0x00016fff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00017000; codePoint <= 0x000187f7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x000187f8; codePoint <= 0x000187ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00018800; codePoint <= 0x00018cd5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00018cd6; codePoint <= 0x00018cfe; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00018cff; codePoint <= 0x00018d08; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x00018d09; codePoint <= 0x0001afef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001aff0; codePoint <= 0x0001aff3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001aff4; codePoint <= 0x0001aff4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001aff5; codePoint <= 0x0001affb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001affc; codePoint <= 0x0001affc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001affd; codePoint <= 0x0001affe; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001afff; codePoint <= 0x0001afff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001b000; codePoint <= 0x0001b122; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001b123; codePoint <= 0x0001b131; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001b132; codePoint <= 0x0001b132; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001b133; codePoint <= 0x0001b14f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001b150; codePoint <= 0x0001b152; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001b153; codePoint <= 0x0001b154; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001b155; codePoint <= 0x0001b155; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001b156; codePoint <= 0x0001b163; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001b164; codePoint <= 0x0001b167; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001b168; codePoint <= 0x0001b16f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001b170; codePoint <= 0x0001b2fb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001b2fc; codePoint <= 0x0001d2ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001d300; codePoint <= 0x0001d356; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001d357; codePoint <= 0x0001d35f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001d360; codePoint <= 0x0001d376; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001d377; codePoint <= 0x0001f003; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f004; codePoint <= 0x0001f004; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f005; codePoint <= 0x0001f0ce; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f0cf; codePoint <= 0x0001f0cf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f0d0; codePoint <= 0x0001f0ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f100; codePoint <= 0x0001f10a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0001f10b; codePoint <= 0x0001f10f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f110; codePoint <= 0x0001f12d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0001f12e; codePoint <= 0x0001f12f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f130; codePoint <= 0x0001f169; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0001f16a; codePoint <= 0x0001f16f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f170; codePoint <= 0x0001f18d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0001f18e; codePoint <= 0x0001f18e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f18f; codePoint <= 0x0001f190; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0001f191; codePoint <= 0x0001f19a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f19b; codePoint <= 0x0001f1ac; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0001f1ad; codePoint <= 0x0001f1ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f200; codePoint <= 0x0001f202; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f203; codePoint <= 0x0001f20f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f210; codePoint <= 0x0001f23b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f23c; codePoint <= 0x0001f23f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f240; codePoint <= 0x0001f248; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f249; codePoint <= 0x0001f24f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f250; codePoint <= 0x0001f251; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f252; codePoint <= 0x0001f25f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f260; codePoint <= 0x0001f265; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f266; codePoint <= 0x0001f2ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f300; codePoint <= 0x0001f320; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f321; codePoint <= 0x0001f32c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f32d; codePoint <= 0x0001f335; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f336; codePoint <= 0x0001f336; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f337; codePoint <= 0x0001f37c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f37d; codePoint <= 0x0001f37d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f37e; codePoint <= 0x0001f393; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f394; codePoint <= 0x0001f39f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f3a0; codePoint <= 0x0001f3ca; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f3cb; codePoint <= 0x0001f3ce; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f3cf; codePoint <= 0x0001f3d3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f3d4; codePoint <= 0x0001f3df; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f3e0; codePoint <= 0x0001f3f0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f3f1; codePoint <= 0x0001f3f3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f3f4; codePoint <= 0x0001f3f4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f3f5; codePoint <= 0x0001f3f7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f3f8; codePoint <= 0x0001f43e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f43f; codePoint <= 0x0001f43f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f440; codePoint <= 0x0001f440; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f441; codePoint <= 0x0001f441; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f442; codePoint <= 0x0001f4fc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f4fd; codePoint <= 0x0001f4fe; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f4ff; codePoint <= 0x0001f53d; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f53e; codePoint <= 0x0001f54a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f54b; codePoint <= 0x0001f54e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f54f; codePoint <= 0x0001f54f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f550; codePoint <= 0x0001f567; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f568; codePoint <= 0x0001f579; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f57a; codePoint <= 0x0001f57a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f57b; codePoint <= 0x0001f594; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f595; codePoint <= 0x0001f596; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f597; codePoint <= 0x0001f5a3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f5a4; codePoint <= 0x0001f5a4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f5a5; codePoint <= 0x0001f5fa; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f5fb; codePoint <= 0x0001f64f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f650; codePoint <= 0x0001f67f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f680; codePoint <= 0x0001f6c5; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f6c6; codePoint <= 0x0001f6cb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f6cc; codePoint <= 0x0001f6cc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f6cd; codePoint <= 0x0001f6cf; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f6d0; codePoint <= 0x0001f6d2; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f6d3; codePoint <= 0x0001f6d4; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f6d5; codePoint <= 0x0001f6d7; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f6d8; codePoint <= 0x0001f6db; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f6dc; codePoint <= 0x0001f6df; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f6e0; codePoint <= 0x0001f6ea; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f6eb; codePoint <= 0x0001f6ec; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f6ed; codePoint <= 0x0001f6f3; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f6f4; codePoint <= 0x0001f6fc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f6fd; codePoint <= 0x0001f7df; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f7e0; codePoint <= 0x0001f7eb; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f7ec; codePoint <= 0x0001f7ef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f7f0; codePoint <= 0x0001f7f0; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f7f1; codePoint <= 0x0001f90b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f90c; codePoint <= 0x0001f93a; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f93b; codePoint <= 0x0001f93b; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f93c; codePoint <= 0x0001f945; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001f946; codePoint <= 0x0001f946; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001f947; codePoint <= 0x0001f9ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001fa00; codePoint <= 0x0001fa6f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001fa70; codePoint <= 0x0001fa7c; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001fa7d; codePoint <= 0x0001fa7f; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001fa80; codePoint <= 0x0001fa89; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001fa8a; codePoint <= 0x0001fa8e; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001fa8f; codePoint <= 0x0001fac6; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001fac7; codePoint <= 0x0001facd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001face; codePoint <= 0x0001fadc; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001fadd; codePoint <= 0x0001fade; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001fadf; codePoint <= 0x0001fae9; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001faea; codePoint <= 0x0001faef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x0001faf0; codePoint <= 0x0001faf8; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0001faf9; codePoint <= 0x0001ffff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00020000; codePoint <= 0x0002fffd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0002fffe; codePoint <= 0x0002ffff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00030000; codePoint <= 0x0003fffd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.W);
            for (var codePoint = 0x0003fffe; codePoint <= 0x000e00ff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000e0100; codePoint <= 0x000e01ef; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000e01f0; codePoint <= 0x000effff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x000f0000; codePoint <= 0x000ffffd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x000ffffe; codePoint <= 0x000fffff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
            for (var codePoint = 0x00100000; codePoint <= 0x0010fffd; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.A);
            for (var codePoint = 0x0010fffe; codePoint <= 0x0010ffff; ++codePoint)
                Validation.Assert(GetWidthType(codePoint) == EastAsianWidthType.N);
        }
#endif
    }
}
