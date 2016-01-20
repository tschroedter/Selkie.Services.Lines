using GeoJSON.Net.Feature;
using JetBrains.Annotations;

namespace Selkie.GeoJson.Importer
{
    public interface IFeaturesValidator
    {
        [NotNull]
        FeatureCollection Features { get; set; }

        [NotNull]
        FeatureCollection FeaturesValid { get; }

        void Validate();
    }
}