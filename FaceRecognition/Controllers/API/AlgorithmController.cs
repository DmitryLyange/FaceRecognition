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
        /// <returns>
        /// 
        /// </returns>
        [Route("")]
        [HttpGet]
        public List<AlgorithmOutputModel> Get()
        {
            return AlgorithmService.GetResults();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="algorithmName">Container alias</param>
        /// <returns>
        /// 
        /// </returns>
        [Route("{algorithmName}")]
        [HttpGet]
        public List<AlgorithmOutputModel> Get(string algorithmName)
        {
            AlgorithmType algorithmType;
            Enum.TryParse(algorithmName, out algorithmType);
            return AlgorithmService.GetResults(algorithmType);
        }
    }
}
