using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrep.SubApps.DupFileScan.Classes
{
    /// <summary>
    /// File scanning class
    /// </summary>
    internal class ScanForFiles
    {
        internal enum HashTypes
        {
            SHA1,
            CRC32,
            SHA512,
            SHA256,
            SHA384,
            RIPEMD160

        }

        private readonly List<FileInfo> _files = new List<FileInfo>();
        private string _fileMask = "*.*";
        private ScanResultsList _filesList = new ScanResultsList();
        private bool _recursiveScan = true;
        private string _rootDir = string.Empty;
        private bool _generateHash = false;
        private HashTypes _hashType = HashTypes.SHA1;
        private string _extensionList = string.Empty;
        private string[] _extensionArray;
        private bool _yieldOtherProcesses = true;
        private bool _cancelOperation = false;

        internal HashTypes HashType
        {
            get { return _hashType; }
            set { _hashType = value; }
        }
        internal bool GenerateHash
        {
            get { return _generateHash; }
            set { _generateHash = value; }
        }
        internal bool YieldOtherProcesses
        {
            get { return _yieldOtherProcesses; }
            set { _yieldOtherProcesses = value; }
        }

        internal string ExtensionList
        {
            get { return _extensionList; }
            set 
            { 
                _extensionList = value;
                _extensionArray = _extensionList.ToUpper().Split(';');
            }
        }

        internal void CancelOperation()
        {
            _cancelOperation = true;
        }
        ///// <summary>
        ///// Gets or sets the file mask.
        ///// </summary>
        ///// <value>The file mask.</value>
        //internal string FileMask
        //{
        //    get { return _fileMask; }
        //    set { SetFileMask(value); }
        //}


        /// <summary>
        /// Gets or sets the files list.
        /// </summary>
        /// <value>The files list.</value>
        internal ScanResultsList FilesList
        {
            get
            {
                if (_files.Count == 0)
                {
                    Scan();//TODO Change this hash in to a property
                }
                return _filesList;
            }
            set { _filesList = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is recursive.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is recursive; otherwise, <c>false</c>.
        /// </value>
        internal bool IsRecursive
        {
            get { return _recursiveScan; }
            set { _recursiveScan = value; }
        }

        /// <summary>
        /// Gets or sets the root dir.
        /// </summary>
        /// <value>The root dir.</value>
        internal string RootDir
        {
            get { return _rootDir; }
            set { SetRootDir(value); }
        }

        /// <summary>
        /// Occurs when a new item is allocated.
        /// </summary>
        internal event AllocateNewEvent OnAllocateNew;

        /// <summary>
        /// Occurs when a directory is found.
        /// </summary>
        internal event ScanningDirectoryEvent OnDirectoryFound;

        /// <summary>
        /// Occurs when a file is found
        /// </summary>
        internal event FileFoundEvent OnFileFound;

        private void FireDirectoryFoundEvent(string directoryName)
        {
            if (OnDirectoryFound != null)
            {
                OnDirectoryFound(directoryName);
            }
        }

        private void FireFileFoundEvent(IScanForFilesData data)
        {
            if (OnFileFound != null)
            {
                OnFileFound(data);
            }
        }

        private IScanForFilesData FireNewItemEvent()
        {
            IScanForFilesData data = null;
            if (OnAllocateNew != null)
            {
                data = OnAllocateNew();
            }
            return data;
        }

        /// <summary>
        /// Gets the dirs.
        /// </summary>
        /// <param name="strPath">The STR path.</param>
        /// <returns></returns>
        protected DirectoryInfo[] GetDirs(string strPath)
        {
            try
            {
                var info = new DirectoryInfo(strPath);
                return info.GetDirectories();
            }
            catch (Exception)
            {
                
               
            }
            return null;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="strPath">The STR path.</param>
        /// <returns></returns>
        protected FileInfo[] GetFiles(string strPath)
        {
            try
            {
                var info = new DirectoryInfo(strPath);
                return info.GetFiles(_fileMask);
            }
            catch (Exception)
            {
                
               
            }
            return null;
        }

        private IScanForFilesData GetNewItem()
        {
            IScanForFilesData data = null;
            data = FireNewItemEvent();
            if (data == null)
            {
                data = new DefaultScanForFilesData();
            }
            return data;
        }

        /// <summary>
        /// Starts the scan for files process.
        /// </summary>
        internal void Scan()
        {
            _files.Clear();
            _filesList.Clear();
            ScanDirForDirsAndFiles(_rootDir);
            _cancelOperation = false;
        }

        /// <summary>
        /// Scans a directory for directories and files.
        /// </summary>
        /// <param name="strPath">The STR path.</param>
        protected void ScanDirForDirsAndFiles(string strPath)
        {
            if (_yieldOtherProcesses)
                Application.DoEvents();
            if (_cancelOperation == true)
                return;
            if (_recursiveScan)
            {
                try
                {
                    DirectoryInfo[] allDirs = GetDirs(strPath);
                    if (allDirs != null)
                    {
                        foreach (DirectoryInfo info in allDirs)
                        {
                            FireDirectoryFoundEvent(info.FullName);
                            ScanDirForDirsAndFiles(info.FullName);
                        }
                    }
                }
                catch (Exception)
                {
                    
                   
                }
            }
            FileInfo[] allFiless = GetFiles(strPath);
            if (allFiless != null)
            {
                foreach (FileInfo info2 in allFiless)
                {
                    if (_yieldOtherProcesses)
                        Application.DoEvents();
                    if (_extensionArray.Contains(info2.Extension.ToUpper()) || _extensionArray.Contains("*.*") ||
                        _extensionArray.Contains(".*"))
                    {
                        _files.Add(info2);
                        IScanForFilesData newItem = GetNewItem();
                        newItem.FileInfo = info2;

                        if (_generateHash)
                        {
                            switch (_hashType)
                            {
                                case HashTypes.SHA1:
                                    newItem.FileHashValue = SHA1Hash.HashFile(info2.FullName);
                                    break;
                                case HashTypes.CRC32: //TODO Get this crc32 working.
                                    //newItem.FileHashValue = CRC32.ComputerForFile(info2.FullName).ToString();
                                    break;
                                case HashTypes.SHA512:
                                    newItem.FileHashValue = SHA512Hash.HashFile(info2.FullName);
                                    break;
                                case HashTypes.RIPEMD160:
                                    newItem.FileHashValue = RIPEMD160Hash.HashFile(info2.FullName);
                                    break;
                                case HashTypes.SHA256:
                                    newItem.FileHashValue = SHA256Hash.HashFile(info2.FullName);
                                    break;
                                case HashTypes.SHA384:
                                    newItem.FileHashValue = SHA384Hash.HashFile(info2.FullName);
                                    break;
                            }


                        }

                        _filesList.Add(newItem);
                        FireFileFoundEvent(newItem);
                    }
                }
            }
        }

        private void SetFileMask(string fileMask)
        {
            string fileName = Path.GetFileName(fileMask);
            string directoryName = Path.GetDirectoryName(fileMask);
            SetRootDir(directoryName);
            _fileMask = fileName;
        }

        private void SetRootDir(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                _rootDir = Environment.CurrentDirectory;
            }
            else if (Path.IsPathRooted(path))
            {
                _rootDir = path;
            }
            else
            {
                _rootDir = Path.GetDirectoryName(path);
            }
        }
    }
}
