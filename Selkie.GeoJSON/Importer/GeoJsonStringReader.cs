using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.Windsor;

namespace Selkie.GeoJSON.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class GeoJsonStringReader : IGeoJsonStringReader
    {
        private readonly ISelkieGeoJsonStringReader m_Reader;

        public GeoJsonStringReader([NotNull] ISelkieGeoJsonStringReader reader)
        {
            m_Reader = reader;
        }

        public FeatureCollection Read(string geoJsonText)
        {
            var featureCollection = m_Reader.Read <FeatureCollection>(geoJsonText);

            return featureCollection;
        }
    }
}