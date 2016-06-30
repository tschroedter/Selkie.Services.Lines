using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.EasyNetQ;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Handlers;

namespace Selkie.Services.Lines.Tests.Handlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class TestLineRequestHandlerTests
    {
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
            Assert.AreEqual(expected,
                            converter.Lines);
        }
    }
}