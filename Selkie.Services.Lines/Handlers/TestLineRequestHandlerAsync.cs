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
    [Interceptor(typeof ( MessageHandlerAspect ))]
    public class TestLineRequestHandlerAsync
        : SelkieMessageHandler <TestLineRequestMessage>
    {
        private readonly ISelkieBus m_Bus;
        private readonly ILinesToLineDtosConverter m_Converter;
        private readonly ILinesSourceManager m_Manager;

        public TestLineRequestHandlerAsync(
            [NotNull] ISelkieBus bus,
            [NotNull] ILinesSourceManager manager,
            [NotNull] ILinesToLineDtosConverter converter)
        {
            m_Bus = bus;
            m_Manager = manager;
            m_Converter = converter;
        }

        public override void Handle(TestLineRequestMessage message)
        {
            IEnumerable <ILine> lines = m_Manager.GetTestLines(message.Types);

            m_Converter.Lines = lines;
            m_Converter.Convert();

            LineDto[] dtos = m_Converter.LineDtos.ToArray();

            var reply = new TestLineResponseMessage
                        {
                            LineDtos = dtos
                        };

            m_Bus.PublishAsync(reply);
        }
    }
}