using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FaceRecognition.PythonScripts
{
    public static class AlgorithmService
    {
        private const string PCAPath = @"C:\projects\FaceRecognition\FaceRecognition\bin\GeneratedData\pca.json";
        private const string CNNPath = @"C:\projects\FaceRecognition\FaceRecognition\bin\GeneratedData\cnn.json";
        private const string LDAPath = @"C:\projects\FaceRecognition\FaceRecognition\bin\GeneratedData\lda.json";

        public static List<AlgorithmOutputModel> GetResults(AlgorithmType algorithmType = AlgorithmType.Multiple)
        {
            //var scriptName = string.Empty;
            //var args = string.Empty;
            //switch (algorithmType)
            //{
            //    case AlgorithmType.PCA:
            //        scriptName = GlobalConfig.PCAScriptName;
            //        args = GlobalConfig.PCADataDirectory;
            //        break;
            //    case AlgorithmType.CNN:
            //        scriptName = GlobalConfig.CNNScriptName;
            //        args = GlobalConfig.CNNDataDirectory;
            //        break;
            //    default:
            //        break;
            //}
            //return ScriptLauncher.RunScript(scriptName, args);

            var result = new List<AlgorithmOutputModel>();

            switch (algorithmType)
            {
                case AlgorithmType.PCA:
                case AlgorithmType.CNN:
                case AlgorithmType.LDA:
                    result.Add(GetData(algorithmType));
                    break;
                case AlgorithmType.Multiple:
                    result.Add(GetData(AlgorithmType.PCA));
                    result.Add(GetData(AlgorithmType.CNN));
                    result.Add(GetData(AlgorithmType.LDA));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, null);
            }

            return result;
        }

        private static AlgorithmOutputModel GetData(AlgorithmType algorithmType)
        {
            string filePath;
            switch (algorithmType)
            {
                case AlgorithmType.PCA:
                    filePath = PCAPath;
                    break;
                case AlgorithmType.CNN:
                    filePath = CNNPath;
                    break;
                case AlgorithmType.LDA:
                    filePath = LDAPath;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, null);
            }            

            using (var streamReader = new StreamReader(filePath))
            {
                var jsonData = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<AlgorithmOutputModel>(jsonData);
            }
        }
    }
}
