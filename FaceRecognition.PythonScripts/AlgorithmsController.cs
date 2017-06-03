namespace FaceRecognition.PythonScripts
{
    public static class AlgorithmsController
    {
        public static OutputModel GetResults(Algorithms algorithmType)
        {
            return new OutputModel();

            switch (algorithmType)
            {
                case Algorithms.PCA:
                    break;
            }
        }
    }
}
