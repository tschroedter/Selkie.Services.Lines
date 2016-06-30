using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.Geometry.Shapes;
using Selkie.NUnit.Extensions;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class LinesToLineDtosConverterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsConvert_WhenCalled(
            [NotNull, Frozen] ILineToLineDtoConverter converter,
            [NotNull] LinesToLineDtosConverter sut)
        {
            // Arrange
            sut.Lines = CreateTwoLines();

            // Act
            sut.Convert();

            // Assert
            converter.Received(2).ConvertFrom(Arg.Any <ILine>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsDtos_WhenCalled(
            [NotNull, Frozen] ILineToLineDtoConverter converter,
            [NotNull] LinesToLineDtosConverter sut)
        {
            // Arrange
            var dtoOne = new LineDto();
            var dtoTwo = new LineDto();

            converter.ConvertFrom(Arg.Any <ILine>()).Returns(dtoOne,
                                                             dtoTwo);
            sut.Lines = CreateTwoLines();

            // Act
            sut.Convert();

            // Assert
            Assert.AreEqual(2,
                            sut.LineDtos.Count());
            Assert.AreEqual(dtoOne,
                            sut.LineDtos.First());
            Assert.AreEqual(dtoTwo,
                            sut.LineDtos.Last());
        }

        [Theory]
        [AutoNSubstituteData]
        public void LineDtos_ReturnsDefaultValue_WhenCalled(
            [NotNull] LinesToLineDtosConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.LineDtos);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Lines_ReturnsDefaultValue_WhenCalled(
            [NotNull] LinesToLineDtosConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Lines);
        }

        private IEnumerable <ILine> CreateTwoLines()
        {
            var lines = new[]
                        {
                            Substitute.For <ILine>(),
                            Substitute.For <ILine>()
                        };

            return lines;
        }
    }
}