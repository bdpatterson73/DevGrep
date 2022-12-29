using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DevGrep.Classes.Config
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    internal class IniFile
    {
        /// <summary>
        /// The path
        /// </summary>
        public string path;

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <param name="INIPath">The INI path.</param>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        /// <summary>
        /// Writes the private profile string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="val">The val.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.Int64.</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
                                                             string key, string val, string filePath);

        /// <summary>
        /// Gets the private profile string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="def">The def.</param>
        /// <param name="retVal">The ret val.</param>
        /// <param name="size">The size.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                                                          string key, string def, StringBuilder retVal,
                                                          int size, string filePath);

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Key">The key.</param>
        /// <param name="Value">The value.</param>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Key">The key.</param>
        /// <returns>System.String.</returns>
        /// <PARAM name="Section"></PARAM>
        ///   <PARAM name="Key"></PARAM>
        ///   <PARAM name="Path"></PARAM>
        public string IniReadValue(string Section, string Key)
        {
            var temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, path);
            return temp.ToString();
        }
    }
}
