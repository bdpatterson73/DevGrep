using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using DevGrep.Classes;

namespace DevGrep.Forms
{
    /// <summary>
    /// Summary description for frmSearchAssist.
    /// </summary>
    public class frmSearchAssist : Form
    {
        private string _ReturnSearchText = "";
        private string _ReturnPath = "";
        private string _ReturnExtensions = "";
        private bool _IncludeSubdirectories;
        private GroupBox groupBox1;
        private TextBox txtSearchText;
        private GroupBox groupBox2;
        private RadioButton radioButton1;
        private CheckBox checkBox2;
        private GroupBox groupBox3;
        private TextBox txtSearchLocation;
        private Button btnBrowse;
        private GroupBox groupBox4;
        private ListBox lbFileTypes;
        private ListBox lbSelectedTypes;
        private Button btnOK;
        private Button btnCancel;
        private TextBox txtAdd;
        private Button btnAdd;
        private Button btnSelectExt;
        private Button btnDeselectExt;
        private ContextMenu cmSelectedTypes;
        private MenuItem mnuVS;
        private MenuItem mnuVisualStudio6;
        private MenuItem mnuVisualStudioNet;
        private MenuItem mnuDocuments;
        private MenuItem mnuScripts;
        private CheckBox cbMatchCase;
        private MenuItem menuItem1;
        private MenuItem mnuClear;
        private ContextMenu cmRegEx;
        private MenuItem mnuREEmailAddress;
        private MenuItem mnuREIPAddress;
        private MenuItem mnuREURL;
        private ToolTip toolTip1;
        private MenuItem mnuWeb;
        private CheckBox cbSubdirectories;
        private MenuItem mnuQuotedText;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuUserDefined;
        private System.Windows.Forms.MenuItem mnuHTMLTags;
        private System.Windows.Forms.MenuItem mnuDirective;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem mnuComment;
        private System.Windows.Forms.MenuItem mnuInteger;
        private System.Windows.Forms.MenuItem mnuHexValue;
        private System.Windows.Forms.RadioButton rbText;
        private MenuItem mnuPython;
        private CheckBox cbIgnoreBinaryFiles;
        private Button btnOpenBinaryExtenList;
        private IContainer components;

