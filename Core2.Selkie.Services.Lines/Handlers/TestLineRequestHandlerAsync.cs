using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using JetBrains.Annotations;
using Core2.Selkie.Aop.Aspects;
using Core2.Selkie.EasyNetQ;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Services.Lines.Common.Dto;
using Core2.Selkie.Services.Lines.Common.Messages;
using Core2.Selkie.Services.Lines.Interfaces;
using Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos;

namespace Core2.Selkie.Services.Lines.Handlers
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    public class TestLineRequestHandlerAsync
        : SelkieMessageHandler <TestLineRequestMessage>
    {
        public TestLineRequestHandlerAsync(
            [NotNull] ISelkieBus bus,
            [NotNull] ILinesSourceManager manager,
            [NotNull] ILinesToLineDtosConverter converter)
        {
            m_Bus = bus;
            m_Manager = manager;
            m_Converter = converter;
        }

        private readonly ISelkieBus m_Bus;
        private readonly ILinesToLineDtosConverter m_Converter;
        private readonly ILinesSourceManager m_Manager;

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