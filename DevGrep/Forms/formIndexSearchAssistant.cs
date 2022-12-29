using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevGrep.Classes;
using DevGrep.Classes.Config;

namespace DevGrep.Forms
{
    public partial class formIndexSearchAssistant : Form
    {
        private bool _IncludeSubdirectories;
        private string _ReturnExtensions = "";
        private string _ReturnPath = "";
        private string _ReturnSearchText = "";

        public formIndexSearchAssistant()
        {
            InitializeComponent();
        }

        private void formIndexSearchAssistant_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            txtSearchLocation.Text = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "LastSearchLocation");
            txtSearchText.Text = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "LastText");
            cbSubdirectories.Checked =
                Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "Subdirectories").ToUpper() == "TRUE"
                    ? true
                    : false;
            AddExtensionList(Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "ExtList"));
            InitFileTypes();
        }

        private void InitFileTypes()
        {
            lbFileTypes.Items.Clear();

            foreach (ExtensionDetail ed in Program.ConfigFile.SavedExtensionList)
            {
                lbFileTypes.Items.Add(ed.Extension);
                //Console.WriteLine(ed.Extension);
            }
        }

        private void AddExtensionList(string ExtensionList)
        {
            string sep = ";";
            string[] results = ExtensionList.Split(sep.ToCharArray());
            foreach (string s in results)
            {
                if (s.Trim().Length != 0)
                {
                    lbSelectedTypes.Items.Add(s);
                }
            }
        }

        public string ReturnSearchText
        {
            get { return _ReturnSearchText; }
        }

        public string ReturnPath
        {
            get { return _ReturnPath; }
        }

        public string ReturnExtensions
        {
            get { return _ReturnExtensions; }
        }

        public bool Subdirectories
        {
            get { return _IncludeSubdirectories; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.SelectedPath = txtSearchLocation.Text;
            fbd.ShowDialog();
            if (fbd.SelectedPath.Trim().Length != 0)
            {
                txtSearchLocation.Text = fbd.SelectedPath;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAdd.Text.Trim().Length != 0)
            {
                if (txtAdd.Text.Substring(0, 1) == ".")
                {
                    lbSelectedTypes.Items.Add(txtAdd.Text);
                    // Add this custom extension type to our standard list of types.
                    if (!Program.ConfigFile.SavedExtensionList.IsExtensionInList(txtAdd.Text))
                    {
                        Program.ConfigFile.SavedExtensionList.Add(new ExtensionDetail(txtAdd.Text, "Custom"));
                        Program.ConfigFile.SavedExtensionList.Save();
                        InitFileTypes();
                    }
                    txtAdd.Text = "";
                }
                else
                {
                    MessageBox.Show("Extensions are in the form of [.ext].", "Invalid Format", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }

        private void btnSelectExt_Click(object sender, EventArgs e)
        {
            if (lbFileTypes.SelectedItems.Count > 0)
            {
                foreach (object o in lbFileTypes.SelectedItems)
                {
                    var s = (string)o;
                    if (StringInListBox(ref lbSelectedTypes, s) == false)
                    {
                        lbSelectedTypes.Items.Add(s);
                    }
                }
            }
        }

        private bool StringInListBox(ref ListBox ctrl, string SearchString)
        {
            foreach (object i in ctrl.Items)
            {
                var s = (string)i;
                if (s.ToUpper() == SearchString.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        private void btnDeselectExt_Click(object sender, EventArgs e)
        {
            // Remove the selected item.
            if (lbSelectedTypes.SelectedItems.Count == 1)
            {
                object o = lbSelectedTypes.SelectedItem;
                lbSelectedTypes.Items.Remove(o);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtSearchText.Text.Trim().Length != 0)
            {
                
                    txtSearchText.Text = Regex.Escape(txtSearchText.Text);
                    Program.ConfigFile.LastSearchType = "TEXT";
                    Program.ConfigFile.Save();
              

                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastSearchLocation", txtSearchLocation.Text);
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastText", txtSearchText.Text);
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ExtList", ExtensionList());
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Subdirectories",
                                           cbSubdirectories.Checked.ToString());
                if (txtSearchLocation.Text.Trim().Length != 0)
                {
                    if (lbSelectedTypes.Items.Count > 0)
                    {
                        _ReturnSearchText = txtSearchText.Text;
                        _ReturnPath = txtSearchLocation.Text;
                        _ReturnExtensions = ExtensionList();
                        _IncludeSubdirectories = cbSubdirectories.Checked;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("You must provide at least 1 file extension to search.", "File Mask",
                                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("You must provide a location to search.", "Search Location", MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("You must provide text to search for.", "Search Text", MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
            }
        }

        private string ExtensionList()
        {
            var sb = new StringBuilder();
            for (int i = 0; i <= lbSelectedTypes.Items.Count - 1; i++)
            {
                sb.Append(lbSelectedTypes.Items[i].ToString());
                if (i != lbSelectedTypes.Items.Count - 1)
                {
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbSelectedTypes.Items.Clear();
        }


    }
}
