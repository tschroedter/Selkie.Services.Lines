using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Services.Lines.Common;
using Core2.Selkie.Services.Lines.Common.Dto;
using Core2.Selkie.Services.Lines.Common.Messages;
using Core2.Selkie.Services.Lines.Handlers;
using NUnit.Framework;
using Core2.Selkie.NUnit.Extensions;
using AutoFixture.NUnit3;
using Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos;
using Core2.Selkie.Services.Lines.Interfaces;
using Core2.Selkie.EasyNetQ.Interfaces;

namespace Core2.Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class TestLineRequestHandlerTests
    {
        [Test]
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

        [Test]
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

        [Test]
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