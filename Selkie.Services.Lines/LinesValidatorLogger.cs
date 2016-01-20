using System.Collections.Generic;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines
{
    [Interceptor(typeof ( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class LinesValidatorLogger : ILinesValidatorLogger
    {
        private readonly ISelkieLogger m_Logger;

        public LinesValidatorLogger([NotNull] ISelkieLogger logger)
        {
            m_Logger = logger;
        }

        public void LogValidateStatus(bool validateIds)
        {
            if ( validateIds )
            {
                m_Logger.Info("Lines are valid!");
            }
            else
            {
                m_Logger.Warn("Lines are invalid!");
            }
        }

        public void LogIdsAreEmpty()
        {
            m_Logger.Warn("Ids array is empty!");
        }

        public string LineToString(ILine line)
        {
            return "[{0}][{1:F2},{2:F2}] - [{3:F2},{4:F2}]".Inject(line.Id,
                                                                   line.X1,
                                                                   line.Y1,
                                                                   line.X2,
                                                                   line.Y1);
        }

        public string LineDtoToString(LineDto lineDto)
        {
            return "[{0}][{1:F2},{2:F2}] - [{3:F2},{4:F2}]".Inject(lineDto.Id,
                                                                   lineDto.X1,
                                                                   lineDto.Y1,
                                                                   lineDto.X2,
                                                                   lineDto.Y1);
        }

        public void LogLines(bool isValid,
                             IEnumerable <ILine> lines)
        {
            LogValidateStatus(isValid);

            foreach ( ILine line in lines )
            {
                m_Logger.Info(LineToString(line));
            }
        }

        public void LogLineDtos(bool isValid,
                                IEnumerable <LineDto> lineDtos)
        {
            LogValidateStatus(isValid);

            foreach ( LineDto lineDto in lineDtos )
            {
                m_Logger.Info(LineDtoToString(lineDto));
            }
        }
    }
}