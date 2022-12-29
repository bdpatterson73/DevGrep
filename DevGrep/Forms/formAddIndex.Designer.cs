namespace DevGrep.Forms
{
    partial class formAddIndex
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolderNamePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.rbKnownFiles = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAllFiles = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a folder to index:";
            // 
            // txtFolderNamePath
            // 
            this.txtFolderNamePath.Location = new System.Drawing.Point(16, 30);
            this.txtFolderNamePath.Name = "txtFolderNamePath";
            this.txtFolderNamePath.Size = new System.Drawing.Size(304, 20);
            this.txtFolderNamePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(326, 28);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // rbKnownFiles
            // 
            this.rbKnownFiles.AutoSize = true;
            this.rbKnownFiles.Checked = true;
            this.rbKnownFiles.Location = new System.Drawing.Point(16, 19);
            this.rbKnownFiles.Name = "rbKnownFiles";
            this.rbKnownFiles.Size = new System.Drawing.Size(124, 17);
            this.rbKnownFiles.TabIndex = 3;
            this.rbKnownFiles.TabStop = true;
            this.rbKnownFiles.Text = "Known file types only";
            this.toolTip1.SetToolTip(this.rbKnownFiles, "Only index files of known types as displayed in the Preferences dialog.");
            this.rbKnownFiles.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAllFiles);
            this.groupBox1.Controls.Add(this.rbKnownFiles);
            this.groupBox1.Location = new System.Drawing.Point(16, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 66);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Types To Index";
            // 
            // rbAllFiles
            // 
            this.rbAllFiles.AutoSize = true;
            this.rbAllFiles.Location = new System.Drawing.Point(16, 42);
            this.rbAllFiles.Name = "rbAllFiles";
            this.rbAllFiles.Size = new System.Drawing.Size(57, 17);
            this.rbAllFiles.TabIndex = 4;
            this.rbAllFiles.Text = "All files";
            this.rbAllFiles.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(32, 175);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.toolTip1.SetToolTip(this.btnStart, "Begin the index process");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(-7, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(449, 3);
            this.label2.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(245, 175);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(-22, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(449, 3);
            this.label3.TabIndex = 8;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoEllipsis = true;
            this.lblStatusMessage.Location = new System.Drawing.Point(13, 133);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(388, 23);
            this.lblStatusMessage.TabIndex = 9;
            // 
            // formAddIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 204);
            this.Controls.Add(this.lblStatusMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFolderNamePath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAddIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Index";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolderNamePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.RadioButton rbKnownFiles;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAllFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatusMessage;
    }
}