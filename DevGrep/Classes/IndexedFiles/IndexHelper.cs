using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevGrep.Classes.Config;
using SmartSearch.Analysis.Standard;
using SmartSearch.Index;
using SmartSearch.Store;
using Directory = System.IO.Directory;

namespace DevGrep.Classes.IndexedFiles
{
    internal class IndexHelper
    {
        private DirectoryInfo INDEX_DIR;

        private string _folderNamePath;
        private bool _allFiles;

        private string _indexFolderBaseName;
        private string _indexFolderFullPath;
        private long _filesIndexed=0;
        private double _indexTime;
        private bool _completed;

        // A delegate type for hooking up change notifications.
        public delegate void FileIndexedEventHandler(string fileName);
        public delegate void IndexCompleteEventHandler();
        public delegate void OptimizingEventHandler();
        public delegate void OptimizingCompleteEventHandler();
        public event FileIndexedEventHandler FileIndexed;
        public event IndexCompleteEventHandler IndexComplete;
        public event OptimizingEventHandler OptimizingStarted;
        public event OptimizingCompleteEventHandler OptimizingComplete;

        internal IndexHelper(string folderNamePath, bool allFiles)
        {
            ApplicationData ad = new ApplicationData();
            _folderNamePath = folderNamePath;
            _allFiles = allFiles;
            
            _indexFolderBaseName = Guid.NewGuid().ToString();
            _indexFolderFullPath = Path.Combine(ad.FolderPath, _indexFolderBaseName);
            _completed = false;
        }

        internal string FolderToIndex { get { return _folderNamePath; } }
        internal string IndexFolderBaseName
        {
            get { return _indexFolderBaseName; }
        }

        internal string IndexFolderFullPath
        {
            get { return _indexFolderFullPath; }
        }

        internal long FilesIndexed
        {
            get { return _filesIndexed; }
        }

        internal double IndexTime
        {
            get { return _indexTime; }
        }

        internal bool Completed
        {
            get { return _completed; }
        }

        internal void BeginIndexFolderAsync()
        {
            Thread t = new Thread(BeginIndexFolder);
            t.Start(); 
        }

        internal void BeginIndexFolder()
        {
            INDEX_DIR = new DirectoryInfo(_indexFolderFullPath);
            if (File.Exists(INDEX_DIR.FullName) || Directory.Exists(INDEX_DIR.FullName))
            {
                Console.Out.WriteLine("Cannot save index to '" + INDEX_DIR + "' directory, please delete it first");
                Environment.Exit(1);
            }

            var docDir = new DirectoryInfo(_folderNamePath);
            var docDirExists = File.Exists(docDir.FullName) || Directory.Exists(docDir.FullName);
            if (!docDirExists) // || !docDir.canRead()) // {{Aroush}} what is canRead() in C#?
            {
                Console.Out.WriteLine("Document directory '" + docDir.FullName + "' does not exist or is not readable, please check the path");
                Environment.Exit(1);
            }

            var start = DateTime.Now;
            try
            {
                using (var writer = new IndexWriter(FSDirectory.Open(INDEX_DIR), new StandardAnalyzer(SmartSearch.Util.Version.SmartSearch_30), true, IndexWriter.MaxFieldLength.LIMITED))
                {
                    Console.Out.WriteLine("Indexing to directory '" + INDEX_DIR + "'...");
                    IndexDirectory(writer, docDir);
                    if (OptimizingStarted != null)
                        OptimizingStarted();
                    Console.Out.WriteLine("Optimizing...");
                    writer.Optimize();
                    writer.Commit();
                    if (OptimizingComplete != null)
                        OptimizingComplete();
                }
                TimeSpan ts = DateTime.Now.Subtract(start);
                _indexTime =ts.TotalSeconds;
                _completed = true;
                if (IndexComplete != null)
                {
                    IndexComplete();
                }
            }
            catch (IOException e)
            {
                Console.Out.WriteLine(" caught a " + e.GetType() + "\n with message: " + e.Message);
            }
        }

        private void IndexDirectory(IndexWriter writer, DirectoryInfo directory)
        {
            foreach (var subDirectory in directory.GetDirectories())
                IndexDirectory(writer, subDirectory);

            foreach (var file in directory.GetFiles())
                IndexDocs(writer, file);
        }

        private void IndexDocs(IndexWriter writer, FileInfo file)
        {
            

            try
            {
                
              // Program.ConfigFile.SavedExtensionList)
                if (_allFiles)
                {
                    _filesIndexed++;
                    //Console.Out.WriteLine("adding " + file);
                    writer.AddDocument(FileDocumentNC.Document(file));
                    if (FileIndexed != null)
                    {
                        FileIndexed(file.FullName);
                    }
                }
                else
                {
                    if (Program.ConfigFile.SavedExtensionList.IsExtensionInList( file.Extension))
                    {
                       // Console.Out.WriteLine("adding " + file);
                        _filesIndexed++;
                       writer.AddDocument(FileDocumentNC.Document(file));
                        if (FileIndexed != null)
                        {
                            FileIndexed(file.FullName);
                        }
                    }
                }
                
            }
            catch (FileNotFoundException)
            {
                // At least on Windows, some temporary files raise this exception with an
                // "access denied" message checking if the file can be read doesn't help.
            }
            catch (UnauthorizedAccessException)
            {
                // Handle any access-denied errors that occur while reading the file.    
            }
            catch (IOException)
            {
                // Generic handler for any io-related exceptions that occur.
            }
        }
    }
}
