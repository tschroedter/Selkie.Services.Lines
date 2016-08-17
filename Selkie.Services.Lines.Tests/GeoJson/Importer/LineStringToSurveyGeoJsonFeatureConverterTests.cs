using System.Diagnostics.CodeAnalysis;
using GeoAPI.Geometries;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using Selkie.Geometry.Surveying;
using Selkie.NUnit.Extensions;
using Selkie.Services.Lines.GeoJson.Importer;
using Constants = Selkie.Geometry.Constants;

namespace Selkie.Services.Lines.Tests.GeoJson.Importer
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class LineStringToSurveyGeoJsonFeatureConverterTests
    {
        private const int FeatureId = 123;

        [Theory]
        [AutoNSubstituteData]
        public void CanConvert_ReturnsFalse_ForLineStringWithWrongNumberOfCoordinates(
            [NotNull] LineStringToSurveyGeoJsonFeatureConverter sut)
        {
            // Arrange
            IFeature feature = CreateLineStringFeatureWithThreeCoordinates();

            // Act
            // Assert
            Assert.False(sut.CanConvert(feature));
        }

        [Theory]
        [AutoNSubstituteData]
        public void CanConvert_ReturnsTrue_ForLineStringFeature(
            [NotNull] LineStringToSurveyGeoJsonFeatureConverter sut)
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
            [NotNull] LineStringToSurveyGeoJsonFeatureConverter sut)
        {
            // Arrange
            Feature feature = CreateFeaturePoint();

            // Act
            // Assert
            Assert.False(sut.CanConvert(feature));
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsCanConvert_WhenCalled(
            [NotNull] LineStringToSurveyGeoJsonFeatureConverter sut)
        {
            // Arrange
            IFeature feature = CreateFeaturePoint();
            sut.Feature = feature;

            // Act
            sut.Convert(FeatureId);

            // Assert
            Assert.True(sut.SurveyGeoJsonFeature.IsUnknown);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsLine_WhenCalled(
            [NotNull] LineStringToSurveyGeoJsonFeatureConverter sut)
        {
            // Arrange
            IFeature feature = CreateLineStringFeature();
            sut.Feature = feature;

            // Act
            sut.Convert(FeatureId);

            // Assert
            ISurveyGeoJsonFeature actual = sut.SurveyGeoJsonFeature;

            AssertFeature(actual);
        }

        private static void AssertFeature(ISurveyGeoJsonFeature actual)
        {
            Assert.False(actual.IsUnknown,
                         "IsUnknown");
            Assert.True(FeatureId == actual.Id,
                        "Id");

            ISurveyFeature surveyFeature = actual.SurveyFeature;

            NUnitHelper.AssertIsEquivalent(0.0,
                                           surveyFeature.StartPoint.X,
                                           "StartPoint.X");
            NUnitHelper.AssertIsEquivalent(1.0,
                                           surveyFeature.StartPoint.Y,
                                           "StartPoint.Y");
            NUnitHelper.AssertIsEquivalent(2.0,
                                           surveyFeature.EndPoint.X,
                                           "EndPoint.X");
            NUnitHelper.AssertIsEquivalent(3.0,
                                           surveyFeature.EndPoint.Y,
                                           "EndPoint.Y");
            Assert.AreEqual(Constants.LineDirection.Forward,
                            surveyFeature.RunDirection);
            Assert.False(surveyFeature.IsUnknown,
                         "IsUnknown");
            Assert.True(FeatureId == surveyFeature.Id,
                        "Id");
        }

        [Theory]
        [AutoNSubstituteData]
        public void Feature_ReturnsDefaultValue_WhenCalled(
            [NotNull] LineStringToSurveyGeoJsonFeatureConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Feature);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Line_ReturnsDefaultValue_WhenCalled(
            [NotNull] LineStringToSurveyGeoJsonFeatureConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.SurveyGeoJsonFeature);
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
    }
}