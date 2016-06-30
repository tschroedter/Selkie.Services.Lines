using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Geometry;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class LineToLineDtoConverterTests
    {
        public LineToLineDtoConverterTests()
        {
            ILine line = new Line(1,
                                  2.0,
                                  3.0,
                                  4.0,
                                  5.0,
                                  Constants.LineDirection.Reverse,
                                  true);

            m_DtoFromLine = new LineToLineDtoConverter().ConvertFrom(line);

            m_Dto = new LineDto
                    {
                        Id = 1,
                        X1 = 2.0,
                        Y1 = 3.0,
                        X2 = 4.0,
                        Y2 = 5.0,
                        IsUnknown = true,
                        RunDirection = "Reverse"
                    };
        }

        private const double Tolerance = 0.01;
        private readonly LineDto m_Dto;
        private readonly LineDto m_DtoFromLine;

        [Test]
        public void IdDefaultForFromLineTest()
        {
            Assert.AreEqual(1,
                            m_DtoFromLine.Id);
        }

        [Test]
        public void IdDefaultTest()
        {
            Assert.AreEqual(1,
                            m_Dto.Id);
        }

        [Test]
        public void IdRoundtripTest()
        {
            m_Dto.X1 = 2;

            Assert.AreEqual(2,
                            m_Dto.X1);
        }

        [Test]
        public void IsUnknownDefaultForFromLineTest()
        {
            Assert.True(m_DtoFromLine.IsUnknown);
        }

        [Test]
        public void IsUnknownDefaultTest()
        {
            Assert.True(m_Dto.IsUnknown);
        }

        [Test]
        public void IsUnknownRoundtripForConvertToLineTest()
        {
            ILine actual = new LineToLineDtoConverter().ConvertToLine(m_Dto);

            Assert.True(actual.IsUnknown);
        }

        [Test]
        public void IsUnknownRoundtripTest()
        {
            m_Dto.IsUnknown = false;

            Assert.False(m_Dto.IsUnknown);
        }

        [Test]
        public void RunDirectionDefaultForFromLineTest()
        {
            Assert.AreEqual(Selkie.Common.Constants.LineDirection.Reverse.ToString(),
                            m_DtoFromLine.RunDirection);
        }

        [Test]
        public void RunDirectionDefaultTest()
        {
            Assert.AreEqual(Selkie.Common.Constants.LineDirection.Reverse.ToString(),
                            m_Dto.RunDirection);
        }

        [Test]
        public void RunDirectionRoundtripForConvertToLineTest()
        {
            ILine actual = new LineToLineDtoConverter().ConvertToLine(m_Dto);

            Assert.NotNull(m_Dto.RunDirection);
            Assert.NotNull(actual.RunDirection);
            Assert.True(string.Compare(m_Dto.RunDirection,
                                       actual.RunDirection.ToString(),
                                       StringComparison.InvariantCulture) == 0);
        }

        [Test]
        public void RunDirectionRoundtripTest()
        {
            m_Dto.RunDirection = Selkie.Common.Constants.LineDirection.Forward.ToString();

            Assert.AreEqual(Selkie.Common.Constants.LineDirection.Forward.ToString(),
                            m_Dto.RunDirection);
        }

        [Test]
        public void X1DefaultForFromLineTest()
        {
            Assert.AreEqual(2.0,
                            m_DtoFromLine.X1);
        }

        [Test]
        public void X1DefaultTest()
        {
            Assert.AreEqual(2.0,
                            m_Dto.X1);
        }

        [Test]
        public void X1RoundtripForConvertToLineTest()
        {
            ILine actual = new LineToLineDtoConverter().ConvertToLine(m_Dto);

            Assert.True(Math.Abs(m_Dto.X1 - actual.X1) < Tolerance);
        }

        [Test]
        public void X1RoundtripForFromLineTest()
        {
            var dto = new LineDto
                      {
                          X1 = 1.0
                      };
            Assert.AreEqual(1.0,
                            dto.X1);

            dto.X1 = 2.0;
            Assert.AreEqual(2.0,
                            dto.X1);
        }

        [Test]
        public void X1RoundtripTest()
        {
            m_Dto.X1 = 2.0;

            Assert.AreEqual(2.0,
                            m_Dto.X1);
        }

        [Test]
        public void X2DefaultForFromLineTest()
        {
            Assert.AreEqual(4.0,
                            m_DtoFromLine.X2);
        }

        [Test]
        public void X2DefaultTest()
        {
            Assert.AreEqual(4.0,
                            m_Dto.X2);
        }

        [Test]
        public void X2RoundtripForConvertToLineTest()
        {
            ILine actual = new LineToLineDtoConverter().ConvertToLine(m_Dto);

            Assert.True(Math.Abs(m_Dto.X2 - actual.X2) < Tolerance);
        }

        [Test]
        public void X2RoundtripTest()
        {
            m_Dto.X2 = 20.0;

            Assert.AreEqual(20.0,
                            m_Dto.X2);
        }

        [Test]
        public void Y1DefaultForFromLineTest()
        {
            Assert.AreEqual(3.0,
                            m_DtoFromLine.Y1);
        }

        [Test]
        public void Y1DefaultTest()
        {
            Assert.AreEqual(3.0,
                            m_Dto.Y1);
        }

        [Test]
        public void Y1RoundtripForConvertToLineTest()
        {
            ILine actual = new LineToLineDtoConverter().ConvertToLine(m_Dto);

            Assert.True(Math.Abs(m_Dto.Y1 - actual.Y1) < Tolerance);
        }

        [Test]
        public void Y1RoundtripTest()
        {
            m_Dto.Y1 = 20.0;

            Assert.AreEqual(20.0,
                            m_Dto.Y1);
        }

        [Test]
        public void Y2DefaultForFromLineTest()
        {
            Assert.AreEqual(5.0,
                            m_DtoFromLine.Y2);
        }

        [Test]
        public void Y2DefaultTest()
        {
            Assert.AreEqual(5.0,
                            m_Dto.Y2);
        }

        [Test]
        public void Y2RoundtripForConvertToLineTest()
        {
            ILine actual = new LineToLineDtoConverter().ConvertToLine(m_Dto);

            Assert.True(Math.Abs(m_Dto.Y2 - actual.Y2) < Tolerance);
        }

        [Test]
        public void Y2RoundtripTest()
        {
            m_Dto.Y2 = 20.0;

            Assert.AreEqual(20.0,
                            m_Dto.Y2);
        }
    }
}