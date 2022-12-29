using System;
using System.ComponentModel;
using System.Windows.Forms;

using DevGrep.Classes;

namespace DevGrep.Forms
{
    /// <summary>
    /// Summary description for frmExportFields.
    /// </summary>
    public class frmExportFields : Form
    {
        private ColumnHeader columnAvailableColumn;
        private ColumnHeader columnHeader1;
        private Button btnAdd;
        private Button btnRemove;
        private GroupBox groupBox3;
        private Button btnOK;
        private Button btnCancel;
        private ListView lvAvailable;
        private System.Windows.Forms.ListView lvSelected;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public frmExportFields()
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
            this.lvAvailable = new System.Windows.Forms.ListView();
            this.columnAvailableColumn = new System.Windows.Forms.ColumnHeader();
            this.lvSelected = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvAvailable
            // 
            this.lvAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                          this.columnAvailableColumn});
            this.lvAvailable.FullRowSelect = true;
            this.lvAvailable.Location = new System.Drawing.Point(8, 8);
            this.lvAvailable.Name = "lvAvailable";
            this.lvAvailable.Size = new System.Drawing.Size(128, 208);
            this.lvAvailable.TabIndex = 2;
            this.lvAvailable.View = System.Windows.Forms.View.Details;
            // 
            // columnAvailableColumn
            // 
            this.columnAvailableColumn.Text = "Available Columns";
            this.columnAvailableColumn.Width = 120;
            // 
            // lvSelected
            // 
            this.lvSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                         this.columnHeader1});
            this.lvSelected.Location = new System.Drawing.Point(200, 8);
            this.lvSelected.Name = "lvSelected";
            this.lvSelected.Size = new System.Drawing.Size(130, 208);
            this.lvSelected.TabIndex = 2;
            this.lvSelected.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Selected Columns";
            this.columnHeader1.Width = 120;
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Location = new System.Drawing.Point(144, 56);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 48);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = ">";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemove.Location = new System.Drawing.Point(145, 136);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(48, 48);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "<";
            // 
            // groupBox3
            // 
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(8, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 8);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(248, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 40);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(160, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 40);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmExportFields
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(338, 288);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lvAvailable);
            this.Controls.Add(this.lvSelected);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmExportFields";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Export Fields for CSV";
            this.Load += new System.EventHandler(this.frmExportFields_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private void frmExportFields_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            LoadFields();
            // Load any persisted values from the registry.
        }

        private void LoadFields()
        {
            ListViewAdd(ref lvAvailable, "File Date/Time");
            ListViewAdd(ref lvAvailable, "File Line");
            ListViewAdd(ref lvAvailable, "File Name");
            ListViewAdd(ref lvAvailable, "File Size");
            ListViewAdd(ref lvAvailable, "File Type");
            ListViewAdd(ref lvAvailable, "Folder");
            ListViewAdd(ref lvAvailable, "Line Matches");
            ListViewAdd(ref lvAvailable, "Line Number");
            ListViewAdd(ref lvAvailable, "Total Matches");
        }

        private void ListViewAdd(ref ListView ctrl, string AddText)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = AddText;
            ctrl.Items.Add(lvi);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            // Move the selected item from lvAvailable to lvSelected
            if (lvAvailable.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvi in lvAvailable.SelectedItems)
                {
                    ListViewItem lviNew = new ListViewItem(lvi.Text) ;
                    lvSelected.Items.Add(lviNew);
                    lvAvailable.Items.Remove(lvi) ;
                }
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            // Save these preferences to the registry
            PersistRegistry();
        }


        private void PersistRegistry()
        {
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "FileCSVLayout", "");    
        }
    }
}