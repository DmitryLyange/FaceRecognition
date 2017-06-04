using System;
using FaceRecognition.Infrastructure;

namespace FaceRecognition.PythonScripts
{
    public static class AlgorithmsController
    {
        public static OutputModel GetResults(AlgorithmType algorithmType)
        {
            return new OutputModel();

            var scriptName = string.Empty;
            var args = string.Empty;;
            switch (algorithmType)
            {
                case AlgorithmType.PCA:
                    //TODO move start file names to config
                    scriptName = "pca.py";
                    args = GlobalConfig.PCADataDirectory;
                    break;
                default:
                    break;
            }

            ScriptLauncher.RunScript(scriptName, args);
        }
    }
}
