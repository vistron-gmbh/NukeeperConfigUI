using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeeperConfigUI
{
    /// <summary>
    /// The supported config items for the nukeeper.exe. This list is not complete!
    /// </summary>
    public class NukeeperConfig
    {
        /// <summary>
        /// Nuget source.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Change policy
        /// </summary>
        public Change Change { get; set; }
        /// <summary>
        /// Prerelease policy
        /// </summary>
        public Prerelease UsePreRelease { get; set; }
    }
}
