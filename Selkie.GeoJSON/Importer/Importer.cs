using Castle.Core;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Aop.Aspects;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.Windsor;

namespace Selkie.GeoJSON.Importer
{
    [Interceptor(typeof ( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class Importer : IImporter
    {
        private readonly IFeaturesValidator m_FeaturesValidator;
        private readonly IFeaturesToLinesConverter m_FeaturesToLinesConverter;
        private readonly IGeoJsonStringReader m_Reader;

        public Importer([NotNull] IGeoJsonStringReader reader,
                        [NotNull] IFeaturesValidator featuresValidator,
                        [NotNull] IFeaturesToLinesConverter featuresToLinesConverter)
        {
            m_Reader = reader;
            m_FeaturesValidator = featuresValidator;
            m_FeaturesToLinesConverter = featuresToLinesConverter;
        }

        public FeatureCollection Features
        {
            get
            {
                return m_FeaturesValidator.Supported;
            }
        }

        public void FromText(string filename)
        {
            FeatureCollection featureCollection = m_Reader.Read(filename);

            m_FeaturesValidator.Features = featureCollection; // todo test
            m_FeaturesValidator.Validate();


        }
    }
}