        public frmSearchAssist()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchAssist));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSearchText = new System.Windows.Forms.TextBox();
            this.cmRegEx = new System.Windows.Forms.ContextMenu();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mnuComment = new System.Windows.Forms.MenuItem();
            this.mnuDirective = new System.Windows.Forms.MenuItem();
            this.mnuHexValue = new System.Windows.Forms.MenuItem();
            this.mnuREEmailAddress = new System.Windows.Forms.MenuItem();
            this.mnuHTMLTags = new System.Windows.Forms.MenuItem();
            this.mnuInteger = new System.Windows.Forms.MenuItem();
            this.mnuREIPAddress = new System.Windows.Forms.MenuItem();
            this.mnuQuotedText = new System.Windows.Forms.MenuItem();
            this.mnuREURL = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuUserDefined = new System.Windows.Forms.MenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.cbMatchCase = new System.Windows.Forms.CheckBox();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnOpenBinaryExtenList = new System.Windows.Forms.Button();
            this.cbIgnoreBinaryFiles = new System.Windows.Forms.CheckBox();
            this.cbSubdirectories = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSearchLocation = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDeselectExt = new System.Windows.Forms.Button();
            this.btnSelectExt = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAdd = new System.Windows.Forms.TextBox();
            this.lbSelectedTypes = new System.Windows.Forms.ListBox();
            this.cmSelectedTypes = new System.Windows.Forms.ContextMenu();
            this.mnuDocuments = new System.Windows.Forms.MenuItem();
            this.mnuVS = new System.Windows.Forms.MenuItem();
            this.mnuVisualStudio6 = new System.Windows.Forms.MenuItem();
            this.mnuVisualStudioNet = new System.Windows.Forms.MenuItem();
            this.mnuScripts = new System.Windows.Forms.MenuItem();
            this.mnuWeb = new System.Windows.Forms.MenuItem();
            this.mnuPython = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuClear = new System.Windows.Forms.MenuItem();
            this.lbFileTypes = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSearchText);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Text";
            // 
            // txtSearchText
            // 
            this.txtSearchText.ContextMenu = this.cmRegEx;
            this.txtSearchText.Location = new System.Drawing.Point(8, 16);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(336, 20);
            this.txtSearchText.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtSearchText, "Right click for more options.");
            // 
            // cmRegEx
            // 
            this.cmRegEx.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.mnuREEmailAddress,
            this.mnuHTMLTags,
            this.mnuInteger,
            this.mnuREIPAddress,
            this.mnuQuotedText,
            this.mnuREURL,
            this.menuItem2,
            this.mnuUserDefined});
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuComment,
            this.mnuDirective,
            this.mnuHexValue});
            this.menuItem3.Text = "C#/C++";
            // 
            // mnuComment
            // 
            this.mnuComment.Index = 0;
            this.mnuComment.Text = "Comment";
            this.mnuComment.Click += new System.EventHandler(this.mnuComment_Click);
            // 
            // mnuDirective
            // 
            this.mnuDirective.Index = 1;
            this.mnuDirective.Text = "Directive";
            this.mnuDirective.Click += new System.EventHandler(this.mnuDirective_Click);
            // 
            // mnuHexValue
            // 
            this.mnuHexValue.Index = 2;
            this.mnuHexValue.Text = "Hex Value";
            this.mnuHexValue.Click += new System.EventHandler(this.mnuHexValue_Click);
            // 
            // mnuREEmailAddress
            // 
            this.mnuREEmailAddress.Index = 1;
            this.mnuREEmailAddress.Text = "EMail Address";
            this.mnuREEmailAddress.Click += new System.EventHandler(this.mnuREEmailAddress_Click);
            // 
            // mnuHTMLTags
            // 
            this.mnuHTMLTags.Index = 2;
            this.mnuHTMLTags.Text = "HTML Tags";
            this.mnuHTMLTags.Click += new System.EventHandler(this.mnuHTMLTags_Click);
            // 
            // mnuInteger
            // 
            this.mnuInteger.Index = 3;
            this.mnuInteger.Text = "Integer";
            this.mnuInteger.Click += new System.EventHandler(this.mnuInteger_Click);
            // 
            // mnuREIPAddress
            // 
            this.mnuREIPAddress.Index = 4;
            this.mnuREIPAddress.Text = "IP Address";
            this.mnuREIPAddress.Click += new System.EventHandler(this.mnuREIPAddress_Click);
            // 
            // mnuQuotedText
            // 
            this.mnuQuotedText.Index = 5;
            this.mnuQuotedText.Text = "Quoted Text";
            this.mnuQuotedText.Click += new System.EventHandler(this.mnuQuotedText_Click);
            // 
            // mnuREURL
            // 
            this.mnuREURL.Index = 6;
            this.mnuREURL.Text = "URL";
            this.mnuREURL.Click += new System.EventHandler(this.mnuREURL_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 7;
            this.menuItem2.Text = "-";
            // 
            // mnuUserDefined
            // 
            this.mnuUserDefined.Index = 8;
            this.mnuUserDefined.Text = "User Defined";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.cbMatchCase);
            this.groupBox2.Controls.Add(this.rbText);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(8, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Type";
            // 
            // checkBox2
            // 
            this.checkBox2.Enabled = false;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBox2.Location = new System.Drawing.Point(200, 40);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(144, 16);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "Find whole word only";
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.Enabled = false;
            this.cbMatchCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbMatchCase.Location = new System.Drawing.Point(200, 16);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(136, 16);
            this.cbMatchCase.TabIndex = 2;
            this.cbMatchCase.Text = "Match Case";
            // 
            // rbText
            // 
            this.rbText.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbText.Location = new System.Drawing.Point(8, 40);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(184, 24);
            this.rbText.TabIndex = 1;
            this.rbText.Text = "Text";
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radioButton1.Location = new System.Drawing.Point(8, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(184, 24);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Normal (regular expressions)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnOpenBinaryExtenList);
            this.groupBox3.Controls.Add(this.cbIgnoreBinaryFiles);
            this.groupBox3.Controls.Add(this.cbSubdirectories);
            this.groupBox3.Controls.Add(this.btnBrowse);
            this.groupBox3.Controls.Add(this.txtSearchLocation);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(8, 144);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(352, 72);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Location";
            // 
            // btnOpenBinaryExtenList
            // 
            this.btnOpenBinaryExtenList.Location = new System.Drawing.Point(286, 43);
            this.btnOpenBinaryExtenList.Name = "btnOpenBinaryExtenList";
            this.btnOpenBinaryExtenList.Size = new System.Drawing.Size(24, 23);
            this.btnOpenBinaryExtenList.TabIndex = 6;
            this.btnOpenBinaryExtenList.Text = "...";
            this.toolTip1.SetToolTip(this.btnOpenBinaryExtenList, "Open binary file extension list");
            this.btnOpenBinaryExtenList.UseVisualStyleBackColor = true;
            this.btnOpenBinaryExtenList.Visible = false;
            // 
            // cbIgnoreBinaryFiles
            // 
            this.cbIgnoreBinaryFiles.AutoSize = true;
            this.cbIgnoreBinaryFiles.Location = new System.Drawing.Point(168, 47);
            this.cbIgnoreBinaryFiles.Name = "cbIgnoreBinaryFiles";
            this.cbIgnoreBinaryFiles.Size = new System.Drawing.Size(112, 17);
            this.cbIgnoreBinaryFiles.TabIndex = 5;
            this.cbIgnoreBinaryFiles.Text = "Ignore Binary Files";
            this.cbIgnoreBinaryFiles.UseVisualStyleBackColor = true;
            // 
            // cbSubdirectories
            // 
            this.cbSubdirectories.Location = new System.Drawing.Point(8, 47);
            this.cbSubdirectories.Name = "cbSubdirectories";
            this.cbSubdirectories.Size = new System.Drawing.Size(144, 16);
            this.cbSubdirectories.TabIndex = 4;
            this.cbSubdirectories.Text = "Include Subdirectories";
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowse.Location = new System.Drawing.Point(304, 16);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(40, 20);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "...";
            this.toolTip1.SetToolTip(this.btnBrowse, "Browse");
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSearchLocation
            // 
            this.txtSearchLocation.Location = new System.Drawing.Point(8, 16);
            this.txtSearchLocation.Name = "txtSearchLocation";
            this.txtSearchLocation.Size = new System.Drawing.Size(288, 20);
            this.txtSearchLocation.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDeselectExt);
            this.groupBox4.Controls.Add(this.btnSelectExt);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.txtAdd);
            this.groupBox4.Controls.Add(this.lbSelectedTypes);
            this.groupBox4.Controls.Add(this.lbFileTypes);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(8, 232);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(352, 144);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "File Types";
            // 
            // btnDeselectExt
            // 
            this.btnDeselectExt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeselectExt.Location = new System.Drawing.Point(152, 64);
            this.btnDeselectExt.Name = "btnDeselectExt";
            this.btnDeselectExt.Size = new System.Drawing.Size(48, 32);
            this.btnDeselectExt.TabIndex = 5;
            this.btnDeselectExt.Text = "<";
            this.toolTip1.SetToolTip(this.btnDeselectExt, "Remove file type from list");
            this.btnDeselectExt.Click += new System.EventHandler(this.btnDeselectExt_Click);
            // 
            // btnSelectExt
            // 
            this.btnSelectExt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelectExt.Location = new System.Drawing.Point(152, 24);
            this.btnSelectExt.Name = "btnSelectExt";
            this.btnSelectExt.Size = new System.Drawing.Size(48, 32);
            this.btnSelectExt.TabIndex = 4;
            this.btnSelectExt.Text = ">";
            this.toolTip1.SetToolTip(this.btnSelectExt, "Add file type to selected list");
            this.btnSelectExt.Click += new System.EventHandler(this.btnSelectExt_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Location = new System.Drawing.Point(272, 112);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 24);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add";
            this.toolTip1.SetToolTip(this.btnAdd, "Add custom file type to list");
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAdd
            // 
            this.txtAdd.Location = new System.Drawing.Point(8, 114);
            this.txtAdd.Name = "txtAdd";
            this.txtAdd.Size = new System.Drawing.Size(264, 20);
            this.txtAdd.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtAdd, "Add custom file type (extension) here");
            // 
            // lbSelectedTypes
            // 
            this.lbSelectedTypes.ContextMenu = this.cmSelectedTypes;
            this.lbSelectedTypes.Location = new System.Drawing.Point(200, 16);
            this.lbSelectedTypes.Name = "lbSelectedTypes";
            this.lbSelectedTypes.Size = new System.Drawing.Size(144, 82);
            this.lbSelectedTypes.Sorted = true;
            this.lbSelectedTypes.TabIndex = 1;
            this.toolTip1.SetToolTip(this.lbSelectedTypes, "Right click here for more options");
            // 
            // cmSelectedTypes
            // 
            this.cmSelectedTypes.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuDocuments,
            this.mnuVS,
            this.mnuScripts,
            this.mnuWeb,
            this.mnuPython,
            this.menuItem1,
            this.mnuClear});
            // 
            // mnuDocuments
            // 
            this.mnuDocuments.Index = 0;
            this.mnuDocuments.Text = "Documents";
            this.mnuDocuments.Click += new System.EventHandler(this.mnuDocuments_Click);
            // 
            // mnuVS
            // 
            this.mnuVS.Index = 1;
            this.mnuVS.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuVisualStudio6,
            this.mnuVisualStudioNet});
            this.mnuVS.Text = "Visual Studio";
            // 
            // mnuVisualStudio6
            // 
            this.mnuVisualStudio6.Index = 0;
            this.mnuVisualStudio6.Text = "Visual Studio 6";
            this.mnuVisualStudio6.Click += new System.EventHandler(this.mnuVisualStudio6_Click);
            // 
            // mnuVisualStudioNet
            // 
            this.mnuVisualStudioNet.Index = 1;
            this.mnuVisualStudioNet.Text = "Visual Studio .NET";
            this.mnuVisualStudioNet.Click += new System.EventHandler(this.mnuVisualStudioNet_Click);
            // 
            // mnuScripts
            // 
            this.mnuScripts.Index = 2;
            this.mnuScripts.Text = "Scripts";
            this.mnuScripts.Click += new System.EventHandler(this.mnuScripts_Click);
            // 
            // mnuWeb
            // 
            this.mnuWeb.Index = 3;
            this.mnuWeb.Text = "Web";
            this.mnuWeb.Click += new System.EventHandler(this.mnuWeb_Click);
            // 
            // mnuPython
            // 
            this.mnuPython.Index = 4;
            this.mnuPython.Text = "Python";
            this.mnuPython.Click += new System.EventHandler(this.mnuPython_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 5;
            this.menuItem1.Text = "-";
            // 
            // mnuClear
            // 
            this.mnuClear.Index = 6;
            this.mnuClear.Text = "&Clear";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // lbFileTypes
            // 
            this.lbFileTypes.Location = new System.Drawing.Point(8, 16);
            this.lbFileTypes.Name = "lbFileTypes";
            this.lbFileTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFileTypes.Size = new System.Drawing.Size(144, 82);
            this.lbFileTypes.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(272, 384);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 40);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(176, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSearchAssist
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(376, 438);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSearchAssist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Assistant";
            this.Load += new System.EventHandler(this.frmSearchAssist_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmSearchAssist_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            txtSearchLocation.Text = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "LastSearchLocation");
            txtSearchText.Text = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "LastText");
            cbSubdirectories.Checked = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Subdirectories").ToUpper() == "TRUE" ? true : false;
            cbIgnoreBinaryFiles.Checked = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "IgnoreBinary").ToUpper() == "TRUE" ? true : false;
            AddExtensionList(Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExtList"));
            InitFileTypes();
        }

        private void InitFileTypes()
        {
            lbFileTypes.Items.Add("*.*");
            lbFileTypes.Items.Add(".cs");
            lbFileTypes.Items.Add(".cpp");
            lbFileTypes.Items.Add(".csproj");
            lbFileTypes.Items.Add(".sln");
            lbFileTypes.Items.Add(".vbproj");
            lbFileTypes.Items.Add(".txt");
            lbFileTypes.Items.Add(".csv");
            lbFileTypes.Items.Add(".xml");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.SelectedPath = txtSearchLocation.Text;
            fbd.ShowDialog();
            if (fbd.SelectedPath.Trim().Length != 0)
            {
                txtSearchLocation.Text = fbd.SelectedPath;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAdd.Text.Trim().Length != 0)
            {
                if (txtAdd.Text.Substring(0, 1) == ".")
                {
                    lbSelectedTypes.Items.Add(txtAdd.Text);
                    txtAdd.Text = "";
                }
                else
                {
                    MessageBox.Show("Extensions are in the form of [.ext].", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSelectExt_Click(object sender, EventArgs e)
        {
            if (lbFileTypes.SelectedItems.Count > 0)
            {
                foreach (object o in lbFileTypes.SelectedItems)
                {
                    string s = (string)o;
                    if (StringInListBox(ref lbSelectedTypes, s) == false)
                    {
                        lbSelectedTypes.Items.Add(s);
                    }
                }
            }
        }

        private bool StringInListBox(ref ListBox ctrl, string SearchString)
        {
            foreach (object i in ctrl.Items)
            {
                string s = (string)i;
                if (s.ToUpper() == SearchString.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        private void btnDeselectExt_Click(object sender, EventArgs e)
        {
            // Remove the selected item.
            if (lbSelectedTypes.SelectedItems.Count == 1)
            {
                object o = lbSelectedTypes.SelectedItem;
                lbSelectedTypes.Items.Remove(o);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtSearchText.Text.Trim().Length != 0)
            {
                if (rbText.Checked == true)
                {
                    txtSearchText.Text = Regex.Escape(txtSearchText.Text); 
                }
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastSearchLocation", txtSearchLocation.Text);
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastText", txtSearchText.Text);
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ExtList", ExtensionList());
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Subdirectories", cbSubdirectories.Checked.ToString());
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "IgnoreBinary", cbIgnoreBinaryFiles.Checked.ToString());
                if (txtSearchLocation.Text.Trim().Length != 0)
                {
                    if (lbSelectedTypes.Items.Count > 0)
                    {
                        this._ReturnSearchText = txtSearchText.Text;
                        this._ReturnPath = txtSearchLocation.Text;
                        this._ReturnExtensions = ExtensionList();
                        this._IncludeSubdirectories = cbSubdirectories.Checked;
                        this._IgnoreBinary = cbIgnoreBinaryFiles.Checked;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You must provide at least 1 file extension to search.", "File Mask", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("You must provide a location to search.", "Search Location", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("You must provide text to search for.", "Search Text", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private string ExtensionList()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= lbSelectedTypes.Items.Count - 1; i++)
            {
                sb.Append(lbSelectedTypes.Items[i].ToString());
                if (i != lbSelectedTypes.Items.Count - 1)
                {
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }

        private void AddExtensionList(string ExtensionList)
        {
            string sep = ";";
            string[] results = ExtensionList.Split(sep.ToCharArray());
            foreach (string s in results)
            {
                if (s.Trim().Length != 0)
                {
                    lbSelectedTypes.Items.Add(s);
                }
            }
        }

        private void mnuVisualStudioNet_Click(object sender, EventArgs e)
        {
            ConditionalListBoxAdd(ref lbSelectedTypes, ".cs");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".vb");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".csproj");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".vbproj");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".config");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".xml");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".cpp");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".h");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".sln");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".asmx");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".aspx");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".ascx");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".asax");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".rpt");

        }

        public string ReturnSearchText
        {
            get
            {
                return _ReturnSearchText;
            }
        }

        public string ReturnPath
        {
            get
            {
                return _ReturnPath;
            }
        }

        public string ReturnExtensions
        {
            get
            {
                return _ReturnExtensions;
            }
        }

        public bool MatchCase
        {
            get
            {
                return cbMatchCase.Checked;
            }
        }

        public bool Subdirectories
        {
            get
            {
                return _IncludeSubdirectories;
            }
        }

        private bool _IgnoreBinary;
        public bool IgnoreBinary
        {
            get
            {
                return _IgnoreBinary;
            }
        }
        private void ConditionalListBoxAdd(ref ListBox ctrl, string ToAdd)
        {
            bool found = false;
            foreach (object i in ctrl.Items)
            {
                string s = (string)i;
                if (s.ToUpper() == ToAdd.ToUpper())
                {
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                ctrl.Items.Add(ToAdd);
            }

        }

        private void mnuDocuments_Click(object sender, EventArgs e)
        {
            ConditionalListBoxAdd(ref lbSelectedTypes, ".doc");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".txt");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".xml");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".nfo");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".me");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".rtf");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".ini");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".rpt");
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            lbSelectedTypes.Items.Clear();
        }

        private void mnuScripts_Click(object sender, EventArgs e)
        {
            ConditionalListBoxAdd(ref lbSelectedTypes, ".vbs");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".bat");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".reg");
        }

        private void mnuVisualStudio6_Click(object sender, EventArgs e)
        {
            ConditionalListBoxAdd(ref lbSelectedTypes, ".c");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".clw");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".cpp");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".dsp");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".dsw");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".frm");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".h");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".mak");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".idl");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".ini");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".rc");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".rpt");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".stt");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".stc");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".opt");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".vb");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".vbp");
        }

        private void mnuREEmailAddress_Click(object sender, EventArgs e)
        {
            txtSearchText.Text = @"[\w\.=-]+@[\w\.-]+\.[\w]{2,3}";
        }

        private void mnuREIPAddress_Click(object sender, EventArgs e)
        {
            txtSearchText.Text = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}";
        }

        private void mnuREURL_Click(object sender, EventArgs e)
        {
            txtSearchText.Text = @"http\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}";
        }

        private void mnuWeb_Click(object sender, EventArgs e)
        {
            ConditionalListBoxAdd(ref lbSelectedTypes, ".asp");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".html");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".htm");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".hta");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".xml");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".css");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".asmx");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".aspx");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".ascx");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".asax");

        }

        private void mnuQuotedText_Click(object sender, EventArgs e)
        {
            txtSearchText.Text = "\"[^\"\\r\\n]*\"";

        }

        private void mnuHTMLTags_Click(object sender, System.EventArgs e)
        {
            txtSearchText.Text=@"<([A-Z][A-Z0-9]*)[^>]*>(.*?)</\1>";
        }

        private void mnuDirective_Click(object sender, System.EventArgs e)
        {
            txtSearchText.Text=@"^\s*#.*$";
        }

        private void mnuComment_Click(object sender, System.EventArgs e)
        {
            txtSearchText.Text=@"//.*$";
        }

        private void mnuInteger_Click(object sender, System.EventArgs e)
        {
            txtSearchText.Text=@"[-+]?\b\d+\b";
        }

        private void mnuHexValue_Click(object sender, System.EventArgs e)
        {
            txtSearchText.Text=@"\b0[xX][0-9a-fA-F]+\b";
        }

        private void mnuPython_Click(object sender, EventArgs e)
        {
            ConditionalListBoxAdd(ref lbSelectedTypes, ".json");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".md");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".py");
            ConditionalListBoxAdd(ref lbSelectedTypes, ".qml");
        }
    }
}