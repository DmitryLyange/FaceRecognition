﻿using System;
using System.Diagnostics;
using FaceRecognition.Infrastructure;

namespace FaceRecognition.PythonScripts
{
    public static class ScriptLauncher
    {
        public static AlgorithmOutputModel RunScript(string scriptName, string args)
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
                    //removing end of line symbols
                    var pythonResult = reader.ReadToEnd().Trim(new[] { '\r', '\n' });
                    //separating values
                    char[] delimeterChars = {' '};
                    var pythonResultArray = pythonResult.Split(delimeterChars);

                    if (pythonResultArray.Length != 4)
                    {
                        throw new Exception(stderr);
                    }

                    return new AlgorithmOutputModel();
                }
            }
        }
    }
}
