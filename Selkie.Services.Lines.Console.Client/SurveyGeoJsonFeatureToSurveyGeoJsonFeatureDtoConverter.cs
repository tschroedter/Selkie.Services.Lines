using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.Geometry;
using Selkie.Geometry.Primitives;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.Services.Common.Dto;

namespace Selkie.Services.Lines.Console.Client
{
    [ExcludeFromCodeCoverage]
    public class SurveyGeoJsonFeatureToSurveyGeoJsonFeatureDtoConverter
    {
        [NotNull]
        public static ISurveyGeoJsonFeature ConvertDtoToFeature([NotNull] SurveyGeoJsonFeatureDto dto)
        {
            SurveyFeatureData data = CreateSurveyFeatureData(dto);

            var surveyFeature = new SurveyFeature(data);
            var feature = new SurveyGeoJsonFeature(surveyFeature,
                                                   dto.SurveyFeatureAsGeoJson);

            return feature;
        }

        [NotNull]
        public static SurveyGeoJsonFeatureDto ConvertFeatureToDto([NotNull] ISurveyGeoJsonFeature feature)
        {
            SurveyFeatureDto surveyFeatureDto = CreateSurveyFeatureDto(feature);

            var dto = new SurveyGeoJsonFeatureDto
                      {
                          SurveyFeatureDto = surveyFeatureDto,
                          SurveyFeatureAsGeoJson = feature.SurveyFeatureAsGeoJson
                      };

            return dto;
        }

        private static SurveyFeatureData CreateSurveyFeatureData(SurveyGeoJsonFeatureDto dto)
        {
            SurveyFeatureDto surveyFeatureDto = dto.SurveyFeatureDto;

            Constants.LineDirection direction;

            Enum.TryParse(surveyFeatureDto.RunDirection,
                          out direction);

            PointDto dtoStartPoint = surveyFeatureDto.StartPoint;
            PointDto dtoEndPoint = surveyFeatureDto.EndPoint;
            var startPoint = new Point(dtoStartPoint.X,
                                       dtoStartPoint.Y);
            var endPoint = new Point(dtoEndPoint.X,
                                     dtoEndPoint.Y);
            Angle angleToXAxisAtStartPoint = Angle.FromDegrees(surveyFeatureDto.AngleToXAxisAtStartPoint);
            Angle angleToXAxisAtEndPoint = Angle.FromDegrees(surveyFeatureDto.AngleToXAxisAtEndPoint);
            var data = new SurveyFeatureData(surveyFeatureDto.Id,
                                             startPoint,
                                             endPoint,
                                             angleToXAxisAtStartPoint,
                                             angleToXAxisAtEndPoint,
                                             direction,
                                             surveyFeatureDto.Length,
                                             surveyFeatureDto.IsUnknown);
            return data;
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