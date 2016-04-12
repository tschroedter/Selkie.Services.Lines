using System.Diagnostics.CodeAnalysis;
using GeoAPI.Geometries;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Selkie.GeoJSON.Importer;
using Selkie.Geometry.Shapes;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;
using Point = NetTopologySuite.Geometries.Point;

namespace Selkie.GeoJson.Tests.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    public sealed class LineStringToLineConverterTests
    {
        private const int LineId = 123;

        [Theory]
        [AutoNSubstituteData]
        public void Feature_ReturnsDefaultValue_WhenCalled(
            [NotNull] LineStringToLineConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Feature);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Line_ReturnsDefaultValue_WhenCalled(
            [NotNull] LineStringToLineConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Line);
        }

        [Theory]
        [AutoNSubstituteData]
        public void CanConvert_ReturnsTrue_ForLineStringFeature(
            [NotNull] LineStringToLineConverter sut)
        {
            // Arrange
            IFeature feature = CreateLineStringFeature();

            // Act
            // Assert
            Assert.True(sut.CanConvert(feature));
        }

        [Theory]
        [AutoNSubstituteData]
        public void CanConvert_ReturnsTrue_ForOtherFeature(
            [NotNull] LineStringToLineConverter sut)
        {
            // Arrange
            Feature feature = CreateFeaturePoint();

            // Act
            // Assert
            Assert.False(sut.CanConvert(feature));
        }

        [Theory]
        [AutoNSubstituteData]
        public void CanConvert_ReturnsFalse_ForLineStringWithWrongNumberOfCoordinates(
            [NotNull] LineStringToLineConverter sut)
        {
            // Arrange
            IFeature feature = CreateLineStringFeatureWithThreeCoordinates();

            // Act
            // Assert
            Assert.False(sut.CanConvert(feature));
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsCanConvert_WhenCalled(
            [NotNull] LineStringToLineConverter sut)
        {
            // Arrange
            IFeature feature = CreateFeaturePoint();
            sut.Feature = feature;

            // Act
            sut.Convert(LineId);

            // Assert
            Assert.True(sut.Line.IsUnknown);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsLine_WhenCalled(
            [NotNull] LineStringToLineConverter sut)
        {
            // Arrange
            IFeature feature = CreateLineStringFeature();
            sut.Feature = feature;

            // Act
            sut.Convert(LineId);

            // Assert
            ILine line = sut.Line;

            Assert.False(line.IsUnknown,
                         "IsUnknown");
            Assert.True(LineId == line.Id,
                        "Id");
            XUnitHelper.AssertIsEquivalent(0.0,
                                           line.X1,
                                           "X1");
            XUnitHelper.AssertIsEquivalent(1.0,
                                           line.Y1,
                                           "Y1");
            XUnitHelper.AssertIsEquivalent(2.0,
                                           line.X2,
                                           "X2");
            XUnitHelper.AssertIsEquivalent(3.0,
                                           line.Y2,
                                           "Y2");
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

        private static IFeature CreateLineStringFeatureWithThreeCoordinates()
        {
            var start = new Coordinate(0.0,
                                       0.0);

            var middle = new Coordinate(50.0,
                                        50.0);

            var end = new Coordinate(100.0,
                                     100.0);

            var coordinates = new[]
                              {
                                  start,
                                  middle,
                                  end
                              };

            var lineString = new LineString(coordinates);

            var attributesTable = new AttributesTable();

            var feature = new Feature(lineString,
                                      attributesTable);

            return feature;
        }

        private static Feature CreateFeaturePoint()
        {
            const double latitude = 45.0;
            const double longitude = 90.0;

            var point = new Point(latitude,
                                  longitude);

            var attributesTable = new AttributesTable();

            return new Feature(point,
                               attributesTable);
        }
    }
}