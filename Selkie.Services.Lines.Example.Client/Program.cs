﻿using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using Castle.Windsor;
using Castle.Windsor.Installer;
using EasyNetQ;
using Selkie.Services.Common;
using ISelkieConsole = Selkie.Common.ISelkieConsole;

namespace Selkie.Services.Lines.Example.Client
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    internal class Program
    {
        private static IServicesManager s_Manager;
        private static ISelkieConsole s_Console;
        private static IBus s_Bus;
        private static ILogger s_Logger;

        private static void Main()
        {
            WindsorContainer container = new WindsorContainer();
            container.Install(FromAssembly.This());

            s_Bus = container.Resolve <IBus>();
            s_Logger = container.Resolve <ILogger>();
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

        private static void CallService()
        {
            LineServiceTestClient client = new LineServiceTestClient(s_Bus,
                                                                     s_Logger,
                                                                     s_Console);

            client.RequestTestLines();
        }
    }
}