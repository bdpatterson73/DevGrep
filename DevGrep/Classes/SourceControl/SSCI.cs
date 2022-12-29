using System;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.Win32;

namespace DevGrep.Classes.SourceControl
{
    /// <summary>
    /// Summary description for SSCI.
    /// </summary>
    public class SSCI
    {
        private string m_sDllFile;

        private static bool _SourceControlPresent = false;

        public SSCI(string dllFile)
        {
            m_sDllFile = dllFile;

        }

        #region Constants
        internal const int _MAX_PATH = 260;
        /// <summary>Keep the file checked out</summary>
        internal const int SCC_KEEP_CHECKEDOUT = 0x1000;
        /// <summary>The SCC provider is expected to automatically detect if the file is text of binary</summary>
        internal const int SCC_FILETYPE_AUTO = 0x00;
        /// <summary>File type is text</summary>
        internal const int SCC_FILETYPE_TEXT = 0x01;
        /// <summary>File type is binary. SCC_FILETYPE_TEXT and SCC_FILETYPE_BINARY flags are mutually exclusive. Set exactly one of neither.</summary>
        internal const int SCC_FILETYPE_BINARY = 0x04;
        /// <summary>Store latest version only - no deltas.</summary>
        internal const int SCC_ADD_STORELATEST = 0x02;
        /// <summary>Ignore case differences</summary>
        internal const int SCC_DIFF_IGNORECASE = 0x0002;
        /// <summary>Ignore white space differences. SCC_DIFF_IGNORECASE and SCC_DIFF_IGNORESPACE flags are optional bitflags.</summary>
        internal const int SCC_DIFF_IGNORESPACE = 0x0004;
        /// <summary>'Quick Diff' by comparing entire file contents.</summary>
        internal const int SCC_DIFF_QD_CONTENTS = 0x0010;
        /// <summary>'Quick Diff' by checksum</summary>
        internal const int SCC_DIFF_QD_CHECKSUM = 0x0020;
        /// <summary>'Quick Diff' by file date/time stamp</summary>
        internal const int SCC_DIFF_QD_TIME = 0x0040;
        /// <summary>This is a mask used to check all the QD bitflags. It should not be passed into a function; the three QD bitflags are mutually exclusive. Quick diff always means no display UI.</summary>
        internal const int SCC_DIFF_QUICK_DIFF = 0x0070;
        /// <summary>The IDE is passing directories, not files.</summary>
        internal const int SCC_PL_DIR = 0x00000001;
        /// <summary>The IDE is passing directories, not files: get all files in these directories.</summary>
        internal const int SCC_GET_ALL = 0x00000001;
        /// <summary>The IDE is passing directories: Get these directories and all their subdirectories.</summary>
        internal const int SCC_GET_RECURRSIVE = 0x00000002;
        /// <summary>Set status of event queue.</summary>
        internal const int SCC_OPT_EVENTQUEUE = 0x00000001;
        /// <summary>Specify user data for SCC_OPT_NAMECHANGEPFN</summary>
        internal const int SCC_OPT_USERDATA = 0x00000002;
        /// <summary>The IDE can handle cancel</summary>
        internal const int SCC_OPT_HASCANCELMODE = 0x00000003;
        /// <summary>Set a callback for name changes</summary>
        internal const int SCC_OPT_NAMECHANGEPFN = 0x00000004;
        /// <summary>Disable SCC provider UI checkout and do not set working directory</summary>
        internal const int SCC_OPT_SCCCHECKOUTONLY = 0x00000005;
        /// <summary>Add from SCC to specify a working directory. Try to share into the associated project if it is a direct descendant.</summary>
        internal const int SCC_OPT_SHARESUBPROJ = 0x00000006;
        /// <summary>Suspend event queue activity</summary>
        internal const int SCC_OPT_EQ_DISABLE = 0x00;
        /// <summary>Enable event queue logging</summary>
        internal const int SCC_OPT_EQ_ENABLE = 0x01;
        /// <summary>(Default) Has no cancel mode, provider must supply if desired.</summary>
        internal const int SCC_OPT_HCM_NO = 0;
        /// <summary>IDE handles cancel</summary>
        internal const int SCC_OPT_HCM_YES = 1;
        /// <summary>(Default) OK to checkout from provider UI, working directory set.</summary>
        internal const int SCC_OPT_SCO_NO = 0;
        /// <summary>No provider UI checkout, no working directory.</summary>
        internal const int SCC_OPT_SCO_YES = 1;
        ///<summary>Supports the SCC_Remove command.</summary>
        internal const int SCC_CAP_REMOVE = 0x00000001;
        ///<summary>Supports the SCC_Rename command.</summary>
        internal const int SCC_CAP_RENAME = 0x00000002;
        ///<summary>Supports the SCC_Diff command.</summary>
        internal const int SCC_CAP_DIFF = 0x00000004;
        ///<summary>Supports the SCC_History command.</summary>
        internal const int SCC_CAP_HISTORY = 0x00000008;
        ///<summary>Supports the SCC_Properties command.</summary>
        internal const int SCC_CAP_PROPERTIES = 0x00000010;
        ///<summary>Supports the SCC_RunScc command.</summary>
        internal const int SCC_CAP_RUNSCC = 0x00000020;
        ///<summary>Supports the SCC_GetCommandOptions command.</summary>
        internal const int SCC_CAP_GETCOMMANDOPTIONS = 0x00000040;
        ///<summary>Supports the SCC_QueryInfo command.</summary>
        internal const int SCC_CAP_QUERYINFO = 0x00000080;
        ///<summary>Supports the SCC_GetEvents command.</summary>
        internal const int SCC_CAP_GETEVENTS = 0x00000100;
        ///<summary>Supports the SCC_GetProjPath command.</summary>
        internal const int SCC_CAP_GETPROJPATH = 0x00000200;
        ///<summary>Supports the SCC_AddFromScc command.</summary>
        internal const int SCC_CAP_ADDFROMSCC = 0x00000400;
        ///<summary>Supports a comment on Check out.</summary>
        internal const int SCC_CAP_COMMENTCHECKOUT = 0x00000800;
        ///<summary>Supports a comment on Check in.</summary>
        internal const int SCC_CAP_COMMENTCHECKIN = 0x00001000;
        ///<summary>Supports a comment on Add.</summary>
        internal const int SCC_CAP_COMMENTADD = 0x00002000;
        ///<summary>Supports a comment on Remove.</summary>
        internal const int SCC_CAP_COMMENTREMOVE = 0x00004000;
        ///<summary>Writes text to an IDE-provided output function.</summary>
        internal const int SCC_CAP_TEXTOUT = 0x00008000;
        ///<summary>Supports storing files without deltas.</summary>
        internal const int SCC_CAP_ADD_STORELATEST = 0x00200000;
        ///<summary>Multiple file history is supported.</summary>
        internal const int SCC_CAP_HISTORY_MULTFILE = 0x00400000;
        ///<summary>Supports case-insensitive file comparison.</summary>
        internal const int SCC_CAP_IGNORECASE = 0x00800000;
        ///<summary>Supports file comparison that ignores white space.</summary>
        internal const int SCC_CAP_IGNORESPACE = 0x01000000;
        ///<summary>Supports finding extra files.</summary>
        internal const int SCC_CAP_POPULATELIST = 0x02000000;
        ///<summary>Supports comments on create project.</summary>
        internal const int SCC_CAP_COMMENTPROJECT = 0x04000000;
        ///<summary>Supports diff in all states if under control.</summary>
        internal const int SCC_CAP_DIFFALWAYS = 0x10000000;
        ///<summary>Provider does not support a UI for SccGet but IDE may still call SccGet function.</summary>
        internal const int SCC_CAP_GET_NOUI = 0x20000000;
        ///<summary>Provider is reentrant and thread safe. In version 1.0, no providers were assumed to be reentrant and thread safe. If a 1.1 provider sets this bit, the host is allowed to open multiple projects in parallel. </summary>
        internal const int SCC_CAP_REENTRANT = 0x40000000;
        ///<summary>Supports the SccCreateSubProject command.</summary>
        internal const int SCC_CAP_CREATESUBPROJECT = 0x00010000;
        ///<summary>Supports the SccGetParentProjectPath command.</summary>
        internal const int SCC_CAP_GETPARENTPROJECT = 0x00020000;
        ///<summary>Supports the SccBeginBatch and SccEndBatch commands.</summary>
        internal const int SCC_CAP_BATCH = 0x00040000;
        ///<summary>Supports the querying of directory status.</summary>
        internal const int SCC_CAP_DIRECTORYSTATUS = 0x00080000;
        ///<summary>Supports differencing on directories.</summary>
        internal const int SCC_CAP_DIRECTORYDIFF = 0x00100000;
        ///<summary>Supports multiple checkouts on a file.</summary>
        internal const int SCC_CAP_MULTICHECKOUT = 0x08000000;
        ///<summary>Supports the MSSCCPRJ.SCC file subject to user/administrator override.</summary>
        internal const long SCC_CAP_SCCFILE = 0x80000000L;
        internal const string STR_SCC_PROVIDER_REG_LOCATION = "Software\\SourceCodeControlProvider";
        internal const string STR_PROVIDERREGKEY = "ProviderRegKey";
        internal const string STR_SCCPROVIDERPATH = "SCCServerPath";
        internal const string STR_SCCPROVIDERNAME = "SCCServerName";
        internal const string STR_SCC_INI_SECTION = "Source Code Control";
        internal const string STR_SCC_INI_KEY = "SourceCodeControlProvider";
        internal const string SCC_PROJECTNAME_KEY = "SCC_Project_Name";
        internal const string SCC_PROJECTAUX_KEY = "SCC_Aux_Path";
        internal const string SCC_STATUS_FILE = "MSSCCPRJ.SCC";
        internal const string SCC_KEY = "SCC";
        internal const string SCC_FILE_SIGNATURE = "This is a source code control file";
        ///<summary>lpSccName, SCCInitialize </summary>
        internal const int SCC_NAME_LEN = 31;
        ///<summary>lpAuxPathLabel, SCCInitialize</summary>
        internal const int SCC_AUXLABEL_LEN = 31;
        ///<summary>lpUser, SCCOpenProject</summary>
        internal const int SCC_USER_LEN = 31;
        ///<summary>lpAuxProjPath, SCCGetProjPath</summary>
        internal const int SCC_PRJPATH_LEN = 300;
        #endregion

