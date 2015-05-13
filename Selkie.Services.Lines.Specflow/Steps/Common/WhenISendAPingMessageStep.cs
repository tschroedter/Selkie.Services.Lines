using Selkie.Services.Common.Messages;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    public class WhenISendAPingMessageStep : BaseStep
    {
        [When(@"I send a ping message")]
        public override void Do()
        {
            Bus.PublishAsync(new PingRequestMessage());
        }
    }
}