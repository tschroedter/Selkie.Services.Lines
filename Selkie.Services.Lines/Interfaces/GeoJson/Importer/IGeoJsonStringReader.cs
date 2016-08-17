using JetBrains.Annotations;
using NetTopologySuite.Features;

namespace Selkie.Services.Lines.Interfaces.GeoJson.Importer
{
    public interface IGeoJsonStringReader
    {
        FeatureCollection Read([NotNull] string geoJsonText);
    }
}