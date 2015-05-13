using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Handlers
{
    [ProjectComponent(Lifestyle.Startable)]
    public sealed class LineValidationRequestHandler : BaseHandler <LineValidationRequestMessage>,
                                                       ILineValidationRequestHandler
    {
        public LineValidationRequestHandler([NotNull] ILogger logger,
                                            [NotNull] IBus bus,
                                            [NotNull] ILinesSourceManager manager)
            : base(logger,
                   bus,
                   manager)
        {
        }

        internal override void Handle(LineValidationRequestMessage message)
        {
            Logger.Debug("LineValidationRequestMessage was called!");

            bool isValid = Manager.ValidateDtos(message.LineDtos);

            LineValidationResponseMessage response = new LineValidationResponseMessage
                                                     {
                                                         LineDtos = message.LineDtos,
                                                         AreValid = isValid
                                                     };

            Bus.PublishAsync(response);
        }
    }
}