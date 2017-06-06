using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace FaceRecognition.WebApi
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var request = actionExecutedContext.Request;
            var exception = actionExecutedContext.Exception;

            var message = string.Format("WebApi: Error occurred. Request: {0}, Http method: {1}.", request.RequestUri, request.Method);

            if (actionExecutedContext.ActionContext.Request.Content.Headers.ContentType != null &&
                actionExecutedContext.ActionContext.Request.Content.Headers.ContentType.MediaType != "multipart/form-data")
            {
                var body = ApiHelper.GetBodyFromRequest(actionExecutedContext);
                message = string.Concat(message, " Body: ", body);
            }

            if (exception is HttpResponseException)
            {
                base.OnException(actionExecutedContext);
                return;
            }

            if (!(exception is WebApiException))
            {
                exception = new RequestedOperationFailedException(exception.Message);
            }

            var responseError = new WebApiError(exception);

            if (exception is RequestedOperationFailedException)
            {
                responseError.Inner = new WebApiError(actionExecutedContext.Exception);
            }

            var response = new WebApiResponse<object>
            {
                Error = responseError,
                Success = false
            };

            actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
