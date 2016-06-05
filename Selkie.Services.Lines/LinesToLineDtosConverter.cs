using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Windsor;

namespace Selkie.Services.Lines
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