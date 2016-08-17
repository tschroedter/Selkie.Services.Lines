using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Surveying;
using Selkie.Services.Lines.Interfaces.GeoJson.Importer;
using Selkie.Windsor;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class FeaturesToISurveyGeoJsonFeaturesConverter : IFeaturesToISurveyGeoJsonFeaturesConverter
    {
        public FeaturesToISurveyGeoJsonFeaturesConverter(
            [NotNull] IFeatureToSurveyGeoJsonFeatureConverter[] converters)
        {
            m_Converters = converters;

            FeatureCollection = new FeatureCollection();
            Features = new ISurveyGeoJsonFeature[0];
        }

        private readonly IFeatureToSurveyGeoJsonFeatureConverter[] m_Converters;

        public FeatureCollection FeatureCollection { get; set; }

        public IEnumerable <ISurveyGeoJsonFeature> Features { get; private set; }

        public void Convert()
        {
            var geoJsonFeatures = new List <ISurveyGeoJsonFeature>();
            var id = 0;

            foreach ( IFeature feature in FeatureCollection.Features )
            {
                ISurveyGeoJsonFeature geoJsonFeature = ConvertFeature(id,
                                                                      feature);

                if ( geoJsonFeature.IsUnknown )
                {
                    continue;
                }

                geoJsonFeatures.Add(geoJsonFeature);
                id++;
            }

            Features = geoJsonFeatures;
        }

        private ISurveyGeoJsonFeature ConvertFeature(
            int id,
            [NotNull] IFeature feature)
        {
            IFeatureToSurveyGeoJsonFeatureConverter converter = m_Converters.FirstOrDefault(x => x.CanConvert(feature));

            if ( converter == null )
            {
                return SurveyGeoJsonFeature.Unknown;
            }

            converter.Feature = feature;
            converter.Convert(id);

            return converter.SurveyGeoJsonFeature;
        }
    }
}