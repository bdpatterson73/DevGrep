using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DevGrep.Forms
{
    /// <summary>
    /// Summary description for frmReplace.
    /// </summary>
    public class frmReplace : Form
    {
        private string _ReplaceText = "";
        private GroupBox groupBox1;
        private TextBox txtReplaceText;
        private Button btnOK;
        private Button btnCancel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public frmReplace()
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (frmReplace));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReplaceText = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReplaceText);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Repalce with the following text";
            // 
            // txtReplaceText
            // 
            this.txtReplaceText.Location = new System.Drawing.Point(8, 24);
            this.txtReplaceText.Name = "txtReplaceText";
            this.txtReplaceText.Size = new System.Drawing.Size(360, 20);
            this.txtReplaceText.TabIndex = 0;
            this.txtReplaceText.Text = "";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(296, 80);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 40);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(208, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmReplace
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(386, 128);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReplace";
            this.Text = "Replace";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtReplaceText.Text.Length == 0)
            {
                this.ReplaceText = "";
            }
            else
            {
                this._ReplaceText = txtReplaceText.Text;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string ReplaceText
        {
            get
            {
                return _ReplaceText;
            }
            set
            {
                _ReplaceText = value;
            }
        }
    }
}