﻿using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Common.Messages;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    public partial class ServiceHandlers
    {
        public ServiceHandlers()
        {
            m_Bus = ( ISelkieBus ) ScenarioContext.Current [ "ISelkieBus" ];
        }

        private readonly ISelkieBus m_Bus;

        public void Subscribe()
        {
            m_Bus.SubscribeAsync <PingResponseMessage>(GetType().FullName,
                                                       PingResponseHandler);

            m_Bus.SubscribeAsync <ServiceStartedResponseMessage>(GetType().FullName,
                                                                 ServiceStartedResponseHandler);

            m_Bus.SubscribeAsync <ServiceStoppedResponseMessage>(GetType().FullName,
                                                                 ServiceStoppedResponseHandler);

            SubscribeOther();
        }

        private void PingResponseHandler([NotNull] PingResponseMessage message)
        {
            ScenarioContext.Current [ "IsReceivedPingResponse" ] = true;
        }

        private void ServiceStartedResponseHandler([NotNull] ServiceStartedResponseMessage message)
        {
            if ( message.ServiceName == Helper.ServiceName )
            {
                ScenarioContext.Current [ "IsReceivedServiceStartedResponse" ] = true;
            }
        }

        private void ServiceStoppedResponseHandler([NotNull] ServiceStoppedResponseMessage message)
        {
            if ( message.ServiceName == Helper.ServiceName )
            {
                ScenarioContext.Current [ "IsReceivedServiceStoppedResponse" ] = true;
            }
        }
    }
}