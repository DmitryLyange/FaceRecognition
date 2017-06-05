using System;
using System.Runtime.Serialization;

namespace FaceRecognition
{
    [DataContract]
    public class WebApiError
    {
        /// <summary>
        /// Exception message
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Exception type
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Inner errors
        /// </summary>
        [DataMember]
        public WebApiError Inner { get; set; }

        public WebApiError()
        {
        }

        public WebApiError(Exception exception)
        {
            if (exception != null)
            {
                this.Type = exception.GetType().Name;
                this.Message = exception.Message;
            }
        }
    }
}