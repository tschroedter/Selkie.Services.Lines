﻿using System;
using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Core2.Selkie.Windsor;

namespace Core2.Selkie.Services.Lines.Console.Client
{
    [ExcludeFromCodeCoverage]
    public class Installer
        : BasicConsoleInstaller,
          IWindsorInstaller
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.",
                                           StringComparison.InvariantCulture);
        }
    }
}