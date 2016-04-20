using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.EasyNetQ;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Handlers;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class TestLineRequestHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_SendsReplyMessage_WhenCalled(
            [NotNull, Frozen] ILinesSourceManager manager,
            [NotNull, Frozen] ISelkieBus bus,
            [NotNull, Frozen] ILinesToLineDtosConverter converter,
            [NotNull] TestLineRequestMessage message,
            [NotNull] TestLineRequestHandlerAsync sut)
        {
            // Arrange
            var expected = new[]
                           {
                               new LineDto(),
                               new LineDto()
                           };

            converter.LineDtos.Returns(expected);

            // Act
            sut.Handle(message);

            // Assert
            bus.Received().PublishAsync(Arg.Is <TestLineResponseMessage>(x => x.LineDtos.Length == expected.Length));
        }

        [Theory]
        [AutoNSubstituteData]
        public void Handle_SetsLines_WhenCalled(
            [NotNull, Frozen] ILinesSourceManager manager,
            [NotNull, Frozen] ILinesToLineDtosConverter converter,
            [NotNull] TestLineRequestMessage message,
            [NotNull] TestLineRequestHandlerAsync sut)
        {
            // Arrange
            var expected = new[]
                           {
                               Substitute.For <ILine>(),
                               Substitute.For <ILine>()
                           };

            manager.GetTestLines(Arg.Any <IEnumerable <TestLineType.Type>>()).Returns(expected);

            // Act
            sut.Handle(message);

            // Assert
            Assert.Equal(expected,
                         converter.Lines);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsConvert_WhenCalled(
            [NotNull, Frozen] ILinesToLineDtosConverter converter,
            [NotNull] TestLineRequestMessage message,
            [NotNull] TestLineRequestHandlerAsync sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            converter.Received().Convert();
        }
    }
}