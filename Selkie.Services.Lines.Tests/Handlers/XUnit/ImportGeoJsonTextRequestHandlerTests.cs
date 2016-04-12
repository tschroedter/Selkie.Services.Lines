using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.EasyNetQ;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Handlers;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    public sealed class ImportGeoJsonTextRequestHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_SetsGeoJsonText_WhenCalled(
            [NotNull, Frozen] IGeoJsonTextToLineDtosConverter converter,
            [NotNull] ImportGeoJsonTextRequestMessage message,
            [NotNull] ImportGeoJsonTextRequestHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            Assert.Equal(message.Text,
                         converter.GeoJsonText);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsConvert_WhenCalled(
            [NotNull, Frozen] IGeoJsonTextToLineDtosConverter converter,
            [NotNull] ImportGeoJsonTextRequestMessage message,
            [NotNull] ImportGeoJsonTextRequestHandler sut)
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
            [NotNull] ImportGeoJsonTextRequestHandler sut)
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
    }
}