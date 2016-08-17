using System.Collections.Generic;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Surveying;

namespace Selkie.Services.Lines.Interfaces.GeoJson.Importer
{
    public interface IFeaturesToISurveyGeoJsonFeaturesConverter
    {
        [NotNull]
        FeatureCollection FeatureCollection { get; set; }

        [NotNull]
        IEnumerable <ISurveyGeoJsonFeature> Features { get; }

        void Convert();
    }
}