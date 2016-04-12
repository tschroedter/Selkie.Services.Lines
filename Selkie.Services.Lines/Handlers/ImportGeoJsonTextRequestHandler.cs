using System.Linq;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Services.Lines.Common.Messages;

namespace Selkie.Services.Lines.Handlers
{
    [Interceptor(typeof ( MessageHandlerAspect ))]
    public class ImportGeoJsonTextRequestHandler
        : SelkieMessageHandler <ImportGeoJsonTextRequestMessage>
    {
        private readonly ISelkieBus m_Bus;
        private readonly IGeoJsonTextToLineDtosConverter m_Converter;

        public ImportGeoJsonTextRequestHandler(
            [NotNull] ISelkieBus bus,
            [NotNull] IGeoJsonTextToLineDtosConverter converter)
        {
            m_Bus = bus;
            m_Converter = converter;
        }

        public override void Handle([NotNull] ImportGeoJsonTextRequestMessage message)
        {
            m_Converter.GeoJsonText = message.Text;
            m_Converter.Convert();

            var reponse = new ImportGeoJsonTextResponseMessage
                          {
                              LineDtos = m_Converter.LineDtos.ToArray()
                          };

            m_Bus.PublishAsync(reponse);
        }
    }
}