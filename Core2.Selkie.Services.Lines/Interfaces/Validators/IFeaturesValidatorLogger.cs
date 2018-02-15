using System.Collections.Generic;
using JetBrains.Annotations;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Geometry.Surveying;
using Core2.Selkie.Services.Lines.Common.Dto;

namespace Core2.Selkie.Services.Lines.Interfaces.Validators
{
    public interface IFeaturesValidatorLogger
    {
        string LineDtoToString([NotNull] LineDto lineDto);

        string LineToString([NotNull] ILine line);

        void LogFeatures(bool isValid,
                         [NotNull] IEnumerable <ILine> lines);

        void LogFeatures(bool isValid,
                         [NotNull] IEnumerable <ISurveyGeoJsonFeature> features);

        void LogIdsAreEmpty();

        void LogLineDtos(bool isValid,
                         [NotNull] IEnumerable <LineDto> lineDtos);

        void LogValidateStatus(bool validateIds);
    }
}