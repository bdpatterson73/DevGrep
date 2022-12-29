using System;
using System.Text;

using Microsoft.Win32;

namespace DevGrep.Classes.SourceControl
{
    /// <summary>
    /// Summary description for Scc.
    /// </summary>
    public class Scc
    {
        #region Events
        //Delegate for internal event fired for calling object
        public delegate bool dListPopulated(SSCI.SccStatus nStatus, string lpFileName);

        // Public event fired for client object
        public event dListPopulated ListPopulated;
        private SSCI.PopListFunc popListfunc; // Delegate instance passed to API.
        #endregion

        #region Private Members
        private IntPtr _handle;
        private IntPtr _sccContext;
        private string _sccName;
        private int _sccCaps;
        private string _sccAuxPathLabel;
        private int _sccCheckoutCommentLength;
        private int _sccCommentLength;
        private static bool _SourceControlPresent = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handle">Handle parent form.</param>
        public Scc(IntPtr handle)
        {
            //Create delegate so the PopulateList method can send back info.
            popListfunc = new SSCI.PopListFunc(ListFunction);
            _handle = handle;
            Initialize();

        }
        #endregion

        #region Initialize
        /// <summary>
        /// Initialize source control
        /// </summary>
        /// to open any dialogs.</param>
        /// <returns>Return value</returns>
        private int Initialize()
        {
            IntPtr iPtr = (IntPtr)null; //new IntPtr() ;
            string callerName = "DevGrep\0";
            StringBuilder sccName = new StringBuilder(SSCI.SCC_NAME_LEN + 1);
            int sccCaps = 0;
            StringBuilder auxPath = new StringBuilder(SSCI.SCC_AUXLABEL_LEN + 1);
            int chcommentLen = 0;
            int commentLen = 0;
            string temp = HKLMReadKey();
            int retval = SSCI.SccInitialize(HKLMReadKey(), ref iPtr, _handle, callerName, sccName, ref sccCaps, auxPath, ref chcommentLen, ref commentLen);
            this._sccAuxPathLabel = auxPath.ToString();
            this._sccCaps = sccCaps;
            this._sccCheckoutCommentLength = chcommentLen;
            this._sccCommentLength = commentLen;
            this._sccContext = iPtr;
            this._sccName = sccName.ToString();
            return retval;
        }
        #endregion

        public int Version()
        {
            return SSCI.SccGetVersion(HKLMReadKey());
        }

        #region GetProjPath
        /// <summary>
        /// Get Project Path
        /// </summary>
        /// <param name="LocalPath">Local path to project</param>
        /// <param name="ProjName">Project path in source control</param>
        /// <returns>Return code</returns>
        public int GetProjPath(ref string ProjName, ref string LocalPath)
        {
            StringBuilder user = new StringBuilder(SSCI.SCC_USER_LEN + 1); //Username 
            StringBuilder projName = new StringBuilder(SSCI._MAX_PATH + 1); //Project name 
            StringBuilder localPath = new StringBuilder(SSCI._MAX_PATH + 1); //Projects working path 
            StringBuilder auxProjPath = new StringBuilder(SSCI.SCC_PRJPATH_LEN + 1); //Returned project path 
            int allowChangePath = 1; //true //Allows source control to change the path.
            int newAllowed = 0; //TRUE=User can create a new project. If true
            //after return then project was created successfully.
            int retval = SSCI.SccGetProjPath(HKLMReadKey(), this._sccContext, _handle, user, projName, localPath, auxProjPath, allowChangePath, ref newAllowed);
            ProjName = projName.ToString();
            LocalPath = localPath.ToString();
            return retval;

        }
        #endregion

        #region Get
        /// <summary>
        /// Gets a file, files or a directory of files.
        /// </summary>
        /// <param name="fileList">String array of files or directories</param>
        /// <returns>Return result</returns>
        public int Get(string[] fileList)
        {
            return SSCI.SccGet(HKLMReadKey(), this._sccContext, this._handle, fileList.Length, fileList, SSCI.SCC_GET_ALL | SSCI.SCC_GET_RECURRSIVE, (IntPtr)null); //SSCI.SCC_GET_RECURRSIVE|
        }
        #endregion

