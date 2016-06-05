using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines
{
    public interface ILinesValidatorLogger
    {
        string LineDtoToString([NotNull] LineDto lineDto);

        string LineToString([NotNull] ILine line);
        void LogIdsAreEmpty();

        void LogLineDtos(bool isValid,
                         [NotNull] IEnumerable <LineDto> lineDtos);

        void LogLines(bool isValid,
                      [NotNull] IEnumerable <ILine> lines);

        void LogValidateStatus(bool validateIds);
    }
}