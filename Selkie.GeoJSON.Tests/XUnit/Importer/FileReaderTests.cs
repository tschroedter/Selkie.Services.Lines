using System.Diagnostics.CodeAnalysis;
using GeoJSON.Net.Feature;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.GeoJson.Importer;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.GeoJson.Tests.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class FileReaderTests
    {
        private const string JsonWithThreeFeatures =
            @"{ 
                'type': 'FeatureCollection',
                'crs': 
                 {
                    'type': 'name','properties': 
                    {
                        'name': 'urn:ogc:def:crs:OGC:1.3:CRS84'
                    }
                },
                'features': [
                { 
                    'type': 'Feature',
                    'geometry': 
                    {
                        'type': 'Point', 
                        'coordinates': [102.0, 0.5]
                    },
                    'properties': {'prop0': 'value0'}
                },
                { 
                    'type': 'Feature',
                    'geometry': 
                    {
                        'type': 'LineString',
                        'coordinates': 
                        [
                            [102.0, 0.0], 
                            [103.0, 1.0], 
                            [104.0, 0.0], 
                            [105.0, 1.0]
                        ]
                    },
                    'properties': 
                    {
                      'prop0': 'value0',
                      'prop1': 0.0
                    }
                },
                { 
                    'type': 'Feature',
                    'geometry': 
                    {
                        'type': 'Polygon',
                        'coordinates': 
                        [
                            [
                                [100.0, 0.0], 
                                [101.0, 0.0], 
                                [101.0, 1.0],
                                [100.0, 1.0], 
                                [100.0, 0.0] 
                            ]
                        ]
                    },
                    'properties': 
                    {
                        'prop0': 'value0',
                        'prop1': {'this': 'that'}
                    }
                }
            ]
        }";

        [Theory]
        [AutoNSubstituteData]
        public void Read_CallsReader_WhenCalled([NotNull] [Frozen] ISelkieStreamReader reader,
                                                [NotNull] FileReader sut)
        {
            // Arrange
            reader.ReadToEnd("Filename").Returns(JsonWithThreeFeatures);

            // Act
            sut.Read("Filename");

            // Assert
            reader.Received().ReadToEnd("Filename");
        }

        [Theory]
        [AutoNSubstituteData]
        public void ReadReturnsFeatures_WhenCalled([NotNull] [Frozen] ISelkieStreamReader reader,
                                                   [NotNull] FileReader sut)
        {
            // Arrange
            reader.ReadToEnd("Filename").Returns(JsonWithThreeFeatures);

            // Act
            FeatureCollection actual = sut.Read("Filename");

            // Assert
            Assert.Equal(3, actual.Features.Count);
        }
    }
}