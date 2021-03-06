using Selkie.Services.Common.Messages;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    public class WhenISendAStopMessageStep : BaseStep
    {
        [When(@"I send a stop message")]
        public override void Do()
        {
            Bus.PublishAsync(new StopServiceRequestMessage
                             {
                                 ServiceName = Helper.ServiceName
                             });
        }
    }
}