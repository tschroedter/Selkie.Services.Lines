using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Lines.Handlers;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class BaseHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void StartSubscribesToMessageTest([NotNull] [Frozen] IBus bus,
                                                 [NotNull] TestSelkieBaseHandler sut)
        {
            string subscriptionId = sut.GetType().ToString();

            sut.Start();

            bus.Received().SubscribeAsync(subscriptionId,
                                          Arg.Any <Func <TestMessage, Task>>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void HandleCallsHandlerTest([NotNull] TestSelkieBaseHandler sut)
        {
            sut.Handle(new TestMessage());

            Assert.True(sut.HandleWasCalled);
        }

        public class TestSelkieBaseHandler : BaseHandler <TestMessage>
        {
            public bool HandleWasCalled;

            public TestSelkieBaseHandler([NotNull] ILogger logger,
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