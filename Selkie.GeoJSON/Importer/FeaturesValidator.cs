using GeoJSON.Net;
using GeoJSON.Net.Feature;
using JetBrains.Annotations;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.GeoJson.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class FeaturesValidator : IFeaturesValidator
    {
        private readonly ISelkieLogger m_Logger;

        public FeaturesValidator([NotNull] ISelkieLogger logger)
        {
            m_Logger = logger;

            Features = new FeatureCollection();
            FeaturesValid = new FeatureCollection();
        }

        public FeatureCollection Features { get; set; }

        public FeatureCollection FeaturesValid { get; set; }

        public void Validate()
        {
            foreach ( Feature feature in Features.Features ) // todo name Features.Features
            {
                if ( IsFeatureSupported(feature) )
                {
                    FeaturesValid.Features.Add(feature);
                }
            }
        }

        private bool IsFeatureSupported(Feature feature)
        {
            GeoJSONObjectType geometryType = feature.Geometry.Type;

            switch ( geometryType )
            {
                case GeoJSONObjectType.LineString:
                    m_Logger.Info("The GeoJSONObjectType '{0}' is supported!".Inject(geometryType));
                    return true;
                case GeoJSONObjectType.Point:
                case GeoJSONObjectType.MultiPoint:
                case GeoJSONObjectType.MultiLineString:
                case GeoJSONObjectType.Polygon:
                case GeoJSONObjectType.MultiPolygon:
                case GeoJSONObjectType.GeometryCollection:
                case GeoJSONObjectType.Feature:
                case GeoJSONObjectType.FeatureCollection:
                    m_Logger.Warn("The GeoJSONObjectType '{0}' is not supported!".Inject(geometryType));
                    return false;

                default:
                    m_Logger.Error("Can't handle GeoJSONObjectType '{0}'!".Inject(geometryType));
                    return false;
            }
        }
    }
}