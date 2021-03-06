﻿using System.Collections.Generic;
using Castle.Core;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Aop.Aspects;
using Selkie.GeoJSON.Importer.Interfaces;
using Selkie.Geometry.Shapes;
using Selkie.Windsor;

namespace Selkie.GeoJSON.Importer
{
    [Interceptor(typeof ( LogAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public class Importer : IImporter
    {
        private readonly IFeaturesToLinesConverter m_Converter;
        private readonly IGeoJsonStringReader m_Reader;
        private readonly IFeaturesValidator m_Validator;

        public Importer([NotNull] IGeoJsonStringReader reader,
                        [NotNull] IFeaturesValidator validator,
                        [NotNull] IFeaturesToLinesConverter converter)
        {
            m_Reader = reader;
            m_Validator = validator;
            m_Converter = converter;
        }

        public FeatureCollection FeatureCollection
        {
            get
            {
                return m_Validator.Supported;
            }
        }

        public IEnumerable <ILine> Lines
        {
            get
            {
                return m_Converter.Lines;
            }
        }

        public void FromText(string text)
        {
            FeatureCollection featureCollection = m_Reader.Read(text);

            m_Validator.FeatureCollection = featureCollection;
            m_Validator.Validate();

            m_Converter.FeatureCollection = m_Validator.FeatureCollection;
            m_Converter.Convert();
        }
    }
}