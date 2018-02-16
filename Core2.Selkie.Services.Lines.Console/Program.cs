using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor.Installer;
using Core2.Selkie.Services.Common;

namespace Core2.Selkie.Services.Lines.Console
{
    [ExcludeFromCodeCoverage]
    public class Program : ServiceMain
    {
        public static void Main()
        {
            StartServiceAndRunForever(FromAssembly.Containing<Installer>(),
                                      Service.ServiceName);

            Environment.Exit(0);
        }
    }
}