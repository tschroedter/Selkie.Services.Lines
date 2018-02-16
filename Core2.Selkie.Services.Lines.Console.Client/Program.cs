using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Services.Common.Interfaces;

namespace Core2.Selkie.Services.Lines.Console.Client
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
            container.Install(FromAssembly.Containing<Installer>());


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