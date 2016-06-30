using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.EasyNetQ;
using Selkie.NUnit.Extensions;
using Selkie.Services.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class ServiceTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void ServiceInitializeSubscribesToPingRequestMessageTest(
            [NotNull] ISelkieBus bus,
            [NotNull] ISelkieLogger logger,
            [NotNull] ISelkieManagementClient client)
        {
            // Arrange
            var sut = new Service(bus,
                                  logger,
                                  client);

            string subscriptionId = sut.GetType().ToString();

            // Act
            sut.Initialize();

            // Assert
            bus.Received().SubscribeAsync(subscriptionId,
                                          Arg.Any <Action <PingRequestMessage>>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void ServiceStartSendsMessageTest(
            [NotNull] ISelkieBus bus,
            [NotNull] ISelkieLogger logger,
            [NotNull] ISelkieManagementClient client)
        {
            // Arrange
            var sut = new Service(bus,
                                  logger,
                                  client);

            // Act
            sut.Start();

            // Assert
            bus.Received().Publish(Arg.Is <ServiceStartedResponseMessage>(x => x.ServiceName == Service.ServiceName));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ServiceStopSendsMessageTest(
            [NotNull] ISelkieBus bus,
            [NotNull] ISelkieLogger logger,
            [NotNull] ISelkieManagementClient client)
        {
            // Arrange
            var sut = new Service(bus,
                                  logger,
                                  client);

            // Act
            sut.Stop();

            // Assert
            bus.Received().Publish(Arg.Is <ServiceStoppedResponseMessage>(x => x.ServiceName == Service.ServiceName));
        }
    }
}