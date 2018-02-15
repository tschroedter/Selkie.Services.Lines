using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using Core2.Selkie.Services.Lines.Common.Dto;
using Core2.Selkie.Services.Lines.Common.Messages;
using Core2.Selkie.Services.Lines.Handlers;
using NUnit.Framework;
using Core2.Selkie.NUnit.Extensions;
using AutoFixture.NUnit3;
using Core2.Selkie.Services.Lines.Interfaces;
using Core2.Selkie.EasyNetQ.Interfaces;

namespace Core2.Selkie.Services.Lines.Tests.Handlers.XUnit
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class LineValidationRequestHandlerTests
    {
        [Test]
        [AutoNSubstituteData]
        public void LineValidationRequestHandlerCallsManagerTest([NotNull] [Frozen] ILinesSourceManager manager,
                                                                 [NotNull] [Frozen] ISelkieBus bus,
                                                                 [NotNull] LineValidationRequestHandlerAsync sut)
        {
            LineValidationRequestMessage request = CreateRequestMessage(new[]
                                                                        {
                                                                            0,
                                                                            1
                                                                        });

            sut.Handle(request);

            manager.Received().ValidateDtos(Arg.Any <IEnumerable <LineDto>>());
        }

        [Test]
        [AutoNSubstituteData]
        public void LineValidationRequestHandlerSendsMessageTest([NotNull] [Frozen] ILinesSourceManager manager,
                                                                 [NotNull] [Frozen] ISelkieBus bus,
                                                                 [NotNull] LineValidationRequestHandlerAsync sut)
        {
            LineValidationRequestMessage request = CreateRequestMessage(new[]
                                                                        {
                                                                            0,
                                                                            1
                                                                        });

            sut.Handle(request);

            bus.Received().PublishAsync(Arg.Any <LineValidationResponseMessage>());
        }

        [NotNull]
        private LineValidationRequestMessage CreateRequestMessage([NotNull] IEnumerable <int> ids)
        {
            var lines = new List <LineDto>();

            foreach ( int id in ids )
            {
                var line = Substitute.For <LineDto>();
                line.Id = id;

                lines.Add(line);
            }

            var request = new LineValidationRequestMessage
                          {
                              LineDtos = lines.ToArray()
                          };

            return request;
        }
    }
}