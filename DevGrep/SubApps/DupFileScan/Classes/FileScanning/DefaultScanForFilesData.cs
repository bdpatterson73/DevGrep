using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.SubApps.DupFileScan.Classes
{
    /// <summary>
    /// Data returned when scanning for files.
    /// </summary>
    internal class DefaultScanForFilesData : IScanForFilesData
    {
        #region IScanForFilesData Members

        /// <summary>
        /// Gets or sets the file info.
        /// </summary>
        /// <value>The file info.</value>
        public FileInfo FileInfo { get; set; }

        public string FileHashValue { get; set; }

        #endregion
    }
}
