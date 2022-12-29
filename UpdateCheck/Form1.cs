using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdateCheck.Properties;

namespace UpdateCheck
{
    public partial class Form1 : Form
    {
        private const string UPDATE_MESSAGE =
            "An update for DevGrep is available. Click the 'Update Now' button to begin the update process. If you wish to wait, you can always install updates at a later time by selecting 'Check for Updates' from the Help menu within DevGrep.\nYour Version: {0}\nLatest Version: {1}";
        private const string DOWNLOADURL = "http://www.borderlinesoftware.com/Software/Installs/DevGrep/";
        private const string VERSION_FILENAME = "DEVGREP.txt";
        private string THIS_VERSION = "";

        public const string updaterPrefix = "M1234_";
        private static string processToEnd = "DevGrep";
        private static string postProcess = Application.StartupPath + @"\" + processToEnd + ".exe";
        public static string updater = Application.StartupPath + @"\DevGrep.exe";

        public const string updateSuccess = "DevGrep has been successfully updated";
        public const string updateCurrent = "No updates available for DevGrep";
        public const string updateInfoError = "Error in retrieving DevGrep information";

        public static List<string> info = new List<string>();
        private string[] _args;
        private bool _verbose;

        public Form1(string[] args)
        {
            _args = args;
            InitializeComponent();
        }

        internal bool AreUpdatesAvailable(bool overrideVerbose)
        {
            if (!InitValues())
            {
                return false;
            }
            else
            {
                if (overrideVerbose)
                {
                    _verbose = false;
                }
                bool updatesAvailable = StartCheckForUpdate();
                if (!updatesAvailable)
                    return false;
                return true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!InitValues())
            {
                if (Program._DEBUG)
                {
                    Program.LogUpdateCheck("InitValues returned false");
                }
                Application.Exit();
            }
            else
            {
                if (Program._DEBUG)
                {
                    Program.LogUpdateCheck("StartCheckForUpdate");
                }
                bool updatesAvailable = StartCheckForUpdate();
                if (!updatesAvailable)
                {
                    if (Program._DEBUG)
                    {
                        Program.LogUpdateCheck("StartCheckForUpdate returned false");
                    }
                    Application.Exit();
                }
                    
            }
        }
        private bool InitValues()
        {
            if (Program._DEBUG)
            {
                string cmdArgs = "";
                if (_args != null && _args.Length > 0)
                {
                    foreach (string s in _args)
                    {
                        cmdArgs += s+", ";
                    }
                    Program.LogUpdateCheck("Args: "+cmdArgs);
                }
                
            }

            if (_args.Length > 0)
            {
                // Validate this is major, minor and build. If not - drop the revision and move on
                THIS_VERSION = _args[0];

                foreach (string s in _args)
                {
                    switch (s.ToUpper())
                    {
                        case "-V":
                            _verbose = true;
                            break;
                    }
                }

                return true;
            }
            return false;
        }
        private void BeginUpdate()
        {
            //Updater.DownloadUnzipExtractRun();
            Updater.InstallUpdateRestart(info[3], info[4], "\"" + Application.StartupPath + "\\", processToEnd, postProcess, "updated",Path.Combine(Application.StartupPath,info[4]));
            this.Cursor = Cursors.Default;
            Close();
        }

        //private void unpackCommandline()
        //{

        //    bool commandPresent = false;
        //    string tempStr = "";

        //    foreach (string arg in Environment.GetCommandLineArgs())
        //    {

        //        if (!commandPresent)
        //        {

        //            commandPresent = arg.Trim().StartsWith("/");

        //        }

        //        if (commandPresent)
        //        {

        //            tempStr += arg;

        //        }

        //    }


        //    if (commandPresent)
        //    {

        //        if (tempStr.Remove(0, 2) == "updated")
        //        {

        //            //updateResult.Visible = true;
        //            //updateResult.Text = updateSuccess;

        //        }

        //    }


        //}

        private bool StartCheckForUpdate()
        {
            if (Program._DEBUG)
            {
                Program.LogUpdateCheck("  StartCheckForUpdate: DownloadURL->"+DOWNLOADURL);
                Program.LogUpdateCheck("  StartCheckForUpdate: VERSION_FILENAME->" + VERSION_FILENAME);
                Program.LogUpdateCheck("  StartCheckForUpdate: StartupPath->" + Application.StartupPath);
            }
            info = Updater.GetUpdateInfo(DOWNLOADURL ,VERSION_FILENAME , Application.StartupPath + @"\", 1);

            if (info == null)
            {
                if (Program._DEBUG)
                {
                    Program.LogUpdateCheck("StartCheckForUpdate returned null from download.");
                }
                if (_verbose)
                    MessageBox.Show("DevGrep is unable to check for updates at this time. Please ensure" +
                               Environment.NewLine + "you are connected to the internet.");
                //Update_bttn.Visible = false;
                //updateResult.Text = updateInfoError;
                //updateResult.Visible = true;
                return false;
            }
            else
            {
                Version remoteVersion = Version.Parse(info[1]);
                Version thisVersion = Version.Parse(THIS_VERSION);
                if (remoteVersion > thisVersion)
                {
                    label1.Text = string.Format(UPDATE_MESSAGE, thisVersion.ToString(), remoteVersion.ToString());
                    label1.Visible = true;
                    this.Text = "Update Available";
                    btnUpdateNow.Visible = true;
                    return true;
                    //MessageBox.Show("Update found");

                }
                else
                {
                    if (Program._DEBUG)
                    {
                        Program.LogUpdateCheck("StartCheckForUpdate-> No updates available.");
                    }
                    //TODO Only show this on verbose switch
                    if (_verbose)
                        MessageBox.Show(Resources.Form1_StartCheckForUpdate_);
                    //Update_bttn.Visible = false;
                    //updateResult.Visible = true;
                    //updateResult.Text = updateCurrent;
                    return false;
                }



            }
        }

        private void btnUpdateNow_Click(object sender, EventArgs e)
        {
            btnUpdateNow.Enabled = false;
            btnLater.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            label1.Text = "Downloading new files for DevGrep. Please wait a few moments...";
            Application.DoEvents();
            BeginUpdate();
        }

        private void btnLater_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
