using NetTopologySuite.Features;

namespace Selkie.GeoJSON.Importer.Interfaces
{
    public interface IImporter
    {
        FeatureCollection Features { get; }
        void FromText(string filename);
    }
}