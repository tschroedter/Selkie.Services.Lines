using Castle.Core;
using JetBrains.Annotations;
using Core2.Selkie.Aop.Aspects;
using Core2.Selkie.EasyNetQ;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Services.Lines.Common.Messages;
using Core2.Selkie.Services.Lines.Interfaces;

namespace Core2.Selkie.Services.Lines.Handlers
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    public class LineValidationRequestHandlerAsync
        : SelkieMessageHandler <LineValidationRequestMessage>
    {
        public LineValidationRequestHandlerAsync(
            [NotNull] ISelkieBus bus,
            [NotNull] ILinesSourceManager manager)
        {
            m_Bus = bus;
            m_Manager = manager;
        }

        private readonly ISelkieBus m_Bus;
        private readonly ILinesSourceManager m_Manager;

        public override void Handle(LineValidationRequestMessage message)
        {
            bool isValid = m_Manager.ValidateDtos(message.LineDtos);

            var response = new LineValidationResponseMessage
                           {
                               LineDtos = message.LineDtos,
                               AreValid = isValid
                           };

            m_Bus.PublishAsync(response);
        }
    }
}