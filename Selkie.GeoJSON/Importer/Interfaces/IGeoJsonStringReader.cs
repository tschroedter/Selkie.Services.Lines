using JetBrains.Annotations;
using NetTopologySuite.Features;

namespace Selkie.GeoJSON.Importer.Interfaces
{
    public interface IGeoJsonStringReader
    {
        FeatureCollection Read([NotNull] string geoJsonText);
    }
}