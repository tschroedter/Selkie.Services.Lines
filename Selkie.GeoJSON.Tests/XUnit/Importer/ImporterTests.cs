using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.GeoJson.Tests.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class ImporterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Features_ReturnsConvertersFeatures_WhenCalled([NotNull] [Frozen] IFeaturesValidator converter,
                                                                  [NotNull] GeoJSON.Importer.Importer sut)
        {
            // Arrange
            var expected = new FeatureCollection();
            converter.Supported.Returns(expected);

            // Act
            // Assert
            Assert.Equal(expected,
                         sut.Features);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FromText_CallsConvert_ForValidFilename([NotNull] [Frozen] IFeaturesValidator converter,
                                                           [NotNull] [Frozen] IGeoJsonStringReader reader,
                                                           [NotNull] GeoJSON.Importer.Importer sut)
        {
            // Arrange
            reader.Read(Arg.Any <string>()).Returns(new FeatureCollection());

            // Act
            sut.FromText("Filename");

            // Assert
            converter.Received().Validate();
        }
    }
}