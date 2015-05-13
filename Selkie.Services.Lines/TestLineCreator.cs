using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Geometry;
using Selkie.Geometry.Shapes;
using Selkie.Windsor;

namespace Selkie.Services.Lines
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    //ncrunch: no coverage start
    public class TestLineCreator : ITestLineCreator
    {
        private const int MinXValue = -100;
        private const int MaxXValue = 1900;
        private const int MinYValue = 0;
        private const int MaxYValue = 1900;

        public List <ILine> CreateLines(int id)
        {
            Point line1StartPoint = new Point(100.0,
                                              100.0);
            Point line1EndPoint = new Point(200.0,
                                            100.0);
            Line line1 = new Line(0,
                                  line1StartPoint,
                                  line1EndPoint);

            Point line2StartPoint = new Point(120.0,
                                              80.0);
            Point line2EndPoint = new Point(180.0,
                                            80.0);
            Line line2 = new Line(1,
                                  line2StartPoint,
                                  line2EndPoint);

            Point line3StartPoint = new Point(120.0,
                                              120.0);
            Point line3EndPoint = new Point(180.0,
                                            120.0);
            Line line3 = new Line(2,
                                  line3StartPoint,
                                  line3EndPoint);

            return new List <ILine>
                   {
                       line1,
                       line2,
                       line3
                   };
        }

        public List <ILine> CreateFixedParallelLines(int id)
        {
            Point line1StartPoint = new Point(100.0,
                                              0.0);
            Point line1EndPoint = new Point(200.0,
                                            0.0);
            Line line1 = new Line(id++,
                                  line1StartPoint,
                                  line1EndPoint);

            Point line2StartPoint = new Point(100.0,
                                              60.0);
            Point line2EndPoint = new Point(200.0,
                                            60.0);
            Line line2 = new Line(id,
                                  line2StartPoint,
                                  line2EndPoint);

            //            var line2 = new Line(id++, line2StartPoint, line2EndPoint);

            //            var line3StartPoint = new Point(100.0, 120.0);
            //            var line3EndPoint = new Point(200.0, 120.0);
            //            var line3 = new Line(id++, line3StartPoint, line3EndPoint);
            //
            //            var line4StartPoint = new Point(100.0, 180.0);
            //            var line4EndPoint = new Point(200.0, 180.0);
            //            var line4 = new Line(id, line4StartPoint, line4EndPoint);
            //
            //            var list = new List<ILine> {line1, line2, line3, line4};

            List <ILine> list = new List <ILine>
                                {
                                    line1,
                                    line2
                                };

            return list;
        }

        // ReSharper disable once TooManyArguments
        public List <ILine> CreateParallelLines(int numberOfLines,
                                                int id,
                                                double x1 = 100.0,
                                                double y1 = 0.0,
                                                double x2 = 300.0,
                                                double y2 = 0.0)
        {
            List <ILine> lines = new List <ILine>();

            for ( int i = 0; i < numberOfLines; i++ )
            {
                Point startPoint = new Point(x1,
                                             y1);
                Point endPoint = new Point(x2,
                                           y2);

                Line line = new Line(id++,
                                     startPoint,
                                     endPoint);

                lines.Add(line);

                y1 += 30;
                y2 += 30;
            }

            return lines;
        }

        // ReSharper disable once TooManyArguments
        public List <ILine> CreateParallelLinesForwardReverse(int numberOfLines,
                                                              int id,
                                                              double x1 = 100.0,
                                                              double y1 = 0.0,
                                                              double x2 = 300.0,
                                                              double y2 = 0.0)
        {
            List <ILine> lines = new List <ILine>();

            for ( int i = 0; i < numberOfLines; i++ )
            {
                Point startPoint = new Point(x1,
                                             y1);
                Point endPoint = new Point(x2,
                                           y2);

                ILine line = i % 2 == 0
                                 ? new Line(id++,
                                            startPoint,
                                            endPoint)
                                 : new Line(id++,
                                            endPoint,
                                            startPoint);

                lines.Add(line);

                y1 += 30;
                y2 += 30;
            }

            return lines;
        }

        // ReSharper disable once TooManyArguments
        public List <ILine> CreateParallelLinesReverse(int numberOfLines,
                                                       int id,
                                                       double x1 = 100.0,
                                                       double y1 = 0.0,
                                                       double x2 = 300.0,
                                                       double y2 = 0.0)
        {
            List <ILine> lines = new List <ILine>();

            for ( int i = 0; i < numberOfLines; i++ )
            {
                Point startPoint = new Point(x2,
                                             y2);
                Point endPoint = new Point(x1,
                                           y1);

                Line line = new Line(id++,
                                     startPoint,
                                     endPoint);

                lines.Add(line);

                y1 += 30;
                y2 += 30;
            }

            return lines;
        }

        // ReSharper disable once MethodTooLong
        public List <ILine> CreateBox(int numberOfLines,
                                      int id = 0)
        {
            List <ILine> lines = new List <ILine>();
            Point offset = new Point(1000.0,
                                     1000.0);

            for ( int i = 0; i < numberOfLines; i++ )
            {
                double delta = 30.0 * i;

                Point line1StartPoint = new Point(offset.X + 0.0 - delta,
                                                  offset.Y + 0.0 - delta);
                Point line1EndPoint = new Point(offset.X + 60.0 + delta,
                                                offset.Y + 0.0 - delta);
                Line line1 = new Line(id++,
                                      line1StartPoint,
                                      line1EndPoint);

                Point line2StartPoint = new Point(offset.X + 0.0 - delta,
                                                  offset.Y + 30.0 - delta);
                Point line2EndPoint = new Point(offset.X + 0.0 - delta,
                                                offset.Y + 60.0 + delta);
                Line line2 = new Line(id++,
                                      line2StartPoint,
                                      line2EndPoint);

                Point line3StartPoint = new Point(offset.X + 0.0 - delta,
                                                  offset.Y + 90.0 + delta);
                Point line3EndPoint = new Point(offset.X + 60.0 + delta,
                                                offset.Y + 90.0 + delta);
                Line line3 = new Line(id++,
                                      line3StartPoint,
                                      line3EndPoint);

                Point line4StartPoint = new Point(offset.X + 60.0 + delta,
                                                  offset.Y + 60.0 + delta);
                Point line4EndPoint = new Point(offset.X + 60.0 + delta,
                                                offset.Y + 30.0 - delta);
                Line line4 = new Line(id++,
                                      line4StartPoint,
                                      line4EndPoint);

                lines.AddRange(new[]
                               {
                                   line1,
                                   line2,
                                   line3,
                                   line4
                               });
            }
            return lines;
        }

        public List <ILine> CreateCross(int id = 0)
        {
            Point line1StartPoint = new Point(0.0,
                                              150.0);
            Point line1EndPoint = new Point(300.0,
                                            150.0);
            Line line1 = new Line(id++,
                                  line1StartPoint,
                                  line1EndPoint);

            Point line2StartPoint = new Point(150.0,
                                              0.0);
            Point line2EndPoint = new Point(150.0,
                                            300.0);
            Line line2 = new Line(id,
                                  line2StartPoint,
                                  line2EndPoint);

            return new List <ILine>
                   {
                       line1,
                       line2
                   };
        }

        public List <ILine> CreateCrossForwardReverse(int id = 0)
        {
            Point line1StartPoint = new Point(0.0,
                                              150.0);
            Point line1EndPoint = new Point(300.0,
                                            150.0);
            Line line1 = new Line(id++,
                                  line1StartPoint,
                                  line1EndPoint,
                                  Constants.LineDirection.Forward);

            Point line2StartPoint = new Point(150.0,
                                              0.0);
            Point line2EndPoint = new Point(150.0,
                                            300.0);
            Line line2 = new Line(id,
                                  line2StartPoint,
                                  line2EndPoint,
                                  Constants.LineDirection.Reverse);

            return new List <ILine>
                   {
                       line1,
                       line2
                   };
        }

        public List <ILine> CreateParallelCrossLinesInCorner(int numberOfLines,
                                                             int id = 0)
        {
            List <ILine> lines = new List <ILine>();

            List <ILine> swCorner = CreateParallelCrossLines(numberOfLines,
                                                             id);

            int lastId = swCorner.Max(x => x.Id) + 1;

            List <ILine> nwCorner = CreateParallelCrossLines(numberOfLines,
                                                             lastId,
                                                             0.0,
                                                             1030.0,
                                                             30.0,
                                                             1000.0);

            lastId = nwCorner.Max(x => x.Id) + 1;

            List <ILine> neCorner = CreateParallelCrossLines(numberOfLines,
                                                             lastId,
                                                             1000.0,
                                                             1030.0,
                                                             1030.0,
                                                             1000.0);

            lastId = neCorner.Max(x => x.Id) + 1;

            List <ILine> seCorner = CreateParallelCrossLines(numberOfLines,
                                                             lastId,
                                                             1000.0,
                                                             30.0,
                                                             1030.0);
            lines.AddRange(swCorner);
            lines.AddRange(nwCorner);
            lines.AddRange(neCorner);
            lines.AddRange(seCorner);

            return lines;
        }

        // ReSharper disable once MethodTooLong
        // ReSharper disable once TooManyArguments
        public List <ILine> CreateParallelCrossLines(int numberOfLines,
                                                     int idCount = 0,
                                                     double x1 = 0.0,
                                                     double y1 = 30.0,
                                                     double x2 = 30.0,
                                                     double y2 = 0)
        {
            List <ILine> lines = new List <ILine>();
            const double distanceBetweenLines = 30.0;
            double length = ( numberOfLines + 1 ) * distanceBetweenLines;

            for ( int i = 0; i < numberOfLines; i++ )
            {
                double offset = distanceBetweenLines * i;

                Point lineStartPoint = new Point(x1,
                                                 y1 + offset);
                Point lineEndPoint = new Point(x1 + length,
                                               y1 + offset);
                Line line = new Line(idCount++,
                                     lineStartPoint,
                                     lineEndPoint);

                lines.Add(line);
            }

            for ( int i = 0; i < numberOfLines; i++ )
            {
                double offset = distanceBetweenLines * i;

                Point line2StartPoint = new Point(x2 + offset,
                                                  y2);
                Point line2EndPoint = new Point(x2 + offset,
                                                y2 + length);
                Line line2 = new Line(idCount++,
                                      line2StartPoint,
                                      line2EndPoint);

                lines.Add(line2);
            }

            return lines;
        }

        // ReSharper disable once TooManyArguments
        public List <ILine> CreateParallelCrossLinesForwardReverse(int numberOfLines,
                                                                   int idCount = 0,
                                                                   double x1 = 0.0,
                                                                   double y1 = 30.0,
                                                                   double x2 = 30.0,
                                                                   double y2 = 0)
        {
            List <ILine> lines = new List <ILine>();
            const double distanceBetweenLines = 30.0;
            double length = ( numberOfLines + 1 ) * distanceBetweenLines;

            for ( int i = 0; i < numberOfLines; i++, idCount++ )
            {
                CreateParallelCrossLineTypeOne(idCount,
                                               x1,
                                               y1,
                                               distanceBetweenLines,
                                               i,
                                               length,
                                               lines);
            }

            for ( int i = 0; i < numberOfLines; i++, idCount++ )
            {
                CreateParallelCrossLineTypeTwo(idCount,
                                               x2,
                                               y2,
                                               distanceBetweenLines,
                                               i,
                                               length,
                                               lines);
            }

            return lines;
        }

        // ReSharper disable once TooManyArguments

        public List <ILine> CreateLinesInRowHorizontal(int numberOfLines,
                                                       int id = 0)
        {
            List <ILine> lines = new List <ILine>();
            const double distanceBetweenLines = 60.0;
            const double length = 30.0;

            for ( int i = 0; i < numberOfLines; i++ )
            {
                double offset = distanceBetweenLines * i;

                Point startPoint = new Point(30.0 + offset,
                                             0.0);
                Point endPoint = new Point(30.0 + offset + length,
                                           0.0);
                Line line = new Line(id++,
                                     startPoint,
                                     endPoint);

                lines.Add(line);
            }

            return lines;
        }

        public List <ILine> CreateRandomLines(int numberOfLines,
                                              int id = 0)
        {
            List <ILine> lines = new List <ILine>();

            Random random = new Random();

            for ( int i = 0; i < numberOfLines; i++ )
            {
                Point startPoint = new Point(random.Next(MinXValue,
                                                         MaxXValue),
                                             random.Next(MinYValue,
                                                         MaxYValue));

                Point endPoint = new Point(random.Next(MinXValue,
                                                       MaxXValue),
                                           random.Next(MinYValue,
                                                       MaxYValue));

                Line line = new Line(id++,
                                     startPoint,
                                     endPoint);

                lines.Add(line);
            }

            return lines;
        }

        public List <ILine> Create45DegreeLines(int numberOfLines,
                                                int id = 0)
        {
            List <ILine> lines = new List <ILine>();

            for ( int i = 0; i < numberOfLines; i++ )
            {
                Point startPoint = new Point(100 + 60 * i,
                                             500);

                Point endPoint = new Point(160 + 60 * i,
                                           560);

                Line line = new Line(id++,
                                     startPoint,
                                     endPoint);

                lines.Add(line);
            }

            return lines;
        }

        public List <ILine> CreateTestLines(int maxId)
        {
            List <ILine> lines = new List <ILine>();

            Line line1 = new Line(maxId++,
                                  new Point(150,
                                            610),
                                  new Point(350,
                                            610));
            Line line2 = new Line(maxId,
                                  new Point(350,
                                            520),
                                  new Point(150,
                                            520));

            lines.Add(line1);
            lines.Add(line2);

            return lines;
        }

        public List <ILine> CreateTestLinesVertical(int maxId)
        {
            List <ILine> lines = new List <ILine>();

            Line line1 = new Line(maxId++,
                                  new Point(200,
                                            800),
                                  new Point(200,
                                            600));
            Line line2 = new Line(maxId,
                                  new Point(260,
                                            800),
                                  new Point(260,
                                            600));

            lines.Add(line1);
            lines.Add(line2);

            return lines;
        }

        // ReSharper disable once TooManyArguments
        private static void CreateParallelCrossLineTypeTwo(int idCount,
                                                           double x2,
                                                           double y2,
                                                           double distanceBetweenLines,
                                                           int i,
                                                           double length,
                                                           [NotNull] ICollection <ILine> lines)
        {
            double offset = distanceBetweenLines * i;

            Point line2StartPoint = new Point(x2 + offset,
                                              y2);
            Point line2EndPoint = new Point(x2 + offset,
                                            y2 + length);
            Constants.LineDirection direction = i % 2 == 0
                                                    ? Constants.LineDirection.Forward
                                                    : Constants.LineDirection.Reverse;

            Line line2 = new Line(idCount,
                                  line2StartPoint,
                                  line2EndPoint);

            if ( direction == Constants.LineDirection.Reverse )
            {
                line2 = line2.Reverse() as Line;
            }

            lines.Add(line2);
        }

        // ReSharper disable once TooManyArguments
        private static void CreateParallelCrossLineTypeOne(int idCount,
                                                           double x1,
                                                           double y1,
                                                           double distanceBetweenLines,
                                                           int i,
                                                           double length,
                                                           [NotNull] List <ILine> lines)
        {
            double offset = distanceBetweenLines * i;

            Point lineStartPoint = new Point(x1,
                                             y1 + offset);
            Point lineEndPoint = new Point(x1 + length,
                                           y1 + offset);
            Constants.LineDirection direction = i % 2 == 0
                                                    ? Constants.LineDirection.Forward
                                                    : Constants.LineDirection.Reverse;

            Line line = new Line(idCount,
                                 lineStartPoint,
                                 lineEndPoint);

            if ( direction == Constants.LineDirection.Reverse )
            {
                line = line.Reverse() as Line;
            }

            lines.Add(line);
        }
    }

    public interface ITestLineCreator
    {
        [NotNull]
        List <ILine> CreateLines(int id);

        [NotNull]
        List <ILine> CreateFixedParallelLines(int id);

        // ReSharper disable once TooManyArguments
        [NotNull]
        List <ILine> CreateParallelLines(int numberOfLines,
                                         int id,
                                         double x1 = 100.0,
                                         double y1 = 0.0,
                                         double x2 = 300.0,
                                         double y2 = 0.0);

        // ReSharper disable once TooManyArguments
        [NotNull]
        List <ILine> CreateParallelLinesForwardReverse(int numberOfLines,
                                                       int id,
                                                       double x1 = 100.0,
                                                       double y1 = 0.0,
                                                       double x2 = 300.0,
                                                       double y2 = 0.0);

        // ReSharper disable once TooManyArguments
        [NotNull]
        List <ILine> CreateParallelLinesReverse(int numberOfLines,
                                                int id,
                                                double x1 = 100.0,
                                                double y1 = 0.0,
                                                double x2 = 300.0,
                                                double y2 = 0.0);

        [NotNull]
        List <ILine> CreateBox(int numberOfLines,
                               int id = 0);

        [NotNull]
        List <ILine> CreateCross(int id = 0);

        [NotNull]
        List <ILine> CreateCrossForwardReverse(int id = 0);

        [NotNull]
        List <ILine> CreateParallelCrossLinesInCorner(int numberOfLines,
                                                      int id = 0);

        // ReSharper disable once TooManyArguments
        [NotNull]
        List <ILine> CreateParallelCrossLines(int numberOfLines,
                                              int idCount = 0,
                                              double x1 = 0.0,
                                              double y1 = 30.0,
                                              double x2 = 30.0,
                                              double y2 = 0);

        // ReSharper disable once TooManyArguments
        [NotNull]
        List <ILine> CreateParallelCrossLinesForwardReverse(int numberOfLines,
                                                            int idCount = 0,
                                                            double x1 = 0.0,
                                                            double y1 = 30.0,
                                                            double x2 = 30.0,
                                                            double y2 = 0);

        [NotNull]
        List <ILine> CreateLinesInRowHorizontal(int numberOfLines,
                                                int id = 0);

        [NotNull]
        List <ILine> CreateRandomLines(int numberOfLines,
                                       int id = 0);

        [NotNull]
        List <ILine> Create45DegreeLines(int numberOfLines,
                                         int id = 0);

        [NotNull]
        List <ILine> CreateTestLines(int maxId);

        [NotNull]
        List <ILine> CreateTestLinesVertical(int maxId);
    }
}