using System;
using FaceRecognition.Infrastructure;

namespace FaceRecognition.PythonScripts
{
    public static class AlgorithmService
    {
        public static OutputModel GetResults(AlgorithmType algorithmType)
        {
            var scriptName = string.Empty;
            var args = string.Empty;
            switch (algorithmType)
            {
                case AlgorithmType.PCA:
                    scriptName = GlobalConfig.PCAScriptName;
                    args = GlobalConfig.PCADataDirectory;
                    break;
                case AlgorithmType.CNN:
                    scriptName = GlobalConfig.CNNScriptName;
                    args = GlobalConfig.CNNDataDirectory;
                    break;
                default:
                    break;
            }

            return ScriptLauncher.RunScript(scriptName, args);
        }
    }
}
