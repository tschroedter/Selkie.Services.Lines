using System.Diagnostics.CodeAnalysis;
using Castle.Windsor.Installer;
using Selkie.Services.Common;

namespace Selkie.Services.Lines.Windows.Service
{
    [ExcludeFromCodeCoverage]
    public class LinesServiceMain : ServiceMain
    {
        public void Run()
        {
            StartServiceAndRunForever(FromAssembly.This(),
                                      Lines.Service.ServiceName);
        }
    }
}