using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Handlers;
using Xunit;

namespace Selkie.Services.Lines.Tests.Handlers.XUnit
{
    // todo better way to exclude from coverage
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class TestLineRequestHandlerTests
    {
        [Fact]
        public void HandleSendsReplyTest()
        {
            var logger = Substitute.For <ILogger>();
            var manager = Substitute.For <ILinesSourceManager>();
            var bus = Substitute.For <IBus>();

            Line[] lines = SetupManager(manager);
            TestLineRequestHandler sut = CreateServiceUnderTest(logger,
                                                                bus,
                                                                manager);

            TestLineRequestMessage message = CreateMessage();

            sut.Handle(message);

            // ReSharper disable once PossibleUnintendedReferenceComparison
            bus.Received().PublishAsync(Arg.Is <TestLineResponseMessage>(x => x.LineDtos.Count() == lines.Count()));
        }

        private TestLineRequestHandler CreateServiceUnderTest([NotNull] ILogger logger,
                                                              [NotNull] IBus bus,
                                                              [NotNull] ILinesSourceManager manager)
        {
            var sut = new TestLineRequestHandler(logger,
                                                 bus,
                                                 manager);

            return sut;
        }

        private static TestLineRequestMessage CreateMessage()
        {
            TestLineType.Type[] availableTestLineses =
            {
                TestLineType.Type.CreateTestLines
            };
            var message = new TestLineRequestMessage
                          {
                              Types = availableTestLineses
                          };
            return message;
        }

        private static Line[] SetupManager(ILinesSourceManager manager)
        {
            Line[] lines =
            {
                new Line(0,
                         1.0,
                         2.0,
                         3.0,
                         4.0)
            };
            TestLineType.Type[] types =
            {
                TestLineType.Type.CreateTestLines
            };

            // ReSharper disable once MaximumChainedReferences
            manager.GetTestLines(types).ReturnsForAnyArgs(lines);

            return lines;
        }
    }
}