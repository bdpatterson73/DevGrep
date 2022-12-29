using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrep.Classes.FileExport
{
    /// <summary>
    /// Class FileExportHTML
    /// </summary>
    /// <remarks>Does a replace on certain tokens within the template file. $SEARCHCRITERIA$,$FILENAME$,$FILETYPE$,
    /// $FILEFOLDER$,$FILEMATCHES$,$FILESIZE$,$FILEDATETIME$,$FILENAMEHEADER$,$FILETYPEHEADER$,
    /// $FILEFOLDERHEADER$,$FILEMATCHESHEADER$,$FILESIZEHEADER$,$FILEDATETIMEHEADER$</remarks>
    internal class FileExportHTML
    {
         private string _outputFile;
        private SearchTaskCollection _searchTasks;
        private string _searchCriteria;
        internal FileExportHTML(string outputFile, SearchTaskCollection searchTasks, string searchCriteria)
        {
            _searchCriteria = searchCriteria;
            _outputFile = outputFile;
            _searchTasks = searchTasks;

            SetOutputFields();
        }

        internal void CreateOutputfile()
        {
            
            string originalFile = File.ReadAllText(ResultsTemplateFile);
            string newFile = originalFile;
            StringBuilder sb = new StringBuilder();
            newFile = newFile.Replace("$FILENAMEHEADER$", "Name");
            newFile = newFile.Replace("$FILETYPEHEADER$", "Type");
            newFile = newFile.Replace("$FILEFOLDERHEADER$", "Folder");
            newFile = newFile.Replace("$FILEMATCHESHEADER$", "Matches");
            newFile = newFile.Replace("$FILESIZEHEADER$", "Size");
            newFile = newFile.Replace("$FILEDATETIMEHEADER$", "DateTime");
            newFile = newFile.Replace("$SEARCHCRITERIA$", _searchCriteria);
            // Extract the DIV tag which contains our template result row.
            string resultTemplate = GetTemplateRow(newFile);
            newFile = newFile.Replace(GetTemplateRowWithDiv(newFile), "$RESULTS$");

            foreach (SearchTask s in _searchTasks)
            {
                string currentResult = resultTemplate;
                
                FileInfo fi = new FileInfo(s.TargetFile);
                string fName = Path.GetFileName(s.TargetFile);
                string fPath = Path.GetDirectoryName(s.TargetFile);
                string fType = Win32Registry.ExtensionDescription(fi.Extension);
                long fMatches = s.MatchesFound;
                long fLength = fi.Length;
                DateTime fDateTime = fi.CreationTime;
                currentResult = currentResult.Replace("$FILEDATETIME$", fDateTime.ToString());
                currentResult = currentResult.Replace("$FILEFOLDER$", fPath);
                currentResult = currentResult.Replace("$FILENAME$", fName);
                currentResult = currentResult.Replace("$FILESIZE$", fLength.ToString());
                currentResult = currentResult.Replace("$FILETYPE$", fType);
                currentResult = currentResult.Replace("$FILEMATCHES$", fMatches.ToString());

                sb.AppendLine(currentResult);
            }
            newFile = newFile.Replace("$RESULTS$", sb.ToString());
          
            File.WriteAllText(_outputFile, newFile);
        }

        private string GetTemplateRow(string templateFile)
        {
            string startDiv = "<div id=\"resultTemplateDiv\" class=\"search-results-template-div\">";
            string endDiv = "</div>";
            int idxStart = templateFile.IndexOf(startDiv );
            if (idxStart < 0)
            {
                throw new ApplicationException("Missing template row start");
            }
            int idxEnd = templateFile.IndexOf(endDiv, idxStart);
            if (idxEnd < 0)
            {
                throw new ApplicationException("Missing template row end");
            }
            // Extract the text from in between this row.
            int startAfterDiv = idxStart + startDiv.Length;
            string subStringged = templateFile.Substring(startAfterDiv, idxEnd - startAfterDiv);
            return subStringged.Trim( );
        }
        private string GetTemplateRowWithDiv(string templateFile)
        {
            string startDiv = "<div id=\"resultTemplateDiv\" class=\"search-results-template-div\">";
            string endDiv = "</div>";
            int idxStart = templateFile.IndexOf(startDiv);
            if (idxStart < 0)
            {
                throw new ApplicationException("Missing template row start");
            }
            int idxEnd = templateFile.IndexOf(endDiv, idxStart);
            if (idxEnd < 0)
            {
                throw new ApplicationException("Missing template row end");
            }
            // Extract the text from in between this row.
            int startAfterDiv = idxStart;
            string subStringged = templateFile.Substring(startAfterDiv, idxEnd - startAfterDiv+endDiv.Length);
            return subStringged.Trim();
        }
        private void SetOutputFields()
        {
            
            //string[] allValues = _exportFields.Split('|');
            //foreach (string s in allValues)
            //{
            //  switch (s.ToUpper())
            //  {
            //      case "FILE DATE/TIME":
            //          FileDateTimeWrite = true;
            //          break;
            //      case "FILE PATH":
            //          FileFullPath = true;
            //          break;
            //      case "FILE NAME":
            //          FileNameWrite = true;
            //          break;
            //      case "FILE SIZE":
            //          FileSizeWrite = true;
            //          break;
            //      case "FILE TYPE":
            //          FileTypeWrite = true;
            //          break;
            //      case "FOLDER":
            //          FileFolderWrite = true;
            //          break;
            //      case "TOTAL MATCHES":
            //          TotalMatchesWrite = true;
            //          break;
            //  }
            //}
        }

        internal string ResultsTemplateFile
        {
            get { string thisApp = Application.StartupPath;
                string templateDir = Path.Combine(thisApp, "Templates");
                return Path.Combine(templateDir, "Results.html");
            }
        }
    }
}
