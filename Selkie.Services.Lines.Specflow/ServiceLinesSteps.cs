using JetBrains.Annotations;
using Selkie.EasyNetQ.Extensions;
using Selkie.Services.Lines.Common.Messages;
using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace
namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    public partial class ServiceHandlers
    {
        public void SubscribeOther()
        {
            m_Bus.SubscribeHandlerAsync <TestLineResponseMessage>(m_Logger,
                                                                  GetType()
                                                                      .FullName,
                                                                  TestLineResponseHandler);

            m_Bus.SubscribeHandlerAsync <LineValidationResponseMessage>(m_Logger,
                                                                        GetType()
                                                                            .FullName,
                                                                        LineValidationResponseHandler);
        }

        private void TestLineResponseHandler([NotNull] TestLineResponseMessage message)
        {
            ScenarioContext.Current["IsReceivedTestLineResponse"] = true;
        }

        private void LineValidationResponseHandler([NotNull] LineValidationResponseMessage message)
        {
            ScenarioContext.Current["IsReceivedLineValidationResponse"] = true;
        }
    }
}