using System.Collections.Generic;
using Core2.Selkie.Services.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface IGeoJsonTextToSurveyGeoJsonFeatureDtosConverter
    {
        string GeoJson { get; set; }
        IEnumerable <SurveyGeoJsonFeatureDto> Dtos { get; }
        void Convert();
    }
}