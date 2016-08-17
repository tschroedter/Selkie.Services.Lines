using Selkie.Geometry.Surveying;
using Selkie.Services.Common.Dto;
using Selkie.Services.Lines.Interfaces.Converters.ToDtos;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Converters.ToDtos
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SurveyGeoJsonFeatureToDtoConverter
        : ISurveyGeoJsonFeatureToDtoConverter
    {
        public SurveyGeoJsonFeatureToDtoConverter()
        {
            Feature = SurveyGeoJsonFeature.Unknown;
            Dto = new SurveyGeoJsonFeatureDto();
        }

        public ISurveyGeoJsonFeature Feature { get; set; }
        public SurveyGeoJsonFeatureDto Dto { get; set; }

        public void Convert(int i)
        {
            SurveyFeatureDto surveyFeatureDto = CreateSurveyFeatureDto(Feature);
            string geoJson = Feature.SurveyFeatureAsGeoJson;

            var dto = new SurveyGeoJsonFeatureDto
                      {
                          SurveyFeatureDto = surveyFeatureDto,
                          SurveyFeatureAsGeoJson = geoJson
                      };

            Dto = dto;
        }

        private static SurveyFeatureDto CreateSurveyFeatureDto(ISurveyGeoJsonFeature surveyGeoJsonFeature)
        {
            ISurveyFeature feature = surveyGeoJsonFeature.SurveyFeature;

            var endPoint = new PointDto
                           {
                               X = feature.EndPoint.X,
                               Y = feature.EndPoint.Y
                           };

            var startPoint = new PointDto
                             {
                                 X = feature.StartPoint.X,
                                 Y = feature.StartPoint.Y
                             };

            var surveyFeatureDto = new SurveyFeatureDto
                                   {
                                       EndPoint = endPoint,
                                       RunDirection = feature.RunDirection.ToString(),
                                       StartPoint = startPoint,
                                       AngleToXAxisAtEndPoint = feature.AngleToXAxisAtEndPoint.Degrees,
                                       AngleToXAxisAtStartPoint = feature.AngleToXAxisAtStartPoint.Degrees,
                                       Id = feature.Id,
                                       IsUnknown = feature.IsUnknown,
                                       Length = feature.Length
                                   };

            return surveyFeatureDto;
        }
    }
}