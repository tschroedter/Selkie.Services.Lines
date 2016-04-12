using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class WhenISendAImportGeoJsonTextRequestMessageStep : BaseStep
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

        [When(@"I send a ImportGeoJsonTextRequestMessage")]
        public override void Do()
        {
            var request = new ImportGeoJsonTextRequestMessage
                          {
                              Text = GeoJsonExample
                          };

            Bus.PublishAsync(request);
        }
    }
}