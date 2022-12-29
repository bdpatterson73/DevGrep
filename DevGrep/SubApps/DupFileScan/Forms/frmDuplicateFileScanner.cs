using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevGrep.Classes.Config;
using DevGrep.Forms;
using DevGrep.SubApps.DupFileScan.Classes;
using DevGrep.SubApps.DupFileScan.Classes.Prefs;

namespace DevGrep.SubApps.DupFileScan.Forms
{
    public partial class frmDuplicateFileScanner : Form
    {
        private int _totalFilesFound = 0;
        private int _totalFoldersFound = 0;
        private DuplicateFileScanPrefs _scanPrefs;
        internal enum CurrentAction
        {
            BeginFileScan,
            BuildingDuplicateList,
            EndScanWithResults,
            EndScanNoresults
        }

        public frmDuplicateFileScanner()
        {
            InitializeComponent();
            _scanPrefs = new DuplicateFileScanPrefs();
        }

        private void frmDuplicateFileScanner_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            //TODO Load and save settings in the registry
            InitExtensions();
            rbCompareSingle.Checked = true;
            cmbComparisonType.Items.Add("General");
            cmbComparisonType.SelectedIndex = 0;
            textBox1.Text = _scanPrefs.LastDuplicateScan;
            textBox2.Text = _scanPrefs.LastDuplicateScanComparison;
        }

        private void InitExtensions()
        {
            SearchExtensions ses = SearchExtensions.Load();
            foreach (SearchExtension se in ses)
            {
                cmbExtensions.Items.Add(se);
            }
            cmbExtensions.SelectedIndex = 0;
        }

        private void SetupScanType()
        {
            if (rbCompareSingle.Checked)
            {
                label2.Visible = false;
                textBox2.Visible = false;
                btnBrowseComparison.Visible = false;
            }
            if (rbCompareTwo.Checked)
            {
                label2.Visible = true;
                textBox2.Visible = true;
                btnBrowseComparison.Visible = true;
            }
        }

        private void rbCompareSingle_CheckedChanged(object sender, EventArgs e)
        {
            SetupScanType();
        }

        private void rbCompareTwo_CheckedChanged(object sender, EventArgs e)
        {
            SetupScanType();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a Folder";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        private void btnBrowseComparison_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a Folder";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //TODO Validate everything is filled in...

            // Save our preferences to the registry.
            SavePreferences();

            lvResults.Items.Clear();
            if (rbCompareSingle.Checked)
                CompareOneDirectory();
            if (rbCompareTwo.Checked)
            {
                MessageBox.Show("This is currently still under development.");
            }
        }

        private void SavePreferences()
        {
// Save preferences for next time.
            _scanPrefs.LastDuplicateScan = textBox1.Text.Trim();
            _scanPrefs.LastDuplicateScanComparison = textBox2.Text.Trim();
        }

