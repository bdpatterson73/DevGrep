using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevGrep.Classes;

namespace DevGrep
{
    public partial class formMainSearch
    {

        /// <summary>
        /// Performs the actual replacement operation
        /// </summary>
        /// <param name="targetFile">Target File</param>
        /// <param name="replaceText">Text to replace with.</param>
        private void PerformReplace(string targetFile, string replaceText)
        {
            string ConfirmEachReplacement = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ConfirmEach");
            string ConfirmFileReplace = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ConfirmFileReplace");
            string tmpFileName = "";
            // Open the source file.
            using (var sr = new StreamReader(targetFile))
            {
                tmpFileName = Path.GetTempFileName();
                using (var srWrite = new StreamWriter(tmpFileName))
                {
                    //Console.WriteLine("reading file: " + searchTask.TargetFile);
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        #region Create Confirmation

                        var sb = new StringBuilder();
                        sb.Append("Perform a replace operation on the following:");
                        sb.Append(Environment.NewLine);
                        sb.Append("File:");
                        sb.Append(targetFile);
                        sb.Append(Environment.NewLine);
                        sb.Append("Line:");
                        sb.Append(line);

                        #endregion

                        if (ConfirmEachReplacement.ToUpper() == "TRUE")
                        {
                            DialogResult dr = MessageBox.Show(sb.ToString(), "Confirm Replacement",
                                                              MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                line = Regex.Replace(line, _SearchText, replaceText, RegexOptions.IgnoreCase);
                                // | RegexOptions.IgnorePatternWhitespace);
                                srWrite.WriteLine(line);
                            }
                            if (dr == DialogResult.Cancel)
                            {
                                // Set flag to cancel the replace operation
                                _ReplaceOperation = false;
                                return;
                            }
                        }
                        else
                        {
                            line = Regex.Replace(line, _SearchText, replaceText, RegexOptions.IgnoreCase);
                            // | RegexOptions.IgnorePatternWhitespace);
                            srWrite.WriteLine(line);
                        }
                    }
                }
            }
            if (ConfirmFileReplace.ToUpper() == "TRUE")
            {
                #region Build file replacement confirmation

                var sb = new StringBuilder();
                sb.Append("Replace file:");
                sb.Append(Environment.NewLine);
                sb.Append(targetFile);
                sb.Append(Environment.NewLine);
                sb.Append("witn the copy containing all replaced text?");

                #endregion

                DialogResult dr = MessageBox.Show(sb.ToString(), "Confirm File Replace", MessageBoxButtons.YesNoCancel,
                                                  MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    FileAttributes fa = File.GetAttributes(targetFile);
                    File.SetAttributes(targetFile, FileAttributes.Normal);
                    File.Delete(targetFile);
                    File.Move(tmpFileName, targetFile);
                    File.SetAttributes(targetFile, fa);
                }
                if (dr == DialogResult.Cancel)
                {
                    // Set flag to cancel the replace operation
                    _ReplaceOperation = false;
                    return;
                }
            }
            else
            {
                FileAttributes fa = File.GetAttributes(targetFile);
                File.SetAttributes(targetFile, FileAttributes.Normal);
                File.Delete(targetFile);
                File.Move(tmpFileName, targetFile);
                File.SetAttributes(targetFile, fa);
            }
        }
    }
}
