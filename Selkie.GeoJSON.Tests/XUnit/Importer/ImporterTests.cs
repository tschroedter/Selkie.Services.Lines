using System.Diagnostics.CodeAnalysis;
using GeoJSON.Net.Feature;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.GeoJson.Importer;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.GeoJson.Tests.XUnit.Importer
{
    // todo ncrunch and XUnit is not working
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class ImporterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Features_ReturnsConvertersFeatures_WhenCalled([NotNull] [Frozen] IFeaturesValidator converter,
                                                                  [NotNull] GeoJson.Importer.Importer sut)
        {
            // Arrange
            var expected = new FeatureCollection();
            converter.FeaturesValid.Returns(expected);

            // Act
            // Assert
            Assert.Equal(expected, sut.Features);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FromFile_SetsFeatures_ForValidFilename([NotNull] [Frozen] IFileReader reader,
                                                           [NotNull] [Frozen] IFeaturesValidator converter,
                                                           [NotNull] GeoJson.Importer.Importer sut)
        {
            // Arrange
            var expected = new FeatureCollection();
            reader.Read(Arg.Any <string>()).Returns(expected);

            // Act
            sut.FromFile("Filename");

            // Assert
            Assert.Equal(expected, converter.Features);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FromFile_CallsConvert_ForValidFilename([NotNull] [Frozen] IFeaturesValidator converter,
                                                           [NotNull] GeoJson.Importer.Importer sut)
        {
            // Arrange
            // Act
            sut.FromFile("Filename");

            // Assert
            converter.Received().Validate();
        }
    }
}