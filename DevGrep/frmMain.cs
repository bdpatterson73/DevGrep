using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using DevGrep.Classes;
using DevGrep.Classes.Misc;
//TODO using DevGrep.DataSets;
using DevGrep.Forms;

namespace DevGrep
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class frmMain : Form
    {
        #region Private Members
        //private Scc _scc;
        private TaskbarNotifier tn;
        private System.Windows.Forms.MenuItem mnuRegularExpressions;
        private System.Windows.Forms.ContextMenu cmSearchType;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem mnuDatabaseSearch;
        private long _lastClick;

        [DllImport("User32.Dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("shell32.dll")]
        private static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            int nShowCmd);

        private const int EM_LINEINDEX = 0xBB;

        private enum CurrentAction
        {
            Nothing,
            Searching,
            DoneSearchingWithResults,
            DoneSearchingWithoutResults,
            SearchItemSelected
        }

        private ListViewSortManager m_sortMgr;
        private int m_nFirstCharOnPage;
        private string _SearchText = "";
        private string _SearchPath = "";
        private string _SearchExtensions = "";
        private bool doContinue = true;
        private bool _MatchCase = false;
        private bool _Subdirectories = true;
        private bool _ignoreBinary = true;
        private string _CurrentFileName = "";
        private PersistWindowState m_windowState;
        private bool _ReplaceOperation = true;
        private MainMenu mainMenu1;
        private ToolBar toolBar1;
        private ToolBarButton tbOpen;
        private ToolBarButton tbSave;
        private ToolBarButton tbPrint;
        private ToolBarButton toolBarButton1;
        private ToolBarButton tbSearch;
        private ToolBarButton tbReplace;
        private ToolBarButton tbSearchAgain;
        private ToolBarButton toolBarButton2;
        private ToolBarButton tbStop;
        private ImageList ilToolbar;
        private Panel panel1;
        private Label lblInfo;
        private ListView lvFiles;
        private ColumnHeader chFilename;
        private ColumnHeader chType;
        private ColumnHeader chFolder;
        private ColumnHeader chMatches;
        private ColumnHeader chSize;
        private ColumnHeader chDateTime;
        private MenuItem mnuFile;
        private MenuItem mnuEdit;
        private MenuItem mnuSearch;
        private MenuItem mnuView;
        private MenuItem mnuOptions;
        private MenuItem mnuWindow;
        private MenuItem mnuHelp;
        private StatusBar statusBar1;
        private MenuItem mnuOpen;
        private MenuItem mnuSaveAs;
        private MenuItem menuItem10;
        private MenuItem mnuPrint;
        private MenuItem mnuPrintSetup;
        private MenuItem menuItem13;
        private MenuItem mnuExit;
        private MenuItem mnuCopy;
        private MenuItem menuItem1;
        private MenuItem mnuSelectAll;
        private MenuItem mnuSearchNow;
        private MenuItem mnuSearchFilesInList;
        private MenuItem menuItem2;
        private MenuItem mnuReplace;
        private MenuItem mnuAllMatches;
        private MenuItem mnuMatchesInSelectedFile;
        private MenuItem mnuSelectedFile;
        private MenuItem menuItem7;
        private MenuItem mnuPreviousFile;
        private MenuItem mnuNextFile;
        private MenuItem menuItem11;
        private MenuItem mnuPreviousMatch;
        private MenuItem mnuNextMatch;
        private MenuItem mnuPreferences;
        private MenuItem menuItem16;
        private MenuItem mnuBeginnerMode;
        private MenuItem mnuExpertMode;
        private ContextMenu cmFiles;
        private MenuItem mnuFileOpen;
        private MenuItem mnuFileProperties;
        private ContextMenu cmResults;
        private MenuItem mnuViewFile;
        private MenuItem mnuViewLine;
        private MenuItem mnuAbout;
        private MenuItem mnuSave;
        private Splitter splitter1;
        private RichTextBoxEx.RichTextBoxEx rtbResults;
        private PageSetupDialog pageSetupDialog1;
        private PrintDialog printDialog1;
        private PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        private MenuItem mnuPrintPreview;
        private ToolBarButton toolBarButton3;
        private ToolBarButton tbResults;
        private MenuItem mnuExport;
        private MenuItem menuItem3;
        private MenuItem menuOpenContaingFolder;
        private MenuItem menuItem8;
        private MenuItem menuFileCSV;
        private MenuItem menuCompiledCSV;
        private MenuItem menuExportFile;
        private MenuItem menuFileHTML;
        private MenuItem menuFileRTF;
        private MenuItem menuFileText;
        private MenuItem menuExportCompiled;
        private MenuItem menuCompiledHTML;
        private MenuItem menuCompiledRTF;
        private MenuItem menuCompiledText;
        private MenuItem menuSourceControl;
        private MenuItem menuItem21;
        private MenuItem menuOpenSourceControl;
        private MenuItem menuChangeSourceControl;
        private MenuItem mnuPrintFileList;
        private IContainer components;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain(string[] args)
        {
            // Check Default Registry Settings
            DefaultRegEntries();

            Application.EnableVisualStyles();
            Application.DoEvents();
            m_windowState = new PersistWindowState();
            m_windowState.Parent = this;

            // set registry path in HKEY_CURRENT_USER
            m_windowState.RegistryPath = @"Software\DevGrep\Main\";

            InitializeComponent();
            // Init ListView soring.
            m_sortMgr = new ListViewSortManager(
                lvFiles,
                new Type[]
                    {
                        typeof (ListViewTextCaseInsensitiveSort),
                        typeof (ListViewTextCaseInsensitiveSort),
                        typeof (ListViewTextCaseInsensitiveSort),
                        typeof (ListViewInt32Sort),
                        typeof (ListViewInt32Sort),
                        typeof (ListViewDateSort),
                    },
                0, // Set default sort column (zero based index)
                SortOrder.Ascending
                );
            // Now we can enable sorting for the ListView control
            m_sortMgr.SortEnabled = true;
            // Check for command line parameters
            ParseCmdLine(args);
            Setup();

        }
        #endregion

        private void Setup()
        {
            tn.SetBackgroundBitmap(new Bitmap(GetType(), "skin3.bmp"), Color.FromArgb(255, 0, 255));
            tn.SetCloseBitmap(new Bitmap(GetType(), "close.bmp"), Color.FromArgb(255, 0, 255), new Point(280, 57));
            tn.TitleRectangle = new Rectangle(75, 57, 200, 28);
            tn.ContentRectangle = new Rectangle(75, 92, 215, 55);
            tn.ContentClick += new EventHandler(tn_ContentClick);

        }

        #region ParseCmdLine
        /// <summary>
        /// Parse the command line parameters
        /// </summary>
        /// <param name="args">CommandLine Args</param>
        private void ParseCmdLine(string[] args)
        {
            if (args.GetUpperBound(0) == 0)
            {
                switch (args[0])
                {
                    case "/?":
                        StringBuilder sb = new StringBuilder();
                        sb.Append("DevGrep Usage" + Environment.NewLine);
                        sb.Append("DevGrep.exe <filename.dgb>");
                        MessageBox.Show(sb.ToString(), "DevGrep Usage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        Application.DoEvents();
                        this.Show();
                        OpenFile(args[0], true);
                        break;
                }

            }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuOpen = new System.Windows.Forms.MenuItem();
            this.mnuSave = new System.Windows.Forms.MenuItem();
            this.mnuSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.mnuExport = new System.Windows.Forms.MenuItem();
            this.menuExportFile = new System.Windows.Forms.MenuItem();
            this.menuFileCSV = new System.Windows.Forms.MenuItem();
            this.menuFileHTML = new System.Windows.Forms.MenuItem();
            this.menuFileRTF = new System.Windows.Forms.MenuItem();
            this.menuFileText = new System.Windows.Forms.MenuItem();
            this.menuExportCompiled = new System.Windows.Forms.MenuItem();
            this.menuCompiledCSV = new System.Windows.Forms.MenuItem();
            this.menuCompiledHTML = new System.Windows.Forms.MenuItem();
            this.menuCompiledRTF = new System.Windows.Forms.MenuItem();
            this.menuCompiledText = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuPrint = new System.Windows.Forms.MenuItem();
            this.mnuPrintPreview = new System.Windows.Forms.MenuItem();
            this.mnuPrintSetup = new System.Windows.Forms.MenuItem();
            this.mnuPrintFileList = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuSourceControl = new System.Windows.Forms.MenuItem();
            this.menuOpenSourceControl = new System.Windows.Forms.MenuItem();
            this.menuChangeSourceControl = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuSelectAll = new System.Windows.Forms.MenuItem();
            this.mnuSearch = new System.Windows.Forms.MenuItem();
            this.mnuSearchNow = new System.Windows.Forms.MenuItem();
            this.mnuSearchFilesInList = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuReplace = new System.Windows.Forms.MenuItem();
            this.mnuView = new System.Windows.Forms.MenuItem();
            this.mnuAllMatches = new System.Windows.Forms.MenuItem();
            this.mnuMatchesInSelectedFile = new System.Windows.Forms.MenuItem();
            this.mnuSelectedFile = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mnuPreviousFile = new System.Windows.Forms.MenuItem();
            this.mnuNextFile = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.mnuPreviousMatch = new System.Windows.Forms.MenuItem();
            this.mnuNextMatch = new System.Windows.Forms.MenuItem();
            this.mnuOptions = new System.Windows.Forms.MenuItem();
            this.mnuPreferences = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.mnuBeginnerMode = new System.Windows.Forms.MenuItem();
            this.mnuExpertMode = new System.Windows.Forms.MenuItem();
            this.mnuRegularExpressions = new System.Windows.Forms.MenuItem();
            this.mnuWindow = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbSearch = new System.Windows.Forms.ToolBarButton();
            this.cmSearchType = new System.Windows.Forms.ContextMenu();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuDatabaseSearch = new System.Windows.Forms.MenuItem();
            this.tbReplace = new System.Windows.Forms.ToolBarButton();
            this.tbSearchAgain = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.tbStop = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.tbResults = new System.Windows.Forms.ToolBarButton();
            this.ilToolbar = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbResults = new RichTextBoxEx.RichTextBoxEx();
            this.cmResults = new System.Windows.Forms.ContextMenu();
            this.mnuViewFile = new System.Windows.Forms.MenuItem();
            this.mnuViewLine = new System.Windows.Forms.MenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMatches = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmFiles = new System.Windows.Forms.ContextMenu();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.menuOpenContaingFolder = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mnuFileProperties = new System.Windows.Forms.MenuItem();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.tn = new DevGrep.Classes.TaskbarNotifier();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuSearch,
            this.mnuView,
            this.mnuOptions,
            this.mnuWindow,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOpen,
            this.mnuSave,
            this.mnuSaveAs,
            this.menuItem10,
            this.mnuExport,
            this.menuItem3,
            this.mnuPrint,
            this.mnuPrintPreview,
            this.mnuPrintSetup,
            this.mnuPrintFileList,
            this.menuItem13,
            this.menuSourceControl,
            this.menuItem21,
            this.mnuExit});
            this.mnuFile.Text = "&File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Index = 0;
            this.mnuOpen.Text = "&Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Index = 1;
            this.mnuSave.Text = "&Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Enabled = false;
            this.mnuSaveAs.Index = 2;
            this.mnuSaveAs.Text = "Save As...";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 3;
            this.menuItem10.Text = "-";
            // 
            // mnuExport
            // 
            this.mnuExport.Index = 4;
            this.mnuExport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuExportFile,
            this.menuExportCompiled});
            this.mnuExport.Text = "Expor&t Results";
            // 
            // menuExportFile
            // 
            this.menuExportFile.Index = 0;
            this.menuExportFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFileCSV,
            this.menuFileHTML,
            this.menuFileRTF,
            this.menuFileText});
            this.menuExportFile.Text = "File";
            // 
            // menuFileCSV
            // 
            this.menuFileCSV.Index = 0;
            this.menuFileCSV.Text = "CSV";
            this.menuFileCSV.Click += new System.EventHandler(this.menuFileCSV_Click);
            // 
            // menuFileHTML
            // 
            this.menuFileHTML.Index = 1;
            this.menuFileHTML.Text = "HTML";
            this.menuFileHTML.Click += new System.EventHandler(this.menuFileHTML_Click);
            // 
            // menuFileRTF
            // 
            this.menuFileRTF.Index = 2;
            this.menuFileRTF.Text = "RTF";
            this.menuFileRTF.Click += new System.EventHandler(this.menuFileRTF_Click);
            // 
            // menuFileText
            // 
            this.menuFileText.Index = 3;
            this.menuFileText.Text = "Text";
            this.menuFileText.Click += new System.EventHandler(this.menuFileText_Click);
            // 
            // menuExportCompiled
            // 
            this.menuExportCompiled.Index = 1;
            this.menuExportCompiled.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuCompiledCSV,
            this.menuCompiledHTML,
            this.menuCompiledRTF,
            this.menuCompiledText});
            this.menuExportCompiled.Text = "Compiled";
            // 
            // menuCompiledCSV
            // 
            this.menuCompiledCSV.Index = 0;
            this.menuCompiledCSV.Text = "CSV";
            this.menuCompiledCSV.Click += new System.EventHandler(this.menuCompiledCSV_Click);
            // 
            // menuCompiledHTML
            // 
            this.menuCompiledHTML.Index = 1;
            this.menuCompiledHTML.Text = "HTML";
            this.menuCompiledHTML.Click += new System.EventHandler(this.menuCompiledHTML_Click);
            // 
            // menuCompiledRTF
            // 
            this.menuCompiledRTF.Index = 2;
            this.menuCompiledRTF.Text = "RTF";
            this.menuCompiledRTF.Click += new System.EventHandler(this.menuCompiledRTF_Click);
            // 
            // menuCompiledText
            // 
            this.menuCompiledText.Index = 3;
            this.menuCompiledText.Text = "Text";
            this.menuCompiledText.Click += new System.EventHandler(this.menuCompiledText_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 5;
            this.menuItem3.Text = "-";
            // 
            // mnuPrint
            // 
            this.mnuPrint.Index = 6;
            this.mnuPrint.Text = "&Print";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // mnuPrintPreview
            // 
            this.mnuPrintPreview.Index = 7;
            this.mnuPrintPreview.Text = "P&rint Preview";
            this.mnuPrintPreview.Click += new System.EventHandler(this.mnuPrintPreview_Click);
            // 
            // mnuPrintSetup
            // 
            this.mnuPrintSetup.Index = 8;
            this.mnuPrintSetup.Text = "Print S&etup...";
            this.mnuPrintSetup.Click += new System.EventHandler(this.mnuPrintSetup_Click);
            // 
            // mnuPrintFileList
            // 
            this.mnuPrintFileList.Index = 9;
            this.mnuPrintFileList.Text = "Print File List";
            this.mnuPrintFileList.Click += new System.EventHandler(this.mnuPrintFileList_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 10;
            this.menuItem13.Text = "-";
            // 
            // menuSourceControl
            // 
            this.menuSourceControl.Enabled = false;
            this.menuSourceControl.Index = 11;
            this.menuSourceControl.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOpenSourceControl,
            this.menuChangeSourceControl});
            this.menuSourceControl.Text = "Source Control";
            // 
            // menuOpenSourceControl
            // 
            this.menuOpenSourceControl.Index = 0;
            this.menuOpenSourceControl.Text = "Open from Source Control";
            // 
            // menuChangeSourceControl
            // 
            this.menuChangeSourceControl.Index = 1;
            this.menuChangeSourceControl.Text = "Change Source Control";
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 12;
            this.menuItem21.Text = "-";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 13;
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuCopy,
            this.menuItem1,
            this.mnuSelectAll});
            this.mnuEdit.Text = "&Edit";
            this.mnuEdit.Visible = false;
            // 
            // mnuCopy
            // 
            this.mnuCopy.Enabled = false;
            this.mnuCopy.Index = 0;
            this.mnuCopy.Text = "Copy";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // mnuSelectAll
            // 
            this.mnuSelectAll.Enabled = false;
            this.mnuSelectAll.Index = 2;
            this.mnuSelectAll.Text = "Select All";
            // 
            // mnuSearch
            // 
            this.mnuSearch.Index = 2;
            this.mnuSearch.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSearchNow,
            this.mnuSearchFilesInList,
            this.menuItem2,
            this.mnuReplace});
            this.mnuSearch.Text = "&Search";
            // 
            // mnuSearchNow
            // 
            this.mnuSearchNow.Index = 0;
            this.mnuSearchNow.Text = "&Search";
            this.mnuSearchNow.Click += new System.EventHandler(this.mnuSearchNow_Click);
            // 
            // mnuSearchFilesInList
            // 
            this.mnuSearchFilesInList.Enabled = false;
            this.mnuSearchFilesInList.Index = 1;
            this.mnuSearchFilesInList.Text = "Search &Files in List";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "-";
            // 
            // mnuReplace
            // 
            this.mnuReplace.Index = 3;
            this.mnuReplace.Text = "&Replace";
            this.mnuReplace.Click += new System.EventHandler(this.mnuReplace_Click);
            // 
            // mnuView
            // 
            this.mnuView.Index = 3;
            this.mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAllMatches,
            this.mnuMatchesInSelectedFile,
            this.mnuSelectedFile,
            this.menuItem7,
            this.mnuPreviousFile,
            this.mnuNextFile,
            this.menuItem11,
            this.mnuPreviousMatch,
            this.mnuNextMatch});
            this.mnuView.Text = "&View";
            this.mnuView.Visible = false;
            // 
            // mnuAllMatches
            // 
            this.mnuAllMatches.Enabled = false;
            this.mnuAllMatches.Index = 0;
            this.mnuAllMatches.Text = "All Matches";
            // 
            // mnuMatchesInSelectedFile
            // 
            this.mnuMatchesInSelectedFile.Enabled = false;
            this.mnuMatchesInSelectedFile.Index = 1;
            this.mnuMatchesInSelectedFile.Text = "Matches in Selected File";
            // 
            // mnuSelectedFile
            // 
            this.mnuSelectedFile.Enabled = false;
            this.mnuSelectedFile.Index = 2;
            this.mnuSelectedFile.Text = "Selected File";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 3;
            this.menuItem7.Text = "-";
            // 
            // mnuPreviousFile
            // 
            this.mnuPreviousFile.Enabled = false;
            this.mnuPreviousFile.Index = 4;
            this.mnuPreviousFile.Text = "Previous File";
            // 
            // mnuNextFile
            // 
            this.mnuNextFile.Enabled = false;
            this.mnuNextFile.Index = 5;
            this.mnuNextFile.Text = "Next File";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 6;
            this.menuItem11.Text = "-";
            // 
            // mnuPreviousMatch
            // 
            this.mnuPreviousMatch.Enabled = false;
            this.mnuPreviousMatch.Index = 7;
            this.mnuPreviousMatch.Text = "Previous Match";
            // 
            // mnuNextMatch
            // 
            this.mnuNextMatch.Enabled = false;
            this.mnuNextMatch.Index = 8;
            this.mnuNextMatch.Text = "Next Match";
            // 
            // mnuOptions
            // 
            this.mnuOptions.Index = 4;
            this.mnuOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuPreferences,
            this.menuItem16,
            this.mnuBeginnerMode,
            this.mnuExpertMode,
            this.mnuRegularExpressions});
            this.mnuOptions.Text = "&Options";
            // 
            // mnuPreferences
            // 
            this.mnuPreferences.Index = 0;
            this.mnuPreferences.Text = "&Preferences";
            this.mnuPreferences.Click += new System.EventHandler(this.mnuPreferences_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 1;
            this.menuItem16.Text = "-";
            this.menuItem16.Visible = false;
            // 
            // mnuBeginnerMode
            // 
            this.mnuBeginnerMode.Enabled = false;
            this.mnuBeginnerMode.Index = 2;
            this.mnuBeginnerMode.Text = "Beginner Mode";
            this.mnuBeginnerMode.Visible = false;
            // 
            // mnuExpertMode
            // 
            this.mnuExpertMode.Enabled = false;
            this.mnuExpertMode.Index = 3;
            this.mnuExpertMode.Text = "Expert Mode";
            this.mnuExpertMode.Visible = false;
            // 
            // mnuRegularExpressions
            // 
            this.mnuRegularExpressions.Index = 4;
            this.mnuRegularExpressions.Text = "Regular Expressions";
            this.mnuRegularExpressions.Click += new System.EventHandler(this.mnuRegularExpressions_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.Index = 5;
            this.mnuWindow.Text = "&Window";
            this.mnuWindow.Visible = false;
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 6;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbout});
            this.mnuHelp.Text = "&Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Index = 0;
            this.mnuAbout.Text = "&About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbOpen,
            this.tbSave,
            this.tbPrint,
            this.toolBarButton1,
            this.tbSearch,
            this.tbReplace,
            this.tbSearchAgain,
            this.toolBarButton2,
            this.tbStop,
            this.toolBarButton3,
            this.tbResults});
            this.toolBar1.ButtonSize = new System.Drawing.Size(16, 16);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.ilToolbar;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(736, 50);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.Wrappable = false;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbOpen
            // 
            this.tbOpen.ImageIndex = 0;
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Text = "Open";
            this.tbOpen.ToolTipText = "Open";
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 1;
            this.tbSave.Name = "tbSave";
            this.tbSave.Text = "Save";
            this.tbSave.ToolTipText = "Save";
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 2;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Text = "Print";
            this.tbPrint.ToolTipText = "Print Results";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbSearch
            // 
            this.tbSearch.DropDownMenu = this.cmSearchType;
            this.tbSearch.ImageIndex = 3;
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tbSearch.Text = "Search";
            this.tbSearch.ToolTipText = "Search";
            // 
            // cmSearchType
            // 
            this.cmSearchType.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.mnuDatabaseSearch});
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "File System";
            // 
            // mnuDatabaseSearch
            // 
            this.mnuDatabaseSearch.Enabled = false;
            this.mnuDatabaseSearch.Index = 1;
            this.mnuDatabaseSearch.Text = "Database";
            this.mnuDatabaseSearch.Click += new System.EventHandler(this.mnuDatabaseSearch_Click);
            // 
            // tbReplace
            // 
            this.tbReplace.ImageIndex = 4;
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Text = "Replace";
            this.tbReplace.ToolTipText = "Replace";
            // 
            // tbSearchAgain
            // 
            this.tbSearchAgain.ImageIndex = 5;
            this.tbSearchAgain.Name = "tbSearchAgain";
            this.tbSearchAgain.Text = "Again";
            this.tbSearchAgain.ToolTipText = "Search Again";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbStop
            // 
            this.tbStop.ImageIndex = 7;
            this.tbStop.Name = "tbStop";
            this.tbStop.Text = "Stop";
            this.tbStop.ToolTipText = "Stop";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbResults
            // 
            this.tbResults.ImageIndex = 16;
            this.tbResults.Name = "tbResults";
            this.tbResults.Text = "Results";
            // 
            // ilToolbar
            // 
            this.ilToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilToolbar.ImageStream")));
            this.ilToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.ilToolbar.Images.SetKeyName(0, "");
            this.ilToolbar.Images.SetKeyName(1, "");
            this.ilToolbar.Images.SetKeyName(2, "");
            this.ilToolbar.Images.SetKeyName(3, "");
            this.ilToolbar.Images.SetKeyName(4, "");
            this.ilToolbar.Images.SetKeyName(5, "");
            this.ilToolbar.Images.SetKeyName(6, "");
            this.ilToolbar.Images.SetKeyName(7, "");
            this.ilToolbar.Images.SetKeyName(8, "");
            this.ilToolbar.Images.SetKeyName(9, "");
            this.ilToolbar.Images.SetKeyName(10, "");
            this.ilToolbar.Images.SetKeyName(11, "");
            this.ilToolbar.Images.SetKeyName(12, "");
            this.ilToolbar.Images.SetKeyName(13, "");
            this.ilToolbar.Images.SetKeyName(14, "");
            this.ilToolbar.Images.SetKeyName(15, "");
            this.ilToolbar.Images.SetKeyName(16, "");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtbResults);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.statusBar1);
            this.panel1.Controls.Add(this.lvFiles);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 319);
            this.panel1.TabIndex = 1;
            // 
            // rtbResults
            // 
            this.rtbResults.ContextMenu = this.cmResults;
            this.rtbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResults.Font = new System.Drawing.Font("Courier New", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbResults.Location = new System.Drawing.Point(0, 243);
            this.rtbResults.Name = "rtbResults";
            this.rtbResults.ReadOnly = true;
            this.rtbResults.Size = new System.Drawing.Size(736, 61);
            this.rtbResults.TabIndex = 6;
            this.rtbResults.Text = "";
            this.rtbResults.WordWrap = false;
            this.rtbResults.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rtbResults_MouseUp);
            // 
            // cmResults
            // 
            this.cmResults.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuViewFile,
            this.mnuViewLine});
            // 
            // mnuViewFile
            // 
            this.mnuViewFile.Index = 0;
            this.mnuViewFile.Text = "View File";
            this.mnuViewFile.Click += new System.EventHandler(this.mnuViewFile_Click);
            // 
            // mnuViewLine
            // 
            this.mnuViewLine.DefaultItem = true;
            this.mnuViewLine.Index = 1;
            this.mnuViewLine.Text = "View Line";
            this.mnuViewLine.Click += new System.EventHandler(this.mnuViewLine_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 240);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(736, 3);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 304);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(736, 15);
            this.statusBar1.TabIndex = 4;
            // 
            // lvFiles
            // 
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chType,
            this.chFolder,
            this.chMatches,
            this.chSize,
            this.chDateTime});
            this.lvFiles.ContextMenu = this.cmFiles;
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvFiles.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(0, 16);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(736, 224);
            this.lvFiles.TabIndex = 1;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
            this.lvFiles.DoubleClick += new System.EventHandler(this.lvFiles_DoubleClick);
            // 
            // chFilename
            // 
            this.chFilename.Text = "Name";
            this.chFilename.Width = 100;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 70;
            // 
            // chFolder
            // 
            this.chFolder.Text = "Folder";
            this.chFolder.Width = 80;
            // 
            // chMatches
            // 
            this.chMatches.Text = "Matches";
            this.chMatches.Width = 70;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.Width = 70;
            // 
            // chDateTime
            // 
            this.chDateTime.Text = "Date/Time";
            this.chDateTime.Width = 150;
            // 
            // cmFiles
            // 
            this.cmFiles.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileOpen,
            this.menuOpenContaingFolder,
            this.menuItem8,
            this.mnuFileProperties});
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.DefaultItem = true;
            this.mnuFileOpen.Index = 0;
            this.mnuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.mnuFileOpen.Text = "Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // menuOpenContaingFolder
            // 
            this.menuOpenContaingFolder.Index = 1;
            this.menuOpenContaingFolder.Text = "Open Containing Folder";
            this.menuOpenContaingFolder.Click += new System.EventHandler(this.menuOpenContaingFolder_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuItem8.Text = "-";
            // 
            // mnuFileProperties
            // 
            this.mnuFileProperties.Index = 3;
            this.mnuFileProperties.Text = "Properties";
            this.mnuFileProperties.Click += new System.EventHandler(this.mnuFileProperties_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(736, 16);
            this.lblInfo.TabIndex = 0;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.ShowIcon = false;
            this.printPreviewDialog1.Visible = false;
            // 
            // tn
            // 
            this.tn.ClientSize = new System.Drawing.Size(160, 34);
            this.tn.ContentText = null;
            this.tn.ControlBox = false;
            this.tn.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.tn.HoverContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(102)))));
            this.tn.HoverContentFont = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tn.HoverTitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tn.HoverTitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.tn.KeepVisibleOnMousOver = true;
            this.tn.Location = new System.Drawing.Point(-32000, -32000);
            this.tn.MaximizeBox = false;
            this.tn.MinimizeBox = false;
            this.tn.Name = "tn";
            this.tn.NormalContentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tn.NormalContentFont = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tn.NormalTitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tn.NormalTitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.tn.ReShowOnMouseOver = false;
            this.tn.ShowInTaskbar = false;
            this.tn.TitleText = null;
            this.tn.TopMost = true;
            this.tn.Visible = false;
            this.tn.CloseClick += new System.EventHandler(this.tn_CloseClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(736, 369);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "frmMain";
            this.Text = "DevGrep";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.Run(new frmMain(args));
        }
        #endregion

        #region EnumFileSystem
        /// <summary>
        /// Scans the file system getting a list of all matching files.
        /// </summary>
        /// <param name="FilePath">Path to files.</param>
        private void EnumFileSystem(string FilePath)
        {
            if (Directory.Exists(FilePath) == true)
            {
                SearchTask st;
                DirectoryInfo di = new DirectoryInfo(FilePath);
                string sep = ";";
                string extensions = this._SearchExtensions;
                char[] sepList = sep.ToCharArray();
                string[] AllExt = extensions.Split(sepList);

                // Loop through everything in this directory.
                try
                {
                    FileSystemInfo[] dummyinfo = di.GetFileSystemInfos();
                    foreach (FileSystemInfo fsi in di.GetFileSystemInfos())
                    {
                        if (doContinue == true)
                        {
                            // Build the destination path
                            //String destName = Path.Combine(dest, fsi.Name);
                            // Only copy if this is an actual file.
                            if (fsi is FileInfo)
                            {
                                try
                                {
                                    Application.DoEvents();
                                    if (ExtIsInList(AllExt, fsi.Extension) == true)
                                    {
                                       
                                        
                                        st = new SearchTask(fsi.FullName, this._SearchText);
                                        if (_ignoreBinary == true )
                                        {
                                            if (!BinFile.isBinary(st.TargetFile))
                                            {
                                                statusBar1.Text = fsi.FullName;
                                                Application.DoEvents();
                                                ProcessFileSearch(st);
                                            }
                                        }
                                        else
                                        {
                                            statusBar1.Text = fsi.FullName;
                                            Application.DoEvents();
                                            //ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFileSearch),st);     
                                            ProcessFileSearch(st);
                                            //HACK statusBar1.Text = "";
                                        }
                                    }
                                }
                                catch (Exception except)
                                {
                                    //Console.WriteLine("Cannot process file: " + fsi.FullName + "->" + except.Message);
                                    //HACK statusBar1.Text = "";
                                }

                            }
                            else
                            {
                                // Create destination directory.
                                //Directory.CreateDirectory(destName);
                                // Recurrsive call to handle contents of this directory.
                                if (this._Subdirectories == true)
                                {
                                    EnumFileSystem(fsi.FullName);
                                }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    //Console.WriteLine("Denied!");
                }


            }
            else
            {
                MessageBox.Show("That directory does not exist.", "Invalid Directory");
            }
            //Console.WriteLine("Total Lines: " + cmTotal.ToString());
            //HACK statusBar1.Text = "";
        }
        #endregion

        #region Form Load
        private void frmMain_Load(object sender, EventArgs e)
        {
            UpdateToolbar(CurrentAction.Nothing);
        }
        #endregion

        #region ExtIsInList
        /// <summary>
        /// Determines if the current extension matches one of the extensions
        /// in our extension array.
        /// </summary>
        /// <param name="AllExtensions">Array of acceptable extensions.</param>
        /// <param name="thisExt">Extension for the current file.</param>
        /// <returns>True if the current extension matches one in the list otherwise
        /// false is returned.</returns>
        private bool ExtIsInList(string[] AllExtensions, string thisExt)
        {
            foreach (string eachExt in AllExtensions)
            {
                if (eachExt == "*.*")
                {
                    return true;
                }
                if (eachExt.Equals(thisExt) == true)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region ProcessFileSearch
        /// <summary>
        /// Reads files with the correct extension in looking for matches.
        /// </summary>
        /// <param name="Parameter">SearchTask object</param>
        private void ProcessFileSearch(object Parameter)
        {
            bool yield = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Yield") == "TRUE" ? true : false;
            MatchLocationCollection mlc = new MatchLocationCollection();
            MatchLocation ml;
            int matchCount = 0;
            int lineCount = 0;
            SearchTask searchTask = (SearchTask)Parameter;

            try
            {
                using (StreamReader sr = new StreamReader(searchTask.TargetFile))
                {
                    //Console.WriteLine("reading file: " + searchTask.TargetFile);
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (yield == true)
                        {
                            Application.DoEvents();
                        }
                        lineCount++;
                        Regex r;
                        if (this._MatchCase == false)
                        {
                            r = new Regex(searchTask.SearchString, RegexOptions.IgnoreCase); // | RegexOptions.IgnorePatternWhitespace);
                        }
                        else
                        {
                            r = new Regex(searchTask.SearchString); //, RegexOptions.IgnorePatternWhitespace);
                        }

                        // Match the regular expression pattern against a text string.
                        Match m = r.Match(line);
                        //Console.WriteLine("*** " + line);
                        while (m.Success)
                        {
                            foreach (Group g in m.Groups)
                            {
                                foreach (Capture c in g.Captures)
                                {
                                    //Console.WriteLine("I found 1! " + c.Index.ToString() + "   " + c.Length.ToString() + "  " + c.Value.ToString());
                                    ml = new MatchLocation(lineCount, c.Index, searchTask.TargetFile, c.Length);
                                    matchCount++;
                                    mlc.Add(ml);
                                    break;
                                }
                                break;
                            }
                            m = m.NextMatch();
                        }
                    }
                    sr.Close();
                    searchTask.MatchesFound = (long)matchCount;
                    searchTask.MatchLocCollection = mlc;
                    if (searchTask.MatchesFound > 0)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = Path.GetFileName(searchTask.TargetFile);
                        FileInfo fi = new FileInfo(searchTask.TargetFile);
                        lvi.SubItems.Add(Win32Registry.ExtensionDescription(fi.Extension));
                        lvi.SubItems.Add(Path.GetDirectoryName(searchTask.TargetFile));
                        lvi.SubItems.Add(searchTask.MatchesFound.ToString());
                        lvi.SubItems.Add(fi.Length.ToString("#,###"));
                        lvi.SubItems.Add(fi.CreationTime.ToString());
                        lvi.Tag = searchTask;
                        lock (lvFiles)
                        {
                            lvFiles.Items.Add(lvi);
                        }
                    }
                }

            }
            catch (IOException)
            {
            }
        }
        #endregion

        #region lvFiles Selected Index Changed
        /// <summary>
        /// ListView Index Change
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 1)
            {
                UpdateResultsContext(true);
                ListViewItem lvi = lvFiles.SelectedItems[0];
                SearchTask st = (SearchTask)lvi.Tag;
                DisplayMatchedLines(st);
            }
            else
            {
                UpdateResultsContext(false);
            }
        }
        #endregion

        #region Toolbar1 Button Click Event
        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Text.ToUpper())
            {
                case "SEARCH":
                    SearchButton_Click();
                    break;
                case "AGAIN":
                    AgainButton_Click();
                    break;
                case "STOP":
                    doContinue = false;
                    break;
                case "VIEW SEARCH RESULTS":
                    ViewSearchResults_Click();
                    break;
                case "REPLACE":
                    Replace_Click();
                    break;
                case "PRINT":
                    Print_Click();
                    break;
                case "SAVE":
                    Save_Click();
                    break;
                case "OPEN":
                    Open_Click();
                    break;
                case "RESULTS":
                    UpdateResultsContext(false);
                    Results_Click();
                    break;
            }
        }

        private void UpdateResultsContext(bool enable)
        {
            foreach (MenuItem mi  in cmResults.MenuItems)
            {
                mi.Enabled = enable;
            }
        }
        #endregion

        #region DisplayMatchedLines
        /// <summary>
        /// Displays all matching lines as Rich Text
        /// </summary>
        /// <param name="searchTask">SearchTask Object</param>
        private void DisplayMatchedLines(SearchTask searchTask)
        {
            int iBefore = 0;
            int iAfter = 0;

            // Read number of lines that should preceed and follow matches.
            string sPre = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Preceeding");
            if (sPre.Length == 0)
            {
                iBefore = 0;
            }
            else
            {
                iBefore = Convert.ToInt32(sPre);
            }

            string sFol = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Following");
            if (sFol.Length == 0)
            {
                iAfter = 0;
            }
            else
            {
                iAfter = Convert.ToInt32(sFol);
            }

            _CurrentFileName = searchTask.TargetFile;

            int lineNumber = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fmodern\fprq1\fcharset0 Courier New;}{\f1\fswiss\fcharset0 Arial;}}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\colortbl ;\red255\green0\blue0;\red255\green0\blue255;}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\*\generator Msftedit 5.41.15.1507;}\viewkind4\uc1\pard");

            #region Create array list of all matched lines plus optional display lines
            ArrayList al = new ArrayList();
            foreach (MatchLocation ml in searchTask.MatchLocCollection)
            {
                int adjLow = ml.Row - iBefore;
                int adjHigh = ml.Row + iAfter;
                if (adjLow < 1)
                {
                    adjLow = 1;
                }
                for (int i = adjLow; i <= adjHigh; i++)
                {
                    if (al.Contains(i) == false)
                    {
                        al.Add(i);
                    }
                }
            }
            #endregion

            foreach (MatchLocation ml  in searchTask.MatchLocCollection)
            {
                lineNumber = 0;
                using (StreamReader sr = new StreamReader(searchTask.TargetFile))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (al.Contains(lineNumber) == true)
                        {
                            al.Remove(lineNumber);
                            sb.Append(@"\cf1\f0\fs16 ");
                            sb.Append(lineNumber.ToString("0####"));
                            sb.Append(@"\cf0      ");
                            string s = line;
                            s = s.Replace(@"\", @"\\").Replace("{", @"\{").Replace(@"}", @"\}");
                            s = HighlightText(s); // s.Replace(this._SearchText,(@"\cf2 " + this._SearchText + @"\cf0 "));
                            s += @"\par" + Environment.NewLine;
                            sb.Append(s.TrimStart());
                            s = null;
                        }
                    }
                }
            }
            sb.Append(@"\f1\par"); //EOL
            sb.Append("}");
            rtbResults.Rtf = sb.ToString();
        }
        #endregion

        #region UpdateToolbar
        /// <summary>
        /// Updates buttons on the toolbar when various events happen.
        /// </summary>
        /// <param name="ca">CurrentAction enumeration</param>
        private void UpdateToolbar(CurrentAction ca)
        {
            switch (ca)
            {
                case CurrentAction.Nothing:
                    toolBar1.Buttons[0].Enabled = true; //Open
                    toolBar1.Buttons[1].Enabled = false; //Save
                    toolBar1.Buttons[2].Enabled = false; //Print
                    toolBar1.Buttons[4].Enabled = true; //Search
                    toolBar1.Buttons[5].Enabled = true; //Replace
                    toolBar1.Buttons[6].Enabled = false; //Again
                    toolBar1.Buttons[8].Enabled = false; //Stop
                    toolBar1.Buttons[10].Enabled = false; //Results
                    //toolBar1.Buttons[11].Enabled = false; //View Matches Only
                    //toolBar1.Buttons[12].Enabled = false; //View whole file
                    break;
                case CurrentAction.Searching:
                    toolBar1.Buttons[0].Enabled = false; //Open
                    toolBar1.Buttons[1].Enabled = false; //Save
                    toolBar1.Buttons[2].Enabled = false; //Print
                    toolBar1.Buttons[4].Enabled = false; //Search
                    toolBar1.Buttons[5].Enabled = false; //Replace
                    toolBar1.Buttons[6].Enabled = false; //Again
                    toolBar1.Buttons[8].Enabled = true; //Stop
                    toolBar1.Buttons[10].Enabled = false; //Results
                    break;
                case CurrentAction.DoneSearchingWithResults:
                    toolBar1.Buttons[0].Enabled = true; //Open
                    toolBar1.Buttons[1].Enabled = true; //Save
                    toolBar1.Buttons[2].Enabled = true; //Print
                    toolBar1.Buttons[4].Enabled = true; //Search
                    toolBar1.Buttons[5].Enabled = true; //Replace
                    toolBar1.Buttons[6].Enabled = true; //Again
                    toolBar1.Buttons[8].Enabled = false; //Stop
                    toolBar1.Buttons[10].Enabled = true; //Results
                    break;
                case CurrentAction.DoneSearchingWithoutResults:
                    toolBar1.Buttons[0].Enabled = true; //Open
                    toolBar1.Buttons[1].Enabled = true; //Save
                    toolBar1.Buttons[2].Enabled = false; //Print
                    toolBar1.Buttons[4].Enabled = true; //Search
                    toolBar1.Buttons[5].Enabled = true; //Replace
                    toolBar1.Buttons[6].Enabled = true; //Again
                    toolBar1.Buttons[8].Enabled = false; //Stop
                    toolBar1.Buttons[10].Enabled = false; //Results
                    break;

            }
        }
        #endregion

        #region frmMain Closing Event
        private void frmMain_Closing(object sender, CancelEventArgs e)
        {
////            try
////            {
////                // Cancel any running threads....
////                doContinue = false;
////                //HACK - prevents the GC process from generating an error when
////                // collecting object from SourceGear vault because it is a managed 
////                // assembly.
////                if (_scc.SourceControlPresent == true)
////                {
////                    _scc.Uninitialize() ;
////                    //Thread.CurrentThread.Abort();
////                }
////            }
////            catch(Exception){}

        }
        #endregion

        #region lvFiles Double Click
        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void lvFiles_DoubleClick(object sender, EventArgs e)
        {
            string ExternalEditor = "";
            string Editor = "";
            Editor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Editor");
            SearchTask st = (SearchTask)lvFiles.SelectedItems[0].Tag;
            LaunchEditorFile( st.TargetFile);
//            if (Editor.ToUpper() == "DEVGREP")
//            {
//                ShellExecute(IntPtr.Zero, null, st.TargetFile, "", "", 4);
//            }
//            else
//            {
//                ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
//                Process.Start(ExternalEditor, st.TargetFile);
//            }
        }
        #endregion

        #region DefaultRegEntries
        /// <summary>
        /// Creates default registry entries if not found.
        /// </summary>
        private void DefaultRegEntries()
        {
            string Editor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Editor");
            if (Editor.Trim().Length == 0)
            {
                DefaultEntries.CreateDefaultSettings();
            }
        }
        #endregion

        #region LaunchEditor
        /// <summary>
        /// Determines what type of editor should be used and launches the editor to view
        /// the file.
        /// </summary>
        /// <param name="FileName">Filename to open</param>
        /// <param name="FileLineNumber">Line number to proceed to</param>
        /// <param name="ColIndex">Column to proceed to.</param>
        private void LaunchEditor(string FileName, int FileLineNumber, int ColIndex)
        {
            string ExternalEditor = "";
            string Editor = "";
            string EdtCmdLine = "";
            Editor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Editor");
            if (Editor.ToUpper() == "DEVGREP")
            {
                try
                {
                    SearchTask st = (SearchTask) lvFiles.SelectedItems[0].Tag;
                    ShellExecute(IntPtr.Zero, null, st.TargetFile, "", "", 4);
                }
                catch(Exception)
                {
                }
                
            }
            if (Editor.ToUpper() == "EXTERNAL")
            {
                ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
                EdtCmdLine = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "CmdLine");
                EdtCmdLine = EdtCmdLine.Replace("%F", FileName);
                EdtCmdLine = EdtCmdLine.Replace("%L", FileLineNumber.ToString());
                EdtCmdLine = EdtCmdLine.Replace("%C", ColIndex.ToString());
                Process.Start(ExternalEditor, EdtCmdLine);
            }

            if (Editor.ToUpper() == "INTERNAL")
            {
                //ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
                DGTe.frmMain fm = new DGTe.frmMain(FileName, FileLineNumber, ColIndex);
                fm.Show();
                //Process.Start(ExternalEditor, FileName);
            }
        }
        #endregion

        #region LaunchEditorFile
        /// <summary>
        /// Launch editor given filename
        /// </summary>
        /// <param name="FileName">File to open</param>
        private void LaunchEditorFile(string FileName)
        {
            string ExternalEditor = "";
            string Editor = "";
            Editor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Editor");
            if (Editor.ToUpper() == "DEVGREP")
            {
                SearchTask st = (SearchTask)lvFiles.SelectedItems[0].Tag;
                ShellExecute(IntPtr.Zero, null, st.TargetFile, "", "", 4);
            }
            if (Editor.ToUpper() == "EXTERNAL")
            {
                ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
                Process.Start(ExternalEditor, FileName);
            }
            if (Editor.ToUpper() == "INTERNAL")
            {
                //ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
                DGTe.frmMain fm = new DGTe.frmMain(FileName);
                fm.Show();
                //Process.Start(ExternalEditor, FileName);
            }
        }
        #endregion

        #region HightlightText
        private string HighlightText(string InString)
        {
            //      s = s.Replace(this._SearchText,(@"\cf2 " + this._SearchText + @"\cf0 "));
            //      return Regex.Replace(InString,this._SearchText,Regex.Escape(@"\cf2 ")+ "$&" + Regex.Escape(@"\cf0 "));
            return Regex.Replace(InString, this._SearchText, @"\cf2 " + "$&" + @"\cf0 ", RegexOptions.IgnoreCase); // | RegexOptions.IgnorePatternWhitespace);
        }
        #endregion

        #region Menu Event Handlers

        #region Menu Print Preview
        /// <summary>
        /// Print Preview menu was clicked
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuPrintPreview_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        #endregion

        #region Menu Print Setup
        /// <summary>
        /// Print Setup was clicked
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuPrintSetup_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }
        #endregion

        #region Menu Print
        /// <summary>
        /// Print was clicked
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuPrint_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
            printDocument1.Print();
        }
        #endregion

        #region Menu About
        /// <summary>
        /// About was clicked
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("by Brian D. Patterson (c) 2005-2023");
        }
        #endregion

        #region Menu Search Now
        /// <summary>
        /// Search now
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuSearchNow_Click(object sender, EventArgs e)
        {
            SearchButton_Click();
        }
        #endregion

        #region Menu Replace
        /// <summary>
        /// Replace was clicked
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuReplace_Click(object sender, EventArgs e)
        {
            Replace_Click();
        }
        #endregion

        #region Menu Exit
        /// <summary>
        /// Exit was clicked
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Menu Open
        /// <summary>
        /// File | Open 
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 1)
            {
                SearchTask st = (SearchTask)lvFiles.SelectedItems[0].Tag;
                LaunchEditorFile(st.TargetFile);
            }
        }
        #endregion

        #region Menu Preferences
        /// <summary>
        /// Preferences
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuPreferences_Click(object sender, EventArgs e)
        {
            frmPreferences fp = new frmPreferences();
            fp.ShowDialog();
        }
        #endregion

        #region Menu View Line
        /// <summary>
        /// View Line
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuViewLine_Click(object sender, EventArgs e)
        {
            int ColIndex;
            //int RowIndex;
            int RowStartIndex;

            //SearchTask st = (SearchTask)lvFiles.SelectedItems[0].Tag;

            // Get RowIndex 
            //RowIndex = rtbResults.GetLineFromCharIndex(rtbResults.SelectionStart) + 1;
            rtbResults.GetCharIndexFromPosition(rtbResults.GetPositionFromCharIndex(rtbResults.SelectionStart));
            // Send message to get Column index.
            RowStartIndex = SendMessage(rtbResults.Handle, EM_LINEINDEX, -1, 0);
            rtbResults.SelectionStart = RowStartIndex;
            rtbResults.SelectionLength = 5;
            int FileLineNumber = 0;
            if (rtbResults.SelectedText.Trim().Length != 0)
            {
                FileLineNumber = Convert.ToInt32(rtbResults.SelectedText);
            }

            rtbResults.SelectionLength = 0;

            // Get Column Index
            ColIndex = rtbResults.SelectionStart - RowStartIndex + 1;
            //MatchLocation ml = st.MatchLocCollection.Item(RowIndex);
            //if (ml != null)
            LaunchEditor(this._CurrentFileName, FileLineNumber, ColIndex);
            //Console.WriteLine("File Line Number:" + FileLineNumber.ToString());
            //Console.WriteLine("File:" + this._CurrentFileName);
        }
        #endregion

        #region Menu View File
        /// <summary>
        /// View File
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuViewFile_Click(object sender, EventArgs e)
        {
            //HACK int ColIndex;
            int RowIndex;
            int RowStartIndex;

            SearchTask st = (SearchTask)lvFiles.SelectedItems[0].Tag;

            // Get RowIndex 
            RowIndex = rtbResults.GetLineFromCharIndex(rtbResults.SelectionStart) + 1;
            rtbResults.GetCharIndexFromPosition(rtbResults.GetPositionFromCharIndex(rtbResults.SelectionStart));
            // Send message to get Column index.
            RowStartIndex = SendMessage(rtbResults.Handle, EM_LINEINDEX, -1, 0);
            rtbResults.SelectionStart = RowStartIndex;
            rtbResults.SelectionLength = 5;
            int FileLineNumber = 0;
            if (rtbResults.SelectedText.Trim().Length != 0)
            {
                FileLineNumber = Convert.ToInt32(rtbResults.SelectedText);
            }

            rtbResults.SelectionLength = 0;

            // Get Column Index
            //HACK ColIndex=rtbResults.SelectionStart-RowStartIndex+1;
            MatchLocation ml = st.MatchLocCollection.Item(RowIndex);
            LaunchEditorFile(ml.FileName);
            Console.WriteLine("File Line Number:" + FileLineNumber.ToString());
            Console.WriteLine("File:" + ml.FileName);
        }
        #endregion

        #region Menu Save Click
        /// <summary>
        /// Save menu item was clicked.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;
            sfd.Filter = "DevGrep files (*.dgb)|*.dgb|All files (*.*)|*.*";
            sfd.FilterIndex = 0;
            sfd.OverwritePrompt = true;
            sfd.Title = "Save DevGrep File";
            sfd.ValidateNames = true;
            sfd.ShowDialog();
            if (sfd.FileName.Length != 0)
            {
                //TODO dsSavedFile dsf = new dsSavedFile();
                //TODO DataRow dr = dsf.Tables[0].NewRow();
                //TODO dr["SearchString"] = this._SearchText;
                //TODO dr["SearchPath"] = this._SearchPath;
                //TODO dr["IncludeSub"] = this._Subdirectories;
                //TODO dr["FileExt"] = this._SearchExtensions;
                //TODO dr["CaseSensative"] = this._MatchCase;
                //TODO dsf.Tables[0].Rows.Add(dr);

                //TODO dsf.WriteXml(sfd.FileName, XmlWriteMode.WriteSchema);
            }
        }
        #endregion

        #region Menu Open
        /// <summary>
        /// Open
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "DevGrep files (*.dgb)|*.dgb|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Title = "Open DevGrep File";
            openFileDialog.ValidateNames = true;
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length != 0)
            {
                OpenFile(openFileDialog.FileName, false);
            }
        }
        #endregion

        #region Menu File Properties
        /// <summary>
        /// Open the property page for this file.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void mnuFileProperties_Click(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count == 1)
            {
                SearchTask st = (SearchTask)lvFiles.SelectedItems[0].Tag;

                SHELLEXECUTEINFO sei = new SHELLEXECUTEINFO();
                sei.cbSize = Marshal.SizeOf(sei);
                sei.lpVerb = "properties";
                sei.fMask = Shell.SEE_MASK_INVOKEIDLIST;
                sei.nShow = 1;

                sei.lpFile = st.TargetFile;
                Shell.ShellExecuteEx(sei);
            }
        }
        #endregion

        #region Menu Open Containing Folder
        /// <summary>
        /// Open containing folder
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void menuOpenContaingFolder_Click(object sender, EventArgs e)
        {
            if (lvFiles.Items.Count > 0)
            {
                string dirName = lvFiles.SelectedItems[0].SubItems[2].Text;
                ShellExecute(IntPtr.Zero, null, dirName, "", "", 4);
            }
        }
        #endregion

        #endregion

        #region OpenFile
        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="fileName">Setting file to open</param>
        /// <param name="AutoSearch">Determines if the search will start automatically</param>
        private void OpenFile(string fileName, bool AutoSearch)
        {
            //TODO dsSavedFile savedFile = new dsSavedFile();
            //TODO savedFile.ReadXml(fileName);
            // Persist to the registry and open the search dialog.
            //TODO string dFileExt = savedFile.Tables[0].Rows[0]["FileExt"].ToString();
            //TODO string dSearchPath = savedFile.Tables[0].Rows[0]["SearchPath"].ToString();
            //TODO string dSearchString = savedFile.Tables[0].Rows[0]["SearchString"].ToString();
            //TODO string dIncludeSub = savedFile.Tables[0].Rows[0]["IncludeSub"].ToString();
            //TODO Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastSearchLocation", dSearchPath);
            //TODO Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastText", dSearchString);
            //TODO Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ExtList", dFileExt);

            //TODO Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Subdirectories", dIncludeSub);
            //TODO if (AutoSearch == false)
            //TODO {
            //TODO  SearchButton_Click();
            //TODO }
            //TODO else
            //TODO {
            //TODO  this._SearchExtensions = dFileExt;
            //TODO this._SearchPath = dSearchPath;
            //TODO this._SearchText = dSearchString;
            //TODO this._Subdirectories = dIncludeSub.ToUpper() == "TRUE" ? true : false;
            //TODO  InitSearch();
            //TODO }
        }
        #endregion

        #region Toolbar Click Event Handlers

        #region Results Click
        private void Results_Click()
        {
            SearchTaskCollection stc = new SearchTaskCollection();
            foreach (ListViewItem lvi in lvFiles.Items)
            {
                SearchTask st = (SearchTask)lvi.Tag;
                stc.Add(st);
            }
            DisplayMatchedSearches(stc);
        }
        #endregion

        #region Toolbar Open Click
        /// <summary>
        /// Open Toolbar Button was clicked
        /// </summary>
        private void Open_Click()
        {
            mnuOpen_Click(this, null);
        }
        #endregion

        #region Print Click
        /// <summary>
        /// Fires when Print toolbar button is clicked.
        /// </summary>
        private void Print_Click()
        {
            if (rtbResults.Text.Trim().Length != 0)
            {
                printDocument1.Print();
            }
        }
        #endregion

        #region Replace Click Handler
        /// <summary>
        /// Click handler does a replace operation.
        /// </summary>
        private void Replace_Click()
        {
            if (lvFiles.Items.Count > 0)
            {
                frmReplace fr = new frmReplace();
                fr.ShowDialog();
                if (fr.DialogResult == DialogResult.OK)
                {
                    _ReplaceOperation = true;
                    foreach (ListViewItem lvi in lvFiles.Items)
                    {
                        SearchTask st = (SearchTask)lvi.Tag;
                        PerformReplace(st.TargetFile, fr.ReplaceText);
                        if (_ReplaceOperation == false)
                        {
                            break;
                        }
                    }
                    lvFiles.Items.Clear();
                    rtbResults.Text = "";
                    MessageBox.Show("Replace operation complete", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You must perform a serach operation and have valid" +
                    Environment.NewLine + "results before you may perform a replace.", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region AgainButton_Click
        /// <summary>
        /// Again Button Event Handler
        /// </summary>
        private void AgainButton_Click()
        {
            lvFiles.Items.Clear();
            lblInfo.Text = "'" + this._SearchText + "' at " + this._SearchPath;
            UpdateToolbar(CurrentAction.Searching);
            DateTime dtStart = DateTime.Now;
            EnumFileSystem(this._SearchPath);
            GC.Collect();
            doContinue = true;

            #region Calc Total Time
            TimeSpan ts = DateTime.Now.Subtract(dtStart);
            lblInfo.Text = "'" + this._SearchText + "' at " + this._SearchPath + "  in " + ts.TotalSeconds.ToString() + " second(s).";
            if (Form.ActiveForm == null)
            {
                string mtch = TotalMatches() == 0 ? "No" : TotalMatches().ToString("#,###");
                tn.Show("DevGrep Complete", "Searched for " + lblInfo.Text + " " + mtch + " matches found.", 750, 15000, 1000);
            }
            #endregion

            if (lvFiles.Items.Count > 0)
            {
                if (Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Autosize").ToUpper() == "TRUE")
                {
                    lvFiles.Columns[0].Width = -1;
                    lvFiles.Columns[1].Width = -1;
                    lvFiles.Columns[4].Width = -1;
                    lvFiles.Columns[5].Width = -1;

                }
                UpdateToolbar(CurrentAction.DoneSearchingWithResults);
            }
            else
            {
                UpdateToolbar(CurrentAction.DoneSearchingWithoutResults);
            }
        }
        #endregion

        #region SearchButton Click
        /// <summary>
        /// Event handler that is fired when the Search toolbar button is
        /// clicked.
        /// </summary>
        private void SearchButton_Click()
        {
            frmSearchAssist fsa = new frmSearchAssist();
            fsa.ShowDialog();
            if (fsa.DialogResult == DialogResult.OK)
            {
                this._SearchExtensions = fsa.ReturnExtensions;
                this._SearchPath = fsa.ReturnPath;
                this._SearchText = fsa.ReturnSearchText;
                this._MatchCase = fsa.MatchCase;
                this._Subdirectories = fsa.Subdirectories;
                this._ignoreBinary = fsa.IgnoreBinary;

                InitSearch();
                statusBar1.Text = "";
            }
        }

        private void InitSearch()
        {
            lvFiles.Items.Clear();
            lblInfo.Text = "'" + this._SearchText + "' at " + this._SearchPath;
            UpdateToolbar(CurrentAction.Searching);
            DateTime dtStart = DateTime.Now;
            EnumFileSystem(this._SearchPath);
            GC.Collect();
            doContinue = true;

            #region Calc Total Time
            TimeSpan ts = DateTime.Now.Subtract(dtStart);
            lblInfo.Text = "'" + this._SearchText + "' at " + this._SearchPath + "  in " + ts.TotalSeconds.ToString() + " second(s).";
            if (Form.ActiveForm == null)
            {
                string mtch = TotalMatches() == 0 ? "No" : TotalMatches().ToString("#,###");
                tn.Show("DevGrep Complete", "Searched for " + lblInfo.Text + " " + mtch + " matches found.", 750, 15000, 1000);
            }
            #endregion

            if (lvFiles.Items.Count > 0)
            {
                if (Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Autosize").ToUpper() == "TRUE")
                {
                    lvFiles.Columns[0].Width = -1;
                    lvFiles.Columns[1].Width = -1;
                    lvFiles.Columns[4].Width = -1;
                    lvFiles.Columns[5].Width = -1;

                }
                UpdateToolbar(CurrentAction.DoneSearchingWithResults);
            }
            else
            {
                UpdateToolbar(CurrentAction.DoneSearchingWithoutResults);
            }
        }
        #endregion

        #region View Search Results Click
        /// <summary>
        /// Fires when the view search results button on the toolbar
        /// is clicked
        /// </summary>
        private void ViewSearchResults_Click()
        {
            if (lvFiles.SelectedItems.Count == 1)
            {
                ListViewItem lvi = lvFiles.SelectedItems[0];
                SearchTask st = (SearchTask)lvi.Tag;
                DisplayMatchedLines(st);
            }
        }
        #endregion

        #region Toolbar Save Click
        /// <summary>
        /// User clicked the Save toolbar option
        /// </summary>
        private void Save_Click()
        {
            mnuSave_Click(this, null);
        }
        #endregion

        #endregion

        #region PerformReplace
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
            using (StreamReader sr = new StreamReader(targetFile))
            {
                tmpFileName = Path.GetTempFileName();
                using (StreamWriter srWrite = new StreamWriter(tmpFileName))
                {
                    //Console.WriteLine("reading file: " + searchTask.TargetFile);
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        #region Create Confirmation
                        StringBuilder sb = new StringBuilder();
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
                            DialogResult dr = MessageBox.Show(sb.ToString(), "Confirm Replacement", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                line = Regex.Replace(line, this._SearchText, replaceText, RegexOptions.IgnoreCase); // | RegexOptions.IgnorePatternWhitespace);
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
                            line = Regex.Replace(line, this._SearchText, replaceText, RegexOptions.IgnoreCase); // | RegexOptions.IgnorePatternWhitespace);
                            srWrite.WriteLine(line);

                        }
                    }
                }
            }
            if (ConfirmFileReplace.ToUpper() == "TRUE")
            {
                #region Build file replacement confirmation
                StringBuilder sb = new StringBuilder();
                sb.Append("Replace file:");
                sb.Append(Environment.NewLine);
                sb.Append(targetFile);
                sb.Append(Environment.NewLine);
                sb.Append("witn the copy containing all replaced text?");
                #endregion

                DialogResult dr = MessageBox.Show(sb.ToString(), "Confirm File Replace", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
        #endregion

        #region Printer Event Handlers

        #region Begin Print
        /// <summary>
        /// Begin Print
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">eventargs</param>
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            // Start at the beginning of the text
            m_nFirstCharOnPage = 0;
        }
        #endregion

        #region End Print
        /// <summary>
        /// EndPrint
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {
            // Clean up cached information
            rtbResults.FormatRangeDone();
        }
        #endregion

        #region Print Page
        /// <summary>
        /// Print Page Handler
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // To print the boundaries of the current page margins
            // uncomment the next line:
            //e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.MarginBounds);
            Pen blackPen = new Pen(Color.Black, 3);

            Font fBold = new Font("Arial", 10, FontStyle.Bold);
            Font fSmall = new Font("Arial", 6, FontStyle.Regular);
            Font f = new Font("Courier New", 6);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            SolidBrush drawBrushB = new SolidBrush(Color.Black);
            SizeF sf = e.Graphics.MeasureString("DevGrep________", fBold, 500);
            e.Graphics.DrawString("DevGrep        ", fBold, drawBrush, e.MarginBounds.X, e.MarginBounds.Y - 40);
            e.Graphics.DrawString("Search: " + _SearchText + " at " + this._SearchPath, fSmall, drawBrush, e.MarginBounds.X + sf.Width, e.MarginBounds.Y - 35);
            e.Graphics.DrawString(this._CurrentFileName, f, drawBrush, e.MarginBounds.X, e.MarginBounds.Y - 20);
            e.Graphics.DrawLine(blackPen, e.MarginBounds.Right, e.MarginBounds.Top, e.MarginBounds.Left, e.MarginBounds.Top);
            e.Graphics.DrawLine(blackPen, e.MarginBounds.Right, e.MarginBounds.Bottom, e.MarginBounds.Left, e.MarginBounds.Bottom);
            e.Graphics.DrawString("DevGrep (c) 2005-2023 Brian D. Patterson", fSmall, drawBrushB, e.MarginBounds.X, e.MarginBounds.Bottom + 20);
            // make the RichTextBoxEx calculate and render as much text as will
            // fit on the page and remember the last character printed for the
            // beginning of the next page
            m_nFirstCharOnPage = rtbResults.FormatRange(false,
                                                        e,
                                                        m_nFirstCharOnPage,
                                                        rtbResults.TextLength);

            // check if there are more pages to print
            if (m_nFirstCharOnPage < rtbResults.TextLength)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
        #endregion

        #endregion

        #region DisplayMatchedSearches
        /// <summary>
        /// Displays the search results within the RichText box.
        /// </summary>
        /// <remarks>These search results are a combination of all files.</remarks>
        /// <param name="searchTaskCollection"></param>
        private void DisplayMatchedSearches(SearchTaskCollection searchTaskCollection)
        {
            int iBefore = 0;
            int iAfter = 0;

            // Read number of lines that should preceed and follow matches.
            string sPre = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Preceeding");
            if (sPre.Length == 0)
            {
                iBefore = 0;
            }
            else
            {
                iBefore = Convert.ToInt32(sPre);
            }

            string sFol = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Following");
            if (sFol.Length == 0)
            {
                iAfter = 0;
            }
            else
            {
                iAfter = Convert.ToInt32(sFol);
            }

            this._CurrentFileName = "Combined Results";
            int lineNumber = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fmodern\fprq1\fcharset0 Courier New;}{\f1\fswiss\fcharset0 Arial;}}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\colortbl ;\red255\green0\blue0;\red255\green0\blue255;}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\*\generator Msftedit 5.41.15.1507;}\viewkind4\uc1\pard");
            foreach (SearchTask searchTask in searchTaskCollection)
            {
                sb.Append(@"\cf1\f0\fs16 ");
                //BUG? sb.Append(Regex.Escape(searchTask.TargetFile));
                sb.Append(searchTask.TargetFile);
                sb.Append(@"\par");

                #region Create array list of all matched lines plus optional display lines
                ArrayList al = new ArrayList();
                foreach (MatchLocation ml in searchTask.MatchLocCollection)
                {
                    int adjLow = ml.Row - iBefore;
                    int adjHigh = ml.Row + iAfter;
                    if (adjLow < 1)
                    {
                        adjLow = 1;
                    }
                    for (int i = adjLow; i <= adjHigh; i++)
                    {
                        if (al.Contains(i) == false)
                        {
                            al.Add(i);
                        }
                    }
                }
                #endregion

                foreach (MatchLocation ml in searchTask.MatchLocCollection)
                {
                    lineNumber = 0;
                    using (StreamReader sr = new StreamReader(searchTask.TargetFile))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNumber++;
                            if (al.Contains(lineNumber) == true)
                            {
                                al.Remove(lineNumber);
                                sb.Append(@"\cf1\f0\fs16 ");
                                sb.Append(lineNumber.ToString("0####"));
                                sb.Append(@"\cf0      ");
                                string s = line;
                                s = s.Replace(@"\", @"\\").Replace("{", @"\{").Replace(@"}", @"\}");
                                s = HighlightText(s);
                                s += @"\par" + Environment.NewLine;
                                sb.Append(s.TrimStart());
                                s = null;

                            }
                        }
                    }
                }
                sb.Append(@"\par");
            }
            sb.Append(@"\f1\par"); //EOL
            sb.Append("}");
            rtbResults.Rtf = sb.ToString();
        }
        #endregion

        private void menuFileCSV_Click(object sender, EventArgs e)
        {
            frmExportFields fef = new frmExportFields();
            fef.ShowDialog();
        }

        private void mnuPrintFileList_Click(object sender, EventArgs e)
        {
            string origResults = rtbResults.Text;
            rtbResults.Text = "Name                                          Type                Matches Size        " + Environment.NewLine;
            rtbResults.Text += " Folder" + Environment.NewLine;
            rtbResults.Text += "-------------------------------------------------------------------------------------------------" + Environment.NewLine;
            //Name 20
            //Type 19
            //Folder 23
            //Matches 7
            //Size 11
            //Date/Time 24
            foreach (ListViewItem lvi in lvFiles.Items)
            {
                rtbResults.Text += MakeSize(lvi.SubItems[0].Text, 45) + " ";
                rtbResults.Text += MakeSize(lvi.SubItems[1].Text, 19) + " ";
                rtbResults.Text += MakeSize(lvi.SubItems[3].Text, 7) + " ";
                rtbResults.Text += MakeSize(lvi.SubItems[4].Text, 11) + " ";
                rtbResults.Text += Environment.NewLine;
                rtbResults.Text += " " + MakeSize(lvi.SubItems[2].Text, 95) + " ";
                rtbResults.Text += Environment.NewLine;
            }
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            rtbResults.Text = origResults;
        }

        public string MakeSize(string InString, int Length)
        {
            if (InString.Length > Length)
            {
                return InString.Substring(0, Length - 2) + "..";
            }
            if (InString.Length < Length)
            {
                return InString.PadRight(Length, (char)32);
            }
            return InString;
        }

        private void tn_CloseClick(object sender, EventArgs e)
        {
            tn.Hide();
        }

        #region TotalMatches
        /// <summary>
        /// Calculates the total number of matches found in the recent search.
        /// </summary>
        /// <returns>Match total.</returns>
        private int TotalMatches()
        {
            int counter = 0;
            foreach (ListViewItem lvi in this.lvFiles.Items)
            {
                if (lvi.SubItems[3].Text.Trim().Length != 0)
                {
                    counter += Convert.ToInt32(lvi.SubItems[3].Text);
                }
            }
            return counter;
        }
        #endregion

        #region ContentClick
        /// <summary>
        /// Content click display the devgrep form when the user clicks
        /// the content of the taskbar notifier window.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void tn_ContentClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.Focus();

        }
        #endregion

        private void rtbResults_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this._CurrentFileName != "Combined Results")
            {
                long interval = DateTime.Now.Ticks - _lastClick; 
                _lastClick = DateTime.Now.Ticks; 


                if(interval < TimeSpan.TicksPerSecond / 4) //one quarter of a second 
                {
                    mnuViewLine_Click(this,null); 
                }

            }

        }

        private void mnuRegularExpressions_Click(object sender, System.EventArgs e)
        {
            Forms.frmRegExBuild reb = new frmRegExBuild() ;
            reb.ShowDialog(); 
        }

        private void mnuDatabaseSearch_Click(object sender, System.EventArgs e)
        {
        Forms.frmDBSeartchAssist dbsa = new frmDBSeartchAssist() ;
            dbsa.ShowDialog(); 
        }

        private void menuFileHTML_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void menuFileRTF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void menuFileText_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void menuCompiledCSV_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void menuCompiledHTML_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void menuCompiledRTF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void menuCompiledText_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO");
        }
    }
}