using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Shapes;

namespace Selkie.GeoJSON.Importer.Interfaces
{
    public interface IFeatureToLineConverter
    {
        [NotNull]
        IFeature Feature { get; set; }

        [NotNull]
        ILine Line { get; }

        bool CanConvert([NotNull] IFeature feature);
        void Convert(int id);
    }
}