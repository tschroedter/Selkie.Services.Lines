using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;

namespace Selkie.Services.Lines.Handlers
{
    [Interceptor(typeof(MessageHandlerAspect))]
    public class TestLineRequestHandler
        : SelkieMessageHandler <TestLineRequestMessage>
    {
        private readonly ISelkieBus m_Bus;
        private readonly ILinesSourceManager m_Manager;

        public TestLineRequestHandler([NotNull] ISelkieBus bus,
                                      [NotNull] ILinesSourceManager manager)
        {
            m_Bus = bus;
            m_Manager = manager;
        }

        public override void Handle(TestLineRequestMessage message)
        {
            IEnumerable <ILine> lines = m_Manager.GetTestLines(message.Types);

            IEnumerable <LineDto> lineDtos = lines.Select(LineToLineDtoConverter.ConvertFrom);

            LineDto[] dtos = lineDtos.ToArray();

            var reply = new TestLineResponseMessage
                        {
                            LineDtos = dtos
                        };

            m_Bus.PublishAsync(reply);
        }
    }
}