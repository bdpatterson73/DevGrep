// ***********************************************************************
// Assembly         : DevGrep
// Author           : Brian
// Created          : 04-16-2013
//
// Last Modified By : Brian
// Last Modified On : 04-16-2013
// ***********************************************************************
// <copyright file="formMainSearch.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLS.Search.Web;
using BLS.Search.Web.Hosts;
using DevGrep.Classes;
using DevGrep.Classes.Entity;
using DevGrep.Classes.FileExport;
using DevGrep.Classes.Misc;
using DevGrep.Controls.DLV;
using DevGrep.DataSets;
using DevGrep.Forms;
using DevGrep.SubApps.DupFileScan.Forms;
using SmartAssembly.ReportUsage;

namespace DevGrep
{
    /// <summary>
    /// Class formMainSearch
    /// </summary>
    public partial class formMainSearch : RibbonForm
    {
        private string[] _args;
        private PersistWindowState m_windowState;
        private TaskbarNotifier tn;
        private const int EM_LINEINDEX = 0xBB;
        private bool doContinue = true;
        private string _CurrentFileName = "";
        private long _lastClick;
        private bool _MatchCase = false;
        private bool _ReplaceOperation = true;
        private string _SearchExtensions = "";
        private string _SearchPath = "";
        private string _SearchText = "";
        private bool _Subdirectories = true;
        private int m_nFirstCharOnPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="formMainSearch"/> class.
        /// </summary>
        public formMainSearch(string[] args)
        {
            //List<WebSearchResult> myResults = GoogleSearch.Search("brian d patterson", null);
            _args = args;
            // Check Default Registry Settings
            DefaultRegEntries();
            Application.EnableVisualStyles();
            Application.DoEvents();
            m_windowState = new PersistWindowState();
            m_windowState.Parent = this;
            // set registry path in HKEY_CURRENT_USER
            m_windowState.RegistryPath = @"Software\DevGrep\Main\";
            InitializeComponent();
            Setup();
            //InitSavedTheme();
            //GenerateThemeXmlFile();
        }

        /// <summary>
        /// Handles the Click event of the roobQuit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void roobQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReportUserUsage()
        {
            string regOrg = "";
            string regOwner = "";
            string a = "";
            string b = "";
            string c = "";

            try
            {
                regOrg = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "");
            }
            catch (Exception) { }

            try
            {
                regOwner = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOwner", "");
            }
            catch (Exception) { }
            try
            {
                a = Environment.UserDomainName;
            }
            catch (Exception) { }
            try
            {
                b = Environment.MachineName;
            }
            catch (Exception) { }
            try
            {
                c = Environment.UserName;
            }
            catch (Exception) { }
            string UsageString = regOrg + "|" + regOwner + "|" + a + "|" + b + "|" + c;
            if (!IsInDesignMode)
                UsageCounter.ReportUsage("User:" + UsageString);
        }

        public static bool IsInDesignMode
        {
            get
            {
                bool isInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime || Debugger.IsAttached == true;

                if (!isInDesignMode)
                {
                    using (var process = Process.GetCurrentProcess())
                    {
                        return process.ProcessName.ToLowerInvariant().Contains("devenv");
                    }
                }

                return isInDesignMode;
            }
        }

        /// <summary>
        /// Handles the Load event of the formMainSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void formMainSearch_Load(object sender, EventArgs e)
        {
            ReportUserUsage();

            UpdateToolbar(CurrentAction.Nothing);
            ////if (Program.dgl != null && Program.dgl.IsLicensed)
            ////{
            ////    menuRegister.Visible = false;
            ////    menuLicense.Visible = true;
            ////}
            ////else
            ////{
            ////    menuRegister.Visible = true;
            ////    menuLicense.Visible = false;
            ////}


            ////CheckTrialInformation();

            ////this.Show();
            ////Application.DoEvents();
            ////// Check for command line parameters
            ////ParseCmdLine(_args);
            ////UpdateCheck(false);
        }

        /// <summary>
        /// Generates the theme XML file.
        /// </summary>
        private void GenerateThemeXmlFile()
        {
            var ct = ((RibbonProfessionalRenderer)ribbon1.Renderer).ColorTable;
            string content = ct.WriteThemeXmlFile();
            System.IO.File.WriteAllText(@".\Themes\WinDefault.xml", content);
        }

        /// <summary>
        /// Inits the saved theme.
        /// </summary>
        private void InitSavedTheme()
        {
           

    string content = System.IO.File.ReadAllText(@".\Themes\SilverBlack.xml");
    ((RibbonProfessionalRenderer)ribbon1.Renderer).ColorTable.ReadThemeXmlFile(content);
    ribbon1.Refresh();

        }

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


