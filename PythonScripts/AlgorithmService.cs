using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FaceRecognition.PythonScripts
{
    public static class AlgorithmService
    {
        private const string PCAPath = @"C:\projects\FaceRecognition\FaceRecognition\bin\GeneratedData\pca.json";
        private const string CNNPath = @"C:\projects\FaceRecognition\FaceRecognition\bin\GeneratedData\cnn.json";
        private const string LDAPath = @"C:\projects\FaceRecognition\FaceRecognition\bin\GeneratedData\lda.json";
        private const string ResultPath = @"C:\Users\-\projects\FaceRecognition\PythonScripts\GeneratedData\result.json";

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

            //TODO remove
            filePath = ResultPath;

            using (var streamReader = new StreamReader(filePath))
            {
                var jsonData = streamReader.ReadToEnd();
                var allData = JsonConvert.DeserializeObject<List<List<int>>>(jsonData);
                var oneRun = allData[0];
                var performanceRates = CalculatePerformanceRatios(oneRun);

                //var metricsData = JsonConvert.SerializeObject(performanceRates);
                //using (var streamWriter = new StreamWriter(@"C:\Users\-\projects\FaceRecognition\FaceRecognition\bin\GeneratedData\pcaMetrics.json"))
                //{
                //    streamWriter.Write(metricsData);
                //}

                oneRun.RemoveAt(0);
                return new AlgorithmOutputModel
                {
                    ContingencyTable = oneRun,
                    PerformanceRates = performanceRates
                };
            }
        }

        private static List<double> CalculatePerformanceRatios(List<int> oneRun)
        {
            var c = oneRun.Select(el => (double) el).ToArray();
            var result = new List<double>();
            var w1 = 1.0;
            var w2 = 1.0;
            var w3 = 1.0;

            var precisionB = 0.0;
            try
            {
                precisionB = (c[1] + c[2])/(c[1] + c[2] + c[3] + c[4]);
            }
            catch (Exception)
            {
                precisionB = -1;
            }
            finally
            {
                result.Add(precisionB);
            }

            var precisionM = 0.0;
            try
            {
                precisionM =
                    (w1*(c[1])/(c[1] + c[3] + c[4]) +
                     w2*(c[6])/(c[5] + c[6] + c[8]) +
                     w3*(c[11])/(c[9] + c[10] + c[11]))/
                    (w1 + w2 + w3);
            }
            catch (Exception)
            {
                precisionM = -1;
            }
            finally
            {
                result.Add(precisionM);
            }

            var recallB = 0.0;
            try
            {
                recallB = (c[1] + c[2])/(c[1] + c[2] + c[5] + c[9]);
            }
            catch (Exception)
            {
                recallB = -1;
            }
            finally
            {
                result.Add(recallB);
            }

            var recallM = 0.0;
            try
            {
                recallM =
                    (w1*(c[1])/(c[1] + c[2] + c[5] + c[9]) +
                     w2*(c[6])/(c[3] + c[6] + c[7] + c[10]) +
                     w3*(c[11])/(c[4] + c[8] + c[10]))/
                    (w1 + w2 + w3);
            }
            catch (Exception)
            {
                recallM = -1;
            }
            finally
            {
                result.Add(recallM);
            }

            var cSum = c.Sum();
            var accuracyB  = 0.0;
            try
            {
                accuracyB = (c[1] + c[2] + c[6] + c[7] + c[8] + c[10] + c[11])/cSum;
            }
            catch (Exception)
            {
                accuracyB = -1;
            }
            finally
            {
                result.Add(accuracyB);
            }

            var accuracyM = 0.0;
            try
            {
                accuracyM =
                    (w1*(c[1] + c[6] + c[7] + c[8] + c[10] + c[11]) +
                     w2*(c[1] + c[2] + c[4] + c[6] + c[9] + c[10]) +
                     w3*(c[1] + c[2] + c[3] + c[5] + c[6] + c[7] + c[11]))/
                    ((w1 + w2 + w3)*cSum);
            }
            catch (Exception)
            {
                accuracyM = -1;
            }
            finally
            {
                result.Add(accuracyM);
            }

            double missRateB;
            if (recallB == -1.0)
            {
                missRateB = -1;
            }
            else
            {
                missRateB = 1 - recallB;
            }
            result.Add(missRateB);
            

            var missRateM = 0.0;
            try
            {
                missRateM =
                    (w1*(c[2] + c[5] + c[9])/(c[1] + c[2] + c[5] + c[9]) +
                     w2*(c[3] + c[7] + c[10])/(c[3] + c[6] + c[7] + c[10]) +
                     w3*(c[4] + c[8])/(c[4] + c[8] + c[11]))/
                    (w1 + w2 + w3);
            }
            catch (Exception)
            {
                missRateM = -1;
            }
            finally
            {
                result.Add(missRateM);
            }

            var fallOutRateB = 0.0;
            try
            {
                fallOutRateB = (c[3] + c[4])/(c[3] + c[4] + c[6] + c[7] + c[8] + c[10] + c[11]);
            }
            catch (Exception)
            {
                fallOutRateB = -1;
            }
            finally
            {
                result.Add(fallOutRateB);
            }

            var fallOutRateM = 0.0;
            try
            {
                fallOutRateM =
                    (w1*(c[3] + c[4])/(c[3] + c[4] + c[6] + c[7] + c[8] + c[10] + c[11]) +
                     w2*(c[5] + c[8])/(c[1] + c[2] + c[4] + c[5] + c[8] + c[9] + c[11]) +
                     w3*(c[9] + c[10])/(c[1] + c[2] + c[3] + c[5] + c[6] + c[7] + c[9] + c[10]))/
                    (w1 + w2 + w3);
            }
            catch (Exception)
            {
                fallOutRateM = -1;
            }
            finally
            {
                result.Add(fallOutRateM);
            }

            return result.Select(pt => Math.Round(pt, 3)).ToList();
        } 
    }
}
