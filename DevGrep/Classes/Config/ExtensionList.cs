using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using DevGrep.Classes.IO.Serializers;
using SmartAssembly.Attributes;

namespace DevGrep.Classes.Config
{
    /// <summary>
    /// Class VerbList
    /// </summary>
    [Serializable]
    [DoNotObfuscateType]
    internal class ExtensionList : List<ExtensionDetail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerbList" /> class.
        /// </summary>
        internal ExtensionList()
        {
            FileVersion = "10";
            SanityCheck();

        }

        /// <summary>
        /// Used for tracking version of the save file and so we know when the file should be upgraded.
        /// </summary>
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
            Add(new ExtensionDetail("*.*", "General"));
            Add(new ExtensionDetail(".asp", "Web"));
            Add(new ExtensionDetail(".aspx", "Web"));
            Add(new ExtensionDetail(".bas", "Basic"));
            Add(new ExtensionDetail(".bat", "Text"));
            Add(new ExtensionDetail(".c", "C"));
            Add(new ExtensionDetail(".cfm", "Web"));
            
            Add(new ExtensionDetail(".class", "Java"));
            Add(new ExtensionDetail(".cpp", "C++"));
            Add(new ExtensionDetail(".cs", "C#"));
            Add(new ExtensionDetail(".csproj", "C#"));
            Add(new ExtensionDetail(".css", "Web"));
            Add(new ExtensionDetail(".csv", "Text"));
            Add(new ExtensionDetail(".dtd", "Web"));
            Add(new ExtensionDetail(".fla", "Web"));
            Add(new ExtensionDetail(".h", "C"));
            Add(new ExtensionDetail(".htm", "Web"));
            Add(new ExtensionDetail(".html", "Web"));
            Add(new ExtensionDetail(".java", "Java"));
            Add(new ExtensionDetail(".js", "JavaScript"));
            Add(new ExtensionDetail(".jsp", "JavaScript"));

            Add(new ExtensionDetail(".log", "Text"));
            Add(new ExtensionDetail(".lua", "Dev"));
            Add(new ExtensionDetail(".m", "C"));
            Add(new ExtensionDetail(".php", "PHP"));
            Add(new ExtensionDetail(".pl", "Perl"));
            Add(new ExtensionDetail(".py", "Python"));
            Add(new ExtensionDetail(".rss", "Web"));
            Add(new ExtensionDetail(".sh", "Shell"));
            Add(new ExtensionDetail(".sln", "Visual Studio"));
            Add(new ExtensionDetail(".sql", "SQL"));

            Add(new ExtensionDetail(".txt", "Text"));

            Add(new ExtensionDetail(".vb", "Basic"));
            Add(new ExtensionDetail(".vcxproj", "C++"));
            Add(new ExtensionDetail(".vbproj", "VB"));
            Add(new ExtensionDetail(".xcodeproj", "C"));
            Add(new ExtensionDetail(".xhtml", "JavaScript"));
            Add(new ExtensionDetail(".xml", "Text"));
            
            Save();
        }

        /// <summary>
        /// Determines whether [is verb in list] [the specified verb].
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <returns><c>true</c> if [is verb in list] [the specified verb]; otherwise, <c>false</c>.</returns>
        internal bool IsExtensionInList(string extension)
        {
            foreach (ExtensionDetail vd in this)
            {
                if (String.Compare(vd.Extension, extension, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;
            }
            return false;
        }

        internal ExtensionDetail GetExtensionObject(string extension)
        {
            foreach (ExtensionDetail vd in this)
            {
                if (String.Compare(vd.Extension, extension, StringComparison.OrdinalIgnoreCase) == 0)
                    return vd;
            }
            return null;
        }

        internal void Save()
        {
            try
            {
                JSONSerializer json = new JSONSerializer();
                System.IO.File.WriteAllText(SaveFileNamePath, json.Serialize(this));
            }
            catch (Exception exc)
            {

                MessageBox.Show("An exception was thrown when trying to save a file to:" + Environment.NewLine +
                                SaveFileNamePath+Environment.NewLine + "Ensure you have permissions to this directory. If the"+Environment.NewLine+"problem persists, delete all the files in that directory.");
                throw exc;
            }
        }

        internal static ExtensionList Load()
        {
            ExtensionList ti = new ExtensionList(); // Create object to ensure it validates file exists and creates defaults if not.

            JSONSerializer json = new JSONSerializer();
            return (ExtensionList)json.Deserialize(File.ReadAllText(SaveFileNamePath), typeof(ExtensionList));
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
                string configNamePath = Path.Combine(ad.FolderPath, "ExtensionList.bls");
                return configNamePath;
            }
        }
    }
}
