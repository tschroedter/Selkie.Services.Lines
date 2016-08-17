using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Geometry.Surveying;
using Selkie.Services.Common.Dto;
using Selkie.Services.Lines.Interfaces.Converters.ToDtos;
using Selkie.Services.Lines.Interfaces.GeoJson.Importer;
using Selkie.Services.Lines.Interfaces.Validators;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Converters.ToDtos
{
    [ProjectComponent(Lifestyle.Transient)]
    public class GeoJsonTextToSurveyGeoJsonFeatureDtosConverter : IGeoJsonTextToSurveyGeoJsonFeatureDtosConverter
    {
        public GeoJsonTextToSurveyGeoJsonFeatureDtosConverter(
            [NotNull] IImporter importer,
            [NotNull] ISurveyGeoJsonFeaturesToDtosConverter converter,
            [NotNull] IFeatureValidator validator)
        {
            m_Importer = importer;
            m_Converter = converter;
            m_Validator = validator;

            GeoJson = string.Empty;
            Dtos = new SurveyGeoJsonFeatureDto[0];
        }

        private readonly ISurveyGeoJsonFeaturesToDtosConverter m_Converter;
        private readonly IImporter m_Importer;
        private readonly IFeatureValidator m_Validator;

        [NotNull]
        public string GeoJson { get; set; }

        [NotNull]
        public IEnumerable <SurveyGeoJsonFeatureDto> Dtos { get; private set; }

        public void Convert()
        {
            ISurveyGeoJsonFeature[] features = ImportFromText(GeoJson).ToArray();

            ValidateFeatures(features);

            Dtos = ConvertFeaturesToDtos(features);
        }

        private IEnumerable <SurveyGeoJsonFeatureDto> ConvertFeaturesToDtos(
            [NotNull] IEnumerable <ISurveyGeoJsonFeature> features)
        {
            m_Converter.Features = features;
            m_Converter.Convert();

            return m_Converter.Dtos;
        }

        private IEnumerable <ISurveyGeoJsonFeature> ImportFromText([NotNull] string text)
        {
            m_Importer.FromText(text);

            return m_Importer.Features;
        }

        private void ValidateFeatures(IEnumerable <ISurveyGeoJsonFeature> features)
        {
            bool isValidateLines = m_Validator.ValidateFeatures(features);

            if ( !isValidateLines )
            {
                throw new ArgumentException("Provided lines are invalid!");
            }
        }
    }
}