        private void Setup()
        {
            this.tn = new DevGrep.Classes.TaskbarNotifier();
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
            tn.SetBackgroundBitmap(new Bitmap(GetType(), "skin3.bmp"), Color.FromArgb(255, 0, 255));
            tn.SetCloseBitmap(new Bitmap(GetType(), "close.bmp"), Color.FromArgb(255, 0, 255), new Point(218, 10));
            tn.TitleRectangle = new Rectangle(13, 12, 200, 28);
            tn.ContentRectangle = new Rectangle(13, 47, 215, 55);
            tn.ContentClick += new EventHandler(tn_ContentClick);
        }

        private void tn_ContentClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            Focus();
        }

        private void tn_CloseClick(object sender, EventArgs e)
        {
            tn.Hide();
        }

        private void rbSearchFileSystem_Click(object sender, EventArgs e)
        {
            OpenSearchAssist(null);
        }

        private void rbuttonSearch_Click(object sender, EventArgs e)
        {
            rbSearchFileSystem_Click(this, null);
        }

        private void OpenSearchAssist(string searchLocation)
        {
            frmSearchAssist fsa;
            if (!string.IsNullOrEmpty(searchLocation))
                fsa = new frmSearchAssist(searchLocation);
            else
            {
                fsa = new frmSearchAssist();
            }
            fsa.ShowDialog();
            if (fsa.DialogResult == DialogResult.OK)
            {
                _SearchExtensions = fsa.ReturnExtensions;
                _SearchPath = fsa.ReturnPath;
                _SearchText = fsa.ReturnSearchText;
                _MatchCase = fsa.MatchCase;
                _Subdirectories = fsa.Subdirectories;

                InitSearch();
                statusLabel.Text = "";
            }
        }

        private void InitSearch()
        {
            objectListView1.Items.Clear();
            lblInfo.Text = "'" + _SearchText + "' at " + _SearchPath;
            UpdateToolbar(CurrentAction.Searching);
            DateTime dtStart = DateTime.Now;
            EnumFileSystem(_SearchPath);
            GC.Collect();
            doContinue = true;

            #region Calc Total Time

            TimeSpan ts = DateTime.Now.Subtract(dtStart);
            lblInfo.Text = "'" + _SearchText + "' at " + _SearchPath + "  in " + ts.TotalSeconds.ToString() +
                           " second(s).";
            if (ActiveForm == null)
            {
                string mtch = TotalMatches() == 0 ? "No" : TotalMatches().ToString("#,###");
                tn.Show("DevGrep Complete", "Searched for " + lblInfo.Text + " " + mtch + " matches found.", 750, 15000,
                        1000);
            }

            #endregion

            if (objectListView1.Items.Count > 0)
            {
               
                UpdateToolbar(CurrentAction.DoneSearchingWithResults);
                objectListView1.AutoResizeColumns();
            }
            else
            {
                UpdateToolbar(CurrentAction.DoneSearchingWithoutResults);
            }
        }

        /// <summary>
        /// Updates buttons on the toolbar when various events happen.
        /// </summary>
        /// <param name="ca">CurrentAction enumeration</param>
        private void UpdateToolbar(CurrentAction ca)
        {
            switch (ca)
            {
                case CurrentAction.Nothing:
                    rbPriOpenSavedSearch.Enabled = true; //Open
                    rbPriSaveCurrentSearch.Enabled = false; //Save
                    rbPriPrintResults.Enabled = false; //Print
                    dmiPrintFileList.Enabled = false;
                    dmiPrint.Enabled = false;
                    dmiPrintPreview.Enabled = false;
                    dmiPrintSetup.Enabled = true;
                    rbuttonSearch.Enabled = true; //Search
                    rbReplace.Enabled = false; //Replace
                    rbAgain.Enabled = false; //Again
                   rbStop.Enabled = false; //Stop
                    rbCombine.Enabled = false; //Results
                    rbuttonVisualize.Enabled = false; //Visualize
                    rdmiToCSV.Enabled = false;
                    rdmiToHTML.Enabled = false;
                    rbCompare.Enabled = false;
                    //menuFileRTF.Enabled = false;
                    //menuFileText.Enabled = false;
                    //menuCompiledCSV.Enabled = false;
                    //menuCompiledHTML.Enabled = false;
                    //menuCompiledRTF.Enabled = false;
                    //menuCompiledText.Enabled = false;
                    //toolBar1.Buttons[11].Enabled = false; //View Matches Only
                    //toolBar1.Buttons[12].Enabled = false; //View whole file
                    break;
                case CurrentAction.Searching:
                    rbPriOpenSavedSearch.Enabled = false; //Open
                    rbPriSaveCurrentSearch.Enabled = false; //Save
                    rbPriPrintResults.Enabled = false; //Print
                    dmiPrintFileList.Enabled = false;
                    dmiPrint.Enabled = false;
                    dmiPrintPreview.Enabled = false;
                    dmiPrintSetup.Enabled = true;
                    rbuttonSearch.Enabled = false; //Search
                    rbReplace.Enabled = false; //Replace
                    rbAgain.Enabled = false; //Again
                   rbStop.Enabled = true; //Stop
                    rbCombine.Enabled = false; //Results
                    rbuttonVisualize.Enabled = false; //Visualize
                    rdmiToCSV.Enabled = false;
                    rdmiToHTML.Enabled = false;
                    rbCompare.Enabled = false;
                   // menuFileRTF.Enabled = false;
                    //menuFileText.Enabled = false;
                    //menuCompiledCSV.Enabled = false;
                    //menuCompiledHTML.Enabled = false;
                    //menuCompiledRTF.Enabled = false;
                    //menuCompiledText.Enabled = false;
                    break;
                case CurrentAction.DoneSearchingWithResults:
                    rbPriOpenSavedSearch.Enabled = true; //Open
                    rbPriSaveCurrentSearch.Enabled = true; //Save
                    rbPriPrintResults.Enabled = true; //Print
                    dmiPrintFileList.Enabled = true;
                    dmiPrint.Enabled = true;
                    dmiPrintPreview.Enabled = true;
                    dmiPrintSetup.Enabled = true;
                    rbuttonSearch.Enabled = true; //Search
                    rbReplace.Enabled = true; //Replace
                    rbAgain.Enabled = true; //Again
                    rbStop.Enabled = false; //Stop
                    rbCombine.Enabled = true; //Results
                    rbuttonVisualize.Enabled = true; //Visualize
                    rdmiToCSV.Enabled = true;
                    rdmiToHTML.Enabled = true;
                    rbCompare.Enabled = true;
                    //menuFileRTF.Enabled = false; //TODO true
                   // menuFileText.Enabled = false; //TODO true
                   // menuCompiledCSV.Enabled = false; //TODO true
                    //menuCompiledHTML.Enabled = false; //TODO true
                    //menuCompiledRTF.Enabled = false;//TODO true
                    //menuCompiledText.Enabled = false;//TODO true
                    break;
                case CurrentAction.DoneSearchingWithoutResults:
                    rbPriOpenSavedSearch.Enabled = true; //Open
                    rbPriSaveCurrentSearch.Enabled = true; //Save
                    rbPriPrintResults.Enabled = false; //Print
                    dmiPrintFileList.Enabled = false;
                    dmiPrint.Enabled = false;
                    dmiPrintPreview.Enabled = false;
                    dmiPrintSetup.Enabled = true;
                    rbuttonSearch.Enabled = true; //Search
                    rbReplace.Enabled = false; //Replace
                    rbAgain.Enabled = true; //Again
                   rbStop.Enabled = false; //Stop
                    rbCombine.Enabled = false; //Results
                    rbuttonVisualize.Enabled = false; //Visualize
                    rdmiToCSV.Enabled = false;
                    rdmiToHTML.Enabled = false;
                    rbCompare.Enabled = true;
                    //menuFileRTF.Enabled = false;
                   // menuFileText.Enabled = false;
                    //menuCompiledCSV.Enabled = false;
                    //menuCompiledHTML.Enabled = false;
                   // menuCompiledRTF.Enabled = false;
                   // menuCompiledText.Enabled = false;
                    break;
            }
        }

        private int TotalMatches()
        {
            //TODO REVIEW FOR OLV
            int counter = 0;
            foreach (OLVListItem lvi in objectListView1.Items)
            {
                if (lvi.SubItems[3].Text.Trim().Length != 0)
                {
                    counter += Convert.ToInt32(lvi.SubItems[3].Text);
                }
            }
            return counter;
        }

        /// <summary>
        /// Scans the file system getting a list of all matching files.
        /// </summary>
        /// <param name="FilePath">Path to files.</param>
        private void EnumFileSystem(string FilePath)
        {
            if (Directory.Exists(FilePath) == true)
            {
                SearchTask st;
                var di = new DirectoryInfo(FilePath);
                string sep = ";";
                string extensions = _SearchExtensions;
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
                                        statusLabel.Text = fsi.FullName;
                                        Application.DoEvents();
                                        st = new SearchTask(fsi.FullName, _SearchText);
                                        //ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFileSearch),st);     
                                        ProcessFileSearch(st);
                                        //HACK statusBar1.Text = "";
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
                                if (_Subdirectories == true)
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

        private void ProcessFileSearch(object Parameter)
        {
            //TODO REVIEW FOR OLV
            bool wasLimited = false;
            bool yield = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Yield") == "TRUE" ? true : false;
            var mlc = new MatchLocationCollection();
            MatchLocation ml;
            int matchCount = 0;
            int lineCount = 0;
            var searchTask = (SearchTask)Parameter;

            try
            {
                using (var sr = new StreamReader(searchTask.TargetFile))
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
                        if (_MatchCase == false)
                        {
                            r = new Regex(searchTask.SearchString, RegexOptions.IgnoreCase);
                            // | RegexOptions.IgnorePatternWhitespace);
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
                        var fi = new FileInfo(searchTask.TargetFile);
                        SearchTaskDisplay std = new SearchTaskDisplay(Path.GetFileName(searchTask.TargetFile),Win32Registry.ExtensionDescription(fi.Extension),Path.GetDirectoryName(searchTask.TargetFile),searchTask.MatchesFound,fi.Length,fi.CreationTime,searchTask);
                        //var lvi = new ListViewItem();
                        //lvi.Text = Path.GetFileName(searchTask.TargetFile);
                        
                        //lvi.SubItems.Add(Win32Registry.ExtensionDescription(fi.Extension));
                        //lvi.SubItems.Add(Path.GetDirectoryName(searchTask.TargetFile));
                        //lvi.SubItems.Add(searchTask.MatchesFound.ToString());
                        //lvi.SubItems.Add(fi.Length.ToString("#,###"));
                        //lvi.SubItems.Add(fi.CreationTime.ToString());
                        //lvi.Tag = searchTask;
                        lock (objectListView1)
                        {
                            //TODO if (Program.dgl.IsLicensed)
                            objectListView1.AddObject(std);
                            
                            //TODO else
                            //TODO {
                            //TODO     if (lvFiles.Items.Count < 100)
                            //TODO     {
                            //TODO         lvFiles.Items.Add(lvi);
                            //TODO         wasLimited = true;
                            //TODO     }

                            //TODO }
                        }
                    }
                }
            }
            catch (IOException)
            {
            }
            //MessageBox.Show("This trial version of DevGrep is limited to 100 file matches." + Environment.NewLine +
            //                "Some results have be omitted.");
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItems.Count == 1)
            {
                var dcs = objectListView1.SelectedItem.RowObject as SearchTaskDisplay;
                if (dcs != null)
                {
                    var st = (SearchTask) dcs.Tag;
                    LaunchEditorFile(st.TargetFile);
                }
            }
        }

        private void tsmiOpenFolder_Click(object sender, EventArgs e)
        {
            if (objectListView1.Items.Count > 0)
            {
                var dcs = objectListView1.SelectedItem.RowObject as SearchTaskDisplay;
                if (dcs != null)
                {
                   
                    Win32API.ShellExecute(IntPtr.Zero, null, dcs.DirName, "", "", 4);
                }
            }
        }

        private void tsmiProperties_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItems.Count == 1)
            {
                 var dcs = objectListView1.SelectedItem.RowObject as SearchTaskDisplay;
                if (dcs != null)
                {
                    var st = (SearchTask) dcs.Tag;

                    var sei = new SHELLEXECUTEINFO();
                    sei.cbSize = Marshal.SizeOf(sei);
                    sei.lpVerb = "properties";
                    sei.fMask = Shell.SEE_MASK_INVOKEIDLIST;
                    sei.nShow = 1;

                    sei.lpFile = st.TargetFile;
                    Shell.ShellExecuteEx(sei);
                }
            }
        }

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
                //var st = (SearchTask)objectListView1.SelectedItems[0].Tag;
                Win32API.ShellExecute(IntPtr.Zero, null, FileName, "", "", 4);
            }
            if (Editor.ToUpper() == "EXTERNAL")
            {
                ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
                Process.Start(ExternalEditor, FileName);
            }
            if (Editor.ToUpper() == "INTERNAL")
            {
                //ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
                var fm = new DGTe.frmMain(FileName);
                fm.Show();
                //Process.Start(ExternalEditor, FileName);
            }
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItems.Count == 1)
            {
                UpdateResultsContext(true);
                  var dcs = objectListView1.SelectedItem.RowObject as SearchTaskDisplay;
                if (dcs != null)
                {
                   
                    var st = (SearchTask) dcs.Tag;
                    DisplayMatchedLines(st);
                }
            }
            else
            {
                UpdateResultsContext(false);
            }
        }

        private void UpdateResultsContext(bool enable)
        {
            foreach (ToolStripItem mi in cmResults.Items)
            {
                mi.Enabled = enable;
            }
        }


        private string HighlightText(string InString)
        {
            //      s = s.Replace(this._SearchText,(@"\cf2 " + this._SearchText + @"\cf0 "));
            //      return Regex.Replace(InString,this._SearchText,Regex.Escape(@"\cf2 ")+ "$&" + Regex.Escape(@"\cf0 "));
            return Regex.Replace(InString, _SearchText, @"\cf2 " + "$&" + @"\cf0 ", RegexOptions.IgnoreCase);
            // | RegexOptions.IgnorePatternWhitespace);
        }

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
            var sb = new StringBuilder();
            sb.Append(
                @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fmodern\fprq1\fcharset0 Courier New;}{\f1\fswiss\fcharset0 Arial;}}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\colortbl ;\red255\green0\blue0;\red255\green0\blue255;}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\*\generator Msftedit 5.41.15.1507;}\viewkind4\uc1\pard");

            #region Create array list of all matched lines plus optional display lines

            var al = new ArrayList();
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
                using (var sr = new StreamReader(searchTask.TargetFile))
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
                            // s.Replace(this._SearchText,(@"\cf2 " + this._SearchText + @"\cf0 "));
                            s += @"\par" + Environment.NewLine;
                            sb.Append(s.TrimStart());
                            s = null;
                        }
                    }
                }
            }
            sb.Append(@"\f1\par"); //EOL
            sb.Append("}");
            richTextBoxEx1.Rtf = sb.ToString();
        }

        private void tsmiViewFile_Click(object sender, EventArgs e)
        {
            //HACK int ColIndex;
            int RowIndex;
            int RowStartIndex;
              var dcs = objectListView1.SelectedItem.RowObject as SearchTaskDisplay;
            if (dcs != null)
            {
                var st = (SearchTask) dcs.Tag;

                // Get RowIndex 
                RowIndex = richTextBoxEx1.GetLineFromCharIndex(richTextBoxEx1.SelectionStart) + 1;
                richTextBoxEx1.GetCharIndexFromPosition(
                    richTextBoxEx1.GetPositionFromCharIndex(richTextBoxEx1.SelectionStart));
                // Send message to get Column index.
                RowStartIndex = Win32API.SendMessage(richTextBoxEx1.Handle, EM_LINEINDEX, -1, 0);
                richTextBoxEx1.SelectionStart = RowStartIndex;
                richTextBoxEx1.SelectionLength = 5;
                int FileLineNumber = 0;
                if (richTextBoxEx1.SelectedText.Trim().Length != 0)
                {
                    FileLineNumber = Convert.ToInt32(richTextBoxEx1.SelectedText);
                }

                richTextBoxEx1.SelectionLength = 0;

                // Get Column Index
                //HACK ColIndex=rtbResults.SelectionStart-RowStartIndex+1;
                MatchLocation ml = st.MatchLocCollection.Item(RowIndex - 1);
                LaunchEditorFile(ml.FileName);
                //Console.WriteLine("File Line Number:" + FileLineNumber.ToString());
                //Console.WriteLine("File:" + ml.FileName);
            }
        }

        private void tsmiViewLine_Click(object sender, EventArgs e)
        {
            int ColIndex;
            //int RowIndex;
            int RowStartIndex;

            //SearchTask st = (SearchTask)lvFiles.SelectedItems[0].Tag;

            // Get RowIndex 
            //RowIndex = rtbResults.GetLineFromCharIndex(rtbResults.SelectionStart) + 1;
            richTextBoxEx1.GetCharIndexFromPosition(richTextBoxEx1.GetPositionFromCharIndex(richTextBoxEx1.SelectionStart));
            // Send message to get Column index.
            RowStartIndex = Win32API.SendMessage(richTextBoxEx1.Handle, EM_LINEINDEX, -1, 0);
            richTextBoxEx1.SelectionStart = RowStartIndex;
            richTextBoxEx1.SelectionLength = 5;
            int FileLineNumber = 0;
            if (richTextBoxEx1.SelectedText.Trim().Length != 0)
            {
                FileLineNumber = Convert.ToInt32(richTextBoxEx1.SelectedText);
            }

            richTextBoxEx1.SelectionLength = 0;

            // Get Column Index
            ColIndex = richTextBoxEx1.SelectionStart - RowStartIndex + 1;
            //MatchLocation ml = st.MatchLocCollection.Item(RowIndex);
            //if (ml != null)
            LaunchEditor(_CurrentFileName, FileLineNumber, ColIndex);
            //Console.WriteLine("File Line Number:" + FileLineNumber.ToString());
            //Console.WriteLine("File:" + this._CurrentFileName);
        }

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
                      var dcs = objectListView1.SelectedItem.RowObject as SearchTaskDisplay;
                    if (dcs != null)
                    {
                        var st = (SearchTask) dcs.Tag;
                        Win32API.ShellExecute(IntPtr.Zero, null, st.TargetFile, "", "", 4);
                    }
                }
                catch (Exception)
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
                var fm = new DGTe.frmMain(FileName, FileLineNumber, ColIndex);
                fm.Show();
                //Process.Start(ExternalEditor, FileName);
            }
        }

        private void rbStop_Click(object sender, EventArgs e)
        {
            doContinue = false;
        }

        private void rbReplace_Click(object sender, EventArgs e)
        {
            if (objectListView1.Items.Count > 0)
            {
                var fr = new frmReplace();
                fr.ShowDialog();
                if (fr.DialogResult == DialogResult.OK)
                {
                    _ReplaceOperation = true;
                    foreach (OLVListItem lvi in objectListView1.Items)
                    {
                        var dcs = lvi.RowObject as SearchTaskDisplay;
                        var st = (SearchTask)dcs.Tag;
                        PerformReplace(st.TargetFile, fr.ReplaceText);
                        if (_ReplaceOperation == false)
                        {
                            break;
                        }
                    }
                    objectListView1.Items.Clear();
                    richTextBoxEx1.Text = "";
                    MessageBox.Show("Replace operation complete", "Done", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You must perform a serach operation and have valid" +
                                Environment.NewLine + "results before you may perform a replace.", "Try Again",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void rbAgain_Click(object sender, EventArgs e)
        {
            InitSearch();
        }

        private void rbPriOpenSavedSearch_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
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

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="fileName">Setting file to open</param>
        /// <param name="AutoSearch">Determines if the search will start automatically</param>
        private void OpenFile(string fileName, bool AutoSearch)
        {
            var savedFile = new dsSavedFile();
            savedFile.ReadXml(fileName);
            // Persist to the registry and open the search dialog.
            string dFileExt = savedFile.Tables[0].Rows[0]["FileExt"].ToString();
            string dSearchPath = savedFile.Tables[0].Rows[0]["SearchPath"].ToString();
            string dSearchString = savedFile.Tables[0].Rows[0]["SearchString"].ToString();
            string dIncludeSub = savedFile.Tables[0].Rows[0]["IncludeSub"].ToString();
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastSearchLocation", dSearchPath);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastText", dSearchString);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ExtList", dFileExt);

            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Subdirectories", dIncludeSub);
            if (AutoSearch == false)
            {
                OpenSearchAssist(null);
            }
            else
            {
                _SearchExtensions = dFileExt;
                _SearchPath = dSearchPath;
                _SearchText = dSearchString;
                _Subdirectories = dIncludeSub.ToUpper() == "TRUE" ? true : false;
                InitSearch();
            }
        }

        private void rbPriSaveCurrentSearch_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
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
                var dsf = new dsSavedFile();
                DataRow dr = dsf.Tables[0].NewRow();
                dr["SearchString"] = _SearchText;
                dr["SearchPath"] = _SearchPath;
                dr["IncludeSub"] = _Subdirectories;
                dr["FileExt"] = _SearchExtensions;
                dr["CaseSensative"] = _MatchCase;
                dsf.Tables[0].Rows.Add(dr);

                dsf.WriteXml(sfd.FileName, XmlWriteMode.WriteSchema);
            }
        }

        private void rbPriPrintResults_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim().Length != 0)
            {
                printDocument1.Print();
            }
        }

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
            richTextBoxEx1.FormatRangeDone();
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
            var blackPen = new Pen(Color.Black, 3);

            var fBold = new Font("Arial", 10, FontStyle.Bold);
            var fSmall = new Font("Arial", 6, FontStyle.Regular);
            var f = new Font("Courier New", 6);
            var drawBrush = new SolidBrush(Color.Blue);
            var drawBrushB = new SolidBrush(Color.Black);
            SizeF sf = e.Graphics.MeasureString("DevGrep________", fBold, 500);
            e.Graphics.DrawString("DevGrep        ", fBold, drawBrush, e.MarginBounds.X, e.MarginBounds.Y - 40);
            e.Graphics.DrawString("Search: " + _SearchText + " at " + _SearchPath, fSmall, drawBrush,
                                  e.MarginBounds.X + sf.Width, e.MarginBounds.Y - 35);
            e.Graphics.DrawString(_CurrentFileName, f, drawBrush, e.MarginBounds.X, e.MarginBounds.Y - 20);
            e.Graphics.DrawLine(blackPen, e.MarginBounds.Right, e.MarginBounds.Top, e.MarginBounds.Left,
                                e.MarginBounds.Top);
            e.Graphics.DrawLine(blackPen, e.MarginBounds.Right, e.MarginBounds.Bottom, e.MarginBounds.Left,
                                e.MarginBounds.Bottom);
            e.Graphics.DrawString("DevGrep (c) 2005-2013 Brian D. Patterson", fSmall, drawBrushB, e.MarginBounds.X,
                                  e.MarginBounds.Bottom + 20);
            // make the RichTextBoxEx calculate and render as much text as will
            // fit on the page and remember the last character printed for the
            // beginning of the next page
            m_nFirstCharOnPage = richTextBoxEx1.FormatRange(false,
                                                        e,
                                                        m_nFirstCharOnPage,
                                                        richTextBoxEx1.TextLength);

            // check if there are more pages to print
            if (m_nFirstCharOnPage < richTextBoxEx1.TextLength)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        #endregion

        private void dmiPrint_Click(object sender, EventArgs e)
        {
            rbPriPrintResults_Click(this, null);
        }

        #endregion

        private void dmiPrintPreview_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void dmiPrintSetup_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void dmiPrintFileList_Click(object sender, EventArgs e)
        {
            string origResults = richTextBoxEx1.Text;
            richTextBoxEx1.Text = "Name                                          Type                             Matches Size        " +
                              Environment.NewLine;
            richTextBoxEx1.Text += " Folder" + Environment.NewLine;
            richTextBoxEx1.Text +=
                "-------------------------------------------------------------------------------------------------" +
                Environment.NewLine;
            //Name 20
            //Type 19
            //Folder 23
            //Matches 7
            //Size 11
            //Date/Time 24
            foreach (ListViewItem lvi in objectListView1.Items)
            {
                richTextBoxEx1.Text += MakeSize(lvi.SubItems[0].Text, 45) + " ";
                richTextBoxEx1.Text += MakeSize(lvi.SubItems[1].Text, 32) + " ";
                richTextBoxEx1.Text += MakeSize(lvi.SubItems[3].Text, 7) + " ";
                richTextBoxEx1.Text += MakeSize(lvi.SubItems[4].Text, 11) + " ";
                richTextBoxEx1.Text += Environment.NewLine;
                richTextBoxEx1.Text += " " + MakeSize(lvi.SubItems[2].Text, 95) + " ";
                richTextBoxEx1.Text += Environment.NewLine;
            }
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            richTextBoxEx1.Text = origResults;
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

        private void rbCombine_Click(object sender, EventArgs e)
        {
            UpdateResultsContext(false);
            Results_Click();
        }

        private void Results_Click()
        {
            var stc = BuildSearchTaskCollection();
            DisplayMatchedSearches(stc);
        }

        private SearchTaskCollection BuildSearchTaskCollection()
        {
            var stc = new SearchTaskCollection();
            foreach (OLVListItem lvi in objectListView1.Items)
            {
                var dcs = lvi.RowObject as SearchTaskDisplay;
                var st = (SearchTask)dcs.Tag;
                stc.Add(st);
            }
            return stc;
        }

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

            _CurrentFileName = "Combined Results";
            int lineNumber = 0;
            var sb = new StringBuilder();
            sb.Append(
                @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fmodern\fprq1\fcharset0 Courier New;}{\f1\fswiss\fcharset0 Arial;}}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\colortbl ;\red255\green0\blue0;\red255\green0\blue255;}");
            sb.Append(Environment.NewLine);
            sb.Append(@"{\*\generator Msftedit 5.41.15.1507;}\viewkind4\uc1\pard");
            foreach (SearchTask searchTask in searchTaskCollection)
            {
                sb.Append(@"\cf1\f0\fs16 ");
                //BUG? sb.Append(Regex.Escape(searchTask.TargetFile));
                sb.Append(searchTask.TargetFile.Replace(@"\", @"\\"));
                sb.Append(@"\par");

                #region Create array list of all matched lines plus optional display lines

                var al = new ArrayList();
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
                    using (var sr = new StreamReader(searchTask.TargetFile))
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
            richTextBoxEx1.Rtf = sb.ToString();
        }

        private void rbuttonVisualize_Click(object sender, EventArgs e)
        {
            VisualizeResults();
        }

        [ReportUsage("Visualize")]
        private void VisualizeResults()
        {
            if (!Program.dgl.IsLicensed)
            {
                if (
                    !Program.FeatureLicensedMsg("Visualizer",
                                                Program.featureStorage.VisualizerRuns))
                    return;
                Program.featureStorage.ReportVisualizerRun();
            }
            var stc = BuildSearchTaskCollection();
            frmVisualizer vis = new frmVisualizer(stc);
            vis.ShowDialog();

        }

        private void rbOpenEditor_Click(object sender, EventArgs e)
        {
            var fm = new DGTe.frmMain();
            fm.Show();
        }

        private void rbScanDuplicates_Click(object sender, EventArgs e)
        {
            if (!Program.dgl.IsLicensed)
            {
                if (
                    !Program.FeatureLicensedMsg("Duplicate File Scanner",
                                                Program.featureStorage.DuplicateFileScannerRuns))
                    return;
                Program.featureStorage.ReportDuplicateFileScannerRun();
            }

            frmDuplicateFileScanner dupForm = new frmDuplicateFileScanner();
            dupForm.ShowDialog();
        }

        private void rdmiToCSV_Click(object sender, EventArgs e)
        {
            var fef = new frmExportFields();
            if (fef.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.CheckFileExists = false;
                sfd.CheckPathExists = true;
                sfd.CreatePrompt = false;
                sfd.DereferenceLinks = false;
                sfd.OverwritePrompt = true;
                sfd.RestoreDirectory = false;
                sfd.ValidateNames = true;
                sfd.DefaultExt = ".csv";
                sfd.InitialDirectory = "C:\\";
                sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "Save Export File";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var stc = BuildSearchTaskCollection();
                    string regValues = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "FileCSVLayout");
                    FileExportCSV exportCSV = new FileExportCSV(sfd.FileName, stc, regValues);
                    exportCSV.CreateOutputfile();
                    MessageBox.Show("Results saved successfully.");
                }

            }
        }

        private void rdmiToHTML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckFileExists = false;
            sfd.CheckPathExists = true;
            sfd.CreatePrompt = false;
            sfd.DereferenceLinks = false;
            sfd.OverwritePrompt = true;
            sfd.RestoreDirectory = false;
            sfd.ValidateNames = true;
            sfd.DefaultExt = ".html";
            sfd.InitialDirectory = "C:\\";
            sfd.Filter = "html files (*.html)|*.html|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.Title = "Save Export File";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var stc = BuildSearchTaskCollection();
                long tms = TotalMatchStrings(stc);
                int tmf = TotalMatchFiles(stc);
                string searchCriteria = "'" + _SearchText + "' at " + _SearchPath + ". " + tms + " matches in " + tmf + " files.";
                FileExportHTML exportCSV = new FileExportHTML(sfd.FileName, stc, searchCriteria);
                exportCSV.CreateOutputfile();
                MessageBox.Show("Results saved successfully.");
            }
        }

        private int TotalMatchFiles(SearchTaskCollection stc)
        {
            return stc.Count;
        }

        private long TotalMatchStrings(SearchTaskCollection stc)
        {
            long ttlCount = 0;
            foreach (SearchTask st in stc)
            {
                ttlCount += st.MatchesFound;
            }
            return ttlCount;
        }

        private void rudPreLines_DownButtonClicked(object sender, MouseEventArgs e)
        {

        }

        private void rudPreLines_UpButtonClicked(object sender, MouseEventArgs e)
        {

        }

        private void rudPostLines_DownButtonClicked(object sender, MouseEventArgs e)
        {

        }

        private void rudPostLines_UpButtonClicked(object sender, MouseEventArgs e)
        {

        }

         [ReportUsage("Duplicate Scanner")]
        private void rbDuplicates_Click(object sender, EventArgs e)
        {
            if (!Program.dgl.IsLicensed)
            {
                if (
                    !Program.FeatureLicensedMsg("Duplicate File Scanner",
                                                Program.featureStorage.DuplicateFileScannerRuns))
                    return;
                Program.featureStorage.ReportDuplicateFileScannerRun();
            }

            frmDuplicateFileScanner dupForm = new frmDuplicateFileScanner();
            dupForm.ShowDialog();
        }
    }
}