        private void CompareOneDirectory()
        {
            SetCurrentAction(CurrentAction.BeginFileScan, "");
            Application.DoEvents();
            ScanForFiles sff = new ScanForFiles();
            sff.IsRecursive = true;
            sff.GenerateHash = true;
            sff.HashType = ScanForFiles.HashTypes.SHA1;
            SearchExtension selectedExtensions = cmbExtensions.SelectedItem as SearchExtension;
            if (selectedExtensions != null)
            {
                sff.ExtensionList = selectedExtensions.ExtensionList;
            }
            else
            {
                if (cmbExtensions.Text != null)
                {
                    sff.ExtensionList = cmbExtensions.Text;   
                }
                else
                {
                    MessageBox.Show("You must provide at least one file extension to search for.");
                    SetCurrentAction(CurrentAction.EndScanNoresults, "");
                    return;
                }
            }
            
            sff.RootDir = textBox1.Text;
            sff.OnFileFound += sff_OnFileFound;
            sff.OnDirectoryFound += sff_OnDirectoryFound;
            sff.Scan();

            Hashtable ht = new Hashtable();
            SetCurrentAction(CurrentAction.BuildingDuplicateList, sff.FilesList.Count.ToString());
            Application.DoEvents();
            foreach (IScanForFilesData blah in sff.FilesList)
            {
                if (blah.FileHashValue != null)
                {
                    if (ht.ContainsKey(blah.FileHashValue))
                        ht[blah.FileHashValue] = (int) ht[blah.FileHashValue] + 1;
                    else
                    {
                        ht.Add(blah.FileHashValue, 1);
                    }
                }
            }
            int ttlDuplicateGroups = 0;
            int ttlDupFiles = 0;
            foreach (DictionaryEntry pair in ht)
            {
                if ((int) pair.Value > 1)
                {
                    ttlDupFiles += (int) pair.Value;
                    ttlDuplicateGroups++;
                    ListViewGroup lvg = new ListViewGroup(pair.Key.ToString(), "Duplicate " + ttlDuplicateGroups);
                    lvResults.Groups.Add(lvg);
                    List<IScanForFilesData> dups = GetAllFilesByHash(pair.Key.ToString(), sff.FilesList);
                    foreach (IScanForFilesData dup in dups)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = dup.FileInfo.Name;
                        lvi.SubItems.Add(dup.FileInfo.DirectoryName);
                        lvi.SubItems.Add(dup.FileInfo.CreationTime.ToString());
                        lvi.SubItems.Add(dup.FileInfo.Length.ToString());
                        lvi.Group = lvg;
                        lvi.Tag = dup.FileInfo;
                        lvResults.Items.Add(lvi);
                    }


                    //Console.WriteLine("{0}={1}", pair.Key, pair.Value);
                }
            }
            if (lvResults.Items.Count > 0)
            {
                SetCurrentAction(CurrentAction.EndScanWithResults, ttlDupFiles.ToString());
                MarkOldFilesInEachGroup();
            }
                
