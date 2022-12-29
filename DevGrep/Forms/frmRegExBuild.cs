using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using DevGrep.Classes;
//TODO using DevGrep.DataSets;

namespace DevGrep.Forms
{
	/// <summary>
	/// Summary description for frmRegExBuild.
	/// </summary>
	public class frmRegExBuild : System.Windows.Forms.Form
	{
        private string rootNodeHash = "";
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvRegex;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox rtbRegex;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenu cmTree;
        private System.Windows.Forms.MenuItem mnuAdd;
        private System.Windows.Forms.MenuItem mnuEdit;
        private System.Windows.Forms.MenuItem mnuRemove;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem mnuNewFolder;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem mnuRename;
        private System.ComponentModel.IContainer components;

		public frmRegExBuild()
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmRegExBuild));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbRegex = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tvRegex = new System.Windows.Forms.TreeView();
            this.btnClose = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmTree = new System.Windows.Forms.ContextMenu();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuRemove = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuNewFolder = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mnuRename = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rtbRegex);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.tvRegex);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 344);
            this.panel1.TabIndex = 0;
            // 
            // rtbRegex
            // 
            this.rtbRegex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRegex.Location = new System.Drawing.Point(179, 0);
            this.rtbRegex.Name = "rtbRegex";
            this.rtbRegex.Size = new System.Drawing.Size(293, 344);
            this.rtbRegex.TabIndex = 2;
            this.rtbRegex.Text = "";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(176, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 344);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // tvRegex
            // 
            this.tvRegex.ContextMenu = this.cmTree;
            this.tvRegex.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvRegex.ImageList = this.imageList1;
            this.tvRegex.Location = new System.Drawing.Point(0, 0);
            this.tvRegex.Name = "tvRegex";
            this.tvRegex.Size = new System.Drawing.Size(176, 344);
            this.tvRegex.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(384, 360);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cmTree
            // 
            this.cmTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                   this.mnuAdd,
                                                                                   this.mnuEdit,
                                                                                   this.mnuRemove,
                                                                                   this.menuItem4,
                                                                                   this.mnuNewFolder,
                                                                                   this.menuItem6,
                                                                                   this.mnuRename});
            // 
            // mnuAdd
            // 
            this.mnuAdd.Index = 0;
            this.mnuAdd.Text = "Add";
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.Text = "Edit";
            // 
            // mnuRemove
            // 
            this.mnuRemove.Index = 2;
            this.mnuRemove.Text = "Remove";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "-";
            // 
            // mnuNewFolder
            // 
            this.mnuNewFolder.Index = 4;
            this.mnuNewFolder.Text = "New Folder";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
            this.menuItem6.Text = "-";
            // 
            // mnuRename
            // 
            this.mnuRename.Index = 6;
            this.mnuRename.Text = "Rename";
            // 
            // frmRegExBuild
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(466, 408);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRegExBuild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Regular Expression Builder";
            this.Load += new System.EventHandler(this.frmRegExBuild_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        private void frmRegExBuild_Load(object sender, System.EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            RootNode();
            // Load custom expressions
            //TODO  DataSets.dsRegEx re = new dsRegEx() ;
            //TODO  re.ReadXml(Application.StartupPath + "\\regex.dat");
            //TODO foreach (DataRow dr in re.Tables[0].Rows)
            //TODO {
            //TODO if (dr["EntryType"].ToString().ToUpper()=="FOLDER" &&
            //TODO dr["ParentHash"].ToString().ToUpper()==rootNodeHash)
            //TODO {
            //TODO Console.WriteLine("!!!! " + dr["EntryName"].ToString() );
            //TODO Classes.RegExTreeItem reti = new RegExTreeItem() ;

            //TODO }
            //TODO }
        }

        private void RootNode()
        {
            Classes.RegExTreeItem rti = new RegExTreeItem() ;
            rti.EntryType="Folder";
            rti.EntryName="Custom Expressions";
            rootNodeHash=rti.HashCode;
            tvRegex.Nodes.Add(rti) ;
        }
	}
}
