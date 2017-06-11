using System;
using System.Collections.Generic;
using System.Web.Http;
using FaceRecognition.PythonScripts;
using FaceRecognition.WebApi;

namespace FaceRecognition
{
    [ExceptionHandler]
    [RoutePrefix("Api/Algorithm")]
    //public class AlgorithmController : BaseWebApiController
    public class AlgorithmController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="algorithmName">Container alias</param>
        /// <returns>
        /// 
        /// </returns>
        [Route("{algorithmName}")]
        [HttpGet]
        public OutputModel Get(string algorithmName)
        {
            AlgorithmType algorithmType;
            Enum.TryParse(algorithmName, out algorithmType);
            var result = AlgorithmService.GetResults(algorithmType);

            return result;
        }
    }
}
