using System.Configuration;

namespace FaceRecognition.Infrastructure
{
    public static class GlobalConfig
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

        private static string _pCAScriptName;
        public static string PCAScriptName
        {
            get
            {
                if (_pCAScriptName == null)
                    LoadConfig();

                return _pCAScriptName;
            }
        }

        private static string _cNNDataDirectory;
        public static string CNNDataDirectory
        {
            get
            {
                if (_cNNDataDirectory == null)
                    LoadConfig();

                return _cNNDataDirectory;
            }
        }

        private static string _cNNScriptName;
        public static string CNNScriptName
        {
            get
            {
                if (_cNNScriptName == null)
                    LoadConfig();

                return _cNNScriptName;
            }
        }


        private static PythonConfigurationSection _pythonConfig;
        private static AlgorithmConfigurationSection _pCAConfig;
        private static AlgorithmConfigurationSection _cNNConfig;

        private static void LoadConfig()
        {
            _pythonConfig = (PythonConfigurationSection)ConfigurationManager.GetSection("pythonConfiguration");
            _pythonExePath = _pythonConfig.ExePath;

            _pCAConfig = (AlgorithmConfigurationSection)ConfigurationManager.GetSection("pCAConfiguration");
            _pCADataDirectory = _pCAConfig.DataDirectory;
            _pCAScriptName = _pCAConfig.ScriptName;

            _cNNConfig = (AlgorithmConfigurationSection)ConfigurationManager.GetSection("cNNConfiguration");
            _cNNDataDirectory = _cNNConfig.DataDirectory;
            _cNNScriptName = _cNNConfig.ScriptName;
        }
    }
}
