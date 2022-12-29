using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrep
{
    public partial class formLicense : Form
    {
        public formLicense()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formLicense_Load(object sender, EventArgs e)
        {
            if (Program.dgl != null && Program.dgl.IsLicensed)
            {
                txtRegisteredUser.Text = Program.dgl.LicenseData.RegisteredUser;
                txtCompany.Text = Program.dgl.LicenseData.CompanyName;
                txtEmail.Text = Program.dgl.LicenseData.EmailAddress;
                txtLicenseCount.Text  = Program.dgl.LicenseData.LicenseCount;
                txtLicenseDate.Text = Program.dgl.LicenseData.DateRegistered;
            }
        }
    }
}
