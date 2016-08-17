using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Surveying;
using Selkie.Services.Common.Dto;

namespace Selkie.Services.Lines.Interfaces.Converters.ToDtos
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