        #region Delegates
        /// <summary>
        /// When the user executes a source code control operation from inside the IDE, 
        /// the SCC provider might want to convery error or status message relating to 
        /// the operation. The provier can display its own message boxes for this 
        /// purpose. However, for more seamless integration, the provider can pass 
        /// strings to the IDE, which then display them in its native way of displaying 
        /// statis information. The mechanism for this is the TextOutProc function. 
        /// The IDE must write a function for displaying error and status.
        /// </summary>
        /// <remarks>The IDE pass the SCC provier a delegate to this function, as the 
        /// TextOutProc parameter, when calling the SccOpenProject function.<br>
        /// Display_String: A text string to display. This string should not be terminated
        /// with a carriage return or a line feed.<br>
        /// Mesg_Type: The type of message. The following lists typical values for this
        /// parameter.</remarks>
        public delegate int TextOutProc(string displayString, SccMsg mesgType);

        public delegate bool PopListFunc(IntPtr pvCallerData, bool fAddRemove, SccStatus nStatus, string lpFileName);
        #endregion

        #region SCCCOMMAND Enumeration
        public enum SCCCOMMAND
        {
            SCC_COMMAND_GET,
            SCC_COMMAND_CHECKOUT,
            SCC_COMMAND_CHECKIN,
            SCC_COMMAND_UNCHECKOUT,
            SCC_COMMAND_ADD,
            SCC_COMMAND_REMOVE,
            SCC_COMMAND_DIFF,
            SCC_COMMAND_HISTORY,
            SCC_COMMAND_RENAME,
            SCC_COMMAND_PROPERTIES,
            SCC_COMMAND_OPTIONS
        } ;
        #endregion

