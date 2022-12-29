using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevGrep.Classes;
using SmartAssembly.ReportUsage;

namespace DevGrep
{
    public partial class formRegister : Form
    {
        public formRegister()
        {
            InitializeComponent();
        }

        [ReportUsage("Add License")]
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string allText = File.ReadAllText(textBox1.Text);
                DevGrepLicense lic = new DevGrepLicense(allText);
                if (lic.IsLicensed)
                {
                    MessageBox.Show("You license is valid and has been saved! Please restart" + Environment.NewLine +
                                    "DevGrep to apply the license changes.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The file you selected is not a valid license for this version of DevGrep.");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = false;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = ".license";
            ofd.DereferenceLinks = true;
            ofd.Filter = "txt files (*.license)|*.license";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = "C:\\";
            ofd.Multiselect = false;
            ofd.ReadOnlyChecked = true;
            ofd.RestoreDirectory = true;
            ofd.ShowReadOnly = true;
            ofd.Title = "Open License File";
            ofd.ValidateNames = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"http://www.borderlinesoftware.com/products/products-windows/product-devgrep");
        }
    }
}
