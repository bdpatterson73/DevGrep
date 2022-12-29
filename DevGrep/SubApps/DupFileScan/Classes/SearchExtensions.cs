// ***********************************************************************
// Assembly         : DevGrep
// Author           : Brian
// Created          : 03-28-2013
//
// Last Modified By : Brian
// Last Modified On : 04-08-2013
// ***********************************************************************
// <copyright file="SearchExtensions.cs" company="">
//     Copyright (c) . All rights reserved.
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


namespace DevGrep.SubApps.DupFileScan.Classes
{
    /// <summary>
    /// Class SearchExtensions
    /// </summary>
    [Serializable]
    [DoNotObfuscateType]
    internal class SearchExtensions:List<SearchExtension>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchExtensions"/> class.
        /// </summary>
        internal SearchExtensions():base()
        {
          
            SanityCheck();
        }

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
            this.Add(new SearchExtension("All Files", "*.*"));
            this.Add(new SearchExtension("Applications", ".exe;.com;.bat;.jar;.dll"));
            this.Add(new SearchExtension("Audio Files", ".aif;.iff;.m3u;.m4a;.mid;.mp3;.mpa;.ra;.wav;.wma"));
            this.Add(new SearchExtension("Compressed Files", ".7z;.cbr;.deb;.gz;.pkg;.rar;.rpm;.sitx;.zip;.zipx"));
            this.Add(new SearchExtension("Data Files", ".csv;.dat;.gbr;.ged;.ibooks;.key;.keychain;.pps;.ppt;.ppt;.pptx;.sdf;.tar;.tax2012;.vcf;.xml"));
            this.Add(new SearchExtension("Developer Files", ".cs;.xml;.csproj;.settings;.aspx;.cshtml;.css;.c;.class;.cpp;.dtd;.fla;.h;.java;.lua;.m;.pl;.py;.sh;.sln;.vcxproj;.xcodeproj"));
            this.Add(new SearchExtension("Documents", ".doc;.docx;.log;.msg;.odt;.pages;.rtf;.tex;.txt;.wpd;.wps;.pdf"));
            this.Add(new SearchExtension("Image Files", ".jpg;.jpeg;.bmp;.tif;.tiff;.png;.ico;.gif;.pcd;.psd;.pspimage;.tga;.ai;.eps;.ps;.svg"));
            this.Add(new SearchExtension("Setting Files", ".cfg;.ini;.prf"));
            this.Add(new SearchExtension("Video Files", ".3g2;.3gp;.asf;.asx;.avi;.flv;.mov;.mp4;.mpg;.rm;.srt;.swf;.vob;.wmv"));
            this.Add(new SearchExtension("Web Files", ".js;.htm;.html;.css;.js;.aspx;.asp;.config;.xml;.cer;.cfm;.csr;.jsp;.php;.rss;.xhtml"));

            Save();
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
        /// <returns>SearchExtensions.</returns>
        internal static SearchExtensions Load()
        {
            SearchExtensions ti = new SearchExtensions(); // Create object to ensure it validates file exists and creates defaults if not.

            JSONSerializer json = new JSONSerializer();
            return (SearchExtensions)json.Deserialize(File.ReadAllText(SaveFileNamePath), typeof(SearchExtensions));
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
                string configNamePath = Path.Combine(ad.FolderPath, "DupeFileFileGroups.bls");
                return configNamePath;
            }
        }
    }
}
