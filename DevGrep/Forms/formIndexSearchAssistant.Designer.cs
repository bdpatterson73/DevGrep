namespace DevGrep.Forms
{
    partial class formIndexSearchAssistant
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuClear = new System.Windows.Forms.MenuItem();
            this.lbFileTypes = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbSubdirectories = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSearchLocation = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnDeselectExt);
            this.groupBox4.Controls.Add(this.btnSelectExt);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.txtAdd);
            this.groupBox4.Controls.Add(this.lbSelectedTypes);
            this.groupBox4.Controls.Add(this.lbFileTypes);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(6, 143);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(352, 162);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "File Types";
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClear.Location = new System.Drawing.Point(152, 54);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(48, 32);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.btnClear, "Clear selected file types");
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDeselectExt
            // 
            this.btnDeselectExt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDeselectExt.Location = new System.Drawing.Point(152, 92);
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
            this.btnSelectExt.Location = new System.Drawing.Point(152, 16);
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
            this.btnAdd.Location = new System.Drawing.Point(272, 130);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 24);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add";
            this.toolTip1.SetToolTip(this.btnAdd, "Add custom file type to list");
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAdd
            // 
            this.txtAdd.Location = new System.Drawing.Point(8, 132);
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
            this.lbSelectedTypes.Size = new System.Drawing.Size(144, 108);
            this.lbSelectedTypes.Sorted = true;
            this.lbSelectedTypes.TabIndex = 1;
            // 
            // cmSelectedTypes
            // 
            this.cmSelectedTypes.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuDocuments,
            this.mnuVS,
            this.mnuScripts,
            this.mnuWeb,
            this.menuItem1,
            this.mnuClear});
            // 
            // mnuDocuments
            // 
            this.mnuDocuments.Index = 0;
            this.mnuDocuments.Text = "Documents";
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
            // 
            // mnuVisualStudioNet
            // 
            this.mnuVisualStudioNet.Index = 1;
            this.mnuVisualStudioNet.Text = "Visual Studio .NET";
            // 
            // mnuScripts
            // 
            this.mnuScripts.Index = 2;
            this.mnuScripts.Text = "Scripts";
            // 
            // mnuWeb
            // 
            this.mnuWeb.Index = 3;
            this.mnuWeb.Text = "Web";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // mnuClear
            // 
            this.mnuClear.Index = 5;
            this.mnuClear.Text = "&Clear";
            // 
            // lbFileTypes
            // 
            this.lbFileTypes.Location = new System.Drawing.Point(8, 16);
            this.lbFileTypes.Name = "lbFileTypes";
            this.lbFileTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFileTypes.Size = new System.Drawing.Size(144, 108);
            this.lbFileTypes.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbSubdirectories);
            this.groupBox3.Controls.Add(this.btnBrowse);
            this.groupBox3.Controls.Add(this.txtSearchLocation);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(6, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(352, 72);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Location";
            // 
            // cbSubdirectories
            // 
            this.cbSubdirectories.Location = new System.Drawing.Point(8, 40);
            this.cbSubdirectories.Name = "cbSubdirectories";
            this.cbSubdirectories.Size = new System.Drawing.Size(264, 16);
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(174, 311);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 31);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(270, 311);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 31);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtSearchText
            // 
            this.txtSearchText.ContextMenu = this.cmRegEx;
            this.txtSearchText.Location = new System.Drawing.Point(8, 16);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(336, 20);
            this.txtSearchText.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtSearchText, "Right click for syntax help.");
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
            // 
            // mnuDirective
            // 
            this.mnuDirective.Index = 1;
            this.mnuDirective.Text = "Directive";
            // 
            // mnuHexValue
            // 
            this.mnuHexValue.Index = 2;
            this.mnuHexValue.Text = "Hex Value";
            // 
            // mnuREEmailAddress
            // 
            this.mnuREEmailAddress.Index = 1;
            this.mnuREEmailAddress.Text = "EMail Address";
            // 
            // mnuHTMLTags
            // 
            this.mnuHTMLTags.Index = 2;
            this.mnuHTMLTags.Text = "HTML Tags";
            // 
            // mnuInteger
            // 
            this.mnuInteger.Index = 3;
            this.mnuInteger.Text = "Integer";
            // 
            // mnuREIPAddress
            // 
            this.mnuREIPAddress.Index = 4;
            this.mnuREIPAddress.Text = "IP Address";
            // 
            // mnuQuotedText
            // 
            this.mnuQuotedText.Index = 5;
            this.mnuQuotedText.Text = "Quoted Text";
            // 
            // mnuREURL
            // 
            this.mnuREURL.Index = 6;
            this.mnuREURL.Text = "URL";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSearchText);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 48);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Text";
            // 
            // formIndexSearchAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 351);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formIndexSearchAssistant";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Indexed Search Assistant";
            this.Load += new System.EventHandler(this.formIndexSearchAssistant_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDeselectExt;
        private System.Windows.Forms.Button btnSelectExt;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAdd;
        private System.Windows.Forms.ListBox lbSelectedTypes;
        private System.Windows.Forms.ContextMenu cmSelectedTypes;
        private System.Windows.Forms.MenuItem mnuDocuments;
        private System.Windows.Forms.MenuItem mnuVS;
        private System.Windows.Forms.MenuItem mnuVisualStudio6;
        private System.Windows.Forms.MenuItem mnuVisualStudioNet;
        private System.Windows.Forms.MenuItem mnuScripts;
        private System.Windows.Forms.MenuItem mnuWeb;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuClear;
        private System.Windows.Forms.ListBox lbFileTypes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbSubdirectories;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSearchLocation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtSearchText;
        private System.Windows.Forms.ContextMenu cmRegEx;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem mnuComment;
        private System.Windows.Forms.MenuItem mnuDirective;
        private System.Windows.Forms.MenuItem mnuHexValue;
        private System.Windows.Forms.MenuItem mnuREEmailAddress;
        private System.Windows.Forms.MenuItem mnuHTMLTags;
        private System.Windows.Forms.MenuItem mnuInteger;
        private System.Windows.Forms.MenuItem mnuREIPAddress;
        private System.Windows.Forms.MenuItem mnuQuotedText;
        private System.Windows.Forms.MenuItem mnuREURL;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuUserDefined;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}