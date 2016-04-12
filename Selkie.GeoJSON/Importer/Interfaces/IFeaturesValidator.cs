using NetTopologySuite.Features;

namespace Selkie.GeoJSON.Importer.Interfaces
{
    public interface IFeaturesValidator
    {
        FeatureCollection Unsupported { get; set; }
        FeatureCollection Features { get; set; }
        FeatureCollection Supported { get; set; }
        void Validate();
    }
}