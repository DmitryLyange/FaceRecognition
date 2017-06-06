using System.Configuration;

namespace FaceRecognition.Infrastructure
{
    public abstract class AlgorithmConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("dataDirectory", IsRequired = true)]
        public string DataDirectory
        {
            get { return (string)this["dataDirectory"]; }
            set { this["dataDirectory"] = value; }
        }

        [ConfigurationProperty("scriptName", IsRequired = true)]
        public string ScriptName
        {
            get { return (string)this["scriptName"]; }
            set { this["scriptName"] = value; }
        }
    }
}
