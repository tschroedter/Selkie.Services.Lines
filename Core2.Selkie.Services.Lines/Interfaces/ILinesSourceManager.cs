using System.Collections.Generic;
using JetBrains.Annotations;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Services.Lines.Common;
using Core2.Selkie.Services.Lines.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces
{
    public interface ILinesSourceManager
    {
        [NotNull]
        IEnumerable <ILine> GetTestLines([NotNull] IEnumerable <TestLineType.Type> types);

        bool ValidateDtos([NotNull] IEnumerable <LineDto> lineDtos);
    }
}