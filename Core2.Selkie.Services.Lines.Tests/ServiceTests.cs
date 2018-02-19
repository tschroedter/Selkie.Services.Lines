﻿using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.NUnit.Extensions;
using Core2.Selkie.Services.Common.Messages;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Services.Lines.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class ServiceTests
    {
        [Test]
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

        [Test]
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

        [Test]
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