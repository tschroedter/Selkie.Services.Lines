using Castle.Core;
using Castle.Core.Logging;
using EasyNetQ;
using JetBrains.Annotations;
using Core2.Selkie.EasyNetQ.Extensions;
using Core2.Selkie.Services.Lines.Interfaces;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Services.Lines.Handlers
{
    // todo use EasyNetQ IConsume https://github.com/EasyNetQ/EasyNetQ/wiki/Auto-Subscriber
    public abstract class BaseHandler <T> : IStartable
        where T : class
    {
        private readonly IBus m_Bus;
        private readonly ISelkieLogger m_Logger;
        private readonly ILinesSourceManager m_Manager;

        protected BaseHandler([NotNull] ISelkieLogger logger,
                              [NotNull] IBus bus,
                              [NotNull] ILinesSourceManager manager)
        {
            m_Logger = logger;
            m_Bus = bus;
            m_Manager = manager;

            m_Logger.Info($"Created handler for <{GetType().FullName}>!");
        }

        [NotNull]
        protected ISelkieLogger Logger
        {
            get
            {
                return m_Logger;
            }
        }

        [NotNull]
        protected IBus Bus
        {
            get
            {
                return m_Bus;
            }
        }

        [NotNull]
        protected ILinesSourceManager Manager
        {
            get
            {
                return m_Manager;
            }
        }

        public void Start()
        {
            m_Logger.Info($"Subscribing to message <{typeof ( T )}>...");

            m_Bus.SubscribeHandlerAsync <T>(m_Logger,
                                            GetType().FullName,
                                            Handle);
        }

        public void Stop()
        {
        }

        internal abstract void Handle([NotNull] T message);
    }
}