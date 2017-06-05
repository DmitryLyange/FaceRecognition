using System;
using FaceRecognition.Infrastructure;

namespace FaceRecognition.PythonScripts
{
    public static class AlgorithmService
    {
        public static OutputModel GetResults(AlgorithmType algorithmType)
        {
            var scriptName = string.Empty;
            var args = string.Empty;;
            switch (algorithmType)
            {
                case AlgorithmType.PCA:
                    //TODO move start file names to config
                    scriptName = "test.py";
                    //args = GlobalConfig.PCADataDirectory;
                    args = "testDir";
                    break;
                default:
                    break;
            }

            ScriptLauncher.RunScript(scriptName, args);

            //TODO
            return new OutputModel();
        }
    }
}
