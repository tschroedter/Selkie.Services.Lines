using System.Collections.Generic;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Surveying;

namespace Selkie.Services.Lines.Interfaces.GeoJson.Importer
{
    public interface IImporter
    {
        FeatureCollection FeatureCollection { get; }
        IEnumerable <ISurveyGeoJsonFeature> Features { get; }
        void FromText([NotNull] string text);
    }
}