        #region SccMsg Enumeration
        public enum SccMsg
        {
            SCC_MSG_RTN_CANCEL = -1, // Returned from call-back to indicate cancel
            SCC_MSG_RTN_OK = 0, // Returned from call-back to continue
            // Message types
            SCC_MSG_INFO = 1, // Message is informational
            SCC_MSG_WARNING, // Message is a warning
            SCC_MSG_ERROR, // Message is an error
            SCC_MSG_STATUS, // Message is meant for status bar
            // IDE supports Cancel operation
            SCC_MSG_DOCANCEL, // No text, IDE returns 0 or SCC_MSG_RTN_CANCEL
            SCC_MSG_STARTCANCEL, // Start a cancel loop
            SCC_MSG_STOPCANCEL // Stop the cancel loop
        } ;
        #endregion

        #region SccStatus Enumeration
        public enum SccStatus
        {
            SCC_STATUS_INVALID = -1,
            SCC_STATUS_NOTCONTROLLED = 0x0000,
            SCC_STATUS_CONTROLLED = 0x0001,
            SCC_STATUS_CHECKEDOUT = 0x0002,
            SCC_STATUS_OUTOTHER = 0x0004,
            SCC_STATUS_OUTEXCLUSIVE = 0x0008,
            SCC_STATUS_OUTMULTIPLE = 0x0010,
            SCC_STATUS_OUTOFDATE = 0x0020,
            SCC_STATUS_DELETED = 0x0040,
            SCC_STATUS_LOCKED = 0x0080,
            SCC_STATUS_MERGED = 0x0100,
            SCC_STATUS_SHARED = 0x0200,
            SCC_STATUS_PINNED = 0x0400,
            SCC_STATUS_MODIFIED = 0x0800,
            SCC_STATUS_OUTBYUSER = 0x1000
        } ;
        #endregion

