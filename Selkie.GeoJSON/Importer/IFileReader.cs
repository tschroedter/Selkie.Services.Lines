using GeoJSON.Net.Feature;

namespace Selkie.GeoJson.Importer
{
    public interface IFileReader
    {
        FeatureCollection Read(string filename); // todo maybe IFeatureCollection
    }
}