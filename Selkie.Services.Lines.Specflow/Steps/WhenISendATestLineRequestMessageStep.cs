using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class WhenISendATestLineRequestMessageStep : BaseStep
    {
        [When(@"I send a TestLineRequestMessage")]
        public override void Do()
        {
            TestLineType.Type[] types = {
                                            TestLineType.Type.CreateLines
                                        };

            TestLineRequestMessage response = new TestLineRequestMessage
                                              {
                                                  Types = types
                                              };

            Bus.PublishAsync(response);
        }
    }
}