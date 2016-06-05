using JetBrains.Annotations;
using Selkie.Services.Lines.Common.Messages;
using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace

namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    public partial class ServiceHandlers
    {
        public void SubscribeOther()
        {
            m_Bus.SubscribeAsync <TestLineResponseMessage>(GetType().FullName,
                                                           TestLineResponseHandler);

            m_Bus.SubscribeAsync <LineValidationResponseMessage>(GetType().FullName,
                                                                 LineValidationResponseHandler);

            m_Bus.SubscribeAsync <ImportGeoJsonTextResponseMessage>(GetType().FullName,
                                                                    ImportGeoJsonTextResponseHandler);
        }

        private void ImportGeoJsonTextResponseHandler([NotNull] ImportGeoJsonTextResponseMessage message)
        {
            ScenarioContext.Current [ "IsImportGeoJsonTextResponseMessage" ] = true;
            ScenarioContext.Current [ "ImportGeoJsonTextResponseMessage_ReceivedLineDtos" ] = message.LineDtos;
        }

        private void LineValidationResponseHandler([NotNull] LineValidationResponseMessage message)
        {
            ScenarioContext.Current [ "IsReceivedLineValidationResponse" ] = true;
        }

        private void TestLineResponseHandler([NotNull] TestLineResponseMessage message)
        {
            ScenarioContext.Current [ "IsReceivedTestLineResponse" ] = true;
        }
    }
}