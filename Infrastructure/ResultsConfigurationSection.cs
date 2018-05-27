using System.Configuration;

namespace FaceRecognition.Infrastructure
{
    public class ResultsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("dataDirectory", IsRequired = true)]
        public string DataDirectory
        {
            get { return (string)this["dataDirectory"]; }
            set { this["dataDirectory"] = value; }
        }
    }
}
