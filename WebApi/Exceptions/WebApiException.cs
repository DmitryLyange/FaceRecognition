using System;

namespace FaceRecognition.WebApi
{
    /// <summary>
    /// Base WebApi Exception
    /// </summary>
    public abstract class WebApiException : ApplicationException
    {
        protected WebApiException()
        { }

        protected WebApiException(string message)
            : base(message)
        { }
    }
}
