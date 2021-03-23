using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NukeeperConfigUI
{
    public static class NukeeperHelper
    {
        /// <summary>
        /// Used to check if the nuget exe is available in any of the known paths to the machine.
        /// </summary>
        /// <param name="pszFile"></param>
        /// <param name="ppszOtherDirs"></param>
        /// <returns></returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern bool PathFindOnPath([In, Out] StringBuilder pszFile, [In] String[] ppszOtherDirs);

        public static string NUGET_EXE { get; set; } = "nuget-5.1.0.exe";

        /// <summary>
        /// Gets the nuget sources from the machine using the <see cref="NUGET_EXE"/> defined here which has be available in the PATH variable.
        /// </summary>
        /// <returns></returns>
        public static IList<NugetSource> GetNugetSourcesOfMachine()
        {
            if (CheckNugetExecutable())
            {
                string output = ExecuteSourceCommand();
                return ParseNugetSources(output);
            }
            return new List<NugetSource>();
        }

        /// <summary>
        /// Parses the output of "nuget.exe sources" into a list of <see cref="NugetSource"/>s
        /// </summary>
        /// <param name="nugetOutput"></param>
        /// <returns></returns>
        private static IList<NugetSource> ParseNugetSources(string nugetOutput)
        {
            List<NugetSource> sources = new List<NugetSource>();

            if (nugetOutput != null)
            {
                var reader = new StringReader(nugetOutput);
                var startsWithNumber = new Regex("  [0-9].  "); //Identifies a line which contains a sources name.
                var blank = new Regex("      "); //Identifies a line which contains a sources target.
                NugetSource current = new NugetSource();

                while (reader.ReadLine() is string line)
                {
                    if (startsWithNumber.IsMatch(line))
                    {
                        current.Name = line;
                    }
                    else if (blank.IsMatch(line))
                    {
                        var target = line.Replace(" ", "");
                        current.Target = target;
                        sources.Add(current);
                        current = new NugetSource();
                    }
                }
            }

            return sources;
        }

        /// <summary>
        /// Executes "nuget.exe sources" on the machine (needs to be on the PATH variable!) to get the available nuget sources.
        /// </summary>
        /// <returns></returns>
        private static string ExecuteSourceCommand()
        {
            try
            {
                using (Process nugetSourceProcess = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = "C:\\",
                        FileName = NUGET_EXE,
                        Arguments = "sources",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                })
                {
                    nugetSourceProcess.Start();

                    string standardOutput = nugetSourceProcess.StandardOutput.ReadToEnd();
                    string standardError = nugetSourceProcess.StandardError.ReadToEnd();
                    if (!string.IsNullOrEmpty(standardError))
                        throw new Exception($"Exception during execution of nuget.exe source. Error out: {standardError}. StandardOut: {standardOutput}");

                    return standardOutput;
                }
            }
            catch (Win32Exception ex)
            {
                Debug.WriteLine($"Error during nuget invocation. Message: {ex.Message} Trace: {ex.StackTrace}");
                return null;
            }
        }

        /// <summary>
        /// Checks if the <see cref="NUGET_EXE"/> is available in any of the known paths.
        /// </summary>
        /// <returns></returns>
        public static bool CheckNugetExecutable()
        {
            StringBuilder b = new StringBuilder().Append(NUGET_EXE);
            bool result =  PathFindOnPath(b, null);
            b.Clear();
            return result;
        }
    }
}
