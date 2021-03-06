﻿using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Specflow
{
    [ExcludeFromCodeCoverage]
    public class Installer
        : BasicConsoleInstaller,
          IWindsorInstaller
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.";
        }
    }
}