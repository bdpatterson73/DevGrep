using System;
using System.ComponentModel;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace DGTe
{
    /// <summary>
    /// Summary description for frmMain.
    /// </summary>
    public class frmMain : Form
    {
        private bool _dirty = false;
        private string _fileName = "";
        private TextEditorControl textEdit;
        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private ToolBar toolBar1;
        private StatusBar statusBar1;
        private StatusBarPanel spLocation;
        private StatusBarPanel spLines;
        private StatusBarPanel spSize;
        private ToolBarButton tbbNew;
        private ToolBarButton tbbOpen;
        private ImageList imageList1;
        private ToolBarButton tbbClose;
        private ToolBarButton tbbSave;
        private StatusBarPanel spDirty;
        private MenuItem mnuNew;
        private MenuItem mnuOpen;
        private MenuItem mnuClose;
        private MenuItem menuItem5;
        private MenuItem mnuSave;
        private MenuItem mnuSaveAs;
        private MenuItem menuItem8;
        private MenuItem mnuExit;
        private StatusBarPanel spHighlightType;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem mnuUndo;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem mnuCut;
        private System.Windows.Forms.MenuItem mnuCopy;
        private System.Windows.Forms.MenuItem mnuPaste;
        private System.Windows.Forms.MenuItem mnuDelete;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem mnuFind;
        private System.Windows.Forms.MenuItem mnuFindNext;
        private System.Windows.Forms.MenuItem mnuReplace;
        private System.Windows.Forms.MenuItem mnuGoto;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem mnuSelectAll;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton tbPrint;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ContextMenu cmTextEdit;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private IContainer components;

        public frmMain(string fileName)
        {
            InitializeComponent();
            Setup(fileName, 1, 1);

        }

        public frmMain(string fileName, int line)
        {
            InitializeComponent();
            Setup(fileName, line, 1);

        }

        public frmMain(string fileName, int line, int column)
        {
            InitializeComponent();
            Setup(fileName, line, column);

        }

        private void Setup(string fileName, int line, int column)
        {
            #region Custom Events
            textEdit.ActiveTextAreaControl.Caret.PositionChanged += new EventHandler(Caret_PositionChanged);
            textEdit.ActiveTextAreaControl.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
            textEdit.ActiveTextAreaControl.Document.UpdateCommited += new EventHandler(Document_UpdateCommited);
            textEdit.ActiveTextAreaControl.Document.TextContentChanged += new EventHandler(Document_TextContentChanged);
            #endregion

            this._fileName = fileName;
            this.Text += " [" + this._fileName + "]";
            textEdit.LoadFile(this._fileName, true);
            this.textEdit.ActiveTextAreaControl.Caret.Column = column - 1;
            this.textEdit.ActiveTextAreaControl.Caret.Line = line - 1;
            //TODO set line and column.
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
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
            this.textEdit = new ICSharpCode.TextEditor.TextEditorControl();
            this.cmTextEdit = new System.Windows.Forms.ContextMenu();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuNew = new System.Windows.Forms.MenuItem();
            this.mnuOpen = new System.Windows.Forms.MenuItem();
            this.mnuClose = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.mnuSave = new System.Windows.Forms.MenuItem();
            this.mnuSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuUndo = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mnuCut = new System.Windows.Forms.MenuItem();
            this.mnuCopy = new System.Windows.Forms.MenuItem();
            this.mnuPaste = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.mnuFind = new System.Windows.Forms.MenuItem();
            this.mnuFindNext = new System.Windows.Forms.MenuItem();
            this.mnuReplace = new System.Windows.Forms.MenuItem();
            this.mnuGoto = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.mnuSelectAll = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbbNew = new System.Windows.Forms.ToolBarButton();
            this.tbbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbbClose = new System.Windows.Forms.ToolBarButton();
            this.tbbSave = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbPrint = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.spLocation = new System.Windows.Forms.StatusBarPanel();
            this.spLines = new System.Windows.Forms.StatusBarPanel();
            this.spSize = new System.Windows.Forms.StatusBarPanel();
            this.spDirty = new System.Windows.Forms.StatusBarPanel();
            this.spHighlightType = new System.Windows.Forms.StatusBarPanel();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.spLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDirty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spHighlightType)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit
            // 
            this.textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit.ContextMenu = this.cmTextEdit;
            this.textEdit.Encoding = ((System.Text.Encoding)(resources.GetObject("textEdit.Encoding")));
            this.textEdit.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.FullRow;
            this.textEdit.Location = new System.Drawing.Point(0, 48);
            this.textEdit.Name = "textEdit";
            this.textEdit.ShowEOLMarkers = true;
            this.textEdit.ShowSpaces = true;
            this.textEdit.ShowTabs = true;
            this.textEdit.ShowVRuler = true;
            this.textEdit.Size = new System.Drawing.Size(600, 328);
            this.textEdit.TabIndex = 0;
            this.textEdit.LocationChanged += new System.EventHandler(this.textEdit_LocationChanged);
            // 
            // cmTextEdit
            // 
            this.cmTextEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                       this.menuItem6,
                                                                                       this.menuItem9,
                                                                                       this.menuItem10});
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Cut";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "Copy";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 2;
            this.menuItem10.Text = "Paste";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.menuItem1,
                                                                                      this.menuItem2,
                                                                                      this.menuItem3,
                                                                                      this.menuItem4});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.mnuNew,
                                                                                      this.mnuOpen,
                                                                                      this.mnuClose,
                                                                                      this.menuItem5,
                                                                                      this.mnuSave,
                                                                                      this.mnuSaveAs,
                                                                                      this.menuItem8,
                                                                                      this.mnuExit});
            this.menuItem1.Text = "&File";
            // 
            // mnuNew
            // 
            this.mnuNew.Enabled = false;
            this.mnuNew.Index = 0;
            this.mnuNew.Text = "New";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Enabled = false;
            this.mnuOpen.Index = 1;
            this.mnuOpen.Text = "Open";
            // 
            // mnuClose
            // 
            this.mnuClose.Enabled = false;
            this.mnuClose.Index = 2;
            this.mnuClose.Text = "Close";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "-";
            // 
            // mnuSave
            // 
            this.mnuSave.Index = 4;
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Enabled = false;
            this.mnuSaveAs.Index = 5;
            this.mnuSaveAs.Text = "Save As...";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 6;
            this.menuItem8.Text = "-";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 7;
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.mnuUndo,
                                                                                      this.menuItem7,
                                                                                      this.mnuCut,
                                                                                      this.mnuCopy,
                                                                                      this.mnuPaste,
                                                                                      this.mnuDelete,
                                                                                      this.menuItem13,
                                                                                      this.mnuFind,
                                                                                      this.mnuFindNext,
                                                                                      this.mnuReplace,
                                                                                      this.mnuGoto,
                                                                                      this.menuItem18,
                                                                                      this.mnuSelectAll});
            this.menuItem2.Text = "&Edit";
            // 
            // mnuUndo
            // 
            this.mnuUndo.Index = 0;
            this.mnuUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.mnuUndo.Text = "&Undo";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "-";
            // 
            // mnuCut
            // 
            this.mnuCut.Index = 2;
            this.mnuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.mnuCut.Text = "Cu&t";
            this.mnuCut.Click += new System.EventHandler(this.mnuCut_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Index = 3;
            this.mnuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuCopy.Text = "&Copy";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuPaste
            // 
            this.mnuPaste.Index = 4;
            this.mnuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.mnuPaste.Text = "&Paste";
            this.mnuPaste.Click += new System.EventHandler(this.mnuPaste_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Index = 5;
            this.mnuDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.mnuDelete.Text = "De&lete";
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 6;
            this.menuItem13.Text = "-";
            // 
            // mnuFind
            // 
            this.mnuFind.Index = 7;
            this.mnuFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.mnuFind.Text = "&Find...";
            this.mnuFind.Click += new System.EventHandler(this.mnuFind_Click);
            // 
            // mnuFindNext
            // 
            this.mnuFindNext.Index = 8;
            this.mnuFindNext.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.mnuFindNext.Text = "Fi&nd Next";
            // 
            // mnuReplace
            // 
            this.mnuReplace.Index = 9;
            this.mnuReplace.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.mnuReplace.Text = "&Replace...";
            // 
            // mnuGoto
            // 
            this.mnuGoto.Index = 10;
            this.mnuGoto.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.mnuGoto.Text = "&Go To...";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 11;
            this.menuItem18.Text = "-";
            // 
            // mnuSelectAll
            // 
            this.mnuSelectAll.Index = 12;
            this.mnuSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.mnuSelectAll.Text = "Select &All";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "F&ormat";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "&View";
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                        this.tbbNew,
                                                                                        this.tbbOpen,
                                                                                        this.tbbClose,
                                                                                        this.tbbSave,
                                                                                        this.toolBarButton1,
                                                                                        this.tbPrint});
            this.toolBar1.ButtonSize = new System.Drawing.Size(21, 21);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(600, 42);
            this.toolBar1.TabIndex = 1;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbbNew
            // 
            this.tbbNew.Enabled = false;
            this.tbbNew.ImageIndex = 1;
            this.tbbNew.Text = "New";
            this.tbbNew.ToolTipText = "New File";
            // 
            // tbbOpen
            // 
            this.tbbOpen.Enabled = false;
            this.tbbOpen.ImageIndex = 2;
            this.tbbOpen.Text = "Open";
            this.tbbOpen.ToolTipText = "Open File";
            // 
            // tbbClose
            // 
            this.tbbClose.Enabled = false;
            this.tbbClose.ImageIndex = 0;
            this.tbbClose.Text = "Close";
            this.tbbClose.ToolTipText = "Close File";
            // 
            // tbbSave
            // 
            this.tbbSave.ImageIndex = 3;
            this.tbbSave.Text = "Save";
            this.tbbSave.ToolTipText = "Save File";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbPrint
            // 
            this.tbPrint.ImageIndex = 4;
            this.tbPrint.Text = "Print";
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 384);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                          this.spLocation,
                                                                                          this.spLines,
                                                                                          this.spSize,
                                                                                          this.spDirty,
                                                                                          this.spHighlightType});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(600, 22);
            this.statusBar1.TabIndex = 2;
            // 
            // spLocation
            // 
            this.spLocation.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.spLocation.MinWidth = 200;
            this.spLocation.Width = 200;
            // 
            // spLines
            // 
            this.spLines.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.spLines.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.spLines.MinWidth = 100;
            // 
            // spSize
            // 
            this.spSize.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spDirty
            // 
            this.spDirty.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.spDirty.MinWidth = 100;
            // 
            // spHighlightType
            // 
            this.spHighlightType.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.spHighlightType.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.spHighlightType.MinWidth = 50;
            this.spHighlightType.Width = 50;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Location = new System.Drawing.Point(88, 116);
            this.printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.TransparencyKey = System.Drawing.Color.Empty;
            this.printPreviewDialog1.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(600, 406);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.textEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DevGrep Editor";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDirty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spHighlightType)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Forms load

            Dirty = false;
        }

        private void textEdit_LocationChanged(object sender, EventArgs e)
        {
        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            int line = this.textEdit.ActiveTextAreaControl.Caret.Line + 1;
            int column = this.textEdit.ActiveTextAreaControl.Caret.Column + 1;
            statusBar1.Panels[0].Text = "Line: " + line.ToString() + " Column: " + column.ToString();
            statusBar1.Panels[1].Text = "Lines: " + this.textEdit.ActiveTextAreaControl.Document.TotalNumberOfLines.ToString();
            statusBar1.Panels[2].Text = this.textEdit.ActiveTextAreaControl.Document.TextLength.ToString() + " bytes.";

        }

        private void Document_DocumentChanged(object sender, DocumentEventArgs e)
        {
            Dirty = true;
        }

        public bool Dirty
        {
            get
            {
                return _dirty;
            }
            set
            {
                _dirty = value;
                statusBar1.Panels[3].Text = (_dirty == true) ? "Dirty" : "Clean";

                // Update Toolbar
                UpdateToolbar();
                //TODO Update menus

            }
        }

        #region UpdateToolbar
        /// <summary>
        /// Updates toolbar buttons depending on the document state.
        /// </summary>
        private void UpdateToolbar()
        {
            if (this.Dirty == true)
            {
                tbbSave.Enabled = true;
                mnuSave.Enabled = true;
            }
            else
            {
                tbbSave.Enabled = false;
                mnuSave.Enabled = false;
            }
        }
        #endregion

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// I have no idea what this does.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Document_UpdateCommited(object sender, EventArgs e)
        {
        }

        private void Document_TextContentChanged(object sender, EventArgs e)
        {
            statusBar1.Panels[4].Text = textEdit.ActiveTextAreaControl.Document.HighlightingStrategy.Name;
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Text.ToUpper())
            {
                case "SAVE":
                    SaveCurrentFile();
                    break;
                case "PRINT":
                    PrintCurrentFile();
                    break;
            }
        }

        #region SaveCurrentFile
        /// <summary>
        /// Saves the current file using the original filename.
        /// </summary>
        private void SaveCurrentFile()
        {
            textEdit.SaveFile(this._fileName);
            Dirty = false;
        }
        #endregion
        private void PrintCurrentFile()
        {
            printPreviewDialog1.Document = textEdit.PrintDocument ;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                textEdit.PrintDocument.Print() ;
            }
            
        }
        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
        }

        private void frmMain_Closing(object sender, CancelEventArgs e)
        {
            if (Dirty == true)
            {
                DialogResult dr = MessageBox.Show("Would you like to save your changes to this document?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                if (dr == DialogResult.Yes)
                {
                    SaveCurrentFile();
                }
            }
        }

        private void mnuCut_Click(object sender, System.EventArgs e)
        {
        Clipboard.SetDataObject(textEdit.ActiveTextAreaControl.SelectionManager.SelectedText);
            textEdit.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
        }

        private void mnuCopy_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetDataObject(textEdit.ActiveTextAreaControl.SelectionManager.SelectedText);
        }

        private void mnuPaste_Click(object sender, System.EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
 
            // Determines whether the data is in a format you can use.
            if(iData.GetDataPresent(DataFormats.Text)) 
            {
                // Yes it is, so display it in a text box.
                string clipText = (String)iData.GetData(DataFormats.Text); 
                //textEdit.ActiveTextAreaControl.SelectionManager.Insert();
            }

        }

        private void menuItem6_Click(object sender, System.EventArgs e)
        {
            mnuCut_Click(this,null);
        }

        private void menuItem9_Click(object sender, System.EventArgs e)
        {
        mnuCopy_Click(this,null);
        }

        private void menuItem10_Click(object sender, System.EventArgs e)
        {
            mnuPaste_Click(this,null);
        }

        private void mnuFind_Click(object sender, System.EventArgs e)
        {
            // Open the find dialog.
        }
    }
}