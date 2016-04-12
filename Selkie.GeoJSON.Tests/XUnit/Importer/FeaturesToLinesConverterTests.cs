using System.Diagnostics.CodeAnalysis;
using GeoAPI.Geometries;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NSubstitute;
using Selkie.GeoJSON.Importer;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.Windsor;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.GeoJson.Tests.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    public sealed class FeaturesToLinesConverterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsConvertersConvert_ForCanConvertReturnsTrue(
            [NotNull] ISelkieLogger logger,
            [NotNull] IFeatureToLineConverter converter,
            [NotNull] IFeatureToLineConverter two)
        {
            // Arrange
            converter.CanConvert(Arg.Any <IFeature>()).Returns(true);
            two.CanConvert(Arg.Any <IFeature>()).Returns(false);

            var converters = new[]
                             {
                                 converter,
                                 two
                             };

            var featureCollection = new FeatureCollection();
            featureCollection.Features.Add(CreateFeaturePoint());

            var sut = new FeaturesToLinesConverter(logger,
                                                   converters)
                      {
                          FeatureCollection = featureCollection
                      };

            // Act
            sut.Convert();

            // Assert
            converter.Received().Convert(0);
            two.DidNotReceive().Convert(1);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsFeatureInConverter_ForCanConvertReturnsTrue(
            [NotNull] ISelkieLogger logger,
            [NotNull] IFeatureToLineConverter converter)
        {
            // Arrange
            converter.CanConvert(Arg.Any <Feature>()).Returns(true);

            var converters = new[]
                             {
                                 converter
                             };

            var featureCollection = new FeatureCollection();
            featureCollection.Features.Add(CreateFeaturePoint());

            var sut = new FeaturesToLinesConverter(logger,
                                                   converters)
                      {
                          FeatureCollection = featureCollection
                      };

            // Act
            sut.Convert();

            // Assert
            Assert.Equal(sut.FeatureCollection.Features [ 0 ],
                         converter.Feature);
        }

        /* todo
        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsLine_ForConverterFound(
            [NotNull] IFeatureToLineConverter converter,
            [NotNull] ILine expected)
        {
            // Arrange
            converter.CanConvert(Arg.Any <Feature>()).Returns(true);
            converter.Line.Returns(expected);

            var converters = new[]
                             {
                                 converter
                             };

            var sut = new FeaturesToLinesConverter(converters);

            // Act
            sut.Convert();

            // Assert
            Assert.Equal(expected,
                         sut.Line);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsLineToUnknown_ForNoConverterFound(
            [NotNull] IFeatureToLineConverter converter,
            [NotNull] ILine expected)
        {
            // Arrange
            var converters = new IFeatureToLineConverter[0];

            var sut = new FeaturesToLinesConverter(converters);

            // Act
            sut.Convert();

            // Assert
            Assert.Equal(Line.Unknown,
                         sut.Line);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Feature_ReturnsDefaultValue_WhenCalled(
            [NotNull] FeaturesToLinesConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Feature);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Line_ReturnsDefaultValue_WhenCalled(
            [NotNull] FeaturesToLinesConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Line);
        }
*/

        private static Feature CreateFeaturePoint()
        {
            var coordinate = new Coordinate(0.0,
                                            0.0);

            var point = new Point(coordinate);

            var attributesTable = new AttributesTable();

            return new Feature(point,
                               attributesTable);
        }
    }
}