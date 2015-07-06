using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines
{
    public interface ILinesValidatorLogger
    {
        void LogLines(bool isValid, [NotNull] IEnumerable<ILine> lines);
        string LineToString([NotNull] ILine line);
        void LogLineDtos(bool isValid, [NotNull] IEnumerable<LineDto> lineDtos);
        string LineDtoToString([NotNull] LineDto lineDto);
        void LogValidateStatus(bool validateIds);
        void LogIdsAreEmpty();
    }
}