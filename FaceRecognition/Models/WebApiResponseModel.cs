using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FaceRecognition
{
    [DataContract]
    public class WebApiResponse<T>
    {
        /// <summary>
        /// Request result
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public virtual T Response { get; set; }

        /// <summary>
        /// Errors list
        /// </summary>
        [DataMember]
        public WebApiError Error { get; set; }

        /// <summary>
        /// Success
        /// </summary>
        [DataMember]
        public bool Success { get; set; }
    }
}