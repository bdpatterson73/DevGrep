using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DGE.Forms
{
	/// <summary>
	/// Summary description for frmEditor.
	/// </summary>
	public class frmEditor : System.Windows.Forms.Form
	{
    private string _FileName;
    private ICSharpCode.TextEditor.TextEditorControl tec;
    private System.Windows.Forms.StatusBar statusBar1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.StatusBarPanel sbLocation;
    private System.Windows.Forms.StatusBarPanel sbSize;
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem menuItem2;
    private System.Windows.Forms.MenuItem menuItem3;
    private System.Windows.Forms.MenuItem menuItem4;
    private System.Windows.Forms.MenuItem menuItem5;
    private System.Windows.Forms.MenuItem menuItem6;
    private System.Windows.Forms.MenuItem menuItem7;
    private System.Windows.Forms.MenuItem menuItem8;
    private System.Windows.Forms.MenuItem menuItem9;
    private System.Windows.Forms.MenuItem menuItem10;
    private System.Windows.Forms.MenuItem menuItem11;
    private System.Windows.Forms.MenuItem menuItem12;
    private System.Windows.Forms.MenuItem menuItem13;
    private System.Windows.Forms.MenuItem menuItem14;
    private System.Windows.Forms.MenuItem menuItem15;
    private System.Windows.Forms.MenuItem menuItem16;
    private System.Windows.Forms.MenuItem menuItem17;
    private System.Windows.Forms.MenuItem menuItem18;
    private System.Windows.Forms.MenuItem menuItem19;
    private System.Windows.Forms.MenuItem menuItem20;
    private System.Windows.Forms.MenuItem menuItem21;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmEditor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


    public frmEditor(string fileName)
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
      _FileName=fileName;
      tec.LoadFile(fileName,true);
      this.Text = fileName;
    
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmEditor));
      this.tec = new ICSharpCode.TextEditor.TextEditorControl();
      this.statusBar1 = new System.Windows.Forms.StatusBar();
      this.sbLocation = new System.Windows.Forms.StatusBarPanel();
      this.sbSize = new System.Windows.Forms.StatusBarPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.mainMenu1 = new System.Windows.Forms.MainMenu();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.menuItem4 = new System.Windows.Forms.MenuItem();
      this.menuItem5 = new System.Windows.Forms.MenuItem();
      this.menuItem6 = new System.Windows.Forms.MenuItem();
      this.menuItem7 = new System.Windows.Forms.MenuItem();
      this.menuItem8 = new System.Windows.Forms.MenuItem();
      this.menuItem9 = new System.Windows.Forms.MenuItem();
      this.menuItem10 = new System.Windows.Forms.MenuItem();
      this.menuItem11 = new System.Windows.Forms.MenuItem();
      this.menuItem12 = new System.Windows.Forms.MenuItem();
      this.menuItem13 = new System.Windows.Forms.MenuItem();
      this.menuItem14 = new System.Windows.Forms.MenuItem();
      this.menuItem15 = new System.Windows.Forms.MenuItem();
      this.menuItem16 = new System.Windows.Forms.MenuItem();
      this.menuItem17 = new System.Windows.Forms.MenuItem();
      this.menuItem18 = new System.Windows.Forms.MenuItem();
      this.menuItem19 = new System.Windows.Forms.MenuItem();
      this.menuItem20 = new System.Windows.Forms.MenuItem();
      this.menuItem21 = new System.Windows.Forms.MenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.sbLocation)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.sbSize)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tec
      // 
      this.tec.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tec.Encoding = ((System.Text.Encoding)(resources.GetObject("tec.Encoding")));
      this.tec.Location = new System.Drawing.Point(0, 0);
      this.tec.Name = "tec";
      this.tec.ShowEOLMarkers = true;
      this.tec.ShowSpaces = true;
      this.tec.ShowTabs = true;
      this.tec.ShowVRuler = true;
      this.tec.Size = new System.Drawing.Size(640, 320);
      this.tec.TabIndex = 0;
      this.tec.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tec_KeyUp);
      // 
      // statusBar1
      // 
      this.statusBar1.Location = new System.Drawing.Point(0, 320);
      this.statusBar1.Name = "statusBar1";
      this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                  this.sbLocation,
                                                                                  this.sbSize});
      this.statusBar1.ShowPanels = true;
      this.statusBar1.Size = new System.Drawing.Size(640, 22);
      this.statusBar1.TabIndex = 1;
      // 
      // sbLocation
      // 
      this.sbLocation.MinWidth = 150;
      this.sbLocation.Width = 150;
      // 
      // sbSize
      // 
      this.sbSize.MinWidth = 150;
      this.sbSize.Width = 150;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.tec);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(640, 320);
      this.panel1.TabIndex = 2;
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuItem16,
                                                                              this.menuItem1,
                                                                              this.menuItem2,
                                                                              this.menuItem3});
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 1;
      this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuItem4,
                                                                              this.menuItem5,
                                                                              this.menuItem6,
                                                                              this.menuItem7,
                                                                              this.menuItem8,
                                                                              this.menuItem9,
                                                                              this.menuItem10,
                                                                              this.menuItem11,
                                                                              this.menuItem12,
                                                                              this.menuItem13,
                                                                              this.menuItem14,
                                                                              this.menuItem15});
      this.menuItem1.MergeOrder = 2;
      this.menuItem1.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem1.Text = "&Edit";
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 2;
      this.menuItem2.MergeOrder = 3;
      this.menuItem2.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem2.Text = "View";
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 3;
      this.menuItem3.MergeOrder = 4;
      this.menuItem3.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem3.Text = "&Format";
      // 
      // menuItem4
      // 
      this.menuItem4.Index = 0;
      this.menuItem4.Text = "Undo";
      // 
      // menuItem5
      // 
      this.menuItem5.Index = 1;
      this.menuItem5.Text = "-";
      // 
      // menuItem6
      // 
      this.menuItem6.Index = 2;
      this.menuItem6.Text = "Cut";
      // 
      // menuItem7
      // 
      this.menuItem7.Index = 3;
      this.menuItem7.Text = "Copy";
      // 
      // menuItem8
      // 
      this.menuItem8.Index = 4;
      this.menuItem8.Text = "Paste";
      // 
      // menuItem9
      // 
      this.menuItem9.Index = 5;
      this.menuItem9.Text = "Paste Special";
      // 
      // menuItem10
      // 
      this.menuItem10.Index = 6;
      this.menuItem10.Text = "Clear";
      // 
      // menuItem11
      // 
      this.menuItem11.Index = 7;
      this.menuItem11.Text = "Select All";
      // 
      // menuItem12
      // 
      this.menuItem12.Index = 8;
      this.menuItem12.Text = "-";
      // 
      // menuItem13
      // 
      this.menuItem13.Index = 9;
      this.menuItem13.Text = "Find";
      // 
      // menuItem14
      // 
      this.menuItem14.Index = 10;
      this.menuItem14.Text = "Find Next";
      // 
      // menuItem15
      // 
      this.menuItem15.Index = 11;
      this.menuItem15.Text = "Replace";
      // 
      // menuItem16
      // 
      this.menuItem16.Index = 0;
      this.menuItem16.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                               this.menuItem17,
                                                                               this.menuItem18,
                                                                               this.menuItem19,
                                                                               this.menuItem20,
                                                                               this.menuItem21});
      this.menuItem16.MergeOrder = 1;
      this.menuItem16.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem16.Text = "&File";
      // 
      // menuItem17
      // 
      this.menuItem17.Index = 0;
      this.menuItem17.MergeOrder = 2;
      this.menuItem17.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem17.Text = "&Save";
      // 
      // menuItem18
      // 
      this.menuItem18.Index = 1;
      this.menuItem18.MergeOrder = 3;
      this.menuItem18.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem18.Text = "Save As...";
      // 
      // menuItem19
      // 
      this.menuItem19.Index = 2;
      this.menuItem19.MergeOrder = 4;
      this.menuItem19.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem19.Text = "-";
      // 
      // menuItem20
      // 
      this.menuItem20.Index = 3;
      this.menuItem20.MergeOrder = 5;
      this.menuItem20.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem20.Text = "Page Setup";
      // 
      // menuItem21
      // 
      this.menuItem21.Index = 4;
      this.menuItem21.MergeOrder = 6;
      this.menuItem21.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem21.Text = "Print";
      // 
      // frmEditor
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(640, 342);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.statusBar1);
      this.Menu = this.mainMenu1;
      this.Name = "frmEditor";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      ((System.ComponentModel.ISupportInitialize)(this.sbLocation)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.sbSize)).EndInit();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void tec_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
     
    }

    public string FileName
    {
      get{return _FileName;}
      set{_FileName=value;}
    }


	}
}
