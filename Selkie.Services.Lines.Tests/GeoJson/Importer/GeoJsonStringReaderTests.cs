using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Lines.GeoJson.Importer;
using Selkie.Services.Lines.Interfaces.GeoJson.Importer;

namespace Selkie.Services.Lines.Tests.GeoJson.Importer
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class GeoJsonStringReaderTests
    {
        private const string GeoJsonExample =
            "{" +
            "  \"type\": \"FeatureCollection\"," +
            "  \"features\": [" +
            "    {" +
            "      \"type\": \"Feature\"," +
            "      \"geometry\": {" +
            "        \"type\": \"LineString\", " +
            "        \"coordinates\": [[0, 0], [0, 10]]" +
            "      }" +
            "    }," +
            "    {" +
            "      \"type\": \"Feature\"," +
            "      \"geometry\": {" +
            "        \"type\": \"LineString\", " +
            "        \"coordinates\": [[10, 0], [10, 10]]" +
            "      }" +
            "    }," +
            "  ]" +
            "}";

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

        [Theory]
        [AutoNSubstituteData]
        public void Read_ReturnsFeatureCollection_WhenCalled()
        {
            // Arrange
            var reader = new SelkieGeoJsonStringReader();
            var sut = new GeoJsonStringReader(reader);

            // Act
            FeatureCollection actual = sut.Read(GeoJsonExample);

            // Assert
            Assert.AreEqual(2,
                            actual.Features.Count);
        }
    }
}