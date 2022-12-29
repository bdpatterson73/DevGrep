using System;
using System.ComponentModel;
using System.Windows.Forms;

using DevGrep.Classes;

namespace DevGrep.Forms
{
    /// <summary>
    /// Summary description for frmPreferences.
    /// </summary>
    public class frmPreferences : Form
    {
        private string _Editor = "";
        private string _ExternalEditor = "";
        private TabControl tabControl1;
        private TabPage tpEditor;
        private RadioButton rbDevGrepEditor;
        private RadioButton rbExternalEditor;
        private TextBox txtExternalEditor;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnOK;
        private Button btnCancel;
        private TabPage tpReplace;
        private CheckBox cbConfirmEachReplacement;
        private CheckBox cbConfirmFileReplacement;
        private CheckBox cbReplaceOverOriginal;
        private Button btnBrowse;
        private TextBox txtCmdLine;
        private Label label5;
        private TabPage tpSearch;
        private CheckBox cbYield;
        private TabPage tpView;
        private Label label6;
        private Label label7;
        private NumericUpDown nudPreceeding;
        private NumericUpDown nudFollowing;
        private CheckBox cbAutosize;
        private RadioButton rbRegisteredEditor;
        private RadioButton rbInternalEditor;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public frmPreferences()
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpView = new System.Windows.Forms.TabPage();
            this.cbAutosize = new System.Windows.Forms.CheckBox();
            this.nudFollowing = new System.Windows.Forms.NumericUpDown();
            this.nudPreceeding = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tpSearch = new System.Windows.Forms.TabPage();
            this.cbYield = new System.Windows.Forms.CheckBox();
            this.tpEditor = new System.Windows.Forms.TabPage();
            this.rbRegisteredEditor = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCmdLine = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExternalEditor = new System.Windows.Forms.TextBox();
            this.rbExternalEditor = new System.Windows.Forms.RadioButton();
            this.rbDevGrepEditor = new System.Windows.Forms.RadioButton();
            this.tpReplace = new System.Windows.Forms.TabPage();
            this.cbReplaceOverOriginal = new System.Windows.Forms.CheckBox();
            this.cbConfirmFileReplacement = new System.Windows.Forms.CheckBox();
            this.cbConfirmEachReplacement = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbInternalEditor = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tpView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFollowing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreceeding)).BeginInit();
            this.tpSearch.SuspendLayout();
            this.tpEditor.SuspendLayout();
            this.tpReplace.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpView);
            this.tabControl1.Controls.Add(this.tpSearch);
            this.tabControl1.Controls.Add(this.tpEditor);
            this.tabControl1.Controls.Add(this.tpReplace);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(416, 256);
            this.tabControl1.TabIndex = 0;
            // 
            // tpView
            // 
            this.tpView.Controls.Add(this.cbAutosize);
            this.tpView.Controls.Add(this.nudFollowing);
            this.tpView.Controls.Add(this.nudPreceeding);
            this.tpView.Controls.Add(this.label7);
            this.tpView.Controls.Add(this.label6);
            this.tpView.Location = new System.Drawing.Point(4, 22);
            this.tpView.Name = "tpView";
            this.tpView.Size = new System.Drawing.Size(408, 230);
            this.tpView.TabIndex = 3;
            this.tpView.Text = "View";
            // 
            // cbAutosize
            // 
            this.cbAutosize.Location = new System.Drawing.Point(8, 104);
            this.cbAutosize.Name = "cbAutosize";
            this.cbAutosize.Size = new System.Drawing.Size(176, 16);
            this.cbAutosize.TabIndex = 4;
            this.cbAutosize.Text = "Autosize result columns";
            // 
            // nudFollowing
            // 
            this.nudFollowing.Location = new System.Drawing.Point(8, 72);
            this.nudFollowing.Maximum = new System.Decimal(new int[]
                {
                    25,
                    0,
                    0,
                    0
                });
            this.nudFollowing.Name = "nudFollowing";
            this.nudFollowing.Size = new System.Drawing.Size(64, 20);
            this.nudFollowing.TabIndex = 3;
            // 
            // nudPreceeding
            // 
            this.nudPreceeding.Location = new System.Drawing.Point(8, 24);
            this.nudPreceeding.Maximum = new System.Decimal(new int[]
                {
                    25,
                    0,
                    0,
                    0
                });
            this.nudPreceeding.Name = "nudPreceeding";
            this.nudPreceeding.Size = new System.Drawing.Size(64, 20);
            this.nudPreceeding.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Location = new System.Drawing.Point(8, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Following Lines:";
            // 
            // label6
            // 
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Preceeding Lines:";
            // 
            // tpSearch
            // 
            this.tpSearch.Controls.Add(this.cbYield);
            this.tpSearch.Location = new System.Drawing.Point(4, 22);
            this.tpSearch.Name = "tpSearch";
            this.tpSearch.Size = new System.Drawing.Size(408, 230);
            this.tpSearch.TabIndex = 2;
            this.tpSearch.Text = "Search";
            // 
            // cbYield
            // 
            this.cbYield.Location = new System.Drawing.Point(8, 8);
            this.cbYield.Name = "cbYield";
            this.cbYield.Size = new System.Drawing.Size(320, 16);
            this.cbYield.TabIndex = 0;
            this.cbYield.Text = "Yield to high priority processes.";
            // 
            // tpEditor
            // 
            this.tpEditor.Controls.Add(this.rbInternalEditor);
            this.tpEditor.Controls.Add(this.rbRegisteredEditor);
            this.tpEditor.Controls.Add(this.label5);
            this.tpEditor.Controls.Add(this.txtCmdLine);
            this.tpEditor.Controls.Add(this.btnBrowse);
            this.tpEditor.Controls.Add(this.label4);
            this.tpEditor.Controls.Add(this.label3);
            this.tpEditor.Controls.Add(this.label2);
            this.tpEditor.Controls.Add(this.label1);
            this.tpEditor.Controls.Add(this.txtExternalEditor);
            this.tpEditor.Controls.Add(this.rbExternalEditor);
            this.tpEditor.Controls.Add(this.rbDevGrepEditor);
            this.tpEditor.Location = new System.Drawing.Point(4, 22);
            this.tpEditor.Name = "tpEditor";
            this.tpEditor.Size = new System.Drawing.Size(408, 230);
            this.tpEditor.TabIndex = 0;
            this.tpEditor.Text = "Editor";
            // 
            // rbRegisteredEditor
            // 
            this.rbRegisteredEditor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbRegisteredEditor.Location = new System.Drawing.Point(8, 16);
            this.rbRegisteredEditor.Name = "rbRegisteredEditor";
            this.rbRegisteredEditor.Size = new System.Drawing.Size(208, 16);
            this.rbRegisteredEditor.TabIndex = 10;
            this.rbRegisteredEditor.Text = "Registered Editor";
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(32, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Command Line Parameters";
            // 
            // txtCmdLine
            // 
            this.txtCmdLine.Location = new System.Drawing.Point(32, 128);
            this.txtCmdLine.Name = "txtCmdLine";
            this.txtCmdLine.Size = new System.Drawing.Size(336, 20);
            this.txtCmdLine.TabIndex = 8;
            this.txtCmdLine.Text = "";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(368, 78);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 24);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(32, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "%C - Column Number";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "%L - Line Number";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "%F - Filename";
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(32, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "External Editor Command Line";
            // 
            // txtExternalEditor
            // 
            this.txtExternalEditor.Location = new System.Drawing.Point(32, 80);
            this.txtExternalEditor.Name = "txtExternalEditor";
            this.txtExternalEditor.Size = new System.Drawing.Size(336, 20);
            this.txtExternalEditor.TabIndex = 2;
            this.txtExternalEditor.Text = "";
            // 
            // rbExternalEditor
            // 
            this.rbExternalEditor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbExternalEditor.Location = new System.Drawing.Point(8, 40);
            this.rbExternalEditor.Name = "rbExternalEditor";
            this.rbExternalEditor.Size = new System.Drawing.Size(208, 16);
            this.rbExternalEditor.TabIndex = 1;
            this.rbExternalEditor.Text = "External Editor";
            // 
            // rbDevGrepEditor
            // 
            this.rbDevGrepEditor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbDevGrepEditor.Location = new System.Drawing.Point(24, 208);
            this.rbDevGrepEditor.Name = "rbDevGrepEditor";
            this.rbDevGrepEditor.Size = new System.Drawing.Size(208, 16);
            this.rbDevGrepEditor.TabIndex = 0;
            this.rbDevGrepEditor.Text = "DevGrep Editor";
            this.rbDevGrepEditor.Visible = false;
            // 
            // tpReplace
            // 
            this.tpReplace.Controls.Add(this.cbReplaceOverOriginal);
            this.tpReplace.Controls.Add(this.cbConfirmFileReplacement);
            this.tpReplace.Controls.Add(this.cbConfirmEachReplacement);
            this.tpReplace.Location = new System.Drawing.Point(4, 22);
            this.tpReplace.Name = "tpReplace";
            this.tpReplace.Size = new System.Drawing.Size(408, 230);
            this.tpReplace.TabIndex = 1;
            this.tpReplace.Text = "Replace";
            // 
            // cbReplaceOverOriginal
            // 
            this.cbReplaceOverOriginal.Checked = true;
            this.cbReplaceOverOriginal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReplaceOverOriginal.Enabled = false;
            this.cbReplaceOverOriginal.Location = new System.Drawing.Point(8, 40);
            this.cbReplaceOverOriginal.Name = "cbReplaceOverOriginal";
            this.cbReplaceOverOriginal.Size = new System.Drawing.Size(160, 16);
            this.cbReplaceOverOriginal.TabIndex = 2;
            this.cbReplaceOverOriginal.Text = "Replace Over Original File";
            this.cbReplaceOverOriginal.CheckedChanged += new System.EventHandler(this.cbReplaceOverOriginal_CheckedChanged);
            // 
            // cbConfirmFileReplacement
            // 
            this.cbConfirmFileReplacement.Location = new System.Drawing.Point(32, 64);
            this.cbConfirmFileReplacement.Name = "cbConfirmFileReplacement";
            this.cbConfirmFileReplacement.Size = new System.Drawing.Size(176, 16);
            this.cbConfirmFileReplacement.TabIndex = 1;
            this.cbConfirmFileReplacement.Text = "Confirm File Replacement";
            // 
            // cbConfirmEachReplacement
            // 
            this.cbConfirmEachReplacement.Location = new System.Drawing.Point(8, 16);
            this.cbConfirmEachReplacement.Name = "cbConfirmEachReplacement";
            this.cbConfirmEachReplacement.Size = new System.Drawing.Size(176, 16);
            this.cbConfirmEachReplacement.TabIndex = 0;
            this.cbConfirmEachReplacement.Text = "Confirm Each Replacement";
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(344, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 40);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(264, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbInternalEditor
            // 
            this.rbInternalEditor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbInternalEditor.Location = new System.Drawing.Point(224, 16);
            this.rbInternalEditor.Name = "rbInternalEditor";
            this.rbInternalEditor.Size = new System.Drawing.Size(176, 16);
            this.rbInternalEditor.TabIndex = 11;
            this.rbInternalEditor.Text = "Internal Editor";
            // 
            // frmPreferences
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(418, 312);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPreferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudFollowing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreceeding)).EndInit();
            this.tpSearch.ResumeLayout(false);
            this.tpEditor.ResumeLayout(false);
            this.tpReplace.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        #region InitValues
        /// <summary>
        /// InitValues
        /// </summary>
        private void InitValues()
        {
            Editor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Editor");
            ExternalEditor = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExternalEditor");
            ConfirmEachReplacement = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ConfirmEach");
            ReplaceOriginal = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ReplaceOriginal");
            AutoSize = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Autosize");
            ConfirmFileReplace = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ConfirmFileReplace");
            CmdLine = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "CmdLine");
            Yield = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Yield");
            string sPre = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Preceeding");
            if (sPre.Length == 0)
            {
                PreceedingLines = 0;
            }
            else
            {
                PreceedingLines = Convert.ToInt32(sPre);
            }

            string sFol = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Following");
            if (sFol.Length == 0)
            {
                FollowingLines = 0;
            }
            else
            {
                FollowingLines = Convert.ToInt32(sFol);
            }

        }
        #endregion

        private void CommitValues()
        {
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Editor", Editor);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ExternalEditor", ExternalEditor);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ConfirmEach", ConfirmEachReplacement);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Autosize", AutoSize);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ReplaceOriginal", ReplaceOriginal);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ConfirmFileReplace", ConfirmFileReplace);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "CmdLine", CmdLine);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Yield", Yield);
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Preceeding", PreceedingLines.ToString());
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Following", FollowingLines.ToString());
        }

        private void cbReplaceOverOriginal_CheckedChanged(object sender, EventArgs e)
        {
            //      if (cbReplaceOverOriginal.Checked == true)
            //      {
            //        cbConfirmFileReplacement.Enabled = true;
            //      }
            //      else
            //      {
            //        cbConfirmFileReplacement.Enabled = false;
            //      }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CommitValues();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Locate Text Editor";
            ofd.ShowDialog();
            if (ofd.FileName.Length != 0)
            {
                txtExternalEditor.Text = ofd.FileName;
            }
        }

        #region Editor Property
        /// <summary>
        /// Editor property
        /// </summary>
        public string Editor
        {
            get
            {
                if (rbRegisteredEditor.Checked == true)
                {
                    return "DevGrep";
                }
                if (rbExternalEditor.Checked == true)
                {
                    return "External";
                }
                return "Internal";
            }

            set
            {
                _Editor = value;
                if (_Editor.ToUpper() == "DEVGREP")
                {
                    rbRegisteredEditor.Checked = true;
                }

                if (_Editor.ToUpper() == "INTERNAL")
                {
                    rbInternalEditor.Checked = true;
                }
                if (_Editor.ToUpper() == "EXTERNAL")
                {
                    rbExternalEditor.Checked = true;
                }
            }
        }
        #endregion

        #region ExternalEditor Property
        /// <summary>
        /// ExternalEditor Property
        /// </summary>
        public string ExternalEditor
        {
            get
            {
                if (txtExternalEditor.Text.Length != 0)
                {
                    _ExternalEditor = txtExternalEditor.Text;
                    return _ExternalEditor;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                _ExternalEditor = value;
                txtExternalEditor.Text = _ExternalEditor;
            }
        }
        #endregion

        public string ConfirmEachReplacement
        {
            get
            {
                if (cbConfirmEachReplacement.Checked == true)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            set
            {
                if (value == "TRUE")
                {
                    cbConfirmEachReplacement.Checked = true;
                }
                else
                {
                    cbConfirmEachReplacement.Checked = false;
                }
            }
        }

        public string ReplaceOriginal
        {
            get
            {
                if (cbReplaceOverOriginal.Checked == true)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            set
            {
                if (value == "TRUE")
                {
                    cbReplaceOverOriginal.Checked = true;
                }
                else
                {
                    cbReplaceOverOriginal.Checked = false;
                }
            }
        }

        public string Yield
        {
            get
            {
                if (cbYield.Checked == true)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            set
            {
                if (value == "TRUE")
                {
                    cbYield.Checked = true;
                }
                else
                {
                    cbYield.Checked = false;
                }
            }
        }

        public string ConfirmFileReplace
        {
            get
            {
                if (cbConfirmFileReplacement.Checked == true)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            set
            {
                if (value == "TRUE")
                {
                    cbConfirmFileReplacement.Checked = true;
                }
                else
                {
                    cbConfirmFileReplacement.Checked = false;
                }
            }
        }

        public string CmdLine
        {
            get
            {
                if (txtCmdLine.Text.Length == 0)
                {
                    return "";
                }
                else
                {
                    return txtCmdLine.Text;
                }
            }
            set
            {
                txtCmdLine.Text = value;
            }
        }

        public int PreceedingLines
        {
            get
            {
                return Convert.ToInt32(nudPreceeding.Value);
            }
            set
            {
                nudPreceeding.Value = value;
            }
        }

        public int FollowingLines
        {
            get
            {
                return Convert.ToInt32(nudFollowing.Value);
            }
            set
            {
                nudFollowing.Value = value;
            }
        }

        public string AutoSize
        {
            get
            {
                if (cbAutosize.Checked == true)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            set
            {
                if (value == "TRUE")
                {
                    cbAutosize.Checked = true;
                }
                else
                {
                    cbAutosize.Checked = false;
                }
            }
        }

    }
}