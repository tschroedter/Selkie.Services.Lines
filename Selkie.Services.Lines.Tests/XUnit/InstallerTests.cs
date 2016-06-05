using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NSubstitute;
using Xunit;

namespace Selkie.Services.Lines.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class InstallerTests
    {
        public InstallerTests()
        {
            m_Container = Substitute.For <IWindsorContainer>();
            var store = Substitute.For <IConfigurationStore>();

            var sut = new Installer();

            sut.Install(m_Container,
                        store);
        }

        private readonly IWindsorContainer m_Container;

        [Fact]
        public void InstallRegistersIServiceTest()
        {
            m_Container.Received().Register(Arg.Any <IRegistration[]>());
        }
    }
}