// ***********************************************************************
// Assembly         : DevGrep
// Author           : Brian Patterson
// Created          : 12-02-2012
//
// Last Modified By : Brian Patterson
// Last Modified On : 12-02-2012
// ***********************************************************************
// <copyright file="IndexDetail.cs" company="Borderline Software, Inc.">
//     Copyright (c) Borderline Software, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAssembly.Attributes;

namespace DevGrep.Classes.IndexedFiles
{
    /// <summary>
    /// Class VerbDetail
    /// </summary>
    [Serializable]
    [DoNotObfuscateType]
    internal class IndexDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerbDetail" /> class.
        /// </summary>
        /// <param name="folderNamePath">The folder name path.</param>
        /// <param name="indexType">Type of the index.</param>
        /// <param name="indexFolderName">Name of the index folder.</param>
        /// <param name="lastUpdate">The last update.</param>
        internal IndexDetail(string folderNamePath, string indexType, string indexFolderName, DateTime lastUpdate)
        {
            FolderNamePath = folderNamePath;
            IndexType = indexType;
            LastUpdate = lastUpdate;
            IndexFolderName = indexFolderName;
        }

        /// <summary>
        /// Gets or sets the name of the index folder where the index data files are stored.
        /// </summary>
        /// <value>The name of the index folder.</value>
        internal string IndexFolderName { get; set; }

        /// <summary>
        /// Gets or sets the folder name path.
        /// </summary>
        /// <value>The folder name path.</value>
        internal string FolderNamePath { get; set; }

        /// <summary>
        /// Gets or sets the type of the index.
        /// </summary>
        /// <value>The type of the index.</value>
        internal string IndexType { get; set; }

        /// <summary>
        /// Gets or sets the last update.
        /// </summary>
        /// <value>The last update.</value>
        public DateTime LastUpdate { get; set; }
    }
}
