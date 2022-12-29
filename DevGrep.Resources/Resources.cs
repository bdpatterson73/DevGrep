using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevGrepResources
{
    public class Resources
    {

        public static Assembly AssemblyReference()
        {
            return Assembly.GetExecutingAssembly();
        }

        public static string GetResourceString(string resourceNamespace)

        {
            string contents = "";
            using (
               StreamReader _textStreamReader =
                   new StreamReader(AssemblyReference().GetManifestResourceStream(resourceNamespace)))
            {
                 contents = _textStreamReader.ReadToEnd();
            }
            return contents;
        }
    }
}
