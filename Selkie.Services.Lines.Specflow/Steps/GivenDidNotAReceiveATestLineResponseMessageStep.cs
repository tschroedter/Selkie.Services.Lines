using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class GivenDidNotAReceiveATestLineResponseMessageStep : BaseStep
    {
        [Given(@"Did not a receive a TestLineResponseMessage")]
        public override void Do()
        {
            ScenarioContext.Current [ "IsReceivedTestLineResponse" ] = false;
        }
    }
}