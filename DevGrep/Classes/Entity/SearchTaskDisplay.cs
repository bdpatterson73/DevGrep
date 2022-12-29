// ***********************************************************************
// Assembly         : DevGrep
// Author           : Brian
// Created          : 04-20-2013
//
// Last Modified By : Brian
// Last Modified On : 04-20-2013
// ***********************************************************************
// <copyright file="SearchTaskDisplay.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartAssembly.Attributes;

namespace DevGrep.Classes.Entity
{
    /// <summary>
    /// User for storing data to be display in the search results list view.
    /// </summary>
    [Serializable]
    [DoNotObfuscateType]
    public class SearchTaskDisplay
    {
        public SearchTaskDisplay()
        {
        }

        public SearchTaskDisplay(string fileName, string extDesc, string dirName, long matchesFound, long fileLength, DateTime creationTime, object tag)
        {
            Filename = fileName;
            ExtDesc = extDesc;
            DirName = dirName;
            MatchesFound = matchesFound;
            FileLength = fileLength;
            CreationTime = creationTime;
            Tag = tag;
        }

        public string Filename { get; set; }
        public string ExtDesc { get; set; }
        public string DirName { get; set; }
        public long MatchesFound { get; set; }
        public long FileLength { get; set; }
        public DateTime CreationTime { get; set; }
        public object Tag { get; set; }
    }
}
