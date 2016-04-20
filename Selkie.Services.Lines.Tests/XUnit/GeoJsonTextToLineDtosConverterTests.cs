using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.XUnit
{
    [ExcludeFromCodeCoverage]
    public sealed class GeoJsonTextToLineDtosConverterTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void GeoJsonText_ReturnsDefaultValue_WhenCalled(
            [NotNull] GeoJsonTextToLineDtosConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.GeoJsonText);
        }

        [Theory]
        [AutoNSubstituteData]
        public void LineDtos_ReturnsDefaultValue_WhenCalled(
            [NotNull] GeoJsonTextToLineDtosConverter sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.LineDtos);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_SetsLineDtos_WhenCalled(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ILinesToLineDtosConverter converter,
            [NotNull, Frozen] ILinesValidator validator,
            [NotNull] GeoJsonTextToLineDtosConverter sut)
        {
            // Arrange
            IEnumerable <ILine> returnThis = CreateLines();
            importer.Lines.Returns(returnThis);

            IEnumerable <LineDto> lineDtos = CreateLineDtos();
            converter.LineDtos.Returns(lineDtos);

            validator.ValidateLines(Arg.Any <IEnumerable <ILine>>()).Returns(true);

            // Act
            sut.Convert();

            // Assert
            Assert.Equal(converter.LineDtos.Count(),
                         sut.LineDtos.Count());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsValidate_WhenCalled(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ILinesToLineDtosConverter converter,
            [NotNull, Frozen] ILinesValidator validator,
            [NotNull] GeoJsonTextToLineDtosConverter sut)
        {
            // Arrange
            IEnumerable <ILine> lines = CreateLines().ToArray();
            importer.Lines.Returns(lines);

            IEnumerable <LineDto> lineDtos = CreateLineDtos();
            converter.LineDtos.Returns(lineDtos);

            validator.ValidateLines(Arg.Any <IEnumerable <ILine>>()).Returns(true);

            // Act
            sut.Convert();

            // Assert
            validator.Received().ValidateLines(Arg.Is <IEnumerable <ILine>>(x => x.Count() == lines.Count()));
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_CallsImportFromFile_WhenCalled(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ILinesToLineDtosConverter converter,
            [NotNull, Frozen] ILinesValidator validator,
            [NotNull] GeoJsonTextToLineDtosConverter sut)
        {
            // Arrange
            IEnumerable <ILine> lines = CreateLines().ToArray();
            importer.Lines.Returns(lines);

            IEnumerable <LineDto> lineDtos = CreateLineDtos();
            converter.LineDtos.Returns(lineDtos);

            validator.ValidateLines(Arg.Any <IEnumerable <ILine>>()).Returns(true);

            sut.GeoJsonText = "Text";

            // Act
            sut.Convert();

            // Assert
            importer.Received().FromText("Text");
        }

        [Theory]
        [AutoNSubstituteData]
        public void Convert_ThrowsException_ForInvalidLines(
            [NotNull, Frozen] IImporter importer,
            [NotNull, Frozen] ILinesToLineDtosConverter converter,
            [NotNull, Frozen] ILinesValidator validator,
            [NotNull] GeoJsonTextToLineDtosConverter sut)
        {
            // Arrange
            IEnumerable <ILine> lines = CreateLines().ToArray();
            importer.Lines.Returns(lines);

            IEnumerable <LineDto> lineDtos = CreateLineDtos();
            converter.LineDtos.Returns(lineDtos);

            validator.ValidateLines(Arg.Any <IEnumerable <ILine>>()).Returns(false);

            // Act
            // Assert
            Assert.Throws <ArgumentException>(() => sut.Convert());
        }

        private IEnumerable <ILine> CreateLines()
        {
            var one = Substitute.For <ILine>();
            one.IsUnknown.Returns(false);

            var two = Substitute.For <ILine>();
            two.IsUnknown.Returns(false);

            var lines = new[]
                        {
                            one,
                            two
                        };

            return lines;
        }

        private IEnumerable <LineDto> CreateLineDtos()
        {
            var dtos = new[]
                       {
                           new LineDto(),
                           new LineDto()
                       };

            return dtos;
        }
    }
}