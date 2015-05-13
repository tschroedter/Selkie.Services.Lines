﻿using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor.Installer;
using Selkie.Services.Common;

namespace Selkie.Services.Lines.Example
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Program : ServiceMain
    {
        // todo TopShelf
        public static void Main()
        {
            StartServiceAndRunForever(FromAssembly.This(),
                                      Service.ServiceName);

            Environment.Exit(0);
        }
    }
}