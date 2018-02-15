using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Geometry.Surveying;
using Core2.Selkie.NUnit.Extensions;
using Core2.Selkie.Services.Lines.Common.Dto;
using Core2.Selkie.Services.Lines.Validators;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Services.Lines.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class FeaturesValidatorTests
    {
        [Test]
        [TestCase(0, 1, true)]
        [TestCase(0, 0, false)]
        [TestCase(1, 0, false)]
        [TestCase(-1, 0, false)]
        [TestCase(1, 2, false)]
        public void ValidateDtosTest(int firstId,
                                     int secondId,
                                     bool result)
        {
            // assemble
            var one = new LineDto
                      {
                          Id = firstId
                      };
            var two = new LineDto
                      {
                          Id = secondId
                      };

            LineDto[] lines =
            {
                one,
                two
            };

            var logger = new FeaturesValidatorLogger(Substitute.For <ISelkieLogger>());

            var sut = new FeatureValidator(logger);

            // act
            // assert
            Assert.AreEqual(result,
                            sut.ValidateDtos(lines));
        }

        [Test]
        [AutoNSubstituteData]
        public void ValidateReturnsFalseForEmptyTest([NotNull] FeatureValidator sut)
        {
            // assemble
            var lines = new LineDto[0];

            // act
            // assert
            Assert.False(sut.ValidateDtos(lines));
        }

        [Test]
        [AutoNSubstituteData]
        public void ValidateReturnsFalseForOneLineTest([NotNull] FeatureValidator sut)
        {
            // assemble
            LineDto[] lines =
            {
                new LineDto()
            };

            // act
            // assert
            Assert.False(sut.ValidateDtos(lines));
        }

        [Test]
        [TestCase(0, 1, true)]
        [TestCase(0, 0, false)]
        [TestCase(1, 0, false)]
        [TestCase(-1, 0, false)]
        [TestCase(1, 2, false)]
        public void ValidateFeaturesTest(int firstId,
                                         int secondId,
                                         bool result)
        {
            // assemble
            var one = Substitute.For <ISurveyGeoJsonFeature>();
            one.Id.Returns(firstId);

            var two = Substitute.For <ISurveyGeoJsonFeature>();
            two.Id.Returns(secondId);

            ISurveyGeoJsonFeature[] features =
            {
                one,
                two
            };

            var logger = new FeaturesValidatorLogger(Substitute.For <ISelkieLogger>());

            var sut = new FeatureValidator(logger);

            // act
            // assert
            Assert.AreEqual(result,
                            sut.ValidateFeatures(features));
        }

        [Test]
        [TestCase(0, 1, true)]
        [TestCase(0, 0, false)]
        [TestCase(1, 0, false)]
        [TestCase(-1, 0, false)]
        [TestCase(1, 2, false)]
        public void ValidateLinesTest(int firstId,
                                      int secondId,
                                      bool result)
        {
            // assemble
            var one = Substitute.For <ILine>();
            one.Id.Returns(firstId);

            var two = Substitute.For <ILine>();
            two.Id.Returns(secondId);

            ILine[] lines =
            {
                one,
                two
            };

            var logger = new FeaturesValidatorLogger(Substitute.For <ISelkieLogger>());

            var sut = new FeatureValidator(logger);

            // act
            // assert
            Assert.AreEqual(result,
                            sut.ValidateLines(lines));
        }
    }
}