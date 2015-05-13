﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Common;
using Selkie.EasyNetQ.Extensions;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines.Example.Client
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class LineServiceTestClient : ILineServiceTestClient
    {
        private readonly IBus m_Bus;
        private readonly ISelkieConsole m_Console;

        public LineServiceTestClient([NotNull] IBus bus,
                                     [NotNull] ILogger logger,
                                     [NotNull] ISelkieConsole console)
        {
            m_Bus = bus;
            m_Console = console;

            m_Bus.SubscribeHandlerAsync <TestLineResponseMessage>(logger,
                                                                  GetType()
                                                                      .ToString(),
                                                                  TestLineResponseHandler);
        }

        public void RequestTestLines()
        {
            m_Console.WriteLine("Request <TestLineRequestMessage>...");

            TestLineType.Type[] types = {
                                            TestLineType.Type.CreateTestLines,
                                            TestLineType.Type.CreateParallelLines
                                        };

            TestLineRequestMessage request = new TestLineRequestMessage
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
    }

    public interface ILineServiceTestClient
    {
        void RequestTestLines();
    }
}