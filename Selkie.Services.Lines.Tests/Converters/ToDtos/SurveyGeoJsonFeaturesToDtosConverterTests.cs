using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.Geometry.Surveying;
using Selkie.NUnit.Extensions;
using Selkie.Services.Common.Dto;
using Selkie.Services.Lines.Converters.ToDtos;
using Selkie.Services.Lines.Interfaces.Converters.ToDtos;

namespace Selkie.Services.Lines.Tests.Converters
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SurveyGeoJsonFeaturesToDtosConverterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsConvertOfConverter_ForFeatures(
            [NotNull, Frozen] ISurveyGeoJsonFeatureToDtoConverter converter,
            [NotNull] ISurveyGeoJsonFeature featureOne,
            [NotNull] ISurveyGeoJsonFeature featureTwo,
            [NotNull] SurveyGeoJsonFeaturesToDtosConverter sut)
        {
            // Arrange
            var features = new[]
                           {
                               featureOne,
                               featureTwo
                           };

            sut.Features = features;

            // Act
            sut.Convert();

            // Assert
            converter.Received().Convert(0);
            converter.Received().Convert(1);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsFeatureOfConverter_ForFeatures(
            [NotNull, Frozen] ISurveyGeoJsonFeatureToDtoConverter converter,
            [NotNull] ISurveyGeoJsonFeature featureOne,
            [NotNull] SurveyGeoJsonFeaturesToDtosConverter sut)
        {
            // Arrange
            var features = new[]
                           {
                               featureOne
                           };

            sut.Features = features;

            // Act
            sut.Convert();

            // Assert
            Assert.AreEqual(featureOne,
                            converter.Feature);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_AddsConverterDtoToDtos_ForFeatures(
            [NotNull, Frozen] ISurveyGeoJsonFeatureToDtoConverter converter,
            [NotNull] ISurveyGeoJsonFeature featureOne,
            [NotNull] ISurveyGeoJsonFeature featureTwo,
            [NotNull] SurveyGeoJsonFeatureDto dtoOne,
            [NotNull] SurveyGeoJsonFeatureDto dtoTwo,
            [NotNull] SurveyGeoJsonFeaturesToDtosConverter sut)
        {
            // Arrange
            converter.Dto.Returns(dtoOne,
                                  dtoTwo);

            var features = new[]
                           {
                               featureOne,
                               featureTwo
                           };

            sut.Features = features;
            // Act
            sut.Convert();

            // Assert
            Assert.AreEqual(2,
                            sut.Dtos.Count());
        }
    }
}