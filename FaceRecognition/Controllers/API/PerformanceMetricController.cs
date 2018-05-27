using System;
using System.Collections.Generic;
using System.Web.Http;
using FaceRecognition.PythonScripts;
using FaceRecognition.WebApi;

namespace FaceRecognition
{
    [ExceptionHandler]
    [RoutePrefix("Api/PerformanceMetric")]
    public class PerformanceMetricController : ApiController
    {
        [Route("Graph/{metricName}")]
        [HttpGet]
        public PerformanceMetricGraph GetGraph(string metricName)
        {
            PerformanceMetric performanceMetric;
            Enum.TryParse(metricName, out performanceMetric);
            var result = PerformanceMetricService.GetGraph(performanceMetric);
            return result;
        }

        [Route("Data/{classifierName}")]
        [HttpGet]
        public List<double> GetData(string classifierName)
        {
            AlgorithmType algorithmType;
            Enum.TryParse(classifierName, out algorithmType);
            var result = PerformanceMetricService.GetData(algorithmType);
            return result;
        }
    }
}
