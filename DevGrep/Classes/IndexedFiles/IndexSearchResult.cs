using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.Classes.IndexedFiles
{
    internal class IndexSearchResult
    {

        private int _documentNumber;
        private float _score;
        private string _fileNamePath;

        internal IndexSearchResult(int documentNumber, float score, string fileNamePath)
        {
            _documentNumber = documentNumber;
            _score = score;
            _fileNamePath = fileNamePath;
        }

        internal int DocumentNumber { get { return _documentNumber; } }
        internal float Score { get { return _score; } }
        internal string FileNamePath { get { return _fileNamePath; } }

    }
}
