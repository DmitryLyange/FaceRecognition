using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FaceRecognition.Infrastructure;
using System.Linq;

namespace FaceRecognition.PythonScripts
{
    public static class PerformanceMetricService
    {
        public static PerformanceMetricGraph GetGraph(PerformanceMetric metric)
        {
            //TODO
            var result = new PerformanceMetricGraph
            {
                MetricName = metric.ToString(),
                XAxis = "",
                YAxis = ""
            };

            var graphs = new List<GraphData>();
            var graph1 = new GraphData
            {
                GraphName = "First graph",
                GraphLines = new List<GraphPoint> { new GraphPoint(5, 0.453), new GraphPoint(10, 0.62), new GraphPoint(50, 0.76) }
            };
            graphs.Add(graph1);
            var graph2 = new GraphData
            {
                GraphName = "Second graph",
                GraphLines = new List<GraphPoint> { new GraphPoint(5, 0.475), new GraphPoint(10, 0.654), new GraphPoint(50, 0.798) }
            };
            graphs.Add(graph2);
            var graph3 = new GraphData
            {
                GraphName = "Third graph",
                GraphLines = new List<GraphPoint> { new GraphPoint(5, 0.522), new GraphPoint(10, 0.732), new GraphPoint(50, 0.885) }
            };
            graphs.Add(graph3);

            result.Graphs = graphs;
            return result;
        }

        public static List<double> GetData(AlgorithmType classifier)
        {
            var resultsDirectory = GlobalConfig.ResultsDirectory;
            var filePath = Path.Combine(resultsDirectory, "pcaMetric.json");
            var result = default(List<double>);
            //TODO

            using (var streamReader = new StreamReader(filePath))
            {
                var jsonData = streamReader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<double>>(jsonData);
            }

            return result;
        }

        #region contingency table
        public static List<AlgorithmOutputModel> GetTable(AlgorithmType algorithmType = AlgorithmType.Multiple)
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
                    result.Add(GetTableData(algorithmType));
                    break;
                case AlgorithmType.Multiple:
                    result.Add(GetTableData(AlgorithmType.PCA));
                    result.Add(GetTableData(AlgorithmType.CNN));
                    result.Add(GetTableData(AlgorithmType.LDA));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, null);
            }

            return result;
        }

        private static AlgorithmOutputModel GetTableData(AlgorithmType algorithmType)
        {
            var resultsDirectory = GlobalConfig.ResultsDirectory;
            string filePath;
            switch (algorithmType)
            {
                case AlgorithmType.PCA:
                    filePath = Path.Combine(resultsDirectory, "pca.json");
                    break;
                case AlgorithmType.CNN:
                    filePath = Path.Combine(resultsDirectory, "cnn.json");
                    break;
                case AlgorithmType.LDA:
                    filePath = Path.Combine(resultsDirectory, "lda.json");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithmType), algorithmType, null);
            }

            //TODO remove
            filePath = Path.Combine(resultsDirectory, "result.json");

            using (var streamReader = new StreamReader(filePath))
            {
                var jsonData = streamReader.ReadToEnd();
                var allData = JsonConvert.DeserializeObject<List<List<int>>>(jsonData);
                var oneRun = allData[0];
                var performanceRates = CalculatePerformanceRatios(oneRun);

                var metricsData = JsonConvert.SerializeObject(performanceRates);
                using (var streamWriter = new StreamWriter(@"C:\projects\FaceRecognition\PythonScripts\GeneratedData\pcaMetricss.json", true))
                {
                    streamWriter.Write(metricsData);
                }

                oneRun.RemoveAt(0);
                return new AlgorithmOutputModel
                {
                    ContingencyTable = oneRun,
                    PerformanceRates = performanceRates
                };
            }
        }
        #endregion

        private static List<double> CalculatePerformanceRatios(List<int> oneRun)
        {
            var c = oneRun.Select(el => (double)el).ToArray();
            var result = new List<double>();
            var w1 = 1.0;
            var w2 = 1.0;
            var w3 = 1.0;

            var precisionB = 0.0;
            try
            {
                precisionB = (c[1] + c[2]) / (c[1] + c[2] + c[3] + c[4]);
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
                    (w1 * (c[1]) / (c[1] + c[3] + c[4]) +
                     w2 * (c[6]) / (c[5] + c[6] + c[8]) +
                     w3 * (c[11]) / (c[9] + c[10] + c[11])) /
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
                recallB = (c[1] + c[2]) / (c[1] + c[2] + c[5] + c[9]);
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
                    (w1 * (c[1]) / (c[1] + c[2] + c[5] + c[9]) +
                     w2 * (c[6]) / (c[3] + c[6] + c[7] + c[10]) +
                     w3 * (c[11]) / (c[4] + c[8] + c[11])) /
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
            var accuracyB = 0.0;
            try
            {
                accuracyB = (c[1] + c[2] + c[6] + c[7] + c[8] + c[10] + c[11]) / cSum;
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
                    (w1 * (c[1] + c[6] + c[7] + c[8] + c[10] + c[11]) +
                     w2 * (c[1] + c[2] + c[4] + c[6] + c[9] + c[10]) +
                     w3 * (c[1] + c[2] + c[3] + c[5] + c[6] + c[7] + c[11])) /
                    ((w1 + w2 + w3) * cSum);
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
                    (w1 * (c[2] + c[5] + c[9]) / (c[1] + c[2] + c[5] + c[9]) +
                     w2 * (c[3] + c[7] + c[10]) / (c[3] + c[6] + c[7] + c[10]) +
                     w3 * (c[4] + c[8]) / (c[4] + c[8] + c[11])) /
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
                fallOutRateB = (c[3] + c[4]) / (c[3] + c[4] + c[6] + c[7] + c[8] + c[10] + c[11]);
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
                    (w1 * (c[3] + c[4]) / (c[3] + c[4] + c[6] + c[7] + c[8] + c[10] + c[11]) +
                     w2 * (c[5] + c[8]) / (c[1] + c[2] + c[4] + c[5] + c[8] + c[9] + c[11]) +
                     w3 * (c[9] + c[10]) / (c[1] + c[2] + c[3] + c[5] + c[6] + c[7] + c[9] + c[10])) /
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
