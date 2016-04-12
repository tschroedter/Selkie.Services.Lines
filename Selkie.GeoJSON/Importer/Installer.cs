using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NetTopologySuite.IO;
using Selkie.Common;

namespace Selkie.GeoJSON.Importer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Installer : SelkieInstaller <Installer>
    {
        protected override void PreInstallComponents(IWindsorContainer container,
                                                     IConfigurationStore store)
        {
            base.PreInstallComponents(container,
                                      store);

            container.Register(Component.For(typeof ( GeoJsonReader ))
                                        .ImplementedBy(typeof ( GeoJsonReader ))
                                        .LifeStyle.Transient);
        }
    }
}