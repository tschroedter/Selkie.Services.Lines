using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Specflow.Steps.Common;
using TechTalk.SpecFlow;

namespace Selkie.Services.Lines.Specflow.Steps
{
    public class WhenISendALineValidationRequestMessageStep : BaseStep
    {
        [When(@"I send a LineValidationRequestMessage")]
        public override void Do()
        {
            var request = new LineValidationRequestMessage
                          {
                              LineDtos = new LineDto[0]
                          };

            Bus.PublishAsync(request);
        }
    }
}