        #region Uninitialize
        /// <summary>
        /// Called once when an SCC plug-in needs to be unplugged.
        /// </summary>
        /// <returns>SCCTRN value</returns>
        public int Uninitialize()
        {
            return SSCI.SccUninitialize(HKLMReadKey(), _sccContext);
        }
        #endregion

        #region PopulateList
        /// <summary>
        /// Populates list
        /// </summary>
        /// <param name="FileNames">Files or folder name</param>
        /// <param name="command">Command to perform</param>
        /// <param name="directoryEntries">True if the list is a directory entry</param>
        /// <returns>Return value</returns>
        public int PopulateList(string[] FileNames, SSCI.SCCCOMMAND command, bool directoryEntries)
        {
            int lpStatus = 0;
            IntPtr pvData = IntPtr.Zero;
            int options = (directoryEntries == true ? 0x00000001 : 0x00000000);
            int retval = SSCI.SccPopulateList(HKLMReadKey(), this._sccContext, command, FileNames.Length, FileNames, popListfunc, pvData, ref lpStatus, options);
            return retval;

        }
        #endregion

        #region Public Properties

        #region SccCommentLength Property
        /// <summary>
        /// Maximum check-in comment length
        /// </summary>
        public int SccCommentLength
        {
            get
            {
                return _sccCommentLength;
            }
            set
            {
                _sccCommentLength = value;
            }
        }
        #endregion

        #region SccCheckoutCommentLength Property
        /// <summary>
        /// Maximum check-out comment length
        /// </summary>
        public int SccCheckoutCommentLength
        {
            get
            {
                return _sccCheckoutCommentLength;
            }
            set
            {
                _sccCheckoutCommentLength = value;
            }
        }
        #endregion

        #region SccAuxPathLabel Property
        /// <summary>
        /// Aux Path Label
        /// </summary>
        public string SccAuxPathLabel
        {
            get
            {
                return _sccAuxPathLabel;
            }
            set
            {
                _sccAuxPathLabel = value;
            }
        }
        #endregion

        #region SccCaps Property
        /// <summary>
        /// Cabability flags of source control
        /// </summary>
        public int SccCaps
        {
            get
            {
                return _sccCaps;
            }
            set
            {
                _sccCaps = value;
            }
        }
        #endregion

        #region SccName Property
        /// <summary>
        /// Name of the source control provider
        /// </summary>
        public string SccName
        {
            get
            {
                return _sccName;
            }
            set
            {
                _sccName = value;
            }
        }
        #endregion

        #region SccContext Property
        /// <summary>
        /// Point to Source Control context structure.
        /// </summary>
        /// <remarks>Used on almost all subsequent calls to source control
        /// after Initialize.</remarks>
        public IntPtr SccContext
        {
            get
            {
                return _sccContext;
            }
            set
            {
                _sccContext = value;
            }
        }
        #endregion

        #endregion

        #region ListFunction
        /// <summary>
        /// Callback method used by SccPopulateList to return information.
        /// </summary>
        /// <remarks>This method, when called fires the ListPopulated event so the 
        /// host object can be notified.</remarks>
        /// <param name="pvCallerData">Caller Data</param>
        /// <param name="fAddRemove">AddRemove</param>
        /// <param name="nStatus">Status</param>
        /// <param name="lpFileName">Filename</param>
        /// <returnsReturn value></returns>
        private bool ListFunction(IntPtr pvCallerData, bool fAddRemove, SSCI.SccStatus nStatus, string lpFileName)
        {
            if (ListPopulated != null)
            {
                ListPopulated(nStatus, lpFileName);
            }
            return true;
        }
        #endregion

        internal string HKLMReadKey()
        {
            string providerRegKey = "";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\SourceCodeControlProvider");
            if (key != null)
            {
                providerRegKey = (string)key.GetValue("ProviderRegKey", "");
            }
            else
            {
                _SourceControlPresent = false;
                return "";
            }
            key = Registry.LocalMachine.OpenSubKey(providerRegKey);
            if (key != null)
            {
                _SourceControlPresent = true;
                return (string)key.GetValue("SccServerPath", "");
            }
            else
            {
                _SourceControlPresent = false;
                return "";
            }

        }

        public bool SourceControlPresent
        {
            get
            {
                HKLMReadKey();
                return _SourceControlPresent;
            }
        }
    }
}