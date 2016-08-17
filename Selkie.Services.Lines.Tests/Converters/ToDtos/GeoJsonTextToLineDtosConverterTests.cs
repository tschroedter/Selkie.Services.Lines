using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.NUnit.Extensions;
using Selkie.Services.Common.Dto;
using Selkie.Services.Lines.Converters.ToDtos;
using Selkie.Services.Lines.Interfaces.Converters.ToDtos;
using Selkie.Services.Lines.Interfaces.GeoJson.Importer;
using Selkie.Services.Lines.Interfaces.Validators;

namespace Selkie.Services.Lines.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class GeoJsonTextToLineDtosConverterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsImportFromFile_WhenCalled(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ISurveyGeoJsonFeaturesToDtosConverter converter,
            [NotNull, Frozen] IFeatureValidator validator,
            [NotNull] GeoJsonTextToSurveyGeoJsonFeatureDtosConverter sut)
        {
            // Arrange
            IEnumerable <ISurveyGeoJsonFeature> features = CreateSurveyGeoJsonFeatures().ToArray();
            importer.Features.Returns(features);

            IEnumerable <SurveyGeoJsonFeatureDto> dtos = CreateSurveyGeoJsonFeatureDto();
            converter.Dtos.Returns(dtos);

            validator.ValidateFeatures(Arg.Any <IEnumerable <ISurveyGeoJsonFeature>>()).Returns(true);

            sut.GeoJson = "Text";

            // Act
            sut.Convert();

            // Assert
            importer.Received().FromText("Text");
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsValidate_WhenCalled(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ISurveyGeoJsonFeaturesToDtosConverter converter,
            [NotNull, Frozen] IFeatureValidator validator,
            [NotNull] GeoJsonTextToSurveyGeoJsonFeatureDtosConverter sut)
        {
            // Arrange
            IEnumerable <ISurveyGeoJsonFeature> features = CreateSurveyGeoJsonFeatures().ToArray();
            importer.Features.Returns(features);

            IEnumerable <SurveyGeoJsonFeatureDto> dtos = CreateSurveyGeoJsonFeatureDto();
            converter.Dtos.Returns(dtos);

            validator.ValidateFeatures(Arg.Any <IEnumerable <ISurveyGeoJsonFeature>>()).Returns(true);

            // Act
            sut.Convert();

            // Assert
            validator.Received()
                     .ValidateFeatures(Arg.Is <IEnumerable <ISurveyGeoJsonFeature>>(x => x.Count() == features.Count()));
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsLineDtos_WhenCalled(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ISurveyGeoJsonFeaturesToDtosConverter converter,
            [NotNull, Frozen] IFeatureValidator validator,
            [NotNull] GeoJsonTextToSurveyGeoJsonFeatureDtosConverter sut)
        {
            // Arrange
            IEnumerable <ISurveyGeoJsonFeature> returnThis = CreateSurveyGeoJsonFeatures();
            importer.Features.Returns(returnThis);

            IEnumerable <SurveyGeoJsonFeatureDto> lineDtos = CreateSurveyGeoJsonFeatureDto();
            converter.Dtos.Returns(lineDtos);

            validator.ValidateFeatures(Arg.Any <IEnumerable <ISurveyGeoJsonFeature>>()).Returns(true);

            // Act
            sut.Convert();

            // Assert
            Assert.AreEqual(converter.Dtos.Count(),
                            sut.Dtos.Count());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_ThrowsException_ForInvalidLines(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ISurveyGeoJsonFeaturesToDtosConverter converter,
            [NotNull, Frozen] IFeatureValidator validator,
            [NotNull] GeoJsonTextToSurveyGeoJsonFeatureDtosConverter sut)
        {
            // Arrange
            IEnumerable <ISurveyGeoJsonFeature> lines = CreateSurveyGeoJsonFeatures().ToArray();
            importer.Features.Returns(lines);

            IEnumerable <SurveyGeoJsonFeatureDto> lineDtos = CreateSurveyGeoJsonFeatureDto();
            converter.Dtos.Returns(lineDtos);

            validator.ValidateLines(Arg.Any <IEnumerable <ILine>>()).Returns(false);

            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => sut.Convert());
        }

        [Theory]
        [AutoNSubstituteData]
        public void GeoJsonText_ReturnsDefaultValue_WhenCalled(
            [NotNull] GeoJsonTextToSurveyGeoJsonFeatureDtosConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.GeoJson);
        }

        [Theory]
        [AutoNSubstituteData]
        public void LineDtos_ReturnsDefaultValue_WhenCalled(
            [NotNull] GeoJsonTextToSurveyGeoJsonFeatureDtosConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Dtos);
        }

        private IEnumerable <ISurveyGeoJsonFeature> CreateSurveyGeoJsonFeatures()
        {
            var one = Substitute.For <ISurveyGeoJsonFeature>();
            one.IsUnknown.Returns(false);

            var two = Substitute.For <ISurveyGeoJsonFeature>();
            two.IsUnknown.Returns(false);

            var lines = new[]
                        {
                            one,
                            two
                        };

            return lines;
        }

        private IEnumerable <SurveyGeoJsonFeatureDto> CreateSurveyGeoJsonFeatureDto()
        {
            var dtos = new[]
                       {
                           new SurveyGeoJsonFeatureDto(),
                           new SurveyGeoJsonFeatureDto()
                       };

            return dtos;
        }
    }
}