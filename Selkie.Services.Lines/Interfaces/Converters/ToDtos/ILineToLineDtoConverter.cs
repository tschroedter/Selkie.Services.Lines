using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface ILineToLineDtoConverter
    {
        LineDto ConvertFrom([NotNull] ILine line);
        ILine ConvertToLine([NotNull] LineDto dto);
    }
}