using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class LinesValidatorTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void ValidateReturnsFalseForEmptyTest([NotNull] LinesValidator sut)
        {
            // assemble
            LineDto[] lines = new LineDto[0];

            // act
            // assert
            Assert.False(sut.ValidateDtos(lines));
        }

        [Theory]
        [AutoNSubstituteData]
        public void ValidateReturnsFalseForOneLineTest([NotNull] LinesValidator sut)
        {
            // assemble
            LineDto[] lines = {
                                  new LineDto()
                              };

            // act
            // assert
            Assert.False(sut.ValidateDtos(lines));
        }

        [Theory]
        [InlineData(0, 1, true)]
        [InlineData(0, 0, false)]
        [InlineData(1, 0, false)]
        [InlineData(-1, 0, false)]
        [InlineData(1, 2, false)]
        public void ValidateDtosTest(int firstId,
                                     int secondId,
                                     bool result)
        {
            // assemble
            LineDto one = new LineDto
                          {
                              Id = firstId
                          };
            LineDto two = new LineDto
                          {
                              Id = secondId
                          };

            LineDto[] lines = {
                                  one,
                                  two
                              };

            LinesValidator sut = new LinesValidator();

            // act
            // assert
            Assert.Equal(result,
                         sut.ValidateDtos(lines));
        }

        [Theory]
        [InlineData(0, 1, true)]
        [InlineData(0, 0, false)]
        [InlineData(1, 0, false)]
        [InlineData(-1, 0, false)]
        [InlineData(1, 2, false)]
        public void ValidateLinesTest(int firstId,
                                      int secondId,
                                      bool result)
        {
            // assemble
            ILine one = Substitute.For <ILine>();
            one.Id.Returns(firstId);

            ILine two = Substitute.For <ILine>();
            two.Id.Returns(secondId);

            ILine[] lines = {
                                one,
                                two
                            };

            LinesValidator sut = new LinesValidator();

            // act
            // assert
            Assert.Equal(result,
                         sut.ValidateLines(lines));
        }
    }
}