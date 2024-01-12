using System;
using System.Diagnostics;

namespace Experiment.CSharp
{
    internal class Program
    {
        private const long MAX_COUNT = 1000L * 1000 * 100;
        private static readonly TimeSpan stepTime = TimeSpan.FromMilliseconds(100);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        static void Main(string[] args)
        {
            while (true)
            {
                _ = Pattern1_DateTimeNow();
                _ = Pattern2_DateTimeOffsetNow();
                _ = Pattern3_Stopwatch();
                _ = Pattern4_SystemTicks();
                var t1 = Pattern1_DateTimeNow();
                var t2 = Pattern2_DateTimeOffsetNow();
                var t3 = Pattern3_Stopwatch();
                var t4 = Pattern4_SystemTicks();

                Console.WriteLine($"DateTime:           {t1.totalTime.TotalSeconds:F6} [sec], n={t1.n}");
                Console.WriteLine($"DateTimeOffset:     {t2.totalTime.TotalSeconds:F6} [sec], n={t2.n}");
                Console.WriteLine($"StopWatch:          {t3.totalTime.TotalSeconds:F6} [sec], n={t3.n}");
                Console.WriteLine($"Environment.Ticks64:{t4.totalTime.TotalSeconds:F6} [sec], n={t4.n}");

                System.Console.Beep();
                _ = System.Console.ReadLine();
            }
        }

        private static (TimeSpan totalTime, long n) Pattern1_DateTimeNow()
        {
            var time1 = Process.GetCurrentProcess().TotalProcessorTime;
            var n = 0L;
            var nextTimeToReport = DateTime.UtcNow;
            for (var count = 0L; count < MAX_COUNT; ++count)
            {
                var now = DateTime.UtcNow;
                if (now >= nextTimeToReport)
                {
                    ++n;
                    nextTimeToReport = now + stepTime;
                }
            }

            return (Process.GetCurrentProcess().TotalProcessorTime - time1, n);
        }

        private static (TimeSpan totalTime, long n) Pattern2_DateTimeOffsetNow()
        {
            var time1 = Process.GetCurrentProcess().TotalProcessorTime;
            var n = 0L;
            var nextTimeToReport = DateTimeOffset.UtcNow;
            for (var count = 0L; count < MAX_COUNT; ++count)
            {
                var now = DateTimeOffset.UtcNow;
                if (now >= nextTimeToReport)
                {
                    ++n;
                    nextTimeToReport = now + stepTime;
                }
            }

            return (Process.GetCurrentProcess().TotalProcessorTime - time1, n);
        }

        private static (TimeSpan totalTime, long n) Pattern3_Stopwatch()
        {
            var time1 = Process.GetCurrentProcess().TotalProcessorTime;
            var n = 0L;
            var sw = new Stopwatch();
            sw.Start();
            for (var count = 0L; count < MAX_COUNT; ++count)
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds >= 100)
                {
                    ++n;
                    sw.Reset();
                }

                sw.Start();
            }

            return (Process.GetCurrentProcess().TotalProcessorTime - time1, n);
        }

        private static (TimeSpan totalTime, long n) Pattern4_SystemTicks()
        {
            var time1 = Process.GetCurrentProcess().TotalProcessorTime;
            var n = 0L;
            var previousTime = 0L;
            for (var count = 0L; count < MAX_COUNT; ++count)
            {
                var now = Environment.TickCount64;
                if (unchecked(now - previousTime) >= 100)
                {
                    ++n;
                    previousTime = now;
                }
            }

            return (Process.GetCurrentProcess().TotalProcessorTime - time1, n);
        }
    }
}
