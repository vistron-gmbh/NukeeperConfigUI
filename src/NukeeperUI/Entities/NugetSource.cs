using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeeperConfigUI
{
    /// <summary>
    /// Describes a nuget source on the target machine which contains nugets.
    /// </summary>
    public class NugetSource
    {
        /// <summary>
        /// Name as returned as the first line for each source by "nuget.exe sources"
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Target as returned as the second line for each source by "nuget.exe sources"
        /// </summary>
        public string Target { get; set; }
    }
}
