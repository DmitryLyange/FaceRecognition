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
        //public WebApiResponse<IEnumerable<string>> Get(string algorithmName)
        public IEnumerable<string> Get(string algorithmName)
        {
            AlgorithmType algorithmType;
            Enum.TryParse(algorithmName, out algorithmType);
            AlgorithmService.GetResults(algorithmType);

            //TODO return smth meaningful
            var test = new[] { "value1", "value2" };
            //return CreateResponse((IEnumerable<string>)test);
            return test;
        }
    }
}
