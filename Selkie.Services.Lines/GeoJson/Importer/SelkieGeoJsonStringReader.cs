using System.Diagnostics.CodeAnalysis;
using NetTopologySuite.IO;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Selkie.Windsor;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public class SelkieGeoJsonStringReader : ISelkieGeoJsonStringReader
    {
        public SelkieGeoJsonStringReader()
        {
            m_Reader = new GeoJsonReader();
        }

        private readonly GeoJsonReader m_Reader;

        public T Read <T>(string geoJsonText) where T : class
        {
            return m_Reader.Read <T>(geoJsonText);
        }
    }
}