using System;
using GeoAPI.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.GeoJson.Importer.Interfaces;
using Point = NetTopologySuite.Geometries.Point;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    public class LineStringToLineConverter : IFeatureToLineConverter
    {
        public LineStringToLineConverter()
        {
            Feature = CreateFeaturePoint();
            Line = Geometry.Shapes.Line.Unknown;
        }

        private const int StartPointIndex = 0;
        private const int EndPointIndex = 1;

        internal Type CanConvertType = typeof( LineString );

        public bool CanConvert(IFeature feature)
        {
            if ( CanConvertType != feature.Geometry.GetType() )
            {
                return false;
            }

            IGeometry geometry = feature.Geometry as LineString;

            return geometry != null && geometry.NumPoints == 2;
        }

        public void Convert(int id)
        {
            if ( !CanConvert(Feature) )
            {
                Line = Geometry.Shapes.Line.Unknown;

                return;
            }

            Line = ConvertFeatureToLine(id);
        }

        public IFeature Feature { get; set; }

        public ILine Line { get; private set; }

        private static Feature CreateFeaturePoint()
        {
            var coordinate = new Coordinate(0.0,
                                            0.0);

            var point = new Point(coordinate);

            var attributesTable = new AttributesTable();

            return new Feature(point,
                               attributesTable);
        }

        private ILine ConvertFeatureToLine(int id)
        {
            IGeometry geometry = ( LineString ) Feature.Geometry;

            Coordinate start = geometry.Coordinates [ StartPointIndex ];
            Coordinate end = geometry.Coordinates [ EndPointIndex ];

            var line = new Line(id,
                                start.X,
                                start.Y,
                                end.X,
                                end.Y);

            return line;
        }
    }
}