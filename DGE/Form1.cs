using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DGE
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
    private Classes.PersistWindowState m_windowState;
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem mnuTEFile;
    private System.Windows.Forms.MenuItem mnuTENew;
    private System.Windows.Forms.MenuItem mnuTEOpen;
    private System.Windows.Forms.MenuItem menuItem7;
    private System.Windows.Forms.MenuItem mnuTEExit;
    private System.Windows.Forms.MenuItem menuItem4;
    private System.Windows.Forms.StatusBar statusBar1;
    private System.Windows.Forms.MenuItem mnuOptions;
    private System.Windows.Forms.MenuItem mnuPreferences;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1(string[] args)
		{
			Application.EnableVisualStyles();
      Application.DoEvents();
      m_windowState = new Classes.PersistWindowState();
      m_windowState.Parent = this;

      // set registry path in HKEY_CURRENT_USER
      m_windowState.RegistryPath = @"Software\DevGrep\DGE\Main\"; 
      InitializeComponent();

      if (args.GetUpperBound(0) >=0)
      {
        OpenFile(args);
      }
		}

    public Form1()
    {
      m_windowState = new Classes.PersistWindowState();
      m_windowState.Parent = this;

      // set registry path in HKEY_CURRENT_USER
      m_windowState.RegistryPath = @"Software\DevGrep\DGE\Main\"; 
      InitializeComponent();
    }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
      this.mainMenu1 = new System.Windows.Forms.MainMenu();
      this.mnuTEFile = new System.Windows.Forms.MenuItem();
      this.mnuTENew = new System.Windows.Forms.MenuItem();
      this.mnuTEOpen = new System.Windows.Forms.MenuItem();
      this.menuItem7 = new System.Windows.Forms.MenuItem();
      this.mnuTEExit = new System.Windows.Forms.MenuItem();
      this.mnuOptions = new System.Windows.Forms.MenuItem();
      this.mnuPreferences = new System.Windows.Forms.MenuItem();
      this.menuItem4 = new System.Windows.Forms.MenuItem();
      this.statusBar1 = new System.Windows.Forms.StatusBar();
      this.SuspendLayout();
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.mnuTEFile,
                                                                              this.mnuOptions,
                                                                              this.menuItem4});
      // 
      // mnuTEFile
      // 
      this.mnuTEFile.Index = 0;
      this.mnuTEFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.mnuTENew,
                                                                              this.mnuTEOpen,
                                                                              this.menuItem7,
                                                                              this.mnuTEExit});
      this.mnuTEFile.MergeOrder = 1;
      this.mnuTEFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.mnuTEFile.Text = "&File";
      // 
      // mnuTENew
      // 
      this.mnuTENew.Index = 0;
      this.mnuTENew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
      this.mnuTENew.Text = "&New";
      // 
      // mnuTEOpen
      // 
      this.mnuTEOpen.Index = 1;
      this.mnuTEOpen.MergeOrder = 1;
      this.mnuTEOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
      this.mnuTEOpen.Text = "&Open";
      // 
      // menuItem7
      // 
      this.menuItem7.Index = 2;
      this.menuItem7.MergeOrder = 7;
      this.menuItem7.Text = "-";
      // 
      // mnuTEExit
      // 
      this.mnuTEExit.Index = 3;
      this.mnuTEExit.MergeOrder = 8;
      this.mnuTEExit.Text = "E&xit";
      // 
      // mnuOptions
      // 
      this.mnuOptions.Index = 1;
      this.mnuOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                               this.mnuPreferences});
      this.mnuOptions.MergeOrder = 5;
      this.mnuOptions.Text = "&Options";
      // 
      // mnuPreferences
      // 
      this.mnuPreferences.Index = 0;
      this.mnuPreferences.Text = "&Preferences";
      // 
      // menuItem4
      // 
      this.menuItem4.Index = 2;
      this.menuItem4.MergeOrder = 6;
      this.menuItem4.Text = "&Help";
      // 
      // statusBar1
      // 
      this.statusBar1.Location = new System.Drawing.Point(0, 244);
      this.statusBar1.Name = "statusBar1";
      this.statusBar1.Size = new System.Drawing.Size(488, 22);
      this.statusBar1.TabIndex = 1;
      this.statusBar1.Text = "statusBar1";
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(488, 266);
      this.Controls.Add(this.statusBar1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.Menu = this.mainMenu1;
      this.Name = "Form1";
      this.Text = "DevGrep Editor";
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			Application.Run(new Form1(args));
		}

    private void OpenFile(string[] args)
    {
      Forms.frmEditor fe = new DGE.Forms.frmEditor(args[0]);
      //fe.FileName = args[0];
      fe.MdiParent = this;
      fe.Show();
    }

	}
}
