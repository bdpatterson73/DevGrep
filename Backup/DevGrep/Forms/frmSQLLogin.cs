using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DevGrep.Forms
{
    /// <summary>
    /// Summary description for frmSQLLogin.
    /// </summary>
    public class frmSQLLogin : Form
    {
        private string SQLAuthTemplate = @"SERVER=~Server~; UID=~UID~;PWD=~PWD~; APP=~App~; DATABASE=~Database~";
        private string WindowsAuthTemplate = @"Server=~Server~;Database=~Database~;Trusted_Connection=True;";
        private string _connectionString;
        private Label label1;
        private ComboBox cmbServer;
        private Button btnBrowse;
        private GroupBox groupBox1;
        private Label label2;
        private TextBox txtLoginName;
        private TextBox txtPassword;
        private Button btnOK;
        private Button btnCancel;
        private GroupBox groupBox2;
        private PictureBox pictureBox1;
        private RadioButton rbWindowsAuthentication;
        private RadioButton rbSQLAuthentication;
        private Label lblLoginName;
        private Label lblPassword;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public frmSQLLogin()
        {
            InitializeComponent();
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSQLLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbWindowsAuthentication = new System.Windows.Forms.RadioButton();
            this.rbSQLAuthentication = new System.Windows.Forms.RadioButton();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(80, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL Server:";
            // 
            // cmbServer
            // 
            this.cmbServer.Location = new System.Drawing.Point(176, 24);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(192, 21);
            this.cmbServer.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(368, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 21);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 8);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(48, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Connect using:";
            // 
            // rbWindowsAuthentication
            // 
            this.rbWindowsAuthentication.Checked = true;
            this.rbWindowsAuthentication.Location = new System.Drawing.Point(72, 96);
            this.rbWindowsAuthentication.Name = "rbWindowsAuthentication";
            this.rbWindowsAuthentication.Size = new System.Drawing.Size(184, 16);
            this.rbWindowsAuthentication.TabIndex = 5;
            this.rbWindowsAuthentication.TabStop = true;
            this.rbWindowsAuthentication.Text = "Windows authentication";
            this.rbWindowsAuthentication.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbSQLAuthentication
            // 
            this.rbSQLAuthentication.Location = new System.Drawing.Point(72, 120);
            this.rbSQLAuthentication.Name = "rbSQLAuthentication";
            this.rbSQLAuthentication.Size = new System.Drawing.Size(184, 16);
            this.rbSQLAuthentication.TabIndex = 6;
            this.rbSQLAuthentication.Text = "SQL Server authentication";
            this.rbSQLAuthentication.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // lblLoginName
            // 
            this.lblLoginName.Enabled = false;
            this.lblLoginName.Location = new System.Drawing.Point(104, 144);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(96, 16);
            this.lblLoginName.TabIndex = 7;
            this.lblLoginName.Text = "Login name:";
            // 
            // lblPassword
            // 
            this.lblPassword.Enabled = false;
            this.lblPassword.Location = new System.Drawing.Point(104, 168);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(96, 16);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            // 
            // txtLoginName
            // 
            this.txtLoginName.Enabled = false;
            this.txtLoginName.Location = new System.Drawing.Point(200, 144);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(192, 20);
            this.txtLoginName.TabIndex = 9;
            this.txtLoginName.Text = "";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(200, 168);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(192, 20);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.Text = "";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(256, 208);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 32);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(336, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 32);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(8, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 8);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // frmSQLLogin
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 248);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLoginName);
            this.Controls.Add(this.rbSQLAuthentication);
            this.Controls.Add(this.rbWindowsAuthentication);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSQLLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL Login";
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
            string template = "";
            if (rbSQLAuthentication.Checked == true)
            {
                template = SQLAuthTemplate;
            }
            else
            {
                template = WindowsAuthTemplate;
            }
            // Build the connection string
            template = template.Replace("~Server~", cmbServer.Text);
            template = template.Replace("~UID~", txtLoginName.Text);
            template = template.Replace("~PWD~", txtPassword.Text);
            template = template.Replace("~Database~", "master");
            template = template.Replace("~App~", "DevGrep");
            // Set return connection string value
            this._connectionString = template;
            //MessageBox.Show(this._connectionString, "frmSQLLogin.cs:269", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Name)
            {
                case "rbSQLAuthentication":
                    txtLoginName.Enabled = true;
                    txtPassword.Enabled = true;
                    lblLoginName.Enabled = true;
                    lblPassword.Enabled = true;
                    break;
                case "rbWindowsAuthentication":
                    txtLoginName.Enabled = false;
                    txtPassword.Enabled = false;
                    lblLoginName.Enabled = false;
                    lblPassword.Enabled = false;
                    break;

            }

        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
    }
}