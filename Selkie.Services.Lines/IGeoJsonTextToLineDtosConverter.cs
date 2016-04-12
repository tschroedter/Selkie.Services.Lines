using System.Collections.Generic;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines
{
    public interface IGeoJsonTextToLineDtosConverter
    {
        string GeoJsonText { get; set; }
        IEnumerable <LineDto> LineDtos { get; }
        void Convert();
    }
}