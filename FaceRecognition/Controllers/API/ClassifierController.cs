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
        [Route("{classifierName}/{datasetType}/{imageFile}")]
        [HttpGet]
        public ClassifierOutput Get(string classifierName, string datasetType, string imageFile)
        {
            Classifier classifier;
            Enum.TryParse(classifierName, out classifier);
            DatasetType dataset;
            Enum.TryParse(datasetType, out dataset);
            var result = ClassifierService.Get(classifier, dataset, imageFile);
            return result;
        }
    }
}
