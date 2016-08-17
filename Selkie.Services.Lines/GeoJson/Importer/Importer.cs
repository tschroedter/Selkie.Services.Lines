using System.Collections.Generic;
using Castle.Core;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Aop.Aspects;
using Selkie.Geometry.Surveying;
using Selkie.Services.Lines.Interfaces.GeoJson.Importer;
using Selkie.Windsor;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    [Interceptor(typeof( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class Importer : IImporter
    {
        public Importer([NotNull] IGeoJsonStringReader reader,
                        [NotNull] IFeaturesValidator validator,
                        [NotNull] IFeaturesToISurveyGeoJsonFeaturesConverter converter)
        {
            m_Reader = reader;
            m_Validator = validator;
            m_Converter = converter;
        }

        private readonly IFeaturesToISurveyGeoJsonFeaturesConverter m_Converter;
        private readonly IGeoJsonStringReader m_Reader;
        private readonly IFeaturesValidator m_Validator;

        public FeatureCollection FeatureCollection
        {
            get
            {
                return m_Validator.Supported;
            }
        }

        public IEnumerable <ISurveyGeoJsonFeature> Features
        {
            get
            {
                return m_Converter.Features;
            }
        }

        public void FromText(string text)
        {
            FeatureCollection featureCollection = m_Reader.Read(text);

            m_Validator.FeatureCollection = featureCollection;
            m_Validator.Validate();

            m_Converter.FeatureCollection = m_Validator.FeatureCollection;
            m_Converter.Convert();
        }
    }
}