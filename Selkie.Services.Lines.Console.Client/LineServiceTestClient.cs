using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Common;
using Selkie.EasyNetQ;
using Selkie.Geometry.Shapes;
using Selkie.Services.Common.Messages;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines.Console.Client
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class LineServiceTestClient : ILineServiceTestClient
    {
        private readonly ISelkieBus m_Bus;
        private readonly ISelkieConsole m_Console;

        public LineServiceTestClient([NotNull] ISelkieBus bus,
                                     [NotNull] ISelkieConsole console)
        {
            m_Bus = bus;
            m_Console = console;

            m_Bus.SubscribeAsync <TestLineResponseMessage>(GetType().ToString(),
                                                           TestLineResponseHandler);
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

        private void TestLineResponseHandler([NotNull] TestLineResponseMessage message)
        {
            m_Console.WriteLine("Received <TestLineResponse>...");

            IEnumerable <ILine> lines = message.LineDtos.Select(LineToLineDtoConverter.ConvertToLine);

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