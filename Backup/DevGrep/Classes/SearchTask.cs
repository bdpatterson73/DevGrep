namespace DevGrep.Classes
{
    /// <summary>
    /// Summary description for SearchTask.
    /// </summary>
    public class SearchTask
    {
        private string _TargetFile;
        private string _SearchString;
        private long _MatchesFound;
        private MatchLocationCollection _MatchLocCollection;

        public SearchTask(string targetFile, string searchString)
        {
            _TargetFile = targetFile;
            _SearchString = searchString;
            _MatchesFound = 0;
        }

        public MatchLocationCollection MatchLocCollection
        {
            get
            {
                return _MatchLocCollection;
            }
            set
            {
                _MatchLocCollection = value;
            }
        }

        public string TargetFile
        {
            get
            {
                return _TargetFile;
            }
        }

        public string SearchString
        {
            get
            {
                return _SearchString;
            }
        }

        public long MatchesFound
        {
            get
            {
                return _MatchesFound;
            }
            set
            {
                _MatchesFound = value;
            }
        }
    }
}