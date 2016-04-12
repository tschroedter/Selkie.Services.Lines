using System.Diagnostics.CodeAnalysis;
using NetTopologySuite.IO;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.Windsor;

namespace Selkie.GeoJSON.Importer
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public class SelkieGeoJsonStringReader : ISelkieGeoJsonStringReader
    {
        private readonly GeoJsonReader m_Reader;

        public SelkieGeoJsonStringReader()
        {
            m_Reader = new GeoJsonReader();
        }

        public T Read <T>(string geoJsonText) where T : class
        {
            return m_Reader.Read <T>(geoJsonText);
        }
    }
}