using System;
using System.Diagnostics;
using FaceRecognition.Infrastructure;

namespace FaceRecognition.PythonScripts
{
    public static class ScriptLauncher
    {
        //TODO return value - (OutputModel)?
        public static void RunScript(string scriptName, string args)
        {
            var start = new ProcessStartInfo
            {
                FileName = GlobalConfig.PythonExePath,
                Arguments = $"{scriptName} {args}",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (var process = Process.Start(start))
            {
                using (var reader = process.StandardOutput)
                {
                    //TODO test
                    var result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }
    }
}
