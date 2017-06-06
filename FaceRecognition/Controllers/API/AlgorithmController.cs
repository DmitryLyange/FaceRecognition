using System;
using System.Collections.Generic;
using System.Web.Http;
using FaceRecognition.PythonScripts;
using FaceRecognition.WebApi;

namespace FaceRecognition
{
    [ExceptionHandler]
    [RoutePrefix("api/Algorithm")]
    public class AlgorithmController : BaseWebApiController
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
        public WebApiResponse<IEnumerable<string>> Get(string algorithmName)
        {
            AlgorithmType algorithmType;
            Enum.TryParse(algorithmName, out algorithmType);
            AlgorithmService.GetResults(algorithmType);


            //TODO return smth meaningful
            var test = new[] { "value1", "value2" };
            return CreateResponse((IEnumerable<string>)test);
        }

        //public string Get(int id)
        //{
        //    return "value";
        //}

        //public void Post([FromBody]string value)
        //{
        //}

        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //public void Delete(int id)
        //{
        //}
    }
}
