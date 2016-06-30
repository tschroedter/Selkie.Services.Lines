using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps.Common
{
    public class ThenTheResultShouldBeThatIReceivedAServiceStoppedMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a ServiceStoppedMessage")]
        public override void Do()
        {
            SleepWaitAndDo(() => GetBoolValueForScenarioContext("IsReceivedServiceStoppedResponse"),
                           DoNothing);

            Assert.True(GetBoolValueForScenarioContext("IsReceivedServiceStoppedResponse"),
                        "Didn't receive service stopped response!");
        }
    }
}