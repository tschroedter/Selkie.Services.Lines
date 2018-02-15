using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Core2.Selkie.Services.Lines.Handlers;
using NUnit.Framework;
using Core2.Selkie.NUnit.Extensions;
using AutoFixture.NUnit3;
using Core2.Selkie.Services.Lines.Interfaces;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class BaseHandlerTests
    {
        [Test]
        [AutoNSubstituteData]
        public void StartSubscribesToMessageTest([NotNull] [Frozen] IBus bus,
                                                 [NotNull] TestSelkieBaseHandler sut)
        {
            string subscriptionId = sut.GetType().ToString();

            sut.Start();

            bus.Received().SubscribeAsync(subscriptionId,
                                          Arg.Any <Func <TestMessage, Task>>());
        }

        [Test]
        [AutoNSubstituteData]
        public void HandleCallsHandlerTest([NotNull] TestSelkieBaseHandler sut)
        {
            sut.Handle(new TestMessage());

            Assert.True(sut.HandleWasCalled);
        }

        public class TestSelkieBaseHandler : BaseHandler <TestMessage>
        {
            public bool HandleWasCalled;

            public TestSelkieBaseHandler([NotNull] ISelkieLogger logger,
                                         [NotNull] IBus bus,
                                         [NotNull] ILinesSourceManager manager)
                : base(logger,
                       bus,
                       manager)
            {
            }

            // todo need a real TestBus to send/receive messages, so we can make this method protected
            internal override void Handle(TestMessage message)
            {
                HandleWasCalled = true;
            }
        }

        public class TestMessage
        {
        }
    }
}