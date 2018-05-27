using System;
using System.Collections.Generic;
using System.Web.Http;
using FaceRecognition.PythonScripts;
using FaceRecognition.WebApi;

namespace FaceRecognition
{
    [ExceptionHandler]
    [RoutePrefix("Api/Classifier")]
    public class ClassifierController : ApiController
    {
        [Route("")]
        [HttpGet]
        public List<AlgorithmOutputModel> Get()
        {
            var result = AlgorithmService.GetResults();
            return result;
        }

        [Route("{classifierName}")]
        [HttpGet]
        public List<AlgorithmOutputModel> Get(string classifierName)
        {
            AlgorithmType algorithmType;
            Enum.TryParse(classifierName, out algorithmType);
            var result = AlgorithmService.GetResults(algorithmType);
            return result;
        }
    }
}
