using System.Collections.Generic;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aop.Aspects;
using Selkie.Geometry.Shapes;
using Selkie.Geometry.Surveying;
using Selkie.Services.Lines.Common.Dto;
using Selkie.Services.Lines.Interfaces.Validators;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines.Validators
{
    [Interceptor(typeof( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class FeaturesValidatorLogger : IFeaturesValidatorLogger
    {
        public FeaturesValidatorLogger([NotNull] ISelkieLogger logger)
        {
            m_Logger = logger;
        }

        private readonly ISelkieLogger m_Logger;

        public void LogFeatures(bool isValid,
                                IEnumerable <ISurveyGeoJsonFeature> features)
        {
            LogValidateStatus(isValid);

            foreach ( ISurveyGeoJsonFeature feature in features )
            {
                m_Logger.Info(FeatureToString(feature));
            }
        }

        public void LogValidateStatus(bool validateIds)
        {
            if ( validateIds )
            {
                m_Logger.Info("Features are valid!");
            }
            else
            {
                m_Logger.Warn("Features are invalid!");
            }
        }

        public void LogIdsAreEmpty()
        {
            m_Logger.Warn("Ids array is empty!");
        }

        public string LineToString(ILine line)
        {
            return "[{0}][{1:F2},{2:F2}] - [{3:F2},{4:F2}]".Inject(line.Id,
                                                                   line.X1,
                                                                   line.Y1,
                                                                   line.X2,
                                                                   line.Y1);
        }

        public string LineDtoToString(LineDto lineDto)
        {
            return "[{0}][{1:F2},{2:F2}] - [{3:F2},{4:F2}]".Inject(lineDto.Id,
                                                                   lineDto.X1,
                                                                   lineDto.Y1,
                                                                   lineDto.X2,
                                                                   lineDto.Y1);
        }

        public void LogFeatures(bool isValid,
                                IEnumerable <ILine> lines)
        {
            LogValidateStatus(isValid);

            foreach ( ILine line in lines )
            {
                m_Logger.Info(LineToString(line));
            }
        }

        public void LogLineDtos(bool isValid,
                                IEnumerable <LineDto> lineDtos)
        {
            LogValidateStatus(isValid);

            foreach ( LineDto lineDto in lineDtos )
            {
                m_Logger.Info(LineDtoToString(lineDto));
            }
        }

        public string FeatureToString(ISurveyGeoJsonFeature feature)
        {
            return
                "[{0}][StartPoint:{1}] - [EndPoint:{2}] [Length:{3}] [AngleToXAxisAtStartPoint:{4}] [AngleToXAxisAtEndPoint{5}]"
                    .Inject(feature.SurveyFeature.Id,
                            feature.SurveyFeature.StartPoint,
                            feature.SurveyFeature.EndPoint,
                            feature.SurveyFeature.Length,
                            feature.SurveyFeature.AngleToXAxisAtStartPoint,
                            feature.SurveyFeature.AngleToXAxisAtEndPoint);
        }
    }
}