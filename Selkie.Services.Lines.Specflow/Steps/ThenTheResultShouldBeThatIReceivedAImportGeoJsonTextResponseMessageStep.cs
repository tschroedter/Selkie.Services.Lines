using NUnit.Framework;
using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class ThenTheResultShouldBeThatIReceivedAImportGeoJsonTextResponseMessageStep : BaseStep
    {
        [Then(@"the result should be that I received a ImportGeoJsonTextResponseMessage")]
        public override void Do()
        {
            var step = new WhenISendALineValidationRequestMessageStep();

            SleepWaitAndDo(() => ( bool ) ScenarioContext.Current [ "IsImportGeoJsonTextResponseMessage" ],
                           step.Do);

            if ( !( bool ) ScenarioContext.Current [ "IsImportGeoJsonTextResponseMessage" ] )
            {
                Assert.Fail("Did not receive ImportGeoJsonTextResponseMessage!");
            }
        }
    }
}