using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.Classes
{
    internal static class BinFile
    {
        static void testBinaryFile(string folderPath)
        {
            List<string> output = new List<string>();
            foreach (string filePath in getFiles(folderPath, true))
            {
                output.Add(isBinary(filePath).ToString() + "  ----  " + filePath);
            }
            //Clipboard.SetText(string.Join("\n", output), TextDataFormat.Text);
            foreach (string s in output)
            {
                Console.WriteLine(s);
            }
        }

        public static List<string> getFiles(string path, bool recursive = false)
        {
            return Directory.Exists(path) ?
                Directory.GetFiles(path, "*.*",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList() :
                new List<string>();
        }

        public static bool isBinary(string path)
        {
           // if (path.ToUpper().Contains("NOP.WEB.EXE"))
            //{
            //    int balh = 0;
           // }
            int maxLengthToRead = 1024;
            FileInfo fi = new FileInfo(path);
            long length = fi.Length;
            if (length == 0) return false;

            using (StreamReader stream = new StreamReader(path))
            {
                int counted = 0;
                int ch;
                while ((ch = stream.Read()) != -1)
                {
                    counted++;
                    if (counted > maxLengthToRead) return false;
                    if (isControlChar(ch))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool isControlChar(int ch)
        {
            return (ch >Chars.NUL && ch < Chars.BS)
                || (ch > Chars.CR && ch < Chars.SUB);
        }

        public static class Chars
        {
            public static char NUL = (char)0; // Null char
            public static char BS = (char)8; // Back Space
            public static char CR = (char)13; // Carriage Return
            public static char SUB = (char)26; // Substitute
        }
    }
}
