using System.Configuration;

namespace FaceRecognition.Infrastructure
{
    public class PythonConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("exePath", IsRequired = true)]
        public string ExePath
        {
            get { return (string)this["exePath"]; }
            set { this["exePath"] = value; }
        }
    }
}
