using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Common;
using Selkie.EasyNetQ;
using Selkie.Geometry.Shapes;
using Selkie.Services.Common.Messages;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines.Console.Client
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class LineServiceTestClient : ILineServiceTestClient
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

        private readonly ISelkieBus m_Bus;
        private readonly ISelkieConsole m_Console;

        public LineServiceTestClient([NotNull] ISelkieBus bus,
                                     [NotNull] ISelkieConsole console)
        {
            m_Bus = bus;
            m_Console = console;

            m_Bus.SubscribeAsync <TestLineResponseMessage>(GetType().ToString(),
                                                           TestLineResponseHandler);

            m_Bus.SubscribeAsync <ImportGeoJsonTextResponseMessage>(GetType().ToString(),
                                                                    ImportGeoJsonTextResponseHandler);
        }

        public void RequestTestLines()
        {
            m_Console.WriteLine("Request <TestLineRequestMessage>...");

            TestLineType.Type[] types =
            {
                TestLineType.Type.CreateTestLines,
                TestLineType.Type.CreateParallelLines
            };

            var request = new TestLineRequestMessage
                          {
                              Types = types
                          };

            m_Bus.PublishAsync(request);
            m_Bus.Publish(request);
        }

        public void RequestGeoJsonImportText()
        {
            m_Console.WriteLine("Request <ImportGeoJsonTextRequest>...");

            var request = new ImportGeoJsonTextRequestMessage
                          {
                              Text = GeoJsonExample
                          };

            m_Bus.PublishAsync(request);
            m_Bus.Publish(request);
        }

        private void TestLineResponseHandler([NotNull] TestLineResponseMessage message)
        {
            m_Console.WriteLine("Received <TestLineResponse>...");

            DisplayLineDtos(message.LineDtos);
        }

        private void ImportGeoJsonTextResponseHandler(ImportGeoJsonTextResponseMessage message)
        {
            m_Console.WriteLine("Received <ImportGeoJsonTextResponseMessage>...");

            DisplayLineDtos(message.LineDtos);
        }

        private void DisplayLineDtos(IEnumerable <LineDto> lineDtos)
        {
            IEnumerable <ILine> lines = lineDtos.Select(LineToLineDtoConverter.ConvertToLine);

            foreach ( ILine line in lines )
            {
                string status = line.IsUnknown
                                    ? "Unknown"
                                    : "Known";

                m_Console.WriteLine("Id {0} [{1}]: {2}".Inject(line.Id,
                                                               status,
                                                               line));
            }
        }

        public void StopService()
        {
            m_Bus.PublishAsync(new StopServiceRequestMessage
                               {
                                   IsStopAllServices = false,
                                   ServiceName = "Lines Service"
                               });
        }
    }

    public interface ILineServiceTestClient
    {
        void RequestTestLines();
    }
}