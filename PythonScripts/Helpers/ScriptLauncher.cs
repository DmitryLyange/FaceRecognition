using System;
using System.Diagnostics;
using System.IO;
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
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var process = Process.Start(start))
            {
                using (var reader = process.StandardOutput)
                {
                    var stderr = process.StandardError.ReadToEnd();
                    var result = reader.ReadToEnd();
                    //TODO return result;
                }
            }
        }
    }
}
