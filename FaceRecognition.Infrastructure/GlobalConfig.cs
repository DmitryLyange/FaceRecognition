using System.Configuration;

namespace FaceRecognition.Infrastructure
{
    public class GlobalConfig
    {
        private static string _pythonExePath;
        public static string PythonExePath
        {
            get
            {
                if (_pythonExePath == null)
                    LoadConfig();

                return _pythonExePath;
            }
        }

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


        private static PythonConfigurationSection _pythonConfig;
        private static PCAConfigurationSection _pcaConfig;

        private static void LoadConfig()
        {
            _pythonConfig = (PythonConfigurationSection)ConfigurationManager.GetSection("PythonConfiguration");
            _pCADataDirectory = _pythonConfig.ExePath;

            _pcaConfig = (PCAConfigurationSection)ConfigurationManager.GetSection("PCAConfiguration");
            _pCADataDirectory = _pcaConfig.Directory;
        }
    }
}