            else
            {
                SetCurrentAction(CurrentAction.EndScanNoresults, "");
            }

        }

        void sff_OnDirectoryFound(string directoryName)
        {
            _totalFoldersFound++;
            string stats = string.Format("Scanning... Files: {0}  Folders: {1}", _totalFilesFound, _totalFoldersFound);
            statusStrip1.InvokeIfRequired(c => { tsStatusLabel.Text = stats; });
        }

        void sff_OnFileFound(IScanForFilesData data)
        {
            _totalFilesFound++;
            string stats = string.Format("Scanning... Files: {0}  Folders: {1}", _totalFilesFound, _totalFoldersFound);
            statusStrip1.InvokeIfRequired(c => { tsStatusLabel.Text = stats; });
        }

        private List<IScanForFilesData> GetAllFilesByHash(string hashNumber, ScanResultsList results)
        {
            List<IScanForFilesData> toReturn = new List<IScanForFilesData>();
            foreach (IScanForFilesData blah in results)
            {
                if (blah.FileHashValue != null)
                {
                    if (blah.FileHashValue == hashNumber)
                    {
                        toReturn.Add(blah);
                    }
                }
            }
            return toReturn;
        }

        private void SetCurrentAction(CurrentAction action, string message)
        {
            switch (action)
            {
                case CurrentAction.BeginFileScan:
                    _totalFilesFound = 0;
                    _totalFoldersFound = 0;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    btnBrowseSource.Enabled = false;
                    btnBrowseComparison.Enabled = false;
                    btnStart.Enabled = false;
                    rbCompareSingle.Enabled = false;
                    rbCompareTwo.Enabled = false;
                    cmbComparisonType.Enabled = false;
                    cmbExtensions.Enabled = false;
                    pictureBox1.Enabled = false;
                    btnDeleteSelected.Enabled = false;
                    btnClose.Enabled = false;
                    statusStrip1.InvokeIfRequired(c => { tsStatusLabel.Text = "Scanning files..."; });
                    //tsStatusLabel.Text = "Scanning files...";
                    this.Cursor = Cursors.WaitCursor;
                    break;
                case CurrentAction.BuildingDuplicateList:
                    statusStrip1.InvokeIfRequired(c => { tsStatusLabel.Text = "Found "+ message + " file(s). Comparing files..."; });
                   
                    break;
                case CurrentAction.EndScanNoresults:
                    textBox1.Enabled = true;
                    btnDeleteSelected.Enabled = false;
                    textBox2.Enabled = true;
                    btnBrowseSource.Enabled = true;
                    btnBrowseComparison.Enabled = true;
                    btnStart.Enabled = true;
                    rbCompareSingle.Enabled = true;
                    rbCompareTwo.Enabled = true;
                    cmbComparisonType.Enabled = true;
                    cmbExtensions.Enabled = true;
                    pictureBox1.Enabled = true;
                    btnClose.Enabled = true;
                    statusStrip1.InvokeIfRequired(c => { tsStatusLabel.Text = "Scan complete. No duplicates found."; });
                    this.Cursor = Cursors.Default;
                    break;
                case CurrentAction.EndScanWithResults:
                   textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    btnBrowseSource.Enabled = true;
                    btnBrowseComparison.Enabled = true;
                    btnStart.Enabled = true;
                    rbCompareSingle.Enabled = true;
                    rbCompareTwo.Enabled = true;
                    cmbComparisonType.Enabled = true;
                    cmbExtensions.Enabled = true;
                    pictureBox1.Enabled = true;
                    btnDeleteSelected.Enabled = true;
                    btnClose.Enabled = true;
                    statusStrip1.InvokeIfRequired(c => { tsStatusLabel.Text = "Scan complete. " + message + " duplicate(s) found."; });
                    this.Cursor = Cursors.Default;
                    break;
            }
            Application.DoEvents();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

           
            //Launch Help Window
            dlgHelp help = new dlgHelp(DevGrep.Properties.Resources.DuplicateExtensionTitle,
                                       DevGrepResources.Resources.GetResourceString( "DevGrepResources.HelpFiles.Extensions.rtf"),true);
            help.ShowDialog();
        }

        private void MarkOldFilesInEachGroup()
        {
            foreach (ListViewGroup lvg in lvResults.Groups)
            {
                DateTime dt = DateTime.MinValue;
                foreach (ListViewItem lvi in lvg.Items)
                {
                    if (Convert.ToDateTime(lvi.SubItems[2].Text) > dt)
                        dt = Convert.ToDateTime(lvi.SubItems[2].Text);
                }
                foreach (ListViewItem lvi in lvg.Items)
                {
                    if (Convert.ToDateTime(lvi.SubItems[2].Text) != dt)
                        lvi.Checked = true;
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            dlgHelp help = new dlgHelp(DevGrep.Properties.Resources.ComparisonTypeTitle,
                                      DevGrepResources.Resources.GetResourceString("DevGrepResources.HelpFiles.ComparisonTypes.rtf"), true);
            help.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            //TODO Confirm delete.
            //TODO Check for readonly files.
            bool areReadOnly = AreFilesReadOnly();
            if (areReadOnly)
            {
               DialogResult dr =  MessageBox.Show(
                    "Some files are set as 'Read-Only'. Would you like to" + Environment.NewLine +
                    "delete these files as well?", "ReadOnly Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ClearReadOnly();
                }
            }
            foreach (ListViewGroup lvg in lvResults.Groups)
            {
                DateTime dt = DateTime.MinValue;
                foreach (ListViewItem lvi in lvg.Items)
                {
                    if (lvi.Checked)
                    {
                        FileInfo fi = lvi.Tag as FileInfo;
                        if (fi != null)
                        {
                            try
                            {
                                File.Delete(fi.FullName);
                            }
                            catch (Exception)
                            {
                                
                               
                            }
                            
                        }
                    }
                }
               
            }

            MessageBox.Show("All selected files have been deleted.");
        }

        private void ClearReadOnly()
        {
            foreach (ListViewGroup lvg in lvResults.Groups)
            {

                foreach (ListViewItem lvi in lvg.Items)
                {
                    if (lvi.Checked)
                    {
                        FileInfo fi = lvi.Tag as FileInfo;
                        if (fi != null)
                        {
                            FileAttributes attributes = fi.Attributes;
                            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            {
                                try
                                {
                                    File.SetAttributes(fi.FullName, FileAttributes.Normal);
                                }
                                catch (Exception)
                                {
                                    
                                   
                                }
                            }
                        }
                    }
                }

            }
        }

        private bool AreFilesReadOnly()
        {
            bool toReturn = false;
            foreach (ListViewGroup lvg in lvResults.Groups)
            {
            
                foreach (ListViewItem lvi in lvg.Items)
                {
                    if (lvi.Checked)
                    {
                        FileInfo fi = lvi.Tag as FileInfo;
                        if (fi != null)
                        {
                            FileAttributes attributes = fi.Attributes;
                             if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                             {
                                 toReturn = true;
                             }      
                        }
                    }
                }
               
            }
            return toReturn;
        }

       
    }
}