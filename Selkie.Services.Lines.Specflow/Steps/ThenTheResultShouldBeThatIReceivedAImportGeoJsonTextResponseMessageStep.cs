using NUnit.Framework;
using Selkie.Services.Lines.Common.Dto;
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

            SleepWaitAndDo(() => GetBoolValueForScenarioContext("IsImportGeoJsonTextResponseMessage"),
                           step.Do);

            if ( !GetBoolValueForScenarioContext("IsImportGeoJsonTextResponseMessage") )
            {
                Assert.Fail("Did not receive ImportGeoJsonTextResponseMessage!");
            }

            var lineDtos = ( LineDto[] ) ScenarioContext.Current [ "ImportGeoJsonTextResponseMessage_ReceivedLineDtos" ];

            Assert.AreEqual(2,
                            lineDtos.Length);
        }
    }
}