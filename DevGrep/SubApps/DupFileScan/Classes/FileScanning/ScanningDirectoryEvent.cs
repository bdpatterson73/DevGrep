using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.SubApps.DupFileScan.Classes
{
    /// <summary>
    /// Scanning directory event
    /// </summary>
    /// <param name="directoryName">Name of the directory.</param>
    /// <remarks></remarks>
    internal delegate void ScanningDirectoryEvent(string directoryName);
}
