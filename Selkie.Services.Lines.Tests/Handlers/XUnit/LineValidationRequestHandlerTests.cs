using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyNetQ;
using JetBrains.Annotations;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Common.Messages;
using Selkie.Services.Lines.Handlers;
using Selkie.XUnit.Extensions;
using Xunit.Extensions;

namespace Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class LineValidationRequestHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void LineValidationRequestHandlerCallsManagerTest([NotNull] [Frozen] ILinesSourceManager manager,
                                                                 [NotNull] [Frozen] IBus bus,
                                                                 [NotNull] LineValidationRequestHandler sut)
        {
            LineValidationRequestMessage request = CreateRequestMessage(new[]
                                                                        {
                                                                            0,
                                                                            1
                                                                        });

            sut.Handle(request);

            manager.Received()
                   .ValidateDtos(Arg.Any <IEnumerable <LineDto>>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void LineValidationRequestHandlerSendsMessageTest([NotNull] [Frozen] ILinesSourceManager manager,
                                                                 [NotNull] [Frozen] IBus bus,
                                                                 [NotNull] LineValidationRequestHandler sut)
        {
            LineValidationRequestMessage request = CreateRequestMessage(new[]
                                                                        {
                                                                            0,
                                                                            1
                                                                        });

            sut.Handle(request);

            bus.Received()
               .PublishAsync(Arg.Any <LineValidationResponseMessage>());
        }

        [NotNull]
        private LineValidationRequestMessage CreateRequestMessage([NotNull] IEnumerable <int> ids)
        {
            List <LineDto> lines = new List <LineDto>();

            foreach ( int id in ids )
            {
                LineDto line = Substitute.For <LineDto>();
                line.Id = id;

                lines.Add(line);
            }

            LineValidationRequestMessage request = new LineValidationRequestMessage
                                                   {
                                                       LineDtos = lines.ToArray()
                                                   };

            return request;
        }
    }
}