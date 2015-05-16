using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Common.Messages;
using Selkie.XUnit.Extensions;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class ServiceTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void ServiceStartSendsMessageTest([NotNull] [Frozen] IBus bus,
                                                 [NotNull] Service sut)
        {
            sut.Start();

            bus.Received().Publish(Arg.Is <ServiceStartedResponseMessage>(x => x.ServiceName == Service.ServiceName));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ServiceStopSendsMessageTest([NotNull] [Frozen] IBus bus,
                                                [NotNull] Service sut)
        {
            sut.Stop();

            bus.Received().Publish(Arg.Is <ServiceStoppedResponseMessage>(x => x.ServiceName == Service.ServiceName));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ServiceInitializeSubscribesToPingRequestMessageTest([NotNull] [Frozen] IBus bus,
                                                                        [NotNull] Service sut)
        {
            sut.Initialize();

            string subscriptionId = sut.GetType().ToString();

            bus.Received().SubscribeAsync(subscriptionId,
                                          Arg.Any <Func <PingRequestMessage, Task>>());
        }
    }
}