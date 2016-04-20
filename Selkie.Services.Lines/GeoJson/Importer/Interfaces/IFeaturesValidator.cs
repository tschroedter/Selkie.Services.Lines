using NetTopologySuite.Features;

namespace Selkie.Services.Lines.GeoJson.Importer.Interfaces
{
    public interface IFeaturesValidator
    {
        FeatureCollection Unsupported { get; set; }
        FeatureCollection FeatureCollection { get; set; }
        FeatureCollection Supported { get; set; }
        void Validate();
    }
}