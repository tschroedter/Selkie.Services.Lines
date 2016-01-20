using Castle.Core;
using GeoJSON.Net.Feature;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Windsor;

namespace Selkie.GeoJson.Importer
{
    [Interceptor(typeof ( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class Importer : IImporter
    {
        private readonly IFeaturesValidator m_Converter;
        private readonly IFileReader m_Reader;

        public Importer([NotNull] IFileReader reader,
                        [NotNull] IFeaturesValidator converter)
        {
            m_Reader = reader;
            m_Converter = converter;
        }

        public FeatureCollection Features
        {
            get
            {
                return m_Converter.FeaturesValid;
            }
        }

        public void FromFile(string filename)
        {
            m_Converter.Features = m_Reader.Read(filename);
            m_Converter.Validate();
        }
    }
}