        #region SCCTRN Enumeration
        /// <summary>
        /// Result of calls to SCC methods.
        /// </summary>
        public enum SCCTRN
        {
            ///<summary>Provider supports adding files from source control in two steps. For more information, see SccSetOption Function. </summary>
            SCC_I_SHARESUBPROJOK = 7,
            ///<summary>The local file is different from the file in the SCC database (for example, SccDiff Function command may return this value). </summary>
            SCC_I_FILEDIFFERS = 6,
            ///<summary>Local file was changed during the SCC operation; the IDE should reload the file if possible. </summary>
            SCC_I_RELOADFILE = 5,
            ///<summary>The file is not affected. </summary>
            SCC_I_FILENOTAFFECTED = 4,
            ///<summary>The Project was created during the SCC operation (for example, during a call to SccOpenProject Function when SCC_OP_CREATEIFNEW flag is specified). </summary>
            SCC_I_PROJECTCREATED = 3,
            ///<summary>Operation was cancelled. </summary>
            SCC_I_OPERATIONCANCELED = 2,
            ///<summary>Provider supports advanced options for the specified command. For more information, see SccGetCommandOptions Function. </summary>
            SCC_I_ADV_SUPPORT = 1,
            ///<summary>Success.</summary>
            SCC_OK = 0,
            ///<summary>Error, initialization failed.</summary>
            SCC_E_INITIALIZEFAILED = -1,
            ///<summary>Error, project is unknown.</summary>
            SCC_E_UNKNOWNPROJECT = -2,
            ///<summary>Error, project could not be created.</summary>
            SCC_E_COULDNOTCREATEPROJECT = -3,
            ///<summary>Error, the file is not checked out.</summary>
            SCC_E_NOTCHECKEDOUT = -4,
            ///<summary>Error, the file is already checked out.</summary>
            SCC_E_ALREADYCHECKEDOUT = -5,
            ///<summary>Error, the file is locked.</summary>
            SCC_E_FILEISLOCKED = -6,
            ///<summary>Error, the file is exclusively checked out.</summary>
            SCC_E_FILEOUTEXCLUSIVE = -7,
            ///<summary>There was a problem accessing the SCC system, probably due to network or contention issues. A retry is recommended. </summary>
            SCC_E_ACCESSFAILURE = -8,
            ///<summary>Error, there was a conflict during check in. </summary>
            SCC_E_CHECKINCONFLICT = -9,
            ///<summary>Error, the file already exists.</summary>
            SCC_E_FILEALREADYEXISTS = -10,
            ///<summary>Error, the file is not under SCC.</summary>
            SCC_E_FILENOTCONTROLLED = -11,
            ///<summary>Error, the file is checked out.</summary>
            SCC_E_FILEISCHECKEDOUT = -12,
            ///<summary>Error, there is no specified version.</summary>
            SCC_E_NOSPECIFIEDVERSION = -13,
            ///<summary>Error, the operation is not supported.</summary>
            SCC_E_OPNOTSUPPORTED = -14,
            ///<summary>Non-specific error.</summary>
            SCC_E_NONSPECIFICERROR = -15,
            ///<summary>Error, the operation was not performed.</summary>
            SCC_E_OPNOTPERFORMED = -16,
            ///<summary>Error, the type of the file, for example, binary, is not supported by the source code control system. </summary>
            SCC_E_TYPENOTSUPPORTED = -17,
            ///<summary>File has been auto-merged but has not been checked because it is pending user verification.</summary>
            SCC_E_VERIFYMERGE = -18,
            ///<summary>File has been auto-merged but has not been checked in due to a merge conflict which must be manually resolved.</summary>
            SCC_E_FIXMERGE = -19,
            ///<summary>Error due to a shell failure.</summary>
            SCC_E_SHELLFAILURE = -20,
            ///<summary>Error, the user is invalid.</summary>
            SCC_E_INVALIDUSER = -21,
            ///<summary>Error, the project is already open.</summary>
            SCC_E_PROJECTALREADYOPEN = -22,
            ///<summary>Project syntax error.</summary>
            SCC_E_PROJSYNTAXERR = -23,
            ///<summary>Error, the file path is invalid.</summary>
            SCC_E_INVALIDFILEPATH = -24,
            ///<summary>Error, the project is not open.</summary>
            SCC_E_PROJNOTOPEN = -25,
            ///<summary>Error, the user is not authorized to perform this operation.</summary>
            SCC_E_NOTAUTHORIZED = -26,
            ///<summary>File syntax error.</summary>
            SCC_E_FILESYNTAXERR = -27,
            ///<summary>Error, the local file does not exist.</summary>
            SCC_E_FILENOTEXIST = -28,
            ///<summary>Error, there was a connection failure.</summary>
            SCC_E_CONNECTIONFAILURE = -29,
            ///<summary>Unknown error.</summary>
            SCC_E_UNKNOWNERROR = -30
        }
        #endregion

