using System.Web.UI.WebControls;

using Microsoft.Win32;

namespace DevGrep.Classes
{
    /// <summary>
    /// Methods for reading and writing the Windows Registry
    /// </summary>
    internal class Win32Registry
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected internal Win32Registry()
        {
        }
        #endregion

        #region HKLMReadKey
        /// <summary>
        /// Reads the specified key from HKEY_Local_Machine.
        /// </summary>
        /// <param name="KeyPath">Path to the specified key. Specified as:
        /// Software\Microsoft\Windows\CurrentVersion</param>
        /// <param name="KeyName">Key name</param>
        /// <returns>Value contained within the registry key. If the key cannot be 
        /// found or the key is emptry, an empty string is returned.</returns>
        internal static string HKLMReadKey(string KeyPath, string KeyName)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(KeyPath);
            if (key != null)
            {
                return (string)key.GetValue(KeyName, "");
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region HKCUReadKey
        /// <summary>
        /// Reads the specified key from HKEY_Current_User.
        /// </summary>
        /// <param name="KeyPath">Path to the specified key. Specified as:
        /// Software\Microsoft\Windows\CurrentVersion</param>
        /// <param name="KeyName">Key name</param>
        /// <returns>Value contained within the registry key. If the key cannot be 
        /// found or the key is emptry, an empty string is returned.</returns>
        internal static string HKCUReadKey(string KeyPath, string KeyName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyPath);
            if (key != null)
            {
                return (string)key.GetValue(KeyName, "");
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region HKLMReadKeys
        /// <summary>
        /// Returns a string array of all key values at the specified registry 
        /// location.
        /// </summary>
        /// <param name="KeyPath">Path to the values to read.Specified as:
        /// Software\Microsoft\Windows\CurrentVersion</param>
        /// <returns>String array containing all values read from the registry</returns>
        internal static string[] HKLMReadKeys(string KeyPath)
        {
            RegistryKey SubKeys = Registry.LocalMachine.OpenSubKey(KeyPath);
            string[] keys = SubKeys.GetValueNames();
            string[] retval = new string[keys.GetUpperBound(0) + 1];
            int Counter = 0;
            foreach (string s in keys)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(KeyPath);
                if (key != null)
                {
                    retval[Counter] = (string)key.GetValue(s, "");
                }
                else
                {
                    retval[Counter] = "";
                }
                Counter++;
            }
            return retval;
        }
        #endregion

        #region HKLMReadKeysNVPair
        /// <summary>
        /// Reads all keys at the specified registry location and returns the key
        /// names and values as ListItem objects in a ListItemCollection.
        /// </summary>
        /// <param name="KeyPath">Path to the values to read.Specified as:
        /// Software\Microsoft\Windows\CurrentVersion</param>
        /// <returns>All key names and values are returned as ListItem objects 
        /// in a ListItemCollection.</returns>
        internal static ListItemCollection HKLMReadKeysNVPair(string KeyPath)
        {
            ListItemCollection lic = new ListItemCollection();
            ;
            ListItem li;
            RegistryKey SubKeys = Registry.LocalMachine.OpenSubKey(KeyPath);
            string[] keys = SubKeys.GetValueNames();
            foreach (string s in keys)
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(KeyPath);
                li = new ListItem();
                if (key != null)
                {
                    li.Text = s;
                    li.Value = (string)key.GetValue(s, "");
                }
                else
                {
                    li.Text = s;
                    li.Value = "";
                }
                lic.Add(li);
            }
            return lic;
        }
        #endregion

        internal static string HKCRReadKey(string KeyPath, string KeyName)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(KeyPath);
            if (key != null)
            {
                return (string)key.GetValue(KeyName, "");
            }
            else
            {
                return "";
            }
        }

        internal static void HKCUWriteKey(string KeyPath, string KeyName, string KeyValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(KeyPath);
            key.SetValue(KeyName, KeyValue);
        }

        public static string ExtensionDescription(string Extension)
        {
            string DescKey = Win32Registry.HKCRReadKey(Extension, null);
            if (DescKey.Length != 0)
            {
                string FileType = Win32Registry.HKCRReadKey(DescKey, null);
                return FileType;
            }
            else
            {
                return Extension + " File";
            }
        }
    }
}