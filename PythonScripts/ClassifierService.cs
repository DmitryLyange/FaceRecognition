using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FaceRecognition.Infrastructure;

namespace FaceRecognition.PythonScripts
{
    public static class ClassifierService
    {
        public static ClassifierOutput Get(Classifier classifier, DatasetType dataset, string imageFile)
        {
            ClassifierOutput result;
            switch (classifier)
            {
                case Classifier.OpenBR:
                    result = new OpenBrOutput()
                    {
                        Results = new List<ResultImage>
                        {
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0012",
                                Probability = 70.5
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0013",
                                Probability = 60.0
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0005",
                                Probability = 53.2
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0002",
                                Probability = 30
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0001",
                                Probability = 20
                            }
                        }
                    };
                    break;
                case Classifier.CNN:
                    result = new CNNOutput()
                    {
                        Results = new List<ResultClass>
                        {
                            new ResultClass
                            {
                                ClassLabel = "Alvaro_Uribe",
                                Probability = 75.5
                            },
                            new ResultClass
                            {
                                ClassLabel = "Alejandro_Toledo",
                                Probability = 60
                            },
                            new ResultClass
                            {
                                ClassLabel = "Angelina_Jolie",
                                Probability = 50
                            },
                            new ResultClass
                            {
                                ClassLabel = "Bill_Gates",
                                Probability = 40
                            },
                            new ResultClass
                            {
                                ClassLabel = "Colin_Powell",
                                Probability = 30
                            }
                        }
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(classifier), classifier, null);
            }

            return result;
        }
    }
}
