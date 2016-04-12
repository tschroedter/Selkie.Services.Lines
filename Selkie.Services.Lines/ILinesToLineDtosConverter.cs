using System.Collections.Generic;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines
{
    public interface ILinesToLineDtosConverter
    {
        IEnumerable <ILine> Lines { get; set; }
        IEnumerable <LineDto> LineDtos { get; }
        void Convert();
    }
}