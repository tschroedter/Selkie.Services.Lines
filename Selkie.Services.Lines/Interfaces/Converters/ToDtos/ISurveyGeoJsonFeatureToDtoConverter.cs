using Selkie.Geometry.Surveying;
using Selkie.Services.Common.Dto;

namespace Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface ISurveyGeoJsonFeatureToDtoConverter
    {
        ISurveyGeoJsonFeature Feature { get; set; }
        SurveyGeoJsonFeatureDto Dto { get; set; }
        void Convert(int i);
    }
}