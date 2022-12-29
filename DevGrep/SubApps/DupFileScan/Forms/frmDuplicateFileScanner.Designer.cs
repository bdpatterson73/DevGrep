namespace DevGrep.SubApps.DupFileScan.Forms
{
    partial class frmDuplicateFileScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDuplicateFileScanner));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbExtensions = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblExtensions = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cmbComparisonType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowseComparison = new System.Windows.Forms.Button();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbCompareTwo = new System.Windows.Forms.RadioButton();
            this.rbCompareSingle = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvResults = new System.Windows.Forms.ListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbExtensions);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lblExtensions);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.cmbComparisonType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnBrowseComparison);
            this.groupBox1.Controls.Add(this.btnBrowseSource);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.rbCompareTwo);
            this.groupBox1.Controls.Add(this.rbCompareSingle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(670, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scanning Options";
            // 
            // cmbExtensions
            // 
            this.cmbExtensions.FormattingEnabled = true;
            this.cmbExtensions.Location = new System.Drawing.Point(303, 102);
            this.cmbExtensions.Name = "cmbExtensions";
            this.cmbExtensions.Size = new System.Drawing.Size(204, 21);
            this.cmbExtensions.TabIndex = 15;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(513, 77);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(513, 103);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblExtensions
            // 
            this.lblExtensions.AutoSize = true;
            this.lblExtensions.Location = new System.Drawing.Point(236, 106);
            this.lblExtensions.Name = "lblExtensions";
            this.lblExtensions.Size = new System.Drawing.Size(61, 13);
            this.lblExtensions.TabIndex = 11;
            this.lblExtensions.Text = "Extensions:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(589, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cmbComparisonType
            // 
            this.cmbComparisonType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComparisonType.FormattingEnabled = true;
            this.cmbComparisonType.Location = new System.Drawing.Point(303, 75);
            this.cmbComparisonType.Name = "cmbComparisonType";
            this.cmbComparisonType.Size = new System.Drawing.Size(204, 21);
            this.cmbComparisonType.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Comparison Type:";
            // 
            // btnBrowseComparison
            // 
            this.btnBrowseComparison.Location = new System.Drawing.Point(487, 39);
            this.btnBrowseComparison.Name = "btnBrowseComparison";
            this.btnBrowseComparison.Size = new System.Drawing.Size(56, 23);
            this.btnBrowseComparison.TabIndex = 7;
            this.btnBrowseComparison.Text = "Browse";
            this.btnBrowseComparison.UseVisualStyleBackColor = true;
            this.btnBrowseComparison.Click += new System.EventHandler(this.btnBrowseComparison_Click);
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Location = new System.Drawing.Point(487, 13);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(56, 23);
            this.btnBrowseSource.TabIndex = 6;
            this.btnBrowseSource.Text = "Browse";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(120, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(361, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(361, 20);
            this.textBox1.TabIndex = 4;
            // 
            // rbCompareTwo
            // 
            this.rbCompareTwo.AutoSize = true;
            this.rbCompareTwo.Location = new System.Drawing.Point(9, 99);
            this.rbCompareTwo.Name = "rbCompareTwo";
            this.rbCompareTwo.Size = new System.Drawing.Size(179, 17);
            this.rbCompareTwo.TabIndex = 3;
            this.rbCompareTwo.TabStop = true;
            this.rbCompareTwo.Text = "Compare Files in Two Directories";
            this.rbCompareTwo.UseVisualStyleBackColor = true;
            this.rbCompareTwo.CheckedChanged += new System.EventHandler(this.rbCompareTwo_CheckedChanged);
            // 
            // rbCompareSingle
            // 
            this.rbCompareSingle.AutoSize = true;
            this.rbCompareSingle.Location = new System.Drawing.Point(9, 76);
            this.rbCompareSingle.Name = "rbCompareSingle";
            this.rbCompareSingle.Size = new System.Drawing.Size(179, 17);
            this.rbCompareSingle.TabIndex = 2;
            this.rbCompareSingle.TabStop = true;
            this.rbCompareSingle.Text = "Compare Files in Single Directory";
            this.rbCompareSingle.UseVisualStyleBackColor = true;
            this.rbCompareSingle.CheckedChanged += new System.EventHandler(this.rbCompareSingle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Comparison Directory:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Directory:";
            // 
            // lvResults
            // 
            this.lvResults.CheckBoxes = true;
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chPath,
            this.chDateTime,
            this.chSize});
            this.lvResults.FullRowSelect = true;
            this.lvResults.Location = new System.Drawing.Point(12, 154);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(670, 262);
            this.lvResults.TabIndex = 1;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 150;
            // 
            // chPath
            // 
            this.chPath.Text = "Path";
            this.chPath.Width = 250;
            // 
            // chDateTime
            // 
            this.chDateTime.Text = "Date/Time";
            this.chDateTime.Width = 120;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.Width = 90;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(694, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Enabled = false;
            this.btnDeleteSelected.Location = new System.Drawing.Point(581, 422);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(95, 30);
            this.btnDeleteSelected.TabIndex = 3;
            this.btnDeleteSelected.Text = "Delete Selected";
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(480, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmDuplicateFileScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 486);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDeleteSelected);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDuplicateFileScanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Duplicate File Scanner";
            this.Load += new System.EventHandler(this.frmDuplicateFileScanner_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rbCompareTwo;
        private System.Windows.Forms.RadioButton rbCompareSingle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowseComparison;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.ComboBox cmbComparisonType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chPath;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblExtensions;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbExtensions;
    }
}