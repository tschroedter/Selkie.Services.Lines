using JetBrains.Annotations;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Services.Lines.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces.Converters.ToDtos
{
    public interface ILineToLineDtoConverter
    {
        LineDto ConvertFrom([NotNull] ILine line);
        ILine ConvertToLine([NotNull] LineDto dto);
    }
}