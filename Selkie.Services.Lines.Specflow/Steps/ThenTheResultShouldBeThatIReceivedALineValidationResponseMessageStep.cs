using NUnit.Framework;
using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class ThenTheResultShouldBeThatIReceivedALineValidationResponseMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a LineValidationResponseMessage")]
        public override void Do()
        {
            var step = new WhenISendALineValidationRequestMessageStep();

            SleepWaitAndDo(() => GetBoolValueForScenarioContext("IsReceivedLineValidationResponse"),
                           step.Do);

            if ( !GetBoolValueForScenarioContext("IsReceivedLineValidationResponse") )
            {
                Assert.Fail("Did not receive LineValidationResponseMessage!");
            }
        }
    }
}