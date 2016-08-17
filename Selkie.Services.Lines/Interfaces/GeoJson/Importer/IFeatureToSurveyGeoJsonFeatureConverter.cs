using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Surveying;

namespace Selkie.Services.Lines.Interfaces.GeoJson.Importer
{
    public interface IFeatureToSurveyGeoJsonFeatureConverter
    {
        [NotNull]
        IFeature Feature { get; set; }

        [NotNull]
        ISurveyGeoJsonFeature SurveyGeoJsonFeature { get; }

        bool CanConvert(IFeature feature);

        void Convert(int id);
    }
}