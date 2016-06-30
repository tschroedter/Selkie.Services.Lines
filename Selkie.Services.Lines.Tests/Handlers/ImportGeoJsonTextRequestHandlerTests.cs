using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.EasyNetQ;
using Selkie.NUnit.Extensions;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Handlers;

namespace Selkie.Services.Lines.Tests.Handlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ImportGeoJsonTextRequestHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsConvert_WhenCalled(
            [NotNull, Frozen] IGeoJsonTextToLineDtosConverter converter,
            [NotNull] ImportGeoJsonTextRequestMessage message,
            [NotNull] ImportGeoJsonTextRequestHandlerAsync sut)
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
            [NotNull, Frozen] ISelkieBus bus,
            [NotNull, Frozen] IGeoJsonTextToLineDtosConverter converter,
            [NotNull] ImportGeoJsonTextRequestMessage message,
            [NotNull] ImportGeoJsonTextRequestHandlerAsync sut)
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
            bus.Received()
               .PublishAsync(Arg.Is <ImportGeoJsonTextResponseMessage>(x => x.LineDtos.Length == expected.Length));
        }

        [Theory]
        [AutoNSubstituteData]
        public void Handle_SetsGeoJsonText_WhenCalled(
            [NotNull, Frozen] IGeoJsonTextToLineDtosConverter converter,
            [NotNull] ImportGeoJsonTextRequestMessage message,
            [NotNull] ImportGeoJsonTextRequestHandlerAsync sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            Assert.AreEqual(message.Text,
                            converter.GeoJsonText);
        }
    }
}