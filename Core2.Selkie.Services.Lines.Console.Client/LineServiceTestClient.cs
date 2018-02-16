using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Core2.Selkie.Common.Interfaces;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Services.Common.Messages;
using Core2.Selkie.Services.Lines.Common;
using Core2.Selkie.Services.Lines.Common.Dto;
using Core2.Selkie.Services.Lines.Common.Messages;
using JetBrains.Annotations;

namespace Core2.Selkie.Services.Lines.Console.Client
{
    [ExcludeFromCodeCoverage]
    public class LineServiceTestClient : ILineServiceTestClient
    {
        public LineServiceTestClient([NotNull] ISelkieBus bus,
                                     [NotNull] ISelkieConsole console)
        {
            m_Bus = bus;
            m_Console = console;

            m_Bus.SubscribeAsync <TestLineResponseMessage>(GetType().ToString(),
                                                           TestLineResponseHandler);
        }

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

        public void StopService()
        {
            m_Bus.PublishAsync(new StopServiceRequestMessage
                               {
                                   IsStopAllServices = false,
                                   ServiceName = "Lines Service"
                               });
        }

        private void DisplayLineDtos(IEnumerable <LineDto> lineDtos)
        {
            IEnumerable <ILine> lines = lineDtos.Select(LineToLineDtoConverter.ConvertToLine);

            foreach ( ILine line in lines )
            {
                string status = line.IsUnknown
                                    ? "Unknown"
                                    : "Known";

                m_Console.WriteLine($"Id {line.Id} [{status}]: {line}");
            }
        }

        private void TestLineResponseHandler([NotNull] TestLineResponseMessage message)
        {
            m_Console.WriteLine("Received <TestLineResponse>...");

            DisplayLineDtos(message.LineDtos);
        }
    }

    public interface ILineServiceTestClient
    {
        void RequestTestLines();
    }
}