using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class GivenDidNotAReceiveALineValidationResponseMessageStep : BaseStep
    {
        [Given(@"Did not a receive a LineValidationResponseMessage")]
        public override void Do()
        {
            ScenarioContext.Current["IsReceivedLineValidationResponse"] = false;
        }
    }
}