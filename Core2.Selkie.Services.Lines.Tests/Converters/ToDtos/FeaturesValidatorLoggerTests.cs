using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Geometry.Surveying;
using Core2.Selkie.NUnit.Extensions;
using Core2.Selkie.Services.Lines.Validators;
using AutoFixture.NUnit3;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Services.Lines.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class FeaturesValidatorLoggerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void LogFeatures_CallsLogValidateStatus_WhenCalled(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            // Act
            sut.LogFeatures(true,
                            new ISurveyGeoJsonFeature[0]);

            // Assert
            logger.Received().Info("Features are valid!");
        }

        [Theory]
        [AutoNSubstituteData]
        public void LogFeatures_CallsLogger_ForEachFeature(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] ISurveyGeoJsonFeature one,
            [NotNull] ISurveyGeoJsonFeature two,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            var features = new[]
                           {
                               one,
                               two
                           };

            // Act
            sut.LogFeatures(true,
                            features);

            // Assert
            logger.Received(3).Info(Arg.Any <string>()); // 3 because of LogValidateStatus calls onces + to features
        }

        [Theory]
        [AutoNSubstituteData]
        public void LogIdsAreEmpty_CallsLogger_WhenCalled(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            // Act
            sut.LogIdsAreEmpty();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void LogValidateStatus_CallsLogger_ForIdsAreValidate(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            // Act
            sut.LogValidateStatus(true);

            // Assert
            logger.Received().Info(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void LogValidateStatus_CallsLogger_ForIdsAreInvalidate(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            // Act
            sut.LogValidateStatus(false);

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void LineToString_CallsLogger_WhenCalled(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] ILine line,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            // Act
            string actual = sut.LineToString(line);

            // Assert
            Assert.True(actual.StartsWith("[" + line.Id + "]"));
        }

        [Theory]
        [AutoNSubstituteData]
        public void LogFeatures_CallsLogValidateStatus_ForLines(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            // Act
            sut.LogFeatures(true,
                            new ILine[0]);

            // Assert
            logger.Received().Info("Features are valid!");
        }

        [Theory]
        [AutoNSubstituteData]
        public void LogFeatures_CallsLogger_ForLines(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] ILine one,
            [NotNull] ILine two,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            var features = new[]
                           {
                               one,
                               two
                           };

            // Act
            sut.LogFeatures(true,
                            features);

            // Assert
            logger.Received(3).Info(Arg.Any <string>()); // 3 because of LogValidateStatus calls onces + to features
        }

        [Theory]
        [AutoNSubstituteData]
        public void LogFeatures_CallsLogger_ForLines(
            [NotNull, Frozen] ISelkieLogger logger,
            [NotNull] ISurveyGeoJsonFeature feature,
            [NotNull] ISurveyFeature surveyFeature,
            [NotNull] FeaturesValidatorLogger sut)
        {
            // Arrange
            feature.SurveyFeature.Returns(surveyFeature);
            surveyFeature.Id.Returns(1);

            // Act
            string actual = sut.FeatureToString(feature);

            // Assert
            Assert.True(actual.StartsWith("[" + surveyFeature.Id + "]"));
        }
    }
}