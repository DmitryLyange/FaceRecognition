namespace FaceRecognition.WebApi
{
    /// <summary>
    /// Core exceptions wrapper for platform
    /// </summary>
    public class RequestedOperationFailedException : WebApiException
    {
        public RequestedOperationFailedException(string message) : base(message)
        {
        }
    }
}
