using JetBrains.Annotations;

namespace Selkie.GeoJson.Importer
{
    public interface ISelkieStreamReader
    {
        string ReadToEnd([NotNull] string filename);
    }
}