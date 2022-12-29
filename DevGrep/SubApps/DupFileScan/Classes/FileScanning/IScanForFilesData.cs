using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.SubApps.DupFileScan.Classes
{
    /// <summary>
    /// Interface for information that should be returned when scanning for files.
    /// </summary>
    internal interface IScanForFilesData
    {
        /// <summary>
        /// Gets or sets the file info.
        /// </summary>
        /// <value>The file info.</value>
        FileInfo FileInfo { get; set; }
        string FileHashValue { get; set; }
    }
}
