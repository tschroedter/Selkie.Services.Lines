using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Interfaces;
using Selkie.Services.Lines.Interfaces.Validators;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines
{
    [Interceptor(typeof( LogAspect ))]
    [ProjectComponent(Lifestyle.Singleton)]
    public class LinesSourceManager : ILinesSourceManager
    {
        public LinesSourceManager([NotNull] ISelkieLogger logger,
                                  [NotNull] ITestLineCreator creator,
                                  [NotNull] IFeatureValidator validator)
        {
            m_Logger = logger;
            m_Creator = creator;
            m_Validator = validator;
        }

        internal const int NumberOfLines = 10;
        private readonly ITestLineCreator m_Creator;
        private readonly ISelkieLogger m_Logger;
        private readonly IFeatureValidator m_Validator;

        public IEnumerable <ILine> GetTestLines(IEnumerable <TestLineType.Type> types)
        {
            ILine[] lines = GetAllRequestedTestLines(types).ToArray();

            if ( m_Validator.ValidateLines(lines) )
            {
                return lines;
            }

            m_Logger.Error("Lines are invalid!");

            return new Line[0];
        }

        public bool ValidateDtos(IEnumerable <LineDto> lineDtos)
        {
            return m_Validator.ValidateDtos(lineDtos);
        }

        // ReSharper disable once MethodTooLong
        // todo replace with table
        [NotNull]
        internal IEnumerable <ILine> GetTestLinesForType(TestLineType.Type type,
                                                         int maxId = 0)
        {
            switch ( type )
            {
                case TestLineType.Type.CreateLines:
                    return m_Creator.CreateLines(maxId);

                case TestLineType.Type.CreateFixedParallelLines:
                    return m_Creator.CreateFixedParallelLines(maxId);

                case TestLineType.Type.CreateParallelLines:
                    return m_Creator.CreateParallelLines(NumberOfLines,
                                                         maxId);

                case TestLineType.Type.CreateParallelLinesForwardReverse:
                    return m_Creator.CreateParallelLinesForwardReverse(NumberOfLines,
                                                                       maxId);

                case TestLineType.Type.CreateParallelLinesReverse:
                    return m_Creator.CreateParallelLinesReverse(NumberOfLines,
                                                                maxId);

                case TestLineType.Type.CreateBox:
                    return m_Creator.CreateBox(NumberOfLines,
                                               maxId);

                case TestLineType.Type.CreateCross:
                    return m_Creator.CreateCross(maxId);

                case TestLineType.Type.CreateCrossForwardReverse:
                    return m_Creator.CreateCrossForwardReverse(maxId);

                case TestLineType.Type.CreateParallelCrossLinesInCorner:
                    return m_Creator.CreateParallelCrossLinesInCorner(NumberOfLines,
                                                                      maxId);

                case TestLineType.Type.CreateParallelCrossLines:
                    return m_Creator.CreateParallelCrossLines(NumberOfLines,
                                                              maxId);

                case TestLineType.Type.CreateParallelCrossLinesForwardReverse:
                    return m_Creator.CreateParallelCrossLinesForwardReverse(NumberOfLines,
                                                                            maxId);

                case TestLineType.Type.CreateLinesInRowHorizontal:
                    return m_Creator.CreateLinesInRowHorizontal(NumberOfLines,
                                                                maxId);

                case TestLineType.Type.CreateRandomLines:
                    return m_Creator.CreateRandomLines(NumberOfLines,
                                                       maxId);

                case TestLineType.Type.Create45DegreeLines:
                    return m_Creator.Create45DegreeLines(NumberOfLines,
                                                         maxId);

                case TestLineType.Type.CreateTestLines:
                    return m_Creator.CreateTestLines(maxId);

                case TestLineType.Type.CreateTestLinesVertical:
                    return m_Creator.CreateTestLinesVertical(maxId);

                default:
                    throw new ArgumentException("Unknown type '{0}'!".Inject(type));
            }
        }

        [NotNull]
        private IEnumerable <ILine> GetAllRequestedTestLines([NotNull] IEnumerable <TestLineType.Type> types)
        {
            var lines = new List <ILine>();

            foreach ( TestLineType.Type type in types )
            {
                // ReSharper disable MaximumChainedReferences
                int maxId = lines.Any()
                                ? lines.Select(x => x.Id).Max() + 1
                                : 0;
                // ReSharper restore MaximumChainedReferences

                IEnumerable <ILine> testLines = GetTestLinesForType(type,
                                                                    maxId);

                lines.AddRange(testLines);
            }

            return lines;
        }
    }
}