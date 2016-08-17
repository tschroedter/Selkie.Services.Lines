using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Interfaces.Validators;
using Selkie.Windsor;

namespace Selkie.Services.Lines.Validators
{
    [Interceptor(typeof( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class FeatureValidator : IFeatureValidator
    {
        public FeatureValidator([NotNull] IFeaturesValidatorLogger logger)
        {
            m_Logger = logger;
        }

        private readonly IFeaturesValidatorLogger m_Logger;

        public bool ValidateDtos(IEnumerable <LineDto> lineDtos)
        {
            IEnumerable <LineDto> array = lineDtos as LineDto[] ?? lineDtos.ToArray();
            IEnumerable <int> ids = array.Select(x => x.Id);

            bool validateDtos = ValidateIds(ids);

            m_Logger.LogLineDtos(validateDtos,
                                 array);

            return validateDtos;
        }

        public bool ValidateLines(IEnumerable <ILine> lines)
        {
            IEnumerable <ILine> array = lines as ILine[] ?? lines.ToArray();
            IEnumerable <int> ids = array.Select(x => x.Id);

            bool validateLines = ValidateIds(ids);

            m_Logger.LogFeatures(validateLines,
                                 array);

            return validateLines;
        }

        public bool ValidateFeatures(IEnumerable <ISurveyGeoJsonFeature> features)
        {
            IEnumerable <ISurveyGeoJsonFeature> array = features as ISurveyGeoJsonFeature[] ?? features.ToArray();
            IEnumerable <int> ids = array.Select(x => x.Id);

            bool validateIds = ValidateIds(ids);

            m_Logger.LogFeatures(validateIds,
                                 array);

            return validateIds;
        }

        private bool Validate(IEnumerable <int> array)
        {
            var expectedId = 0;

            bool validateIds = array.All(id => expectedId++ == id);
            return validateIds;
        }

        private bool ValidateIds([NotNull] IEnumerable <int> ids)
        {
            int[] array = ids.ToArray();

            if ( array.Length <= 1 )
            {
                m_Logger.LogIdsAreEmpty();

                return false;
            }

            bool validateIds = Validate(array);

            m_Logger.LogValidateStatus(validateIds);

            return validateIds;
        }
    }
}