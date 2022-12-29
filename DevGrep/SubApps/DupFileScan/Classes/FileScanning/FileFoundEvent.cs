using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.SubApps.DupFileScan.Classes
{
    /// <summary>
    /// File not found
    /// </summary>
    /// <param name="data">The data.</param>
    /// <remarks></remarks>
    internal delegate void FileFoundEvent(IScanForFilesData data);
}
