using System.Collections.Generic;
using JetBrains.Annotations;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Services.Lines.Common.Dto;
using Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos;
using Core2.Selkie.Windsor;

namespace Core2.Selkie.Services.Lines.Converters.ToDtos
{
    [ProjectComponent(Lifestyle.Transient)]
    public class LinesToLineDtosConverter : ILinesToLineDtosConverter
    {
        public LinesToLineDtosConverter([NotNull] ILineToLineDtoConverter converter)
        {
            m_Converter = converter;

            Lines = new ILine[0];
            LineDtos = new LineDto[0];
        }

        private readonly ILineToLineDtoConverter m_Converter;

        [NotNull]
        public IEnumerable <ILine> Lines { get; set; }

        [NotNull]
        public IEnumerable <LineDto> LineDtos { get; private set; }

        public void Convert()
        {
            var dtos = new List <LineDto>();

            foreach ( ILine line in Lines )
            {
                LineDto dto = m_Converter.ConvertFrom(line);

                dtos.Add(dto);
            }

            LineDtos = dtos;
        }
    }
}