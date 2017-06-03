using System.Configuration;

namespace FaceRecognition.Infrastructure
{
    public class PCAConfigurationSection: ConfigurationSection
    {
        [ConfigurationProperty("directory", IsRequired = true)]
        public string Directory
        {
            get { return (string)this["directory"]; }
            set { this["directory"] = value; }
        }
    }
}
