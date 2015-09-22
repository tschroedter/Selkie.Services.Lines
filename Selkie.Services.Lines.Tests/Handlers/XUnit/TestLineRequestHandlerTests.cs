using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Selkie.EasyNetQ;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Handlers;
using Xunit;

namespace Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class TestLineRequestHandlerTests
    {
        [Fact]
        public void HandleSendsReplyTest()
        {
            var manager = Substitute.For <ILinesSourceManager>();
            var bus = Substitute.For <ISelkieBus>();

            Line[] lines = SetupManager(manager);
            TestLineRequestHandler sut = CreateServiceUnderTest(bus,
                                                                manager);

            TestLineRequestMessage message = CreateMessage();

            sut.Handle(message);

            // ReSharper disable once PossibleUnintendedReferenceComparison
            bus.Received().PublishAsync(Arg.Is <TestLineResponseMessage>(x => x.LineDtos.Length == lines.Length));
        }

        private TestLineRequestHandler CreateServiceUnderTest([NotNull] ISelkieBus bus,
                                                              [NotNull] ILinesSourceManager manager)
        {
            var sut = new TestLineRequestHandler(bus,
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