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
                                FileName = "Alvaro_Uribe_0013",
                                Probability = 100
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0012",
                                Probability = 83.2
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0005",
                                Probability = 71.7
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0004",
                                Probability = 65.1
                            },
                            new ResultImage
                            {
                                FileName = "Alvaro_Uribe_0006",
                                Probability = 62.1
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
                                ClassLabel = "Jennifer_Capriati",
                                Probability = 15.4
                            },
                            new ResultClass
                            {
                                ClassLabel = "Amelie_Mauresmo",
                                Probability = 7.3
                            },
                            new ResultClass
                            {
                                ClassLabel = "Anna_Kournikova",
                                Probability = 6.8
                            },
                            new ResultClass
                            {
                                ClassLabel = "Lleyton_Hewitt",
                                Probability = 3.5
                            },
                            new ResultClass
                            {
                                ClassLabel = "Serena_Williams",
                                Probability = 2.7
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
