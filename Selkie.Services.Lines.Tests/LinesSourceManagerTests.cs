using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class LinesSourceManagerTests
    {
        public LinesSourceManagerTests()
        {
            var logger = Substitute.For <ISelkieLogger>();
            m_Creator = Substitute.For <ITestLineCreator>();
            m_Validator = Substitute.For <ILinesValidator>();

            m_Sut = new LinesSourceManager(logger,
                                           m_Creator,
                                           m_Validator);
        }

        private readonly ITestLineCreator m_Creator;
        private readonly LinesSourceManager m_Sut;
        private readonly ILinesValidator m_Validator;

        [Test]
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

        [Test]
        public void GetTestLinesForTypeCreateTestLinesVerticalTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateTestLinesVertical);

            m_Creator.Received().CreateTestLinesVertical(0);
        }

        [Test]
        public void GetTestLinesForTypeFoCreateParallelCrossLinesInCornerTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelCrossLinesInCorner);

            m_Creator.Received().CreateParallelCrossLinesInCorner(LinesSourceManager.NumberOfLines);
        }

        [Test]
        public void GetTestLinesForTypeForCreate45DegreeLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.Create45DegreeLines);

            m_Creator.Received().Create45DegreeLines(LinesSourceManager.NumberOfLines);
        }

        [Test]
        public void GetTestLinesForTypeForCreateBoxTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateBox);

            m_Creator.Received().CreateBox(LinesSourceManager.NumberOfLines);
        }

        [Test]
        public void GetTestLinesForTypeForCreateCrossForwardReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateCrossForwardReverse);

            m_Creator.Received().CreateCrossForwardReverse();
        }

        [Test]
        public void GetTestLinesForTypeForCreateCrossTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateCross);

            m_Creator.Received().CreateCross();
        }

        [Test]
        public void GetTestLinesForTypeForCreateFixedParallelLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateFixedParallelLines);

            m_Creator.Received().CreateFixedParallelLines(0);
        }

        [Test]
        public void GetTestLinesForTypeForCreateLinesCallsCreatorTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateLines);

            m_Creator.Received().CreateLines(0);
        }

        [Test]
        public void GetTestLinesForTypeForCreateLinesCallsCreatorWithMaxIdTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateLines,
                                      123);

            m_Creator.Received().CreateLines(123);
        }

        [Test]
        public void GetTestLinesForTypeForCreateLinesInRowHorizontalTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateLinesInRowHorizontal);

            m_Creator.Received().CreateLinesInRowHorizontal(LinesSourceManager.NumberOfLines);
        }

        [Test]
        public void GetTestLinesForTypeForCreateParallelCrossLinesForwardReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelCrossLinesForwardReverse);

            m_Creator.Received().CreateParallelCrossLinesForwardReverse(LinesSourceManager.NumberOfLines);
        }

        [Test]
        public void GetTestLinesForTypeForCreateParallelCrossLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelCrossLines);

            m_Creator.Received().CreateParallelCrossLines(LinesSourceManager.NumberOfLines);
        }

        [Test]
        public void GetTestLinesForTypeForCreateParallelLinesForwardReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelLinesForwardReverse);

            m_Creator.Received().CreateParallelLinesForwardReverse(LinesSourceManager.NumberOfLines,
                                                                   0);
        }

        [Test]
        public void GetTestLinesForTypeForCreateParallelLinesReverseTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelLinesReverse);

            m_Creator.Received().CreateParallelLinesReverse(10,
                                                            0);
        }

        [Test]
        public void GetTestLinesForTypeForCreateParallelLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateParallelLines);

            m_Creator.Received().CreateParallelLines(10,
                                                     0);
        }

        [Test]
        public void GetTestLinesForTypeForCreateRandomLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateRandomLines);

            m_Creator.Received().CreateRandomLines(LinesSourceManager.NumberOfLines);
        }

        [Test]
        public void GetTestLinesForTypeForCreateTestLinesTest()
        {
            m_Sut.GetTestLinesForType(TestLineType.Type.CreateTestLines);

            m_Creator.Received().CreateTestLines(0);
        }

        [Test]
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

            Assert.AreEqual(0,
                            actual.Count());
        }

        [Test]
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

            Assert.AreEqual(1,
                            actual.Count());
        }

        [Test]
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

            Assert.AreEqual(2,
                            actual.Count());
        }

        [Test]
        public void ValidateDtosCallsValidatorTest()
        {
            var lineDtos = new LineDto[0];

            m_Sut.ValidateDtos(lineDtos);

            m_Validator.Received().ValidateDtos(lineDtos);
        }
    }
}