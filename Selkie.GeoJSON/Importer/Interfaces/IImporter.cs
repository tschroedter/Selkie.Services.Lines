﻿using System.Collections.Generic;
using JetBrains.Annotations;
using NetTopologySuite.Features;
using Selkie.Geometry.Shapes;

namespace Selkie.GeoJSON.Importer.Interfaces
{
    public interface IImporter
    {
        FeatureCollection FeatureCollection { get; }
        IEnumerable <ILine> Lines { get; }
        void FromText([NotNull] string text);
    }
}