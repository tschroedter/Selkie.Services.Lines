using System.Diagnostics.CodeAnalysis;
using GeoAPI.Geometries;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Lines.GeoJson.Importer;
using Selkie.Windsor;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.GeoJson.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    public sealed class FeaturesValidatorTests
    {
        private static FeatureCollection CreateSelkie()
        {
            return new FeatureCollection();
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_AddsValidFeaturesToSupportedWithCorrectLength_ForGivenFeatures(
            [NotNull] ISelkieLogger logger,
            [NotNull] FeatureCollection features,
            [NotNull] FeatureCollection supported,
            [NotNull] FeatureCollection unsupported)
        {
            // Arrange
            Feature one = CreateFeatureLineString();
            Feature two = CreateFeatureLineString();
            FeatureCollection testFeatures = CreateSelkie();

            var sut = new FeaturesValidator(logger,
                                            features,
                                            supported,
                                            unsupported);

            testFeatures.Add(one);
            testFeatures.Add(two);
            sut.FeatureCollection = testFeatures;

            // Act
            sut.Validate();

            // Assert
            Assert.Equal(2,
                         sut.Supported.Count);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_AddsSupportedFeature_ForGivenFeatures(
            [NotNull] ISelkieLogger logger,
            [NotNull] FeatureCollection features,
            [NotNull] FeatureCollection supported,
            [NotNull] FeatureCollection unsupported)

        {
            // Arrange
            Feature expected = CreateFeatureLineString();
            FeatureCollection testFeatures = CreateSelkie();

            var sut = new FeaturesValidator(logger,
                                            features,
                                            supported,
                                            unsupported);

            testFeatures.Add(expected);
            sut.FeatureCollection = testFeatures;

            // Act
            sut.Validate();

            // Assert
            Assert.Equal(expected,
                         sut.Supported [ 0 ]);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_AddsInvalidFeaturesToUnsupportedWithCorrectLength_ForGivenFeatures(
            [NotNull] ISelkieLogger logger,
            [NotNull] FeatureCollection features,
            [NotNull] FeatureCollection supported,
            [NotNull] FeatureCollection unsupported)
        {
            // Arrange
            Feature one = CreateFeaturePoint();
            Feature two = CreateFeaturePoint();
            FeatureCollection testFeatures = CreateSelkie();

            var sut = new FeaturesValidator(logger,
                                            features,
                                            supported,
                                            unsupported);

            testFeatures.Add(one);
            testFeatures.Add(two);
            sut.FeatureCollection = testFeatures;

            // Act
            sut.Validate();

            // Assert
            Assert.Equal(one,
                         sut.Unsupported [ 0 ]);
            Assert.Equal(two,
                         sut.Unsupported [ 1 ]);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_AddsInvalidAndValidFeatures_ForGivenFeatures(
            [NotNull] ISelkieLogger logger,
            [NotNull] FeatureCollection features,
            [NotNull] FeatureCollection supported,
            [NotNull] FeatureCollection unsupported)
        {
            // Arrange
            Feature one = CreateFeaturePoint();
            Feature two = CreateFeatureLineString();
            FeatureCollection testFeatures = CreateSelkie();

            testFeatures.Add(one);
            testFeatures.Add(two);

            var sut = new FeaturesValidator(logger,
                                            features,
                                            supported,
                                            unsupported)
                      {
                          FeatureCollection = testFeatures
                      };

            // Act
            sut.Validate();

            // Assert            
            Assert.Equal(one,
                         sut.Unsupported [ 0 ]);
            Assert.Equal(two,
                         sut.Supported [ 0 ]);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_AddsInvalidAndValidFeaturesCount_ForGivenFeatures(
            [NotNull] ISelkieLogger logger,
            [NotNull] FeatureCollection features,
            [NotNull] FeatureCollection supported,
            [NotNull] FeatureCollection unsupported)
        {
            // Arrange
            Feature one = CreateFeaturePoint();
            Feature two = CreateFeatureLineString();
            FeatureCollection testFeatures = CreateSelkie();

            testFeatures.Add(one);
            testFeatures.Add(two);

            var sut = new FeaturesValidator(logger,
                                            features,
                                            supported,
                                            unsupported)
                      {
                          FeatureCollection = testFeatures
                      };

            // Act
            sut.Validate();

            // Assert            
            Assert.Equal(1,
                         sut.Unsupported.Count);
            Assert.Equal(1,
                         sut.Supported.Count);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Features_IsNotNull_ByDefault([NotNull] FeaturesValidator sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.FeatureCollection);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FeaturesValid_IsNotNull_ByDefault([NotNull] FeaturesValidator sut)
        {
            // Arrange
            // Act
            // Assert
            Assert.NotNull(sut.Supported);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Validate_LogsInfo_ForLineString([NotNull] [Frozen] ISelkieLogger logger,
                                                    [NotNull] FeaturesValidator sut)
        {
            // Arrange
            FeatureCollection features = CreateSelkie();

            features.Add(CreateFeatureLineString());
            sut.FeatureCollection = features;

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
            FeatureCollection features = CreateSelkie();

            features.Add(CreateFeaturePoint());
            sut.FeatureCollection = features;

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
            FeatureCollection features = CreateSelkie();

            features.Add(CreateFeatureMultiPoint());
            sut.FeatureCollection = features;

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
            FeatureCollection features = CreateSelkie();

            features.Add(CreateFeatureMultiLineString());
            sut.FeatureCollection = features;

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
            FeatureCollection features = CreateSelkie();

            features.Add(CreateFeaturePolygon());
            sut.FeatureCollection = features;

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
            Feature feature = CreateFeaturePoint();
            FeatureCollection features = CreateSelkie();

            features.Add(feature);
            sut.FeatureCollection = features;

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
            FeatureCollection features = CreateSelkie();

            features.Add(CreateFeatureMultiPolygon());
            sut.FeatureCollection = features;

            // Act
            sut.Validate();

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        private Feature CreateFeatureMultiPolygon()
        {
            var linearRing = new LinearRing(CreateCoordinatesForPolygon());
            var polygon = new Polygon(linearRing);
            IPolygon[] polygons =
            {
                polygon
            };

            var multiPolygon = new MultiPolygon(polygons);
            var attributesTable = new AttributesTable();

            return new Feature(multiPolygon,
                               attributesTable);
        }

        private Feature CreateFeaturePolygon()
        {
            var linearRing = new LinearRing(CreateCoordinatesForPolygon());
            var polygon = new Polygon(linearRing);
            var attributesTable = new AttributesTable();

            return new Feature(polygon,
                               attributesTable);
        }

        private Coordinate[] CreateCoordinatesForPolygon()
        {
            var coordinates = new[]
                              {
                                  new Coordinate(1.0,
                                                 2.0),
                                  new Coordinate(3.0,
                                                 4.0),
                                  new Coordinate(5.0,
                                                 6.0),
                                  new Coordinate(1.0,
                                                 2.0)
                              };

            return coordinates;
        }

        private static ILineString[] CreateListOfLineStrings()
        {
            LineString lineString = CreateLineString();
            ILineString[] lineStrings =
            {
                lineString
            };

            return lineStrings;
        }

        private Feature CreateFeatureMultiLineString()
        {
            ILineString[] lineStrings = CreateListOfLineStrings();

            var multiLineString = new MultiLineString(lineStrings);
            var attributesTable = new AttributesTable();

            return new Feature(multiLineString,
                               attributesTable);
        }

        private Feature CreateFeatureLineString()
        {
            LineString lineString = CreateLineString();

            var attributesTable = new AttributesTable();

            return new Feature(lineString,
                               attributesTable);
        }

        private static LineString CreateLineString()
        {
            var start = new Coordinate(0.0,
                                       0.0);

            var end = new Coordinate(0.0,
                                     0.0);

            var coordinates = new[]
                              {
                                  start,
                                  end
                              };

            var lineString = new LineString(coordinates);

            return lineString;
        }

        private static Feature CreateFeaturePoint()
        {
            Point point = CreatePoint();

            var attributesTable = new AttributesTable();

            return new Feature(point,
                               attributesTable);
        }

        private static Point CreatePoint()
        {
            var coordinate = new Coordinate(0.0,
                                            0.0);

            var point = new Point(coordinate);
            return point;
        }


        private Feature CreateFeatureMultiPoint()
        {
            Point one = CreatePoint();
            Point two = CreatePoint();

            var multiPoint = new MultiPoint(new IPoint[]
                                            {
                                                one,
                                                two
                                            });

            var attributesTable = new AttributesTable();

            return new Feature(multiPoint,
                               attributesTable);
        }
    }
}