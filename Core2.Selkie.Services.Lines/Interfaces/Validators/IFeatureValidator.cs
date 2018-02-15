using System.Collections.Generic;
using JetBrains.Annotations;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Geometry.Surveying;
using Core2.Selkie.Services.Lines.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces.Validators
{
    public interface IFeatureValidator
    {
        bool ValidateDtos([NotNull] IEnumerable <LineDto> lineDtos);
        bool ValidateFeatures(IEnumerable <ISurveyGeoJsonFeature> features);
        bool ValidateLines([NotNull] IEnumerable <ILine> lines);
    }
}