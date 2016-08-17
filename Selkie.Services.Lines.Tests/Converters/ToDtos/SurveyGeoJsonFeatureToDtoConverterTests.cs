using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.NUnit.Extensions;
using Selkie.Services.Common.Dto;
using Selkie.Services.Lines.Converters.ToDtos;
using Constants = Selkie.Geometry.Constants;

namespace Selkie.Services.Lines.Tests.Converters
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SurveyGeoJsonFeatureToDtoConverterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Convert_ReturnsDto_ForFeature(
            [NotNull] SurveyGeoJsonFeature feature,
            [NotNull] SurveyGeoJsonFeatureToDtoConverter sut)
        {
            // Arrange
            SurveyGeoJsonFeatureDto expected = CreateExpectedDto(feature);

            sut.Feature = feature;

            // Act
            sut.Convert(1);

            // Assert
            SurveyGeoJsonFeatureDto actual = sut.Dto;

            AssertDto(expected,
                      actual);
        }

        private static void AssertDto(
            [NotNull] SurveyGeoJsonFeatureDto expected,
            [NotNull] SurveyGeoJsonFeatureDto actual)
        {
            SurveyFeatureDto expectedDto = expected.SurveyFeatureDto;
            string expectedGeoJson = expected.SurveyFeatureAsGeoJson;

            SurveyFeatureDto actualDto = actual.SurveyFeatureDto;
            string actualGeoJson = actual.SurveyFeatureAsGeoJson;

            AssertSurveyFeatureDto(expectedDto,
                                   actualDto);

            AssertGeoJson(expectedGeoJson,
                          actualGeoJson);
        }

        private static void AssertSurveyFeatureDto(
            [NotNull] SurveyFeatureDto expected,
            [NotNull] SurveyFeatureDto actual)
        {
            Assert.AreEqual(expected.RunDirection,
                            actual.RunDirection,
                            "RunDirection");

            NUnitHelper.AssertIsEquivalent(expected.StartPoint.X,
                                           actual.StartPoint.X,
                                           "StartPoint.X");

            NUnitHelper.AssertIsEquivalent(expected.StartPoint.Y,
                                           actual.StartPoint.Y,
                                           "StartPoint.Y");

            NUnitHelper.AssertIsEquivalent(expected.EndPoint.X,
                                           actual.EndPoint.X,
                                           "EndPoint.X");

            NUnitHelper.AssertIsEquivalent(expected.EndPoint.Y,
                                           actual.EndPoint.Y,
                                           "EndPoint.Y");

            NUnitHelper.AssertIsEquivalent(expected.AngleToXAxisAtEndPoint,
                                           actual.AngleToXAxisAtEndPoint,
                                           "AngleToXAxisAtEndPoint");

            NUnitHelper.AssertIsEquivalent(expected.AngleToXAxisAtStartPoint,
                                           actual.AngleToXAxisAtStartPoint,
                                           "AngleToXAxisAtStartPoint");

            Assert.AreEqual(expected.Id,
                            actual.Id,
                            "Id");

            Assert.AreEqual(expected.IsUnknown,
                            actual.IsUnknown,
                            "IsUnknown");

            NUnitHelper.AssertIsEquivalent(expected.Length,
                                           actual.Length,
                                           "Length");
        }

        private static void AssertGeoJson(
            [NotNull] string expected,
            [NotNull] string actual)
        {
            Assert.AreEqual(expected,
                            actual,
                            "GeoJson");
        }

        private static SurveyFeatureDto CreateSurveyFeatureDto(ISurveyGeoJsonFeature surveyGeoJsonFeature)
        {
            ISurveyFeature feature = surveyGeoJsonFeature.SurveyFeature;

            var endPoint = new PointDto
                           {
                               X = feature.EndPoint.X,
                               Y = feature.EndPoint.Y
                           };

            var startPoint = new PointDto
                             {
                                 X = feature.StartPoint.X,
                                 Y = feature.StartPoint.Y
                             };

            var surveyFeatureDto = new SurveyFeatureDto
                                   {
                                       EndPoint = endPoint,
                                       RunDirection = feature.RunDirection.ToString(),
                                       StartPoint = startPoint,
                                       AngleToXAxisAtEndPoint = feature.AngleToXAxisAtEndPoint.Degrees,
                                       AngleToXAxisAtStartPoint = feature.AngleToXAxisAtStartPoint.Degrees,
                                       Id = feature.Id,
                                       IsUnknown = feature.IsUnknown,
                                       Length = feature.Length
                                   };
            return surveyFeatureDto;
        }

        private static SurveyGeoJsonFeatureDto CreateExpectedDto(
            [NotNull] ISurveyGeoJsonFeature feature)
        {
            var dto = new SurveyGeoJsonFeatureDto
                      {
                          SurveyFeatureDto = CreateSurveyFeatureDto(feature),
                          SurveyFeatureAsGeoJson = feature.SurveyFeatureAsGeoJson
                      };

            return dto;
        }

        private static SurveyFeature CreateSurveyFeature()
        {
            var feature = new SurveyFeature(1,
                                            new Point(1.0,
                                                      2.0),
                                            new Point(10.0,
                                                      20.0),
                                            Angle.For45Degrees,
                                            Angle.For45Degrees,
                                            Constants.LineDirection.Forward,
                                            123.0,
                                            false);
            return feature;
        }
    }
}