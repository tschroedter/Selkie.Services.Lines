using Core2.Selkie.Geometry.Surveying;
using Core2.Selkie.Services.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface ISurveyGeoJsonFeatureToDtoConverter
    {
        ISurveyGeoJsonFeature Feature { get; set; }
        SurveyGeoJsonFeatureDto Dto { get; set; }
        void Convert(int i);
    }
}