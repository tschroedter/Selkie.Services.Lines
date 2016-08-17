namespace Selkie.Services.Lines.Interfaces.GeoJson.Importer
{
    public interface ISelkieGeoJsonStringReader
    {
        T Read <T>(string geoJsonText) where T : class;
    }
}