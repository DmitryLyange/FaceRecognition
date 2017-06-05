using System;
using System.Diagnostics;
using System.IO;
using FaceRecognition.Infrastructure;

namespace FaceRecognition
{
    public static class ScriptLauncher
    {
        //TODO return value - (OutputModel)?
        public static void RunScript(string scriptName, string args)
        {
            var script = Path.Combine(@"C:\Users\dlyange\projects\FaceRecognition\FaceRecognition.PythonScripts\Scripts", scriptName);
            var start = new ProcessStartInfo
            {
                FileName = @"C:\Users\dlyange\AppData\Local\Continuum\Anaconda3\python.exe",
                //FileName = GlobalConfig.PythonExePath,
                Arguments = $"{script} {args}",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (var process = Process.Start(start))
            {
                using (var reader = process.StandardOutput)
                {
                    //TODO interpret data
                    var result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }
    }
}
