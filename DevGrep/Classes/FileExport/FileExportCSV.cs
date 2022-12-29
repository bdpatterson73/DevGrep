using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.Classes.FileExport
{
    internal class FileExportCSV
    {
        private string _outputFile;
        private SearchTaskCollection _searchTasks;
        private string _exportFields;
        internal FileExportCSV(string outputFile, SearchTaskCollection searchTasks, string exportFields)
        {
            _outputFile = outputFile;
            _searchTasks = searchTasks;
            _exportFields = exportFields;

            SetOutputFields();
        }

        internal void CreateOutputfile()
        {
            StringBuilder sb = new StringBuilder();
            string fileHeader = "";
            if (FileDateTimeWrite)
                fileHeader += ("DateTime,");
            if (FileFullPath)
                fileHeader += "FullPath,";
            if (FileFolderWrite)
                fileHeader += ("Folder,");
            if (FileNameWrite)
                fileHeader += ("Filename,");
            if (FileSizeWrite)
                fileHeader += ("FileSize,");
            if (FileTypeWrite)
                fileHeader += ("FileType,");
            if (TotalMatchesWrite)
                fileHeader += ("TotalMatches,");

            fileHeader = fileHeader.Substring(0, fileHeader.Length - 1);
            sb.AppendLine(fileHeader);


            foreach (SearchTask s in _searchTasks)
            {
                string currentResult = "";
                FileInfo fi = new FileInfo(s.TargetFile);
                string fName = Path.GetFileName(s.TargetFile);
                string fPath = Path.GetDirectoryName(s.TargetFile);
                string fType = Win32Registry.ExtensionDescription(fi.Extension);
                long fMatches = s.MatchesFound;
                long fLength = fi.Length;
                DateTime fDateTime = fi.CreationTime;
                if (FileDateTimeWrite)
                    currentResult+=(fDateTime.ToString() + ",");
                if (FileFullPath)
                    currentResult += fi.FullName + ",";
                if (FileFolderWrite)
                    currentResult += (fPath + ",");
                if (FileNameWrite)
                    currentResult += (fName + ",");
                if (FileSizeWrite)
                    currentResult += (fLength.ToString() + ",");
                if (FileTypeWrite)
                    currentResult += (fType + ",");
                if (TotalMatchesWrite)
                    currentResult += (fMatches.ToString() + ",");

                currentResult = currentResult.Substring(0, currentResult.Length - 1);
                sb.AppendLine(currentResult);
            }

            string finalOutput = sb.ToString();
            File.WriteAllText(_outputFile, finalOutput);
        }


        private void SetOutputFields()
        {
            
            string[] allValues = _exportFields.Split('|');
            foreach (string s in allValues)
            {
              switch (s.ToUpper())
              {
                  case "FILE DATE/TIME":
                      FileDateTimeWrite = true;
                      break;
                  case "FILE PATH":
                      FileFullPath = true;
                      break;
                  case "FILE NAME":
                      FileNameWrite = true;
                      break;
                  case "FILE SIZE":
                      FileSizeWrite = true;
                      break;
                  case "FILE TYPE":
                      FileTypeWrite = true;
                      break;
                  case "FOLDER":
                      FileFolderWrite = true;
                      break;
                  case "TOTAL MATCHES":
                      TotalMatchesWrite = true;
                      break;
              }
            }
        }
        private bool FileDateTimeWrite { get; set; }
        private bool FileNameWrite { get; set; }
        private bool FileSizeWrite { get; set; }
        private bool FileTypeWrite { get; set; }
        private bool FileFolderWrite { get; set; }
        private bool TotalMatchesWrite { get; set; }
        private bool FileFullPath { get; set; }
    }
}
