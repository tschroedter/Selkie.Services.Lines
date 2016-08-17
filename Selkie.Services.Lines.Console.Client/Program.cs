using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Selkie.EasyNetQ;
using Selkie.Services.Common;
using ISelkieConsole = Selkie.Common.Interfaces.ISelkieConsole;

namespace Selkie.Services.Lines.Console.Client
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static IServicesManager s_Manager;
        private static ISelkieConsole s_Console;
        private static ISelkieBus s_Bus;

        private static void CallService()
        {
            var client = new LineServiceTestClient(s_Bus,
                                                   s_Console);

            client.RequestTestLines();

            client.RequestGeoJsonImportText();

            client.StopService();
        }

        private static void Main()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            s_Bus = container.Resolve <ISelkieBus>();
            s_Console = container.Resolve <ISelkieConsole>();
            s_Manager = container.Resolve <IServicesManager>();

            s_Manager.WaitForAllServices();

            CallService();

            s_Console.WriteLine("Press 'Return' to exit!");
            s_Console.ReadLine();

            StopServices();

            container.Dispose();
        }

        private static void StopServices()
        {
            s_Console.WriteLine("Stopping services...");
            s_Manager.StopServices();
        }
    }
}