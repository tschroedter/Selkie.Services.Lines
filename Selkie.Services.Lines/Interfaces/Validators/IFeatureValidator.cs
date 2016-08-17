using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.Services.Lines.Common.Dto;

namespace Selkie.Services.Lines.Interfaces.Validators
{
    public interface IFeatureValidator
    {
        bool ValidateDtos([NotNull] IEnumerable <LineDto> lineDtos);
        bool ValidateFeatures(IEnumerable <ISurveyGeoJsonFeature> features);
        bool ValidateLines([NotNull] IEnumerable <ILine> lines);
    }
}