using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Services.Lines.Common.Messages;

namespace Selkie.Services.Lines.Handlers
{
    [Interceptor(typeof ( MessageHandlerAspect ))]
    public class LineValidationRequestHandler
        : SelkieMessageHandler <LineValidationRequestMessage>
    {
        private readonly ISelkieBus m_Bus;
        private readonly ILinesSourceManager m_Manager;

        public LineValidationRequestHandler([NotNull] ISelkieBus bus,
                                            [NotNull] ILinesSourceManager manager)
        {
            m_Bus = bus;
            m_Manager = manager;
        }

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