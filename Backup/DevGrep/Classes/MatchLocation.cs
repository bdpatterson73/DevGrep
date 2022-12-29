namespace DevGrep.Classes
{
    /// <summary>
    /// Summary description for MatchLocation.
    /// </summary>
    public class MatchLocation
    {
        private int _Row;
        private int _Column;
        private int _Length;
        private string _FileName;

        public MatchLocation()
        {
            _Row = 0;
            _Column = 0;
            _FileName = "";
            _Length = 0;
        }

        public MatchLocation(int row, int column, string fileName, int Length)
        {
            _Row = row;
            _Column = column;
            _FileName = fileName;
            _Length = Length;
        }

        public int Row
        {
            get
            {
                return _Row;
            }
            set
            {
                _Row = value;
            }
        }

        public int Column
        {
            get
            {
                return _Column;
            }
            set
            {
                _Column = value;
            }
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                _Length = value;
            }
        }
    }
}