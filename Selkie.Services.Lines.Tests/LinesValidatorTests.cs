using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class LinesValidatorTests
    {
        [Theory]
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

            var logger = new LinesValidatorLogger(Substitute.For <ISelkieLogger>());

            var sut = new LinesValidator(logger);

            // act
            // assert
            Assert.AreEqual(result,
                            sut.ValidateDtos(lines));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ValidateReturnsFalseForEmptyTest([NotNull] LinesValidator sut)
        {
            // assemble
            var lines = new LineDto[0];

            // act
            // assert
            Assert.False(sut.ValidateDtos(lines));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ValidateReturnsFalseForOneLineTest([NotNull] LinesValidator sut)
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

            var logger = new LinesValidatorLogger(Substitute.For <ISelkieLogger>());

            var sut = new LinesValidator(logger);

            // act
            // assert
            Assert.AreEqual(result,
                            sut.ValidateLines(lines));
        }
    }
}