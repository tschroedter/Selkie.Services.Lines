using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Selkie.Common;
using Selkie.EasyNetQ;
using Selkie.Services.Common;
using Selkie.Services.Lines.GeoJson.Importer;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;

namespace Selkie.Services.Lines
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Installer : SelkieInstaller <Installer>
    {
        // ReSharper disable once CodeAnnotationAnalyzer
        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            var register = container.Resolve <IRegisterMessageHandlers>();

            register.Register(container,
                              Assembly.GetAssembly(typeof( Installer )));

            container.Release(register);

            // ReSharper disable MaximumChainedReferences
            container.Register(
                               Classes.FromThisAssembly()
                                      .BasedOn <IService>()
                                      .WithServiceFromInterface(typeof( IService ))
                                      .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));
            // ReSharper restore MaximumChainedReferences

            container.Register(Component.For <IFeatureToLineConverter>()
                                        .ImplementedBy <LineStringToLineConverter>());

            container.Register(Component.For(typeof( GeoJsonReader ))
                                        .ImplementedBy(typeof( GeoJsonReader ))
                                        .LifeStyle.Transient);

            container.Register(Component.For(typeof( FeatureCollection ))
                                        .ImplementedBy(typeof( FeatureCollection ))
                                        .LifeStyle.Transient);
        }
    }
}