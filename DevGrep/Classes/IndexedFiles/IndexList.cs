// ***********************************************************************
// Assembly         : DevGrep
// Author           : Brian Patterson
// Created          : 12-02-2012
//
// Last Modified By : Brian Patterson
// Last Modified On : 12-02-2012
// ***********************************************************************
// <copyright file="IndexList.cs" company="Borderline Software, Inc.">
//     Copyright (c) Borderline Software, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGrep.Classes.Config;
using DevGrep.Classes.IO.Serializers;
using SmartAssembly.Attributes;

namespace DevGrep.Classes.IndexedFiles
{
    /// <summary>
    /// Class VerbList
    /// </summary>
    [Serializable]
    [DoNotObfuscateType]
    internal class IndexList : List<IndexDetail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexList" /> class.
        /// </summary>
        internal IndexList()
        {
            FileVersion = "1.0";
            SanityCheck();

        }

        /// <summary>
        /// Used for tracking version of the save file and so we know when the file should be upgraded.
        /// </summary>
        /// <value>The file version.</value>
        internal string FileVersion { get; set; }

        /// <summary>
        /// Look for all items required by this class.
        /// </summary>
        private void SanityCheck()
        {
            // Check for saved tag items - if not found then create defaults.
            if (!DoesSaveFileExist())
            {
                CreateDefaultSaveFile();
            }
        }

        /// <summary>
        /// Populates this collection with default values.
        /// </summary>
        private void CreateDefaultSaveFile()
        {
            Save();
        }


        /// <summary>
        /// Determines whether [is index in list] [the specified folder name path].
        /// </summary>
        /// <param name="folderNamePath">The folder name path.</param>
        /// <returns><c>true</c> if [is index in list] [the specified folder name path]; otherwise, <c>false</c>.</returns>
        internal bool IsIndexInList(string folderNamePath)
        {
            foreach (IndexDetail vd in this)
            {
                if (String.Compare(vd.FolderNamePath, folderNamePath, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;
            }
            return false;
        }

        internal void DeleteByFolderNamePath(string folderNamePath)
        {
            foreach (IndexDetail vd in this)
            {
                if (String.Compare(vd.FolderNamePath, folderNamePath, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.Remove(vd);
                    return;
                }
            }
        }

        internal bool IsIndexContainedInList(string folderNamePath)
        {
            foreach (IndexDetail vd in this)
            {
                if (String.Compare(vd.FolderNamePath, folderNamePath, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;
                if (folderNamePath.ToUpper().Contains(vd.FolderNamePath.ToUpper()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gets an Index record for the given path.
        /// </summary>
        /// <param name="folderNamePath">The folder name path.</param>
        /// <returns>IndexDetail.</returns>
        internal IndexDetail GetIndexObject(string folderNamePath)
        {
            foreach (IndexDetail vd in this)
            {
                if (String.Compare(vd.FolderNamePath, folderNamePath, StringComparison.OrdinalIgnoreCase) == 0)
                    return vd;
            }
            return null;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        internal void Save()
        {
            JSONSerializer json = new JSONSerializer();
            System.IO.File.WriteAllText(SaveFileNamePath, json.Serialize(this));
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns>IndexList.</returns>
        internal static IndexList Load()
        {
            IndexList ti = new IndexList(); // Create object to ensure it validates file exists and creates defaults if not.

            JSONSerializer json = new JSONSerializer();
            return (IndexList)json.Deserialize(File.ReadAllText(SaveFileNamePath), typeof(IndexList));
        }

        /// <summary>
        /// Determines if a serialized copy of this collection is found in our config folder.
        /// </summary>
        /// <returns><c>true</c> if the file is found, <c>false</c> otherwise</returns>
        private bool DoesSaveFileExist()
        {
            return File.Exists(SaveFileNamePath);
        }

        /// <summary>
        /// Gets the save file name path.
        /// </summary>
        /// <value>The save file name path.</value>
        internal static string SaveFileNamePath
        {
            get
            {
                var ad = new ApplicationData();
                string configNamePath = Path.Combine(ad.FolderPath, "IndexList.bls");
                return configNamePath;
            }
        }
    }
}
