using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Shapes;

namespace Selkie.Services.Lines.GeoJson.Importer.Interfaces
{
    public interface IFeatureToLineConverter
    {
        [NotNull]
        IFeature Feature { get; set; }

        [NotNull]
        ILine Line { get; }

        bool CanConvert(IFeature feature);

        void Convert(int id);
    }
}