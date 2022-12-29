using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAssembly.Attributes;

namespace DevGrep.SubApps.DupFileScan.Classes
{
    [Serializable]
    [DoNotObfuscateType]
    internal class SearchExtension
    {
        internal SearchExtension()
        {
        }
        internal SearchExtension(string displayName, string extensionList)
        {
            DisplayName = displayName;
            ExtensionList = extensionList;
        }

        public override string ToString()
        {
            return DisplayName;
        }
        internal string DisplayName { get; set; }
        internal string ExtensionList { get; set; }
    }
}
