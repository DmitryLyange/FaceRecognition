using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.PythonScripts
{
    public enum Classifier
    {
        OpenBR,
        CNN
    }

    public enum DatasetType
    {
        FiveSmall,
        FiveBig,
        FiftySmall,
        FiftyBig
    }

    public class ClassifierOutput
    { 
    }

    public class OpenBrOutput : ClassifierOutput
    {
        public List<ResultImage> Results { get; set; }
    }

    public class ResultImage
    {
        public string FileName { get; set; }

        public double Probability { get; set; }
    }

    public class CNNOutput : ClassifierOutput
    {
        public List<ResultClass> Results { get; set; }
    }

    public class ResultClass
    {
        public string ClassLabel { get; set; }

        public double Probability { get; set; }
    }
}
