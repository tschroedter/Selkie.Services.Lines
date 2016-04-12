using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.GeoJSON.Importer;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.XUnit.Extensions;
using Xunit;
using Xunit.Extensions;

namespace Selkie.GeoJson.Tests.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public sealed class GeoJsonStringReaderTests
    {
        private const string GeoJsonExample =
            @"{""type"":""FeatureCollection"",""features"":[{""type"":""Feature"",""properties"":{},""geometry"":{""type"":""Polygon"",""coordinates"":[[[-2.537841796875,53.50111704294316],[-2.537841796875,54.226707764386695],[-1.0986328125,54.226707764386695],[-1.0986328125,53.50111704294316],[-2.537841796875,53.50111704294316]]]}},{""type"":""Feature"",""properties"":{},""geometry"":{""type"":""Polygon"",""coordinates"":[[[-2.724609375,52.34205163638784],[-2.724609375,53.08082737207479],[-0.9667968749999999,53.08082737207479],[-0.9667968749999999,52.34205163638784],[-2.724609375,52.34205163638784]]]}}]}";

        [Theory]
        [AutoNSubstituteData]
        public void Read_CallsReader_WhenCalled()
        {
            // Arrange
            var reader = new SelkieGeoJsonStringReader();
            var sut = new GeoJsonStringReader(reader);

            // Act
            FeatureCollection actual = sut.Read(GeoJsonExample);

            // Assert
            Assert.Equal(2,
                         actual.Features.Count);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Read_CallsReader_WhenCalled(
            [NotNull, Frozen] ISelkieGeoJsonStringReader reader,
            [NotNull] GeoJsonStringReader sut)
        {
            // Arrange
            // Act
            sut.Read(GeoJsonExample);

            // Assert
            reader.Received().Read <FeatureCollection>(GeoJsonExample);
        }
    }
}