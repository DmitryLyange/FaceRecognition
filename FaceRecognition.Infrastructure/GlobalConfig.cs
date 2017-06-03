using System.Configuration;

namespace FaceRecognition.Infrastructure
{
    public class GlobalConfig
    {
        private static string _pCADataDirectory;
        public static string PCADataDirectory
        {
            get
            {
                if (_pCADataDirectory == null)
                    LoadConfig();

                return _pCADataDirectory;
            }
        }


        private static PCAConfigurationSection _pcaConfig;

        private static void LoadConfig()
        {
            _pcaConfig = (PCAConfigurationSection)ConfigurationManager.GetSection("PCAConfiguration");
            _pCADataDirectory = _pcaConfig.Directory;
        }
    }
}
