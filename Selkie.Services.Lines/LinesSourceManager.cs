﻿using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines
{
    [ProjectComponent(Lifestyle.Singleton)]
    public class LinesSourceManager : ILinesSourceManager
    {
        private readonly ITestLineCreator m_Creator;
        private readonly ILogger m_Logger;
        private readonly ILinesValidator m_Validator;

        public LinesSourceManager([NotNull] ILogger logger,
                                  [NotNull] ITestLineCreator creator,
                                  [NotNull] ILinesValidator validator)
        {
            m_Logger = logger;
            m_Creator = creator;
            m_Validator = validator;
        }

        public IEnumerable <ILine> GetTestLines(IEnumerable <TestLineType.Type> types)
        {
            ILine[] lines = GetAllRequestedTestLines(types).ToArray();

            if ( !m_Validator.ValidateLines(lines) )
            {
                m_Logger.Error("Lines are invalid!");

                return new Line[0];
            }

            return lines;
        }

        public bool ValidateDtos(IEnumerable <LineDto> lineDtos)
        {
            return m_Validator.ValidateDtos(lineDtos);
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

        // ReSharper disable once MethodTooLong
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
                    return m_Creator.CreateParallelLines(10,
                                                         maxId);

                case TestLineType.Type.CreateParallelLinesForwardReverse:
                    return m_Creator.CreateParallelLinesForwardReverse(10,
                                                                       maxId);

                case TestLineType.Type.CreateParallelLinesReverse:
                    return m_Creator.CreateParallelLinesReverse(10,
                                                                maxId);

                case TestLineType.Type.CreateBox:
                    return m_Creator.CreateBox(maxId);

                case TestLineType.Type.CreateCross:
                    return m_Creator.CreateCross(maxId);

                case TestLineType.Type.CreateCrossForwardReverse:
                    return m_Creator.CreateCrossForwardReverse(maxId);

                case TestLineType.Type.CreateParallelCrossLinesInCorner:
                    return m_Creator.CreateParallelCrossLinesInCorner(maxId);

                case TestLineType.Type.CreateParallelCrossLines:
                    return m_Creator.CreateParallelCrossLines(maxId);

                case TestLineType.Type.CreateParallelCrossLinesForwardReverse:
                    return m_Creator.CreateParallelCrossLinesForwardReverse(maxId);

                case TestLineType.Type.CreateLinesInRowHorizontal:
                    return m_Creator.CreateLinesInRowHorizontal(maxId);

                case TestLineType.Type.CreateRandomLines:
                    return m_Creator.CreateRandomLines(maxId);

                case TestLineType.Type.Create45DegreeLines:
                    return m_Creator.Create45DegreeLines(maxId);

                case TestLineType.Type.CreateTestLines:
                    return m_Creator.CreateTestLines(maxId);

                case TestLineType.Type.CreateTestLinesVertical:
                    return m_Creator.CreateTestLinesVertical(maxId);

                default:
                    throw new ArgumentException("Unknown type '{0}'!".Inject(type));
            }
        }
    }
}