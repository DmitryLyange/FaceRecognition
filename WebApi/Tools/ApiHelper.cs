using System.Web.Http.Filters;

namespace FaceRecognition.WebApi
{
    public static class ApiHelper
    {
        public static string GetBodyFromRequest(HttpActionExecutedContext context)
        {
            string data;
            using (var stream = context.Request.Content.ReadAsStreamAsync().Result)
            {
                if (stream.CanSeek)
                {
                    stream.Position = 0;
                }
                data = context.Request.Content.ReadAsStringAsync().Result;
            }
            return data;
        }
    }
}
