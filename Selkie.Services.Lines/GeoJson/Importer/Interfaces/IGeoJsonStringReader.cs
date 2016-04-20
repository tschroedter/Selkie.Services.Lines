using JetBrains.Annotations;
using NetTopologySuite.Features;

namespace Selkie.Services.Lines.GeoJson.Importer.Interfaces
{
    public interface IGeoJsonStringReader
    {
        FeatureCollection Read([NotNull] string geoJsonText);
    }
}