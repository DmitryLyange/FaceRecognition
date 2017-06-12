using System;
using System.Runtime.Serialization;

namespace FaceRecognition.PythonScripts
{
    public enum AlgorithmType
    {
        //Principal Component Analysis
        PCA,
        //Convolutional Neural Network
        CNN
    }

    [DataContract]
    [Serializable]
    public class OutputModel
    {
        [DataMember]
        public string FirstTypeErrors { get; set; }

        [DataMember]
        public string SecondTypeErrors { get; set; }

        [DataMember]
        public string LearningSpeed { get; set; }

        [DataMember]
        public string RecognizingSpeed { get; set; }
    }
}
