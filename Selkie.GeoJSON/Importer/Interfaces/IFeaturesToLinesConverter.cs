using System.Collections.Generic;
using NetTopologySuite.Features;
using Selkie.Geometry.Shapes;

namespace Selkie.GeoJSON.Importer.Interfaces
{
    public interface IFeaturesToLinesConverter
    {
        FeatureCollection FeatureCollection { get; set; }
        IEnumerable <ILine> Lines { get; }
        void Convert();
    }
}