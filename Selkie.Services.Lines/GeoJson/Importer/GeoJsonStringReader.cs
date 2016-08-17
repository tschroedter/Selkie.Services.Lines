using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Services.Lines.Interfaces.GeoJson.Importer;
using Selkie.Windsor;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class GeoJsonStringReader : IGeoJsonStringReader
    {
        public GeoJsonStringReader([NotNull] ISelkieGeoJsonStringReader reader)
        {
            m_Reader = reader;
        }

        private readonly ISelkieGeoJsonStringReader m_Reader;

        public FeatureCollection Read(string geoJsonText)
        {
            var featureCollection = m_Reader.Read <FeatureCollection>(geoJsonText);

            return featureCollection;
        }
    }
}