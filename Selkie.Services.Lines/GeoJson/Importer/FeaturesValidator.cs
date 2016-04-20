using System;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class FeaturesValidator
        : IFeaturesValidator
    {
        private readonly ISelkieLogger m_Logger;

        public FeaturesValidator([NotNull] ISelkieLogger logger,
                                 [NotNull] FeatureCollection featureCollection,
                                 [NotNull] FeatureCollection supported,
                                 [NotNull] FeatureCollection unsupported)
        {
            m_Logger = logger;

            FeatureCollection = featureCollection;
            Supported = supported;
            Unsupported = unsupported;
        }

        public FeatureCollection Unsupported { get; set; }

        public FeatureCollection FeatureCollection { get; set; }

        public FeatureCollection Supported { get; set; }

        public void Validate()
        {
            Supported.Features.Clear();
            Unsupported.Features.Clear();

            foreach ( IFeature feature in FeatureCollection.Features )
            {
                if ( IsFeatureSupported(feature) )
                {
                    Supported.Add(feature);
                }
                else
                {
                    Unsupported.Add(feature);
                }
            }
        }

        private bool IsFeatureSupported(IFeature feature)
        {
            Type type = feature.Geometry.GetType();

            if ( typeof ( LineString ) == type )
            {
                m_Logger.Info("The GeoJSONObjectType '{0}' is supported!".Inject(type));
                return true;
            }

            m_Logger.Warn("The GeoJSONObjectType '{0}' is not supported!".Inject(type));

            return false;
        }
    }
}