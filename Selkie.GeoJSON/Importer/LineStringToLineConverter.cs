using System;
using GeoAPI.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.Geometry.Shapes;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;
using Point = NetTopologySuite.Geometries.Point;

namespace Selkie.GeoJSON.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class LineStringToLineConverter : IFeatureToLineConverter
    {
        private const int StartPointIndex = 0;
        private const int EndPointIndex = 1;
        private const double Tolerance = 0.000001;

        internal Type CanConvertType = typeof ( LineString );

        public LineStringToLineConverter()
        {
            Feature = CreateFeaturePoint();
            Line = Geometry.Shapes.Line.Unknown;
        }

        public bool CanConvert(IFeature feature)
        {
            if ( CanConvertType != feature.Geometry.GetType() )
            {
                return false;
            }

            IGeometry geometry = feature.Geometry as LineString;

            if ( geometry == null ||
                 geometry.NumPoints != 2 )
            {
                return false;
            }

            return true;
        }

        public void Convert(int id)
        {
            if ( !CanConvert(Feature) )
            {
                Line = Geometry.Shapes.Line.Unknown;

                return; // todo maybe throw exception
            }

            Line = ConvertFeatureToLine(id);
        }

        public IFeature Feature { get; set; }

        public ILine Line { get; private set; }

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

            ValidateCreateLine(line,
                               geometry);

            return line;
        }

        private void ValidateCreateLine(Line line,
                                        IGeometry geometry)
        {
            if ( Math.Abs(line.Length - geometry.Length) > Tolerance )
            {
                throw new Exception("Length is for line is incorrect!" +
                                    " - Expected: {0} Actual: {1}".Inject(geometry.Length,
                                                                          Line.Length)
                                    + Environment.NewLine + " Line: {0}".Inject(line.ToString())
                                    + Environment.NewLine + " GeoJsonLine: {0}".Inject(geometry.ToString()));
            }
        }

        private static Feature CreateFeaturePoint()
        {
            var coordinate = new Coordinate(0.0,
                                            0.0);

            var point = new Point(coordinate);

            var attributesTable = new AttributesTable();

            return new Feature(point,
                               attributesTable);
        }
    }
}