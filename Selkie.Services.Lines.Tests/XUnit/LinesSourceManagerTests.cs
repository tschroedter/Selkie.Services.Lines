using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Core.Logging;
using NSubstitute;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;
using Xunit;

namespace Selkie.Services.Lines.Tests.XUnit
{
    // ReSharper disable once ClassTooBig
    [ExcludeFromCodeCoverage]
    public class LinesSourceManagerTests
    {
        private readonly ITestLineCreator m_Creator;
        private readonly LinesSourceManager m_Sut;
        private readonly ILinesValidator m_Validator;

        public LinesSourceManagerTests()
        {
            var logger = Substitute.For <ILogger>();
            m_Creator = Substitute.For <ITestLineCreator>();
            m_Validator = Substitute.For <ILinesValidator>();

            m_Sut = new LinesSourceManager(logger,
                                           m_Creator,
                                           m_Validator);
        }

        [Fact]
        public void GetTestLinesCallsGetTestLinesForTypeTest()
        {
            var lines = new List <ILine>
                        {
                            Substitute.For <ILine>()
                        };

            m_Creator.CreateLines(-1).ReturnsForAnyArgs(lines);

            m_Sut.GetTestLines(new[]
                               {
                                   TestLineType.Type.CreateLines
                               });

            m_Creator.Received().CreateLines(0);
        }

        [Fact]
        public void GetTestLinesForTypeCreateTestLinesVerticalTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateTestLinesVertical);

            m_Creator.Received().CreateTestLinesVertical(0);
        }

        [Fact]
        public void GetTestLinesForTypeFoCreateParallelCrossLinesInCornerTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelCrossLinesInCorner);

            m_Creator.Received().CreateParallelCrossLinesInCorner(LinesSourceManager.NumberOfLines);
        }

        [Fact]
        public void GetTestLinesForTypeForCreate45DegreeLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.Create45DegreeLines);

            m_Creator.Received().Create45DegreeLines(LinesSourceManager.NumberOfLines);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateBoxTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateBox);

            m_Creator.Received().CreateBox(LinesSourceManager.NumberOfLines);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateCrossForwardReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateCrossForwardReverse);

            m_Creator.Received().CreateCrossForwardReverse();
        }

        [Fact]
        public void GetTestLinesForTypeForCreateCrossTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateCross);

            m_Creator.Received().CreateCross();
        }

        [Fact]
        public void GetTestLinesForTypeForCreateFixedParallelLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateFixedParallelLines);

            m_Creator.Received().CreateFixedParallelLines(0);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateLinesCallsCreatorTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateLines);

            m_Creator.Received().CreateLines(0);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateLinesCallsCreatorWithMaxIdTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateLines,
                                      123);

            m_Creator.Received().CreateLines(123);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateLinesInRowHorizontalTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateLinesInRowHorizontal);

            m_Creator.Received().CreateLinesInRowHorizontal(LinesSourceManager.NumberOfLines);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateParallelCrossLinesForwardReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelCrossLinesForwardReverse);

            m_Creator.Received().CreateParallelCrossLinesForwardReverse(LinesSourceManager.NumberOfLines);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateParallelCrossLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelCrossLines);

            m_Creator.Received().CreateParallelCrossLines(LinesSourceManager.NumberOfLines);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateParallelLinesForwardReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelLinesForwardReverse);

            m_Creator.Received().CreateParallelLinesForwardReverse(LinesSourceManager.NumberOfLines,
                                                                   0);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateParallelLinesReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelLinesReverse);

            m_Creator.Received().CreateParallelLinesReverse(10,
                                                            0);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateParallelLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelLines);

            m_Creator.Received().CreateParallelLines(10,
                                                     0);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateRandomLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateRandomLines);

            m_Creator.Received().CreateRandomLines(LinesSourceManager.NumberOfLines);
        }

        [Fact]
        public void GetTestLinesForTypeForCreateTestLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateTestLines);

            m_Creator.Received().CreateTestLines(0);
        }

        [Fact]
        public void GetTestLinesReturnsEmptyLinesForInvalidLinesTest()
        {
            var linesOne = new List <ILine>
                           {
                               Substitute.For <ILine>()
                           };

            m_Creator.CreateLines(-1).ReturnsForAnyArgs(linesOne);

            // ReSharper disable once MaximumChainedReferences
            m_Validator.ValidateLines(Arg.Any <ILine[]>()).ReturnsForAnyArgs(false);

            IEnumerable <ILine> actual = m_Sut.GetTestLines(new[]
                                                            {
                                                                TestLineType.Type.CreateLines
                                                            });

            Assert.Equal(0,
                         actual.Count());
        }

        [Fact]
        public void GetTestLinesReturnsLinesForOneTypeTest()
        {
            var lines = new List <ILine>
                        {
                            Substitute.For <ILine>()
                        };

            m_Creator.CreateLines(-1).ReturnsForAnyArgs(lines);
            // ReSharper disable once MaximumChainedReferences
            m_Validator.ValidateLines(Arg.Any <ILine[]>()).ReturnsForAnyArgs(true);

            IEnumerable <ILine> actual = m_Sut.GetTestLines(new[]
                                                            {
                                                                TestLineType.Type.CreateLines
                                                            });

            Assert.Equal(1,
                         actual.Count());
        }

        [Fact]
        public void GetTestLinesReturnsLinesForTwoTypesTest()
        {
            var linesOne = new List <ILine>
                           {
                               Substitute.For <ILine>()
                           };
            var linesTwo = new List <ILine>
                           {
                               Substitute.For <ILine>()
                           };

            m_Creator.CreateLines(-1).ReturnsForAnyArgs(linesOne);
            m_Creator.CreateBox(-1).ReturnsForAnyArgs(linesTwo);
            // ReSharper disable once MaximumChainedReferences
            m_Validator.ValidateLines(Arg.Any <ILine[]>()).ReturnsForAnyArgs(true);

            IEnumerable <ILine> actual = m_Sut.GetTestLines(new[]
                                                            {
                                                                TestLineType.Type.CreateLines,
                                                                TestLineType.Type.CreateBox
                                                            });

            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void ValidateDtosCallsValidatorTest()
        {
            var lineDtos = new LineDto[0];

            m_Sut.ValidateDtos(lineDtos);

            m_Validator.Received().ValidateDtos(lineDtos);
        }
    }
}