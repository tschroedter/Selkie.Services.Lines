using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Handlers
{
    [ProjectComponent(Lifestyle.Startable)]
    public sealed class TestLineRequestHandler
        : BaseHandler <TestLineRequestMessage>,
          ITestLineRequestHandler
    {
        public TestLineRequestHandler([NotNull] ILogger logger,
                                      [NotNull] IBus bus,
                                      [NotNull] ILinesSourceManager manager)
            : base(logger,
                   bus,
                   manager)
        {
        }

        internal override void Handle(TestLineRequestMessage message)
        {
            IEnumerable <ILine> lines = Manager.GetTestLines(message.Types);

            IEnumerable <LineDto> lineDtos = lines.Select(LineToLineDtoConverter.ConvertFrom);

            LineDto[] dtos = lineDtos.ToArray();

            var reply = new TestLineResponseMessage
                        {
                            LineDtos = dtos
                        };

            Bus.PublishAsync(reply);
        }
    }
}