using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevGrep.Classes.Config;
using DevGrep.Classes.IndexedFiles;

namespace DevGrep.Forms
{
    public partial class formAddIndex : Form
    {
        private IndexHelper indexer;
        public formAddIndex()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolderNamePath.Text))
            {
                //TODO Make sure this folder hasn't already been indexed, or the sub folder of an index...
                IndexList ll = IndexList.Load();
                if (ll.IsIndexContainedInList(txtFolderNamePath.Text))
                {
                    MessageBox.Show("This location has already been indexed.");
                    return;
                }
                indexer = new IndexHelper(txtFolderNamePath.Text, rbAllFiles.Checked);
                indexer.FileIndexed += indexer_FileIndexed;
                indexer.IndexComplete += indexer_IndexComplete;
                indexer.OptimizingComplete += indexer_OptimizingComplete;
                indexer.OptimizingStarted += indexer_OptimizingStarted;
                btnClose.Enabled = false;
                btnBrowse.Enabled = false;
                btnStart.Enabled = false;
                txtFolderNamePath.Enabled = false;
                rbAllFiles.Enabled = false;
                rbKnownFiles.Enabled = false;
                indexer.BeginIndexFolder();

            }
            else
            {
                MessageBox.Show("You must select a folder to index.");
            }
        }

        void indexer_OptimizingStarted()
        {
            lblStatusMessage.InvokeIfRequired(c => { c.Text = "Optimizing..."; });
        }

        void indexer_OptimizingComplete()
        {
            lblStatusMessage.InvokeIfRequired(c => { c.Text = "Optimizing Complete..."; });
        }

        void indexer_IndexComplete()
        {
            btnBrowse.Enabled = true;
            btnClose.Enabled = true;
            btnStart.Enabled = true;
            txtFolderNamePath.Enabled = true;
            rbAllFiles.Enabled = true;
            rbKnownFiles.Enabled = true;
            lblStatusMessage.InvokeIfRequired(c => { c.Text = ""; });
            Application.DoEvents();
            //TODO Save the details of this index to our folder...
            IndexList il = IndexList.Load();
            IndexDetail id = new IndexDetail(indexer.FolderToIndex, rbKnownFiles.Checked ? "Known" : "All", indexer.IndexFolderFullPath,
                                             DateTime.Now);
            il.Add(id);
            il.Save();
            MessageBox.Show("Index process complete!" + Environment.NewLine + "Files: " + indexer.FilesIndexed+Environment.NewLine + "Time: " + (int)indexer.IndexTime +" seconds.");
        }

        void indexer_FileIndexed(string fileName)
        {
            lblStatusMessage.InvokeIfRequired(c => { c.Text  = fileName; });
            Application.DoEvents();

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select A Folder";
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtFolderNamePath.Text = fbd.SelectedPath;
            }
        }

       
    }
}
