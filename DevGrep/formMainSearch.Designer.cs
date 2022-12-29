namespace DevGrep
{
    partial class formMainSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMainSearch));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.omiOpen = new System.Windows.Forms.RibbonOrbMenuItem();
            this.omiSave = new System.Windows.Forms.RibbonOrbMenuItem();
            this.roobExport = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator7 = new System.Windows.Forms.RibbonSeparator();
            this.rdmiToCSV = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.rdmiToHTML = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.ribbonSeparator5 = new System.Windows.Forms.RibbonSeparator();
            this.omiPrint = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator3 = new System.Windows.Forms.RibbonSeparator();
            this.dmiPrint = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.dmiPrintSetup = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.dmiPrintPreview = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.ribbonSeparator8 = new System.Windows.Forms.RibbonSeparator();
            this.dmiPrintFileList = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.omiSend = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator4 = new System.Windows.Forms.RibbonSeparator();
            this.omiHelp = new System.Windows.Forms.RibbonOrbMenuItem();
            this.roobQuit = new System.Windows.Forms.RibbonOrbOptionButton();
            this.ribbonOrbOptionButton2 = new System.Windows.Forms.RibbonOrbOptionButton();
            this.ribbonOrbRecentItem1 = new System.Windows.Forms.RibbonOrbRecentItem();
            this.rbPriOpenSavedSearch = new System.Windows.Forms.RibbonButton();
            this.rbPriSaveCurrentSearch = new System.Windows.Forms.RibbonButton();
            this.rbPriPrintResults = new System.Windows.Forms.RibbonButton();
            this.rtHome = new System.Windows.Forms.RibbonTab();
            this.rpTasks = new System.Windows.Forms.RibbonPanel();
            this.rbuttonSearch = new System.Windows.Forms.RibbonButton();
            this.rbSearchFileSystem = new System.Windows.Forms.RibbonButton();
            this.rbSearchIndexed = new System.Windows.Forms.RibbonButton();
            this.rbSearchDatabase = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator2 = new System.Windows.Forms.RibbonSeparator();
            this.rbSearchWebCrawl = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.rbReplace = new System.Windows.Forms.RibbonButton();
            this.rbAgain = new System.Windows.Forms.RibbonButton();
            this.rbStop = new System.Windows.Forms.RibbonButton();
            this.rpResults = new System.Windows.Forms.RibbonPanel();
            this.rbCompare = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator6 = new System.Windows.Forms.RibbonSeparator();
            this.rbCombine = new System.Windows.Forms.RibbonButton();
            this.rbuttonVisualize = new System.Windows.Forms.RibbonButton();
            this.tabTools = new System.Windows.Forms.RibbonTab();
            this.panelEditor = new System.Windows.Forms.RibbonPanel();
            this.rbOpenEditor = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator9 = new System.Windows.Forms.RibbonSeparator();
            this.rbDuplicates = new System.Windows.Forms.RibbonButton();
            this.rbSpace = new System.Windows.Forms.RibbonButton();
            this.tabOptions = new System.Windows.Forms.RibbonTab();
            this.panelResults = new System.Windows.Forms.RibbonPanel();
            this.rudPreLines = new System.Windows.Forms.RibbonUpDown();
            this.rudPostLines = new System.Windows.Forms.RibbonUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpace1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.objectListView1 = new DevGrep.Controls.DLV.ObjectListView();
            this.olvName = ((DevGrep.Controls.DLV.OLVColumn)(new DevGrep.Controls.DLV.OLVColumn()));
            this.olvType = ((DevGrep.Controls.DLV.OLVColumn)(new DevGrep.Controls.DLV.OLVColumn()));
            this.olvFolder = ((DevGrep.Controls.DLV.OLVColumn)(new DevGrep.Controls.DLV.OLVColumn()));
            this.olvMatches = ((DevGrep.Controls.DLV.OLVColumn)(new DevGrep.Controls.DLV.OLVColumn()));
            this.olvSize = ((DevGrep.Controls.DLV.OLVColumn)(new DevGrep.Controls.DLV.OLVColumn()));
            this.olvDateTime = ((DevGrep.Controls.DLV.OLVColumn)(new DevGrep.Controls.DLV.OLVColumn()));
            this.cmsFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBoxEx1 = new RichTextBoxEx.RichTextBoxEx();
            this.cmResults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiViewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewLine = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.cmsFiles.SuspendLayout();
            this.cmResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.omiOpen);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.omiSave);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.roobExport);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonSeparator5);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.omiPrint);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.omiSend);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.ribbonSeparator4);
            this.ribbon1.OrbDropDown.MenuItems.Add(this.omiHelp);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.OptionItems.Add(this.roobQuit);
            this.ribbon1.OrbDropDown.OptionItems.Add(this.ribbonOrbOptionButton2);
            this.ribbon1.OrbDropDown.RecentItems.Add(this.ribbonOrbRecentItem1);
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 342);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbImage = null;
            this.ribbon1.OrbText = "";
            // 
            // 
            // 
            this.ribbon1.QuickAcessToolbar.Items.Add(this.rbPriOpenSavedSearch);
            this.ribbon1.QuickAcessToolbar.Items.Add(this.rbPriSaveCurrentSearch);
            this.ribbon1.QuickAcessToolbar.Items.Add(this.rbPriPrintResults);
            this.ribbon1.Size = new System.Drawing.Size(657, 131);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.Tabs.Add(this.rtHome);
            this.ribbon1.Tabs.Add(this.tabTools);
            this.ribbon1.Tabs.Add(this.tabOptions);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbon1.Text = "ribbon1";
            // 
            // omiOpen
            // 
            this.omiOpen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.omiOpen.Image = global::DevGrep.Properties.Resources.open32;
            this.omiOpen.SmallImage = global::DevGrep.Properties.Resources.open32;
            this.omiOpen.Text = "Open";
            // 
            // omiSave
            // 
            this.omiSave.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.omiSave.Image = global::DevGrep.Properties.Resources.saveas32;
            this.omiSave.SmallImage = global::DevGrep.Properties.Resources.saveas32;
            this.omiSave.Text = "Save";
            // 
            // roobExport
            // 
            this.roobExport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.roobExport.DropDownItems.Add(this.ribbonSeparator7);
            this.roobExport.DropDownItems.Add(this.rdmiToCSV);
            this.roobExport.DropDownItems.Add(this.rdmiToHTML);
            this.roobExport.Image = global::DevGrep.Properties.Resources.Export32;
            this.roobExport.SmallImage = global::DevGrep.Properties.Resources.Export32;
            this.roobExport.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            this.roobExport.Text = "Export";
            // 
            // ribbonSeparator7
            // 
            this.ribbonSeparator7.Text = "Export To Other Formats";
            // 
            // rdmiToCSV
            // 
            this.rdmiToCSV.Description = "Export all fields to a CSV file.";
            this.rdmiToCSV.DescriptionBounds = new System.Drawing.Rectangle(46, 47, 315, 28);
            this.rdmiToCSV.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rdmiToCSV.Image = global::DevGrep.Properties.Resources.CSV32;
            this.rdmiToCSV.SmallImage = global::DevGrep.Properties.Resources.CSV32;
            this.rdmiToCSV.Text = "To CSV";
            this.rdmiToCSV.Click += new System.EventHandler(this.rdmiToCSV_Click);
            // 
            // rdmiToHTML
            // 
            this.rdmiToHTML.Description = "Export to a formatted HTML file.";
            this.rdmiToHTML.DescriptionBounds = new System.Drawing.Rectangle(46, 100, 315, 28);
            this.rdmiToHTML.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rdmiToHTML.Image = global::DevGrep.Properties.Resources.ExportHTML32;
            this.rdmiToHTML.SmallImage = global::DevGrep.Properties.Resources.ExportHTML32;
            this.rdmiToHTML.Text = "To HTML";
            this.rdmiToHTML.Click += new System.EventHandler(this.rdmiToHTML_Click);
            // 
            // omiPrint
            // 
            this.omiPrint.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.omiPrint.DropDownItems.Add(this.ribbonSeparator3);
            this.omiPrint.DropDownItems.Add(this.dmiPrint);
            this.omiPrint.DropDownItems.Add(this.dmiPrintSetup);
            this.omiPrint.DropDownItems.Add(this.dmiPrintPreview);
            this.omiPrint.DropDownItems.Add(this.ribbonSeparator8);
            this.omiPrint.DropDownItems.Add(this.dmiPrintFileList);
            this.omiPrint.Image = global::DevGrep.Properties.Resources.print32;
            this.omiPrint.SmallImage = global::DevGrep.Properties.Resources.print32;
            this.omiPrint.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            this.omiPrint.Text = "Print";
            // 
            // ribbonSeparator3
            // 
            this.ribbonSeparator3.Text = "Preview and print the document";
            // 
            // dmiPrint
            // 
            this.dmiPrint.Description = "Select a printer, number of copies and other options before printing";
            this.dmiPrint.DescriptionBounds = new System.Drawing.Rectangle(46, 47, 315, 28);
            this.dmiPrint.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.dmiPrint.Image = global::DevGrep.Properties.Resources.print32;
            this.dmiPrint.SmallImage = global::DevGrep.Properties.Resources.print32;
            this.dmiPrint.Text = "Print";
            this.dmiPrint.Click += new System.EventHandler(this.dmiPrint_Click);
            // 
            // dmiPrintSetup
            // 
            this.dmiPrintSetup.Description = "View and make changes to the printer.";
            this.dmiPrintSetup.DescriptionBounds = new System.Drawing.Rectangle(46, 100, 315, 28);
            this.dmiPrintSetup.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.dmiPrintSetup.Image = global::DevGrep.Properties.Resources.printquick32;
            this.dmiPrintSetup.SmallImage = global::DevGrep.Properties.Resources.printquick32;
            this.dmiPrintSetup.Text = "Print Setup";
            this.dmiPrintSetup.Click += new System.EventHandler(this.dmiPrintSetup_Click);
            // 
            // dmiPrintPreview
            // 
            this.dmiPrintPreview.Description = "Print preview the results currently displayed in the results list.";
            this.dmiPrintPreview.DescriptionBounds = new System.Drawing.Rectangle(46, 153, 315, 28);
            this.dmiPrintPreview.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.dmiPrintPreview.Image = global::DevGrep.Properties.Resources.printpreview32;
            this.dmiPrintPreview.SmallImage = global::DevGrep.Properties.Resources.printpreview32;
            this.dmiPrintPreview.Text = "Print Preview";
            this.dmiPrintPreview.Click += new System.EventHandler(this.dmiPrintPreview_Click);
            // 
            // dmiPrintFileList
            // 
            this.dmiPrintFileList.Description = "Print a list of files in the current search results.";
            this.dmiPrintFileList.DescriptionBounds = new System.Drawing.Rectangle(46, 210, 315, 28);
            this.dmiPrintFileList.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.dmiPrintFileList.Image = global::DevGrep.Properties.Resources.ExportHTML32;
            this.dmiPrintFileList.SmallImage = global::DevGrep.Properties.Resources.ExportHTML32;
            this.dmiPrintFileList.Text = "Print File List";
            this.dmiPrintFileList.Click += new System.EventHandler(this.dmiPrintFileList_Click);
            // 
            // omiSend
            // 
            this.omiSend.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.omiSend.Enabled = false;
            this.omiSend.Image = global::DevGrep.Properties.Resources.send32;
            this.omiSend.SmallImage = global::DevGrep.Properties.Resources.send32;
            this.omiSend.Text = "Send";
            // 
            // omiHelp
            // 
            this.omiHelp.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.omiHelp.Image = global::DevGrep.Properties.Resources.Help;
            this.omiHelp.SmallImage = global::DevGrep.Properties.Resources.Help;
            this.omiHelp.Text = "Help";
            // 
            // roobQuit
            // 
            this.roobQuit.Image = global::DevGrep.Properties.Resources.exit16;
            this.roobQuit.SmallImage = global::DevGrep.Properties.Resources.exit16;
            this.roobQuit.Text = "Exit DevGrep";
            this.roobQuit.Click += new System.EventHandler(this.roobQuit_Click);
            // 
            // ribbonOrbOptionButton2
            // 
            this.ribbonOrbOptionButton2.Image = global::DevGrep.Properties.Resources.options16;
            this.ribbonOrbOptionButton2.SmallImage = global::DevGrep.Properties.Resources.options16;
            this.ribbonOrbOptionButton2.Text = "DevGrep Options";
            // 
            // ribbonOrbRecentItem1
            // 
            this.ribbonOrbRecentItem1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem1.Image")));
            this.ribbonOrbRecentItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbRecentItem1.SmallImage")));
            this.ribbonOrbRecentItem1.Text = "RecentFile.bla";
            // 
            // rbPriOpenSavedSearch
            // 
            this.rbPriOpenSavedSearch.Image = ((System.Drawing.Image)(resources.GetObject("rbPriOpenSavedSearch.Image")));
            this.rbPriOpenSavedSearch.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbPriOpenSavedSearch.SmallImage = global::DevGrep.Properties.Resources.open16;
            this.rbPriOpenSavedSearch.Text = "Open";
            this.rbPriOpenSavedSearch.ToolTip = "Open a saved search.";
            this.rbPriOpenSavedSearch.Click += new System.EventHandler(this.rbPriOpenSavedSearch_Click);
            // 
            // rbPriSaveCurrentSearch
            // 
            this.rbPriSaveCurrentSearch.Image = ((System.Drawing.Image)(resources.GetObject("rbPriSaveCurrentSearch.Image")));
            this.rbPriSaveCurrentSearch.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbPriSaveCurrentSearch.SmallImage = global::DevGrep.Properties.Resources.save16;
            this.rbPriSaveCurrentSearch.Text = "ribbonButton2";
            this.rbPriSaveCurrentSearch.Click += new System.EventHandler(this.rbPriSaveCurrentSearch_Click);
            // 
            // rbPriPrintResults
            // 
            this.rbPriPrintResults.Image = ((System.Drawing.Image)(resources.GetObject("rbPriPrintResults.Image")));
            this.rbPriPrintResults.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbPriPrintResults.SmallImage = global::DevGrep.Properties.Resources.printquick16;
            this.rbPriPrintResults.Text = "ribbonButton3";
            this.rbPriPrintResults.Click += new System.EventHandler(this.rbPriPrintResults_Click);
            // 
            // rtHome
            // 
            this.rtHome.Panels.Add(this.rpTasks);
            this.rtHome.Panels.Add(this.rpResults);
            this.rtHome.Text = "Home";
            // 
            // rpTasks
            // 
            this.rpTasks.Items.Add(this.rbuttonSearch);
            this.rpTasks.Items.Add(this.ribbonSeparator1);
            this.rpTasks.Items.Add(this.rbReplace);
            this.rpTasks.Items.Add(this.rbAgain);
            this.rpTasks.Items.Add(this.rbStop);
            this.rpTasks.Text = "Tasks";
            // 
            // rbuttonSearch
            // 
            this.rbuttonSearch.DropDownItems.Add(this.rbSearchFileSystem);
            this.rbuttonSearch.DropDownItems.Add(this.rbSearchIndexed);
            this.rbuttonSearch.DropDownItems.Add(this.rbSearchDatabase);
            this.rbuttonSearch.DropDownItems.Add(this.ribbonSeparator2);
            this.rbuttonSearch.DropDownItems.Add(this.rbSearchWebCrawl);
            this.rbuttonSearch.Image = global::DevGrep.Properties.Resources.Search;
            this.rbuttonSearch.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbuttonSearch.SmallImage = global::DevGrep.Properties.Resources.Search16;
            this.rbuttonSearch.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            this.rbuttonSearch.Text = "Search";
            this.rbuttonSearch.Click += new System.EventHandler(this.rbuttonSearch_Click);
            // 
            // rbSearchFileSystem
            // 
            this.rbSearchFileSystem.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbSearchFileSystem.Image = ((System.Drawing.Image)(resources.GetObject("rbSearchFileSystem.Image")));
            this.rbSearchFileSystem.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSearchFileSystem.SmallImage")));
            this.rbSearchFileSystem.Text = "File System";
            this.rbSearchFileSystem.Click += new System.EventHandler(this.rbSearchFileSystem_Click);
            // 
            // rbSearchIndexed
            // 
            this.rbSearchIndexed.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbSearchIndexed.Image = ((System.Drawing.Image)(resources.GetObject("rbSearchIndexed.Image")));
            this.rbSearchIndexed.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSearchIndexed.SmallImage")));
            this.rbSearchIndexed.Text = "Indexed";
            // 
            // rbSearchDatabase
            // 
            this.rbSearchDatabase.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbSearchDatabase.Enabled = false;
            this.rbSearchDatabase.Image = ((System.Drawing.Image)(resources.GetObject("rbSearchDatabase.Image")));
            this.rbSearchDatabase.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSearchDatabase.SmallImage")));
            this.rbSearchDatabase.Text = "Database";
            // 
            // rbSearchWebCrawl
            // 
            this.rbSearchWebCrawl.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.rbSearchWebCrawl.Enabled = false;
            this.rbSearchWebCrawl.Image = ((System.Drawing.Image)(resources.GetObject("rbSearchWebCrawl.Image")));
            this.rbSearchWebCrawl.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSearchWebCrawl.SmallImage")));
            this.rbSearchWebCrawl.Text = "Web Crawler";
            // 
            // rbReplace
            // 
            this.rbReplace.Image = ((System.Drawing.Image)(resources.GetObject("rbReplace.Image")));
            this.rbReplace.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbReplace.SmallImage = global::DevGrep.Properties.Resources.Replace16;
            this.rbReplace.Text = "Replace";
            this.rbReplace.Click += new System.EventHandler(this.rbReplace_Click);
            // 
            // rbAgain
            // 
            this.rbAgain.Image = ((System.Drawing.Image)(resources.GetObject("rbAgain.Image")));
            this.rbAgain.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbAgain.SmallImage = global::DevGrep.Properties.Resources.SearchAgain16;
            this.rbAgain.Text = "Again";
            this.rbAgain.ToolTip = "Repeat the last search";
            this.rbAgain.Click += new System.EventHandler(this.rbAgain_Click);
            // 
            // rbStop
            // 
            this.rbStop.Image = ((System.Drawing.Image)(resources.GetObject("rbStop.Image")));
            this.rbStop.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbStop.SmallImage = global::DevGrep.Properties.Resources.stop;
            this.rbStop.Text = "Stop";
            this.rbStop.ToolTip = "Stop the current search task.";
            this.rbStop.Click += new System.EventHandler(this.rbStop_Click);
            // 
            // rpResults
            // 
            this.rpResults.Items.Add(this.rbCompare);
            this.rpResults.Items.Add(this.ribbonSeparator6);
            this.rpResults.Items.Add(this.rbCombine);
            this.rpResults.Items.Add(this.rbuttonVisualize);
            this.rpResults.Text = "Results";
            // 
            // rbCompare
            // 
            this.rbCompare.Image = global::DevGrep.Properties.Resources.Compare32;
            this.rbCompare.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbCompare.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbCompare.SmallImage")));
            this.rbCompare.Text = "Compare";
            this.rbCompare.ToolTip = "Compare the results of a previous search to a new one to determine what has chang" +
    "ed.";
            this.rbCompare.ToolTipImage = global::DevGrep.Properties.Resources.Compare32;
            this.rbCompare.ToolTipTitle = "Compare Results";
            // 
            // rbCombine
            // 
            this.rbCombine.Image = ((System.Drawing.Image)(resources.GetObject("rbCombine.Image")));
            this.rbCombine.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbCombine.SmallImage = global::DevGrep.Properties.Resources.Combine16;
            this.rbCombine.Text = "Combine";
            this.rbCombine.ToolTip = "Combine all search results and matching lines in to one listing.";
            this.rbCombine.ToolTipTitle = "Aggregate Results";
            this.rbCombine.Click += new System.EventHandler(this.rbCombine_Click);
            // 
            // rbuttonVisualize
            // 
            this.rbuttonVisualize.Image = ((System.Drawing.Image)(resources.GetObject("rbuttonVisualize.Image")));
            this.rbuttonVisualize.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbuttonVisualize.SmallImage = global::DevGrep.Properties.Resources.graphhs;
            this.rbuttonVisualize.Text = "Visualize";
            this.rbuttonVisualize.ToolTip = "View search results in a Treemap.";
            this.rbuttonVisualize.ToolTipTitle = "The Big Picture";
            this.rbuttonVisualize.Click += new System.EventHandler(this.rbuttonVisualize_Click);
            // 
            // tabTools
            // 
            this.tabTools.Panels.Add(this.panelEditor);
            this.tabTools.Text = "Tools";
            // 
            // panelEditor
            // 
            this.panelEditor.Items.Add(this.rbOpenEditor);
            this.panelEditor.Items.Add(this.ribbonSeparator9);
            this.panelEditor.Items.Add(this.rbDuplicates);
            this.panelEditor.Items.Add(this.rbSpace);
            this.panelEditor.Text = "Files";
            // 
            // rbOpenEditor
            // 
            this.rbOpenEditor.Image = global::DevGrep.Properties.Resources.Editor;
            this.rbOpenEditor.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.rbOpenEditor.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbOpenEditor.SmallImage")));
            this.rbOpenEditor.Text = "Editor";
            this.rbOpenEditor.ToolTip = "Open the text editor.";
            this.rbOpenEditor.Click += new System.EventHandler(this.rbOpenEditor_Click);
            // 
            // rbDuplicates
            // 
            this.rbDuplicates.Image = global::DevGrep.Properties.Resources.Duplicate;
            this.rbDuplicates.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbDuplicates.SmallImage = global::DevGrep.Properties.Resources.Duplicate;
            this.rbDuplicates.Text = "Duplicates";
            this.rbDuplicates.Click += new System.EventHandler(this.rbDuplicates_Click);
            // 
            // rbSpace
            // 
            this.rbSpace.Image = ((System.Drawing.Image)(resources.GetObject("rbSpace.Image")));
            this.rbSpace.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.rbSpace.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSpace.SmallImage")));
            this.rbSpace.Text = "Space";
            // 
            // tabOptions
            // 
            this.tabOptions.Panels.Add(this.panelResults);
            this.tabOptions.Text = "Options";
            // 
            // panelResults
            // 
            this.panelResults.Items.Add(this.rudPreLines);
            this.panelResults.Items.Add(this.rudPostLines);
            this.panelResults.Text = "Results";
            // 
            // rudPreLines
            // 
            this.rudPreLines.LabelWidth = 70;
            this.rudPreLines.Text = "Pre-Lines";
            this.rudPreLines.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            this.rudPreLines.TextBoxText = "0";
            this.rudPreLines.TextBoxWidth = 50;
            this.rudPreLines.ToolTip = "Determines the number of lines that are displayed before a match.";
            this.rudPreLines.UpButtonClicked += new System.Windows.Forms.MouseEventHandler(this.rudPreLines_UpButtonClicked);
            this.rudPreLines.DownButtonClicked += new System.Windows.Forms.MouseEventHandler(this.rudPreLines_DownButtonClicked);
            // 
            // rudPostLines
            // 
            this.rudPostLines.LabelWidth = 70;
            this.rudPostLines.Text = "Post-Lines";
            this.rudPostLines.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            this.rudPostLines.TextBoxText = "0";
            this.rudPostLines.TextBoxWidth = 50;
            this.rudPostLines.ToolTip = "Determines the number of lines that are displayed after a match.";
            this.rudPostLines.UpButtonClicked += new System.Windows.Forms.MouseEventHandler(this.rudPostLines_UpButtonClicked);
            this.rudPostLines.DownButtonClicked += new System.Windows.Forms.MouseEventHandler(this.rudPostLines_DownButtonClicked);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblInfo,
            this.lblSpace1,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 445);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(657, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(22, 17);
            this.lblInfo.Text = "     ";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpace1
            // 
            this.lblSpace1.AutoSize = false;
            this.lblSpace1.Name = "lblSpace1";
            this.lblSpace1.Size = new System.Drawing.Size(30, 17);
            // 
            // statusLabel
            // 
            this.statusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(22, 17);
            this.statusLabel.Text = "     ";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 137);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.objectListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxEx1);
            this.splitContainer1.Size = new System.Drawing.Size(633, 293);
            this.splitContainer1.SplitterDistance = 162;
            this.splitContainer1.TabIndex = 2;
            // 
            // objectListView1
            // 
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvName,
            this.olvType,
            this.olvFolder,
            this.olvMatches,
            this.olvSize,
            this.olvDateTime});
            this.objectListView1.ContextMenuStrip = this.cmsFiles;
            this.objectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView1.EmptyListMsg = "No results to display";
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.HelpLink = null;
            this.objectListView1.Location = new System.Drawing.Point(0, 0);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowCommandMenuOnRightClick = true;
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(633, 162);
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
            // 
            // olvName
            // 
            this.olvName.AspectName = "Filename";
            this.olvName.Text = "Name";
            this.olvName.Width = 120;
            // 
            // olvType
            // 
            this.olvType.AspectName = "ExtDesc";
            this.olvType.Text = "Type";
            this.olvType.Width = 120;
            // 
            // olvFolder
            // 
            this.olvFolder.AspectName = "DirName";
            this.olvFolder.Text = "Folder";
            this.olvFolder.Width = 120;
            // 
            // olvMatches
            // 
            this.olvMatches.AspectName = "MatchesFound";
            this.olvMatches.Text = "Matches";
            this.olvMatches.Width = 90;
            // 
            // olvSize
            // 
            this.olvSize.AspectName = "FileLength";
            this.olvSize.AspectToStringFormat = "{0:#,0}";
            this.olvSize.Text = "Size";
            this.olvSize.Width = 90;
            // 
            // olvDateTime
            // 
            this.olvDateTime.AspectName = "CreationTime";
            this.olvDateTime.Text = "Date/Time";
            this.olvDateTime.Width = 110;
            // 
            // cmsFiles
            // 
            this.cmsFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.tsmiOpenFolder,
            this.toolStripSeparator1,
            this.tsmiProperties});
            this.cmsFiles.Name = "cmsFiles";
            this.cmsFiles.Size = new System.Drawing.Size(202, 76);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpen.Size = new System.Drawing.Size(201, 22);
            this.tsmiOpen.Text = "Open";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiOpenFolder
            // 
            this.tsmiOpenFolder.Name = "tsmiOpenFolder";
            this.tsmiOpenFolder.Size = new System.Drawing.Size(201, 22);
            this.tsmiOpenFolder.Text = "Open Containing Folder";
            this.tsmiOpenFolder.Click += new System.EventHandler(this.tsmiOpenFolder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // tsmiProperties
            // 
            this.tsmiProperties.Name = "tsmiProperties";
            this.tsmiProperties.Size = new System.Drawing.Size(201, 22);
            this.tsmiProperties.Text = "Properties";
            this.tsmiProperties.Click += new System.EventHandler(this.tsmiProperties_Click);
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.ContextMenuStrip = this.cmResults;
            this.richTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEx1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.Size = new System.Drawing.Size(633, 127);
            this.richTextBoxEx1.TabIndex = 0;
            this.richTextBoxEx1.Text = "";
            // 
            // cmResults
            // 
            this.cmResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiViewFile,
            this.tsmiViewLine});
            this.cmResults.Name = "cmResults";
            this.cmResults.Size = new System.Drawing.Size(125, 48);
            // 
            // tsmiViewFile
            // 
            this.tsmiViewFile.Name = "tsmiViewFile";
            this.tsmiViewFile.Size = new System.Drawing.Size(124, 22);
            this.tsmiViewFile.Text = "View File";
            this.tsmiViewFile.Click += new System.EventHandler(this.tsmiViewFile_Click);
            // 
            // tsmiViewLine
            // 
            this.tsmiViewLine.Name = "tsmiViewLine";
            this.tsmiViewLine.Size = new System.Drawing.Size(124, 22);
            this.tsmiViewLine.Text = "View Line";
            this.tsmiViewLine.Click += new System.EventHandler(this.tsmiViewLine_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            // 
            // formMainSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 467);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbon1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formMainSearch";
            this.Text = "DevGrep";
            this.Load += new System.EventHandler(this.formMainSearch_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.cmsFiles.ResumeLayout(false);
            this.cmResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonButton rbPriOpenSavedSearch;
        private System.Windows.Forms.RibbonButton rbPriSaveCurrentSearch;
        private System.Windows.Forms.RibbonButton rbPriPrintResults;
        private System.Windows.Forms.RibbonTab rtHome;
        private System.Windows.Forms.RibbonPanel rpTasks;
        private System.Windows.Forms.RibbonButton rbuttonSearch;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.RibbonButton rbReplace;
        private System.Windows.Forms.RibbonButton rbAgain;
        private System.Windows.Forms.RibbonOrbMenuItem omiHelp;
        private System.Windows.Forms.RibbonOrbMenuItem omiSave;
        private System.Windows.Forms.RibbonOrbMenuItem omiPrint;
        private System.Windows.Forms.RibbonOrbMenuItem omiSend;
        private System.Windows.Forms.RibbonOrbMenuItem omiOpen;
        private System.Windows.Forms.RibbonPanel rpResults;
        private System.Windows.Forms.RibbonButton rbCombine;
        private System.Windows.Forms.RibbonButton rbuttonVisualize;
        private System.Windows.Forms.RibbonButton rbSearchFileSystem;
        private System.Windows.Forms.RibbonButton rbSearchIndexed;
        private System.Windows.Forms.RibbonButton rbSearchDatabase;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator2;
        private System.Windows.Forms.RibbonButton rbSearchWebCrawl;
        private System.Windows.Forms.RibbonTab tabTools;
        private System.Windows.Forms.RibbonPanel panelEditor;
        private System.Windows.Forms.RibbonTab tabOptions;
        private System.Windows.Forms.RibbonDescriptionMenuItem dmiPrint;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator3;
        private System.Windows.Forms.RibbonDescriptionMenuItem dmiPrintSetup;
        private System.Windows.Forms.RibbonDescriptionMenuItem dmiPrintPreview;
        private System.Windows.Forms.RibbonOrbOptionButton roobQuit;
        private System.Windows.Forms.RibbonOrbOptionButton ribbonOrbOptionButton2;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator5;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator4;
        private System.Windows.Forms.RibbonButton rbCompare;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator6;
        private System.Windows.Forms.RibbonOrbMenuItem roobExport;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator7;
        private System.Windows.Forms.RibbonDescriptionMenuItem rdmiToCSV;
        private System.Windows.Forms.RibbonDescriptionMenuItem rdmiToHTML;
        private System.Windows.Forms.RibbonOrbRecentItem ribbonOrbRecentItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.DLV.ObjectListView objectListView1;
        private RichTextBoxEx.RichTextBoxEx richTextBoxEx1;
        private Controls.DLV.OLVColumn olvName;
        private Controls.DLV.OLVColumn olvType;
        private Controls.DLV.OLVColumn olvFolder;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private Controls.DLV.OLVColumn olvMatches;
        private Controls.DLV.OLVColumn olvSize;
        private Controls.DLV.OLVColumn olvDateTime;
        private System.Windows.Forms.ToolStripStatusLabel lblSpace1;
        private System.Windows.Forms.ContextMenuStrip cmsFiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiProperties;
        private System.Windows.Forms.ContextMenuStrip cmResults;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewLine;
        private System.Windows.Forms.RibbonButton rbStop;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator8;
        private System.Windows.Forms.RibbonDescriptionMenuItem dmiPrintFileList;
        private System.Windows.Forms.RibbonButton rbOpenEditor;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator9;
        private System.Windows.Forms.RibbonButton rbDuplicates;
        private System.Windows.Forms.RibbonPanel panelResults;
        private System.Windows.Forms.RibbonUpDown rudPreLines;
        private System.Windows.Forms.RibbonUpDown rudPostLines;
        private System.Windows.Forms.RibbonButton rbSpace;
    }
}