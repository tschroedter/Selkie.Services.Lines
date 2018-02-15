using System.Collections.Generic;
using JetBrains.Annotations;
using Core2.Selkie.Geometry.Surveying;
using Core2.Selkie.Services.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface ISurveyGeoJsonFeaturesToDtosConverter
    {
        [NotNull]
        IEnumerable <ISurveyGeoJsonFeature> Features { get; set; }

        [NotNull]
        IEnumerable <SurveyGeoJsonFeatureDto> Dtos { get; set; }

        void Convert();
    }
}