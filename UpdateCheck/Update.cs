using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace UpdateCheck
{


    internal class Updater
    {

        /// <summary>Get update and version information from specified online file - returns a List</summary>
        /// <param name="downloadsURL">URL to download file from</param>
        /// <param name="versionFile">Name of the pipe| delimited version file to download</param>
        /// <param name="resourceDownloadFolder">Folder on the local machine to download the version file to</param>
        /// <param name="startLine">Line number, of the version file, to read the version information from</param>
        /// <returns>List containing the information from the pipe delimited version file</returns>
        public static List<string> GetUpdateInfo(string downloadsURL, string versionFile, string resourceDownloadFolder, int startLine)
        {

            bool updateChecked = false;

            //create download folder if it does not exist
            if (!Directory.Exists(resourceDownloadFolder))
            {

                Directory.CreateDirectory(resourceDownloadFolder);

            }

            //let's try and download update information from the web
            updateChecked = WebData.downloadFromWeb(downloadsURL, versionFile, resourceDownloadFolder);

            //if the download of the file was successful
            if (updateChecked)
            {

                //get information out of download info file
                return PopulateInfoFromWeb(versionFile, resourceDownloadFolder, startLine);

            }
            //there is a chance that the download of the file was not successful
            else
            {

                return null;

            }

        }



        /// <summary>Download file from the web immediately</summary>
        /// <param name="downloadsURL">URL to download file from</param>
        /// <param name="filename">Name of the file to download</param>
        /// <param name="downloadTo">Folder on the local machine to download the file to</param>
        /// <param name="unzip">Unzip the contents of the file</param>
        /// <returns>Void</returns>
        public static void InstallUpdateNow(string downloadsURL, string filename, string downloadTo, bool unzip)
        {

            bool downloadSuccess = WebData.downloadFromWeb(downloadsURL, filename, downloadTo);

            if (unzip)
            {

                UnZip(downloadTo + filename, downloadTo);

            }

        }

        /// <summary>
        /// Downloads the unzip extract run.
        /// </summary>
        /// <param name="downloadURL">The download URL.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="downloadTo">The download to.</param>
        /// <param name="endTask">Process name to end before updating the software. This should exclude the extension of the application. ('notepad' instead of 'notepad.exe'</param>
        /// <param name="postProcess">The post process.</param>
        public static void DownloadUnzipExtractRun(string downloadURL, string fileName, string downloadTo,
                                                   string endTask, string postProcess,string extractTo)
        {
            if (!string.IsNullOrEmpty(endTask))
            {
                // Get all instances of Notepad running on the local
                // computer.
                Process[] localByName = Process.GetProcessesByName(endTask);
                foreach (Process p in localByName)
                {
                    p.Kill();
                }
            }

            // Unzip the file...
            bool downloadSuccess = WebData.downloadFromWeb(downloadURL, fileName, downloadTo);
            UnZip(Path.Combine( downloadTo , fileName), extractTo);

            // Launch the newly updated application now.
            Process.Start(postProcess);

        }


        internal static void EndProcess(string processName)
        {
            Process[] localByName = Process.GetProcessesByName(processName);
            foreach (Process p in localByName)
            {
                p.Kill();
            }
        }

        /// <summary>Starts the update application passing across relevant information</summary>
        /// <param name="downloadsURL">URL to download file from</param>
        /// <param name="filename">Name of the file to download</param>
        /// <param name="destinationFolder">Folder on the local machine to download the file to</param>
        /// <param name="processToEnd">Name of the process to end before applying the updates</param>
        /// <param name="postProcess">Name of the process to restart</param>
        /// <param name="startupCommand">Command line to be passed to the process to restart</param>
        /// <param name="updater"></param>
        /// <returns>Void</returns>
        public static void InstallUpdateRestart(string downloadsURL, string filename, string destinationFolder, string processToEnd, string postProcess, string startupCommand, string updater)
        {
            EndProcess(processToEnd);
            bool downloadSuccess = WebData.downloadFromWeb(downloadsURL, filename, destinationFolder);
            StartProcess(updater);

            // Quit out of the update checker.

        }

        private static void StartProcess(string pathToExe)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pathToExe
                }
            };
            process.Start();
        }

        private static void StartProcessAndWait(string pathToExe)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pathToExe
                }
            };
            process.Start();
            process.WaitForExit();
        }

        private static List<string> PopulateInfoFromWeb(string versionFile, string resourceDownloadFolder, int line)
        {

            List<string> tempList = new List<string>();
            int ln;
            int i;

            ln = 0;

            foreach (string strline in File.ReadAllLines(resourceDownloadFolder + versionFile))
            {

                if (ln == line)
                {

                    string[] parts = strline.Split('|');
                    foreach (string part in parts)
                    {

                        tempList.Add(part);

                    }

                    return tempList;

                }

                ln++;
            }


            return null;

        }




        private static bool UnZip(string file, string unZipToFolder)//, bool deleteZipOnCompletion)
        {
            try
            {

                // Specifying Console.Out here causes diagnostic msgs to be sent to the Console
                // In a WinForms or WPF or Web app, you could specify nothing, or an alternate
                // TextWriter to capture diagnostic messages. 

                using (ZipFile zip = ZipFile.Read(file))
                {
                    // This call to ExtractAll() assumes:
                    //   - none of the entries are password-protected.
                    //   - want to extract all entries to current working directory
                    //   - none of the files in the zip already exist in the directory;
                    //     if they do, the method will throw.
                    zip.ExtractAll(unZipToFolder);
                }

                //if (deleteZipOnCompletion) File.Delete(unZipTo + file);

            }
            catch (System.Exception)
            {
                return false;
            }

            return true;

        }

        /// <summary>Updates the update application by renaming prefixed files</summary>
        /// <param name="updaterPrefix">Prefix of files to be renamed</param>
        /// <param name="containingFolder">Folder on the local machine where the prefixed files exist</param>
        /// <returns>Void</returns>
        public static void UpdateMe(string updaterPrefix, string containingFolder)
        {

            DirectoryInfo dInfo = new DirectoryInfo(containingFolder);
            FileInfo[] updaterFiles = dInfo.GetFiles(updaterPrefix + "*");
            int fileCount = updaterFiles.Length;

            foreach (FileInfo file in updaterFiles)
            {

                string newFile = containingFolder + file.Name;
                string origFile = containingFolder + @"\" + file.Name.Substring(updaterPrefix.Length, file.Name.Length - updaterPrefix.Length);

                if (File.Exists(origFile)) { File.Delete(origFile); }

                File.Move(newFile, origFile);

            }

        }



    }
}
