using NUnit.Framework;
using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class ThenTheResultShouldBeThatIReceivedATestLineResponseMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a TestLineResponseMessage")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsReceivedTestLineResponse" ] = false;

            var step = new WhenISendALineValidationRequestMessageStep();

            SleepWaitAndDo(() => ( bool ) ScenarioContext.Current [ "IsReceivedTestLineResponse" ],
                           step.Do);

            if ( !( bool ) ScenarioContext.Current [ "IsReceivedTestLineResponse" ] )
            {
                Assert.Fail("Did not receive TestLineResponseMessage!");
            }
        }
    }
}