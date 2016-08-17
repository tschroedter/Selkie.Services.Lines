using System.Collections.Generic;
using Selkie.Services.Common.Dto;

namespace Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface IGeoJsonTextToSurveyGeoJsonFeatureDtosConverter
    {
        string GeoJson { get; set; }
        IEnumerable <SurveyGeoJsonFeatureDto> Dtos { get; }
        void Convert();
    }
}