        #region SccDirStatus Enumeration
        internal enum SccDirStatus
        {
            SCC_DIRSTATUS_INVALID = -1,
            SCC_DIRSTATUS_NOTCONTROLLED = 0x0000,
            SCC_DIRSTATUS_CONTROLLED = 0x0001,
            SCC_DIRSTATUS_EMPTYPROJ = 0x0002
        } ;
        #endregion

        #region LPCMDOPTS Structure
        /// <summary>
        /// Incomplete!
        /// </summary>
        internal IntPtr LPCMDOPTS = IntPtr.Zero;

//        internal struct LPCMDOPTS
//        {
//            public int dummy;
//        }
        #endregion

        #region SccInitialize
        [DllImport(@"DSC.dll", CharSet=CharSet.Auto, SetLastError=true)]
        internal static extern int SccInitialize(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            ref IntPtr ppvContext, // LPVOID*
            IntPtr hRef, // HWND
            [MarshalAs(UnmanagedType.LPStr)] string lpCallerName, // LPCSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpSccName, // LPSTR
            ref int lpSccCaps, // LPLONG
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpAuxPathLabel, // LPSTR
            ref int pnCheckoutCommentLen, // LPLONG
            ref int pnCommentLen // LPLPNG
            );
        #endregion

        #region SccGetProjPath
        /// <summary>
        /// Returns the project path for a selected source control project.
        /// </summary>
        /// <param name="pvContext">Context obtained from SccInitialize</param>
        /// <param name="hWnd">Handle to parent applications window</param>
        /// <param name="lpUser">Returned username</param>
        /// <param name="lpProjName">Returned project name</param>
        /// <param name="lpLocalPath">Local path for project</param>
        /// <param name="lpAuxProjPath">AuxPath</param>
        /// <param name="bAllowChangePath">True to allow source control to change
        /// the path.</param>
        /// <param name="pbNew">True to allow the user to create a new
        /// project.  If this is true after the return, then the project was created
        /// successfully.</param>
        /// <returns></returns>
        [DllImport(@"DSC.dll", CharSet=CharSet.Auto, SetLastError=true)]
        internal static extern int SccGetProjPath(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, // LPVOID
            IntPtr hWnd, // HWND
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpUser, // LPSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpProjName, // LPSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpLocalPath, // LPSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpAuxProjPath, // LPSTR
            int bAllowChangePath, // BOOL
            ref int pbNew // LPBOOL
            );
        #endregion

