using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.Geometry.Shapes;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.GeoJSON.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class FeaturesToLinesConverter : IFeaturesToLinesConverter
    {
        private readonly IFeatureToLineConverter[] m_Converters;
        private readonly ISelkieLogger m_Logger;

        public FeaturesToLinesConverter(
            [NotNull] ISelkieLogger logger,
            [NotNull] IFeatureToLineConverter[] converters)
        {
            m_Logger = logger;
            m_Converters = converters;

            FeatureCollection = new FeatureCollection();
            Lines = new ILine[0];
        }

        [NotNull]
        public FeatureCollection FeatureCollection { get; set; }

        [NotNull]
        public IEnumerable <ILine> Lines { get; private set; }

        public void Convert()
        {
            var lines = new List <ILine>();
            var id = 0;

            foreach ( IFeature feature in FeatureCollection.Features )
            {
                ConvertFeature(id++,
                               feature,
                               lines);
            }

            Lines = lines;
        }

        private void ConvertFeature(int id,
                                    [NotNull] IFeature feature,
                                    [NotNull] List <ILine> lines)
        {
            IFeatureToLineConverter converter = m_Converters.FirstOrDefault(x => x.CanConvert(feature));

            if ( converter != null )
            {
                converter.Feature = feature;
                converter.Convert(id);

                lines.Add(converter.Line);
            }
            else
            {
                m_Logger.Warn("Feature {0} is not supported!".Inject(feature));
            }
        }
    }
}