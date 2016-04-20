using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Lines.Tests.GeoJson.XUnit.Importer
{
    [ExcludeFromCodeCoverage]
    public sealed class XUnitHelper
    {
        public const double Epsilon = 0.00001;

        public static void AssertIsEquivalent(double value1,
                                              double value2,
                                              [NotNull] string text = "")
        {
            AssertIsEquivalent(value1,
                               value2,
                               Epsilon,
                               text);
        }

        public static void AssertIsEquivalent(double value1,
                                              double value2,
                                              double epsilon)
        {
            AssertIsEquivalent(value1,
                               value2,
                               Epsilon,
                               string.Empty);
        }

        // ReSharper disable once TooManyArguments
        public static void AssertIsEquivalent(double value1,
                                              double value2,
                                              double epsilon,
                                              [NotNull] string text)
        {
            double abs = Math.Abs(value1 - value2);

            if ( double.IsNaN(abs) )
            {
                throw new ArgumentException(text + " - Absolute difference is NaN!");
            }

            if ( abs > epsilon )
            {
                throw new ArgumentException(text + " Absolute difference {0} but epsilon is {1}!".Inject(abs,
                                                                                                         epsilon));
            }
        }

        public static bool IsEquivalent(double value1,
                                        double value2)
        {
            return IsEquivalent(value1,
                                value2,
                                Epsilon);
        }

        public static bool IsEquivalent(double value1,
                                        double value2,
                                        double epsilon)
        {
            double abs = Math.Abs(value1 - value2);

            return !double.IsNaN(abs) && abs < epsilon;
        }
    }
}