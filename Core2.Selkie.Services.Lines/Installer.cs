using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.Selkie.Common;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Services.Common.Interfaces;

[assembly: InternalsVisibleTo("Core2.Selkie.Services.Lines.Tests")]

namespace Core2.Selkie.Services.Lines
{
    [ExcludeFromCodeCoverage]
    public class Installer : SelkieInstaller <Installer>
    {
        // ReSharper disable once CodeAnnotationAnalyzer
        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            var register = container.Resolve <IRegisterMessageHandlers>();

            register.Register(container,
                              Assembly.GetAssembly(typeof( Installer )));

            container.Release(register);

            // ReSharper disable MaximumChainedReferences
            Assembly assembly = Assembly.GetAssembly(typeof(Installer));

            container.Register(
                               Classes.FromAssembly(assembly)
                                      .BasedOn <IService>()
                                      .WithServiceFromInterface(typeof( IService ))
                                      .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));
            // ReSharper restore MaximumChainedReferences
        }
    }
}