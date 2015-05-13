﻿using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Example
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Installer : BasicConsoleInstaller,
                             IWindsorInstaller
    {
    }
}