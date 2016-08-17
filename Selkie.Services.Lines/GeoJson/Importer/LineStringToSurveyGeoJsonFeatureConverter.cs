using System;
using GeoAPI.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.Services.Lines.Interfaces.GeoJson.Importer;
using Point = NetTopologySuite.Geometries.Point;

namespace Selkie.Services.Lines.GeoJson.Importer
{
    public class LineStringToSurveyGeoJsonFeatureConverter : IFeatureToSurveyGeoJsonFeatureConverter
    {
        public LineStringToSurveyGeoJsonFeatureConverter()
        {
            Feature = CreateFeaturePoint();
            SurveyGeoJsonFeature = Geometry.Surveying.SurveyGeoJsonFeature.Unknown;
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
                SurveyGeoJsonFeature = Geometry.Surveying.SurveyGeoJsonFeature.Unknown;

                return;
            }

            SurveyGeoJsonFeature = ConvertFeatureToSurveyGeoJsonFeature(id);
        }

        public IFeature Feature { get; set; }

        public ISurveyGeoJsonFeature SurveyGeoJsonFeature { get; private set; }

        private static Feature CreateFeaturePoint()
        {
            var coordinate = new Coordinate(0.0,
                                            0.0);

            var point = new Point(coordinate);

            var attributesTable = new AttributesTable();

            return new Feature(point,
                               attributesTable);
        }

        private ISurveyGeoJsonFeature ConvertFeatureToSurveyGeoJsonFeature(int id)
        {
            IGeometry geometry = ( LineString ) Feature.Geometry;

            Coordinate start = geometry.Coordinates [ StartPointIndex ];
            Coordinate end = geometry.Coordinates [ EndPointIndex ];

            var line = new Line(id,
                                start.X,
                                start.Y,
                                end.X,
                                end.Y);

            var data = new SurveyFeatureData(
                id,
                line.StartPoint,
                line.EndPoint,
                line.AngleToXAxisAtStartPoint,
                line.AngleToXAxisAtEndPoint,
                line.RunDirection,
                line.Length,
                line.IsUnknown);

            var surveyFeature = new SurveyFeature(data);

            var surveyGeoJsonFeature = new SurveyGeoJsonFeature(surveyFeature,
                                                                geometry.AsText());
            return surveyGeoJsonFeature;
        }
    }
}