        #region SccOpenProject
        [DllImport(@"DSC.dll")]
        internal static extern int SccOpenProject(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, // LPVOID
            IntPtr hWnd, //HWND
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpUser, //LPSTR
            [MarshalAs(UnmanagedType.LPStr)] string lpProjName, //LPCSTR
            [MarshalAs(UnmanagedType.LPStr)] string lpLocalProjPath, //LPCSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpAuxProjPath, //LPSTR
            [MarshalAs(UnmanagedType.LPStr)] string lpComment, //LPCSTR
            TextOutProc lpTextOutProc, //LPTEXTOUTPROC
            long dwFlags //LONG
            );
        #endregion

        #region SccCloseProject
        [DllImport(@"DSC.dll")]
        internal static extern int SccCloseProject(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext //LPVOID
            );
        #endregion

        #region SccGetCommandOptions
        /// <summary>
        /// Returns the options
        /// </summary>
        /// <param name="pvContext">Context returned during initialize</param>
        /// <param name="hWnd">Handle to parent window in case a dialog needs to
        /// be shown</param>
        /// <param name="iCommand">Command type</param>
        /// <param name="ppvOptions">Must be null on first call</param>
        /// <returns></returns>
        [DllImport(@"DSC.dll")]
        internal static extern int SccGetCommandOptions(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, // LPVOID
            IntPtr hWnd, // HWND
            SCCCOMMAND iCommand, // enum SCCCOMMAND
            ref IntPtr ppvOptions // LPCMDOPTS* (null is valid)
            );
        #endregion

