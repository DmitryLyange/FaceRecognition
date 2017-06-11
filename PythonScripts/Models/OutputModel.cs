using System;

namespace FaceRecognition.PythonScripts
{
    public enum AlgorithmType
    {
        //Principal Component Analysis
        PCA,
        //Convolutional Neural Network
        CNN
    }

    [Serializable]
    public class OutputModel
    {
        public string FirstTypeErrors { get; set; }

        public string SecondTypeErrors { get; set; }

        public string LearningSpeed { get; set; }

        public string RecognizingSpeed { get; set; }
    }
}
