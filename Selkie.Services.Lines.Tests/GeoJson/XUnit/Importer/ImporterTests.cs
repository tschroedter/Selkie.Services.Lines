using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.GeoJson.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class ImporterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Features_ReturnsConvertersFeatures_WhenCalled(
            [NotNull] [Frozen] IFeaturesValidator converter,
            [NotNull] Lines.GeoJson.Importer.Importer sut)
        {
            // Arrange
            var expected = new FeatureCollection();
            converter.Supported.Returns(expected);

            // Act
            // Assert
            Assert.Equal(expected,
                         sut.FeatureCollection);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FromText_CallsConvert_ForValidFilename(
            [NotNull] [Frozen] IFeaturesValidator converter,
            [NotNull] [Frozen] IGeoJsonStringReader reader,
            [NotNull] Lines.GeoJson.Importer.Importer sut)
        {
            // Arrange
            reader.Read(Arg.Any <string>()).Returns(new FeatureCollection());

            // Act
            sut.FromText("Filename");

            // Assert
            converter.Received().Validate();
        }

        [Theory]
        [AutoNSubstituteData]
        public void FromText_CallsValidate_ForText(
            [NotNull] [Frozen] IGeoJsonStringReader reader,
            [NotNull] [Frozen] IFeaturesToLinesConverter converter,
            [NotNull] Lines.GeoJson.Importer.Importer sut)
        {
            // Arrange
            reader.Read(Arg.Any <string>()).Returns(new FeatureCollection());

            // Act
            sut.FromText("Some Text");

            // Assert
            converter.Received().Convert();
        }

        [Theory]
        [AutoNSubstituteData]
        public void Lines_ReturnsConvertersLines_WhenCalled(
            [NotNull] [Frozen] IGeoJsonStringReader reader,
            [NotNull] [Frozen] IFeaturesToLinesConverter converter,
            [NotNull] Lines.GeoJson.Importer.Importer sut)
        {
            // Arrange
            reader.Read(Arg.Any <string>()).Returns(new FeatureCollection());

            var expected = new ILine[0];
            converter.Lines.Returns(expected);

            // Act
            // Assert
            Assert.Equal(expected,
                         sut.Lines);
        }
    }
}