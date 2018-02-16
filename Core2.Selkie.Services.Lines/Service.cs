using Castle.Core;
using JetBrains.Annotations;
using Core2.Selkie.Aop.Aspects;
using Core2.Selkie.EasyNetQ.Interfaces;
using Core2.Selkie.Services.Common;
using Core2.Selkie.Services.Common.Interfaces;
using Core2.Selkie.Services.Common.Messages;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Services.Lines
{
    [ProjectComponent(Lifestyle.Singleton)]
    public class Service
        : BaseService,
          IService
    {
        public Service([NotNull] ISelkieBus bus,
                       [NotNull] ISelkieLogger logger,
                       [NotNull] ISelkieManagementClient client)
            : base(bus,
                   logger,
                   client,
                   ServiceName)
        {
        }

        public const string ServiceName = "Lines Service";

        protected override void ServiceInitialize()
        {
        }

        protected override void ServiceStart()
        {
            var message = new ServiceStartedResponseMessage
                          {
                              ServiceName = ServiceName
                          };

            Bus.Publish(message);
        }

        protected override void ServiceStop()
        {
            var message = new ServiceStoppedResponseMessage
                          {
                              ServiceName = ServiceName
                          };

            Bus.Publish(message);
        }
    }
}