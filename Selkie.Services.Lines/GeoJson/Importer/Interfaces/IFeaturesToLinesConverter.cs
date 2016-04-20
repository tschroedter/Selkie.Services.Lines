using System.Collections.Generic;
using NetTopologySuite.Features;
using Selkie.Geometry.Shapes;

namespace Selkie.Services.Lines.GeoJson.Importer.Interfaces
{
    public interface IFeaturesToLinesConverter
    {
        FeatureCollection FeatureCollection { get; set; }
        IEnumerable <ILine> Lines { get; }
        void Convert();
    }
}