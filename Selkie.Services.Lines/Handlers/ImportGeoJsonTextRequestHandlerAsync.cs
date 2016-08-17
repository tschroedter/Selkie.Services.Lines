using System.Linq;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.EasyNetQ;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Interfaces.Converters.ToDtos;

namespace Selkie.Services.Lines.Handlers
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    public class ImportGeoJsonTextRequestHandlerAsync
        : SelkieMessageHandler <ImportGeoJsonTextRequestMessage>
    {
        public ImportGeoJsonTextRequestHandlerAsync(
            [NotNull] ISelkieBus bus,
            [NotNull] IGeoJsonTextToSurveyGeoJsonFeatureDtosConverter converter)
        {
            m_Bus = bus;
            m_Converter = converter;
        }

        private readonly ISelkieBus m_Bus;
        private readonly IGeoJsonTextToSurveyGeoJsonFeatureDtosConverter m_Converter;

        public override void Handle([NotNull] ImportGeoJsonTextRequestMessage message)
        {
            m_Converter.GeoJson = message.Text;
            m_Converter.Convert();

            var reponse = new ImportGeoJsonTextResponseMessage
                          {
                              Dtos = m_Converter.Dtos.ToArray()
                          };

            m_Bus.PublishAsync(reponse);
        }
    }
}