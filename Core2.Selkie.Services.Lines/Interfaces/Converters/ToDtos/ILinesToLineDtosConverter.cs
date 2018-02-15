using System.Collections.Generic;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Services.Lines.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface ILinesToLineDtosConverter
    {
        IEnumerable <ILine> Lines { get; set; }
        IEnumerable <LineDto> LineDtos { get; }
        void Convert();
    }
}