using System;
using System.Threading;
using JetBrains.Annotations;

namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    public static class Helper
    {
        public const string ServiceName = "Lines Service";

        public const string WorkingFolder =
            @"C:\Development\Selkie\Services\" +
            @"Lines\Selkie.Services.Lines.Console\bin\Debug\";

        public const string FilenName =
            WorkingFolder + "Selkie.Services.Lines.Console.exe";

        private static readonly TimeSpan SleepTime = TimeSpan.FromSeconds(1.0);

        public static void SleepWaitAndDo([NotNull] Func <bool> breakIfTrue,
                                          [NotNull] Action doSomething)
        {
            for ( var i = 0 ; i < 10 ; i++ )
            {
                Thread.Sleep(SleepTime);

                if ( breakIfTrue() )
                {
                    break;
                }

                doSomething();
            }
        }
    }
}