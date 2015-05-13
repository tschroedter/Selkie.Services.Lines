using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines
{
    public interface ILinesSourceManager
    {
        [NotNull]
        IEnumerable <ILine> GetTestLines([NotNull] IEnumerable <TestLineType.Type> types);

        bool ValidateDtos([NotNull] IEnumerable <LineDto> lineDtos);
    }
}