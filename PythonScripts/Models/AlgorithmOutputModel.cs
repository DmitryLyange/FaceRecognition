﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FaceRecognition.PythonScripts
{
    public enum AlgorithmType
    {
        //Principal Component Analysis
        PCA,
        //Convolutional Neural Network
        CNN,
        //
        LDA,
        //
        Multiple
    }

    [DataContract]
    [Serializable]
    public class AlgorithmOutputModel
    {
        [DataMember]
        public List<int> ContingencyTable { get; set; }

        [DataMember]
        public List<double> PerformanceRates { get; set; }

        [DataMember]
        public RocAuc RocCurve { get; set; }
    }

    [DataContract]
    [Serializable]
    public class RocAuc
    {
        [DataMember]
        public List<Point> Points { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Point
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }
    }
}
