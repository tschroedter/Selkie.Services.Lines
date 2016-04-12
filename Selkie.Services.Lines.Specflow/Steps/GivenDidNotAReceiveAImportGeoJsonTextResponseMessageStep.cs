using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class GivenDidNotAReceiveAImportGeoJsonTextResponseMessageStep : BaseStep
    {
        [Given(@"Did not a receive a ImportGeoJsonTextResponseMessage")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsImportGeoJsonTextResponseMessage" ] = false;
            ScenarioContext.Current [ "ImportGeoJsonTextResponseMessage_ReceivedLineDtos" ] = new LineDto[0];
        }
    }
}