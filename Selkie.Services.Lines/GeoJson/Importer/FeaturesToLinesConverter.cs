using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Selkie.Windsor;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class FeaturesToLinesConverter : IFeaturesToLinesConverter
    {
        public FeaturesToLinesConverter(
            [NotNull] IFeatureToLineConverter[] converters)
        {
            m_Converters = converters;

            FeatureCollection = new FeatureCollection();
            Lines = new ILine[0];
        }

        private readonly IFeatureToLineConverter[] m_Converters;

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
                ILine line = ConvertFeature(id,
                                            feature);

                if ( line.IsUnknown )
                {
                    continue;
                }

                lines.Add(line);
                id++;
            }

            Lines = lines;
        }

        private ILine ConvertFeature(int id,
                                     [NotNull] IFeature feature)
        {
            IFeatureToLineConverter converter = m_Converters.FirstOrDefault(x => x.CanConvert(feature));

            if ( converter == null )
            {
                return Line.Unknown;
            }

            converter.Feature = feature;
            converter.Convert(id);

            return converter.Line;
        }
    }
}