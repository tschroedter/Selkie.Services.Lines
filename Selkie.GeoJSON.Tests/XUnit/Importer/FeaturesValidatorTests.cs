using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.GeoJson.Importer;
using Selkie.Windsor;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.GeoJson.Tests.XUnit.Importer
{
    public sealed class FeaturesValidatorTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Validate_AddsValidFeatures_ForFeatures_Count([NotNull] FeaturesValidator sut)
        {
            // Arrange
            var features = new FeatureCollection();
            features.Features.Add(CreateFeaturePoint());
            features.Features.Add(CreateFeatureLineString());
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            Assert.Equal(1, sut.FeaturesValid.Features.Count);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_AddsValidFeatures_ForFeatures_Feature([NotNull] FeaturesValidator sut)
        {
            // Arrange
            Feature lineString = CreateFeatureLineString();
            Feature point = CreateFeaturePoint();
            var features = new FeatureCollection();
            features.Features.Add(point);
            features.Features.Add(lineString);
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            Feature actual = sut.FeaturesValid.Features.First();

            Assert.Equal(lineString, actual);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Features_IsNotNull_ByDefault([NotNull] FeaturesValidator sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Features);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FeaturesValid_IsNotNull_ByDefault([NotNull] FeaturesValidator sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.FeaturesValid);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsInfo_ForLineString([NotNull] [Frozen] ISelkieLogger logger,
                                                    [NotNull] FeaturesValidator sut)
        {
            // Arrange
            var features = new FeatureCollection();
            features.Features.Add(CreateFeatureLineString());
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Info(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsWarn_ForPoint([NotNull] [Frozen] ISelkieLogger logger,
                                               [NotNull] FeaturesValidator sut)
        {
            // Arrange
            var features = new FeatureCollection();
            features.Features.Add(CreateFeaturePoint());
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsWarn_ForMultiPoint([NotNull] [Frozen] ISelkieLogger logger,
                                                    [NotNull] FeaturesValidator sut)
        {
            // Arrange
            var features = new FeatureCollection();
            features.Features.Add(CreateFeatureMultiPoint());
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsWarn_ForMultiLineString([NotNull] [Frozen] ISelkieLogger logger,
                                                         [NotNull] FeaturesValidator sut)
        {
            // Arrange
            var features = new FeatureCollection();
            features.Features.Add(CreateFeatureMultiLineString());
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsWarn_ForPolygon([NotNull] [Frozen] ISelkieLogger logger,
                                                 [NotNull] FeaturesValidator sut)
        {
            // Arrange
            var features = new FeatureCollection();
            features.Features.Add(CreateFeaturePolygon());
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsWarn_ForGeometryCollection([NotNull] [Frozen] ISelkieLogger logger,
                                                            [NotNull] FeaturesValidator sut)
        {
            // Arrange
            var collection = new GeometryCollection();
            var feature = new Feature(collection);
            var features = new FeatureCollection();
            features.Features.Add(feature);
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsWarn_ForMultiPolygon([NotNull] [Frozen] ISelkieLogger logger,
                                                      [NotNull] FeaturesValidator sut)
        {
            // Arrange
            var features = new FeatureCollection();
            features.Features.Add(CreateFeatureMultiPolygon());
            sut.Features = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        private Feature CreateFeatureMultiPolygon()
        {
            var polygon = new Polygon(CreateListOfLineStringsForPolygon());
            var polygons = new[]
                           {
                               polygon
                           };

            var multiPolygon = new MultiPolygon(polygons.ToList());

            return new Feature(multiPolygon);
        }

        private Feature CreateFeaturePolygon()
        {
            List <LineString> lineStrings = CreateListOfLineStringsForPolygon();

            var polygon = new Polygon(lineStrings);

            return new Feature(polygon);
        }

        private List <LineString> CreateListOfLineStringsForPolygon()
        {
            var positions = new[]
                            {
                                new GeographicPosition(100.0,
                                                       0.0,
                                                       0.0),
                                new GeographicPosition(101.0,
                                                       0.0,
                                                       0.0),
                                new GeographicPosition(101.0,
                                                       1.0,
                                                       0.0),
                                new GeographicPosition(100.0,
                                                       1.0,
                                                       0.0),
                                new GeographicPosition(100.0,
                                                       0.0,
                                                       0.0)
                            };

            var lineString = new LineString(positions);

            return new[]
                   {
                       lineString
                   }.ToList();
        }

        private static List <LineString> CreateListOfLineStrings()
        {
            LineString lineString = CreateLineString();
            LineString[] lineStrings =
            {
                lineString
            };

            return lineStrings.ToList();
        }

        private Feature CreateFeatureMultiLineString()
        {
            List <LineString> lineStrings = CreateListOfLineStrings();

            var multiLineString = new MultiLineString(lineStrings.ToList());

            return new Feature(multiLineString);
        }

        private Feature CreateFeatureLineString()
        {
            LineString lineString = CreateLineString();

            return new Feature(lineString);
        }

        private static LineString CreateLineString()
        {
            var positionStart = new GeographicPosition(0.0,
                                                       0.0,
                                                       0.0);

            var positionEnd = new GeographicPosition(0.0,
                                                     0.0,
                                                     0.0);

            var positions = new[]
                            {
                                positionStart,
                                positionEnd
                            };

            var lineString = new LineString(positions);
            return lineString;
        }

        private static Feature CreateFeaturePoint()
        {
            var position = new GeographicPosition(0.0,
                                                  0.0,
                                                  0.0);
            var point = new Point(position);

            return new Feature(point);
        }

        private Feature CreateFeatureMultiPoint()
        {
            var multiPoint = new MultiPoint();

            return new Feature(multiPoint);
        }
    }
}