using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Surveying;
using Selkie.Services.Common.Dto;
using Selkie.Services.Lines.Interfaces.Converters.ToDtos;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Converters.ToDtos
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SurveyGeoJsonFeaturesToDtosConverter
        : ISurveyGeoJsonFeaturesToDtosConverter
    {
        public SurveyGeoJsonFeaturesToDtosConverter(
            [NotNull] ISurveyGeoJsonFeatureToDtoConverter converter)
        {
            m_Converter = converter;
            Features = new ISurveyGeoJsonFeature[0];
            Dtos = new SurveyGeoJsonFeatureDto[0];
        }

        private readonly ISurveyGeoJsonFeatureToDtoConverter m_Converter;
        public IEnumerable <ISurveyGeoJsonFeature> Features { get; set; }
        public IEnumerable <SurveyGeoJsonFeatureDto> Dtos { get; set; }

        public void Convert()
        {
            var nextId = 0;

            var dtos = new List <SurveyGeoJsonFeatureDto>();

            foreach ( ISurveyGeoJsonFeature feature in Features )
            {
                m_Converter.Feature = feature;
                m_Converter.Convert(nextId++);

                dtos.Add(m_Converter.Dto);
            }

            Dtos = dtos;
        }
    }
}