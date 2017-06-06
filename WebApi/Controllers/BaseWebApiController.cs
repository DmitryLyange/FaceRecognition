using System.Web.Http;

namespace FaceRecognition.WebApi
{
    public class BaseWebApiController : ApiController
    {
        protected WebApiResponse<T> CreateResponse<T>(T result)
        {
            var apiResponse = new WebApiResponse<T>
            {
                Response = result,
                Success = true
            };
            return apiResponse;
        }

        protected WebApiResponse<object> CreateResponse()
        {
            return new WebApiResponse<object>
            {
                Success = true
            };
        }
    }
}