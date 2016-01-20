using System.Diagnostics.CodeAnalysis;
using System.IO;
using Selkie.Windsor;

namespace Selkie.GeoJson.Importer
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class SelkieStreamReader : ISelkieStreamReader
    {
        public string ReadToEnd(string filename)
        {
            string geoJsonText;

            using ( var r = new StreamReader(filename) )
            {
                geoJsonText = r.ReadToEnd();
            }

            return geoJsonText;
        }
    }
}