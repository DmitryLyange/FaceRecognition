using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.PythonScripts
{
    public enum PerformanceMetric
    {
        Precision,
        Recall,
        Accuracy,
        MissRate,
        FalloutRate
    }

    public class PerformanceMetricGraph
    {
        public string MetricName { get; set; }

        public string XAxis { get; set; }

        public string YAxis { get; set; }

        public List<GraphData> Graphs { get; set; }
    }

    public class GraphData
    {
        public string GraphName { get; set; }

        public List<GraphPoint> GraphLines { get; set; }
    }

    public class GraphPoint
    {
        public GraphPoint(int x, double y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public double Y { get; set; }
    }
}
