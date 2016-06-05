using System;
using JetBrains.Annotations;
using Selkie.EasyNetQ;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    [Binding]
    public abstract class BaseStep
    {
        protected BaseStep()
        {
            m_Bus = ( ISelkieBus ) ScenarioContext.Current [ "ISelkieBus" ];
        }

        protected ISelkieBus Bus
        {
            get
            {
                return m_Bus;
            }
        }

        private readonly ISelkieBus m_Bus;

        public abstract void Do();

        public void DoNothing()
        {
        }

        public void SleepWaitAndDo([NotNull] Func <bool> breakIfTrue,
                                   [NotNull] Action doSomething)
        {
            Helper.SleepWaitAndDo(breakIfTrue,
                                  doSomething);
        }
    }
}