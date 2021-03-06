﻿using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.Geometry;
using Selkie.Geometry.Shapes;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines.Console.Client
{
    [ExcludeFromCodeCoverage]
    public class LineToLineDtoConverter
    {
        [NotNull]
        public static LineDto ConvertFrom([NotNull] ILine line)
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
        public static ILine ConvertToLine([NotNull] LineDto dto)
        {
            Constants.LineDirection direction;

            Enum.TryParse(dto.RunDirection,
                          out direction);

            var line = new Line(dto.Id,
                                dto.X1,
                                dto.Y1,
                                dto.X2,
                                dto.Y2,
                                direction,
                                dto.IsUnknown);

            return line;
        }
    }
}