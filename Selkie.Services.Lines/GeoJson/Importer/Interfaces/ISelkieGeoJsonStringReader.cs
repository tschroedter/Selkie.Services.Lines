namespace Selkie.Services.Lines.GeoJson.Importer.Interfaces
{
    public interface ISelkieGeoJsonStringReader
    {
        T Read <T>(string geoJsonText) where T : class;
    }
}