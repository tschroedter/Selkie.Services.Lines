using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Selkie.Services.Common;
using Selkie.Services.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Lines
{
    [ProjectComponent(Lifestyle.Singleton)]
    public class Service : BaseService,
                           IService
    {
        public const string ServiceName = "Lines Service";

        public Service([NotNull] IBus bus,
                       [NotNull] ILogger logger,
                       [NotNull] ISelkieManagementClient client)
            : base(bus,
                   logger,
                   client,
                   ServiceName)
        {
        }

        protected override void ServiceStart()
        {
            ServiceStartedResponseMessage message = new ServiceStartedResponseMessage
                                                    {
                                                        ServiceName = ServiceName
                                                    };

            Bus.Publish(message);
        }

        protected override void ServiceStop()
        {
            ServiceStoppedResponseMessage message = new ServiceStoppedResponseMessage
                                                    {
                                                        ServiceName = ServiceName
                                                    };

            Bus.Publish(message);
        }

        protected override void ServiceInitialize()
        {
        }
    }
}