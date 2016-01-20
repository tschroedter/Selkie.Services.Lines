using GeoJSON.Net.Feature;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Selkie.Windsor;

namespace Selkie.GeoJson.Importer
{
    [ProjectComponent(Lifestyle.Transient)]
    public class FileReader : IFileReader
    {
        private readonly ISelkieStreamReader m_Reader;

        public FileReader([NotNull] ISelkieStreamReader reader)
        {
            m_Reader = reader;
        }

        public FeatureCollection Read(string filename) // todo IFeatureCollection
        {
            string geoJsonText = m_Reader.ReadToEnd(filename);

            var features =
                JsonConvert.DeserializeObject <FeatureCollection>(geoJsonText,
                                                                  new JsonSerializerSettings
                                                                  {
                                                                      ContractResolver =
                                                                          new CamelCasePropertyNamesContractResolver()
                                                                  });

            return features;
        }
    }
}