        #region SccGetVersion
        /// <summary>
        /// Returns an int data type that contains the version number of the SCC DLL. 
        /// IDEs written and tested against a particular version of an SCC DLL may 
        /// check the version number and take appropriate action in case the IDE 
        /// does not support all functionality in the returned value of the 
        /// version number.
        /// </summary>
        /// <returns>Int containing version number.</returns>
        [DllImport(@"DSC.dll")]
        internal static extern int SccGetVersion([MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath); // LPCSTR);
        #endregion

        #region SccSetOption
        /// <summary>
        /// The SccSetOption function allows the IDE to set options that control 
        /// the behavior of the SCC plug-in. It is a generic function used to set 
        /// a wide variety of options. Each option starts with SCC_OPT_xxx and 
        /// has its own defined set of values.
        /// </summary>
        /// <param name="pvContext">Context obtained during initialization</param>
        /// <param name="nOption">The option that is being set</param>
        /// <param name="dwVal">Settings for the option</param>
        /// <returns>Status</returns>
        [DllImport(@"DSC.dll")]
        internal static extern int SccSetOption(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            int nOption, //LONG
            int dwVal //LONG
            );
        #endregion

        #region SccUninitialize
        /// <summary>
        /// Called once when an SCC plug-in needs to be unplugged.
        /// </summary>
        /// <param name="pvContext">Context obtained during call to Initialize</param>
        /// <returns>Return code</returns>
        [DllImport(@"DSC.dll", CharSet=CharSet.Auto, SetLastError=true)]
        internal static extern int SccUninitialize(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext //LPVOID
            );
        #endregion

        #region SccAdd
        /*Then, what you have to do is create an IntPtr array which you will
        marshal the strings to (using the static StringToCoTaskMemAnsi method, or
        something similar).*/

        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccAdd(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPCSTR*
            string lpComment, //LPCSTR
            ref int pfOptions, //LONG*
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccAddFromScc
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccAddFromScc(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            ref int lpnFiles, //LPLONG
            IntPtr[] lplpFileNames //LPCSTR**
            );
        #endregion

        #region SccCheckin
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccCheckin(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPSTR*
            string lpComment, //LPCSTR
            int fOptions, //LONG
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccCheckout
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccCheckout(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPCSTR*
            string lpComment, //LPCSTR
            int fOptions, //LONG
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccDiff
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccDiff(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            string lpFileName, //LPCSTR
            int fOptions, //LONG
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccGet
        [DllImport(@"DSC.dll")]
        internal static extern int SccGet(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            [MarshalAs(UnmanagedType.LPArray)] String[] lpFileNames, //LPCSTR*
            int fOptions, //LONG
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccGetEvents
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccGetEvents(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpFileName, //LPSTR
            ref int lpStatus, //LPLONG
            ref int pnEventsRemaining //LPLONG
            );
        #endregion

        #region SccHistory
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccHistory(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPCSTR*
            int fOptions, //LONG
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccPopulateList
        [DllImport(@"DSC.dll")]
        internal static extern int SccPopulateList(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            SCCCOMMAND nCommand, //SCCCOMMAND
            int nFiles, //LONG
            String[] lpFileNames, //LPCSTR*
            PopListFunc pfnPopulate, //POPLISTFUNC
            IntPtr pvCallerData, //LPVOID
            ref int lpStatus, //LPLONG
            int fOptions //LONG
            );
        #endregion

        #region SccProperties
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccProperties(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            string lpFileName //LPCSTR
            );
        #endregion

        #region SccQueryInfo
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccQueryInfo(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPCSTR*
            ref int lpStatus //LPLONG
            );
        #endregion

        #region SccRemove
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccRemove(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPCSTR*
            string lpComment, //LPCSTR
            int fOptions, //LONG
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccRename
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccRename(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            string lpFileName, //LPCSTR
            string lpNewName //LPCSTR
            );
        #endregion

        #region SccRunScc
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccRunScc(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            IntPtr[] lpFileNames //LPCSTR*
            );
        #endregion

        #region SccUncheckout
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccUncheckout(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pvContext, //LPVOID
            IntPtr hWnd, //HWND
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPCSTR*
            int fOptions, //LONG
            IntPtr pvOptions
            );
        #endregion

        #region SccBeginBatch
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccBeginBatch([MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath); // LPCSTR);
        #endregion

        #region SccCreateSubProject
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccCreateSubProject(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pContext, //LPVOID
            IntPtr hWnd, //HWND
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpUser, //LPSTR
            string lpParentProjPath, //LPCSTR
            string lpSubProjName, //LPCSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpAuxProjPath, //LPSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpSubProjPath //LPSTR
            );
        #endregion

        #region SccDirDiff
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccDirDiff(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pContext, //LPVOID
            IntPtr hWnd, //HWND
            string lpDirName, //LPCSTR
            int dwFlags, //LONG
            IntPtr pvOptions //LPCMDOPTS
            );
        #endregion

        #region SccDirQueryInfo
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccDirQueryInfo(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pContext, //LPVOID
            int nDirs, //LONG
            IntPtr[] lpDirNames, //LPCSTR*
            ref int lpStatus //LPLONG
            );
        #endregion

        #region SccEndBatch
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccEndBatch([MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath); // LPCSTR);
        #endregion

        #region SccGetParentProjectPath
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccGetParentProjectPath(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pContext, //LPVOID
            IntPtr hWnd, //HWND
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpUser, //LPSTR
            string lpProjPath, //LPCSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpAuxProjPath, //LPSTR
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpParentProjPath //LPSTR
            );
        #endregion

        #region SccIsMultiCheckoutEnabled
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccIsMultiCheckoutEnabled(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pContext, //LPVOID
            ref bool pbMultiCheckout //LPBOOL
            );
        #endregion

        #region SccWillCreateSccFile
        [DllImport(@"DSC.dll")]
        internal static extern SCCTRN SccWillCreateSccFile(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibraryPath, // LPCSTR
            IntPtr pContext, //LPVOID
            int nFiles, //LONG
            IntPtr[] lpFileNames, //LPCSTR*
            ref bool pbSccFiles //LPBOOL
            );
        #endregion

        internal static string HKLMReadKey()
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

        public static bool SourceControlPresent
        {
            get
            {
                HKLMReadKey();
                return _SourceControlPresent;
            }
        }
    }
}