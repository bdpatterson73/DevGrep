using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DevGrep.Classes.Config
{
    /// <summary>
    /// Class INIConfig
    /// </summary>
    internal class INIConfig
    {

        /// <summary>
        /// The _application name
        /// </summary>
        private string _applicationName;
        /// <summary>
        /// The _company
        /// </summary>
        private string _company;
        /// <summary>
        /// The in file
        /// </summary>
        private IniFile inFile;

        //private TagItems _savedTagItems;
        /// <summary>
        /// The _document object literal
        /// </summary>
        private string _lastSearchType;

        private string _lastSearchPath;


        

       

      

        //public TagItems SavedTagItems
        //{
        //    get { return _savedTagItems; }
        //    set { _savedTagItems = value; }
        //}

        public ExtensionList SavedExtensionList { get; set; }

        /// <summary>
        /// Gets or sets the document object literal.
        /// </summary>
        /// <value>The document object literal.</value>
        public string LastSearchType
        {
            get { return _lastSearchType; }
            set { _lastSearchType = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="INIConfig" /> class.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="applicationName">Name of the application.</param>
        internal INIConfig(string company, string applicationName)
        {
            _company = company;
            _applicationName = applicationName;
            inFile = new IniFile(ConfigNamePath);
            EnsureFileExists();
            CheckDefaults();
            LoadValues();
        }

        /// <summary>
        /// Gets the config name path.
        /// </summary>
        /// <value>The config name path.</value>
        public string ConfigNamePath
        {
            get
            {
                var ad = new ApplicationData();
                string configNamePath = Path.Combine(ad.FolderPath, _applicationName + ".ini");
                return configNamePath;
            }
        }

        /// <summary>
        /// Ensures the file exists.
        /// </summary>
        private void EnsureFileExists()
        {
            if (!File.Exists(ConfigNamePath))
            {
                File.WriteAllText(ConfigNamePath, "");
            }
        }

        /// <summary>
        /// Saves all the current settings to the INI file.
        /// </summary>
        public void Save()
        {
            inFile.IniWriteValue("Search", "LastSearchPath", _lastSearchPath);
            inFile.IniWriteValue("Search", "LastSearchType", _lastSearchType);
            //SavedTagItems.Save();
            SavedExtensionList.Save();

        }

        /// <summary>
        /// Loads the values.
        /// </summary>
        private void LoadValues()
        {
            _lastSearchPath = inFile.IniReadValue("Search", "LastSearchPath");
            _lastSearchType = inFile.IniReadValue("Search", "LastSearchType");
            //SavedTagItems = TagItems.Load();
            SavedExtensionList = ExtensionList.Load();

        }

        /// <summary>
        /// Checks the defaults for each setting and if a setting cannot be found or is empty - a default value is added..
        /// </summary>
        private void CheckDefaults()
        {;
            IfEmptyCreate("Search", "LastSearchType", "RegEx");
            IfEmptyCreate("Search", "LastSearchPath", "C:\\");


        }


        /// <summary>
        /// Ifs the empty create.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="keyValue">The key value.</param>
        private void IfEmptyCreate(string sectionName, string keyName, string keyValue)
        {
            string keyNameValue = inFile.IniReadValue(sectionName, keyName);
            if (string.IsNullOrEmpty(keyNameValue))
                inFile.IniWriteValue(sectionName, keyName, keyValue);
        }
    }
}
