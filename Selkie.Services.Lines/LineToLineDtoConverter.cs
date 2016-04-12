using System;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Geometry;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Windsor;

namespace Selkie.Services.Lines
{
    [Interceptor(typeof ( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class LineToLineDtoConverter : ILineToLineDtoConverter
    {
        [NotNull]
        public LineDto ConvertFrom(ILine line)
        {
            var dto = new LineDto
                      {
                          X1 = line.X1,
                          Y1 = line.Y1,
                          X2 = line.X2,
                          Y2 = line.Y2,
                          Id = line.Id,
                          IsUnknown = line.IsUnknown,
                          RunDirection = line.RunDirection.ToString()
                      };

            return dto;
        }

        [NotNull]
        public ILine ConvertToLine(LineDto dto)
        {
            Constants.LineDirection direction;

            Enum.TryParse(dto.RunDirection,
                          out direction);

            var line = new Line(dto.Id,
                                dto.X1,
                                dto.Y1,
                                dto.X2,
                                dto.Y2,
                                dto.IsUnknown,
                                direction);

            return line;
        }
    }
}