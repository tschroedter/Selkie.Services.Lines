using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Selkie.Windsor;

namespace Selkie.Services.Lines
{
    [ProjectComponent(Lifestyle.Transient)]
    public class GeoJsonTextToLineDtosConverter : IGeoJsonTextToLineDtosConverter
    {
        private readonly ILinesToLineDtosConverter m_Converter;
        private readonly IImporter m_Importer;
        private readonly ILinesValidator m_Validator;

        public GeoJsonTextToLineDtosConverter(
            [NotNull] IImporter importer,
            [NotNull] ILinesToLineDtosConverter converter,
            [NotNull] ILinesValidator validator)
        {
            m_Importer = importer;
            m_Converter = converter;
            m_Validator = validator;

            GeoJsonText = string.Empty;
            LineDtos = new LineDto[0];
        }

        [NotNull]
        public string GeoJsonText { get; set; }

        [NotNull]
        public IEnumerable <LineDto> LineDtos { get; private set; }

        public void Convert()
        {
            ILine[] lines = ImportFromText(GeoJsonText).ToArray();

            ValidateLines(lines);

            LineDtos = ConvertLinesToDtos(lines);
        }

        private IEnumerable <ILine> ImportFromText([NotNull] string text)
        {
            m_Importer.FromText(text);

            return m_Importer.Lines;
        }

        private IEnumerable <LineDto> ConvertLinesToDtos([NotNull] IEnumerable <ILine> lines)
        {
            m_Converter.Lines = lines;
            m_Converter.Convert();

            return m_Converter.LineDtos;
        }

        private void ValidateLines(IEnumerable <ILine> lines)
        {
            bool isValidateLines = m_Validator.ValidateLines(lines);

            if ( !isValidateLines )
            {
                throw new ArgumentException("Provided lines are invalid!");
            }
        }
    }
}