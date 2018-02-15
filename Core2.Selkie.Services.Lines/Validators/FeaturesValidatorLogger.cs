using System.Collections.Generic;
using Castle.Core;
using JetBrains.Annotations;
using Core2.Selkie.Aop.Aspects;
using Core2.Selkie.Geometry.Shapes;
using Core2.Selkie.Geometry.Surveying;
using Core2.Selkie.Services.Lines.Common.Dto;
using Core2.Selkie.Services.Lines.Interfaces.Validators;
using Core2.Selkie.Windsor;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Services.Lines.Validators
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
            return $"[{line.Id}][{line.X1:F2},{line.Y1:F2}] - [{line.X2:F2},{line.Y2:F2}]";
        }

        public string LineDtoToString(LineDto lineDto)
        {
            return $"[{lineDto.Id}][{lineDto.X1:F2},{lineDto.Y1:F2}] - [{lineDto.X2:F2},{lineDto.Y2:F2}]";
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

        public string FeatureToString(ISurveyGeoJsonFeature feature)    // todo remove ISurveyGeoJsonFeature
        {
            return
                $"[{feature.SurveyFeature.Id}]" +
                $"[StartPoint:{feature.SurveyFeature.StartPoint}] - [EndPoint:{feature.SurveyFeature.EndPoint}] " +
                $"[Length:{feature.SurveyFeature.Length}] " +
                $"[AngleToXAxisAtStartPoint:{feature.SurveyFeature.AngleToXAxisAtStartPoint}] " +
                $"[AngleToXAxisAtEndPoint{feature.SurveyFeature.AngleToXAxisAtEndPoint}]";
        }
    }
}