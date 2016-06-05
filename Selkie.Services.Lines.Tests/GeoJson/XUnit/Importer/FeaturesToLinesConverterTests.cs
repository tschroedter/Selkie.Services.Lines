using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeoAPI.Geometries;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NSubstitute;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.GeoJson.Importer;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Selkie.Windsor;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;
using Point = NetTopologySuite.Geometries.Point;

namespace Selkie.Services.Lines.Tests.GeoJson.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    public sealed class FeaturesToLinesConverterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsConvertersConvert_ForCanConvertReturnsTrue(
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

            var sut = new FeaturesToLinesConverter(converters)
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
        public void Convert_DoesNotAddUnknownLine_ForNoConverterFound(
            [NotNull] ISelkieLogger logger,
            [NotNull] IFeatureToLineConverter converter)
        {
            // Arrange
            converter.CanConvert(Arg.Any <Feature>()).Returns(false);

            var converters = new[]
                             {
                                 converter
                             };

            var featureCollection = new FeatureCollection();
            featureCollection.Features.Add(CreateLineStringFeature());

            var sut = new FeaturesToLinesConverter(converters)
                      {
                          FeatureCollection = featureCollection
                      };

            // Act
            sut.Convert();

            // Assert
            Assert.Equal(0,
                         sut.Lines.Count());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsFeatureInConverter_ForCanConvertReturnsTrue(
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

            var sut = new FeaturesToLinesConverter(converters)
                      {
                          FeatureCollection = featureCollection
                      };

            // Act
            sut.Convert();

            // Assert
            Assert.Equal(sut.FeatureCollection.Features [ 0 ],
                         converter.Feature);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsLines_ForConvertersFound(
            [NotNull] ISelkieLogger logger,
            [NotNull] IFeatureToLineConverter converter,
            [NotNull] ILine expected)
        {
            // Arrange
            expected.IsUnknown.Returns(false);
            converter.CanConvert(Arg.Any <Feature>()).Returns(true);
            converter.Line.Returns(expected);

            var converters = new[]
                             {
                                 converter
                             };

            var featureCollection = new FeatureCollection();
            featureCollection.Features.Add(CreateLineStringFeature());

            var sut = new FeaturesToLinesConverter(converters)
                      {
                          FeatureCollection = featureCollection
                      };

            // Act
            sut.Convert();

            // Assert
            Assert.Equal(1,
                         sut.Lines.Count());
        }

        [Theory]
        [AutoNSubstituteData]
        public void FeatureCollection_ReturnsDefaultValue_WhenCalled(
            [NotNull] FeaturesToLinesConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.FeatureCollection);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Lines_ReturnsDefaultValue_WhenCalled(
            [NotNull] FeaturesToLinesConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Lines);
        }

        private static Feature CreateFeaturePoint()
        {
            var coordinate = new Coordinate(0.0,
                                            0.0);

            var point = new Point(coordinate);

            var attributesTable = new AttributesTable();

            return new Feature(point,
                               attributesTable);
        }

        private static IFeature CreateLineStringFeature()
        {
            var start = new Coordinate(0.0,
                                       1.0);

            var end = new Coordinate(2.0,
                                     3.0);

            var coordinates = new[]
                              {
                                  start,
                                  end
                              };

            var lineString = new LineString(coordinates);

            var attributesTable = new AttributesTable();

            var feature = new Feature(lineString,
                                      attributesTable);

            return feature;
        }
    }
}