using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FaceRecognition.Infrastructure;

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
                XAxis = "x TODO",
                YAxis = "y TODO"
            };

            var graphs = new List<GraphData>();
            var graph1 = new GraphData
            {
                GraphName = "First graph",
                GraphLines = new List<GraphPoint> { new GraphPoint(5, 0.3), new GraphPoint(10, 0.4), new GraphPoint(50, 0.3) }
            };
            graphs.Add(graph1);
            var graph2 = new GraphData
            {
                GraphName = "Second graph",
                GraphLines = new List<GraphPoint> { new GraphPoint(5, 0.6), new GraphPoint(10, 0.5), new GraphPoint(50, 0.2) }
            };
            graphs.Add(graph2);

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
    }
}
