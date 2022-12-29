using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrep.Forms
{
    public partial class dlgHelp : Form
    {
        private string _title;
        private string _message;
        private bool _rtf;
        public dlgHelp(string title, string message,bool rtf)
        {
            _title = title;
            _message = message;
            _rtf = rtf;
            InitializeComponent();
        }

        private void dlgHelp_Load(object sender, EventArgs e)
        {
            label1.Text = _title;
            if (_rtf)
                richTextBox1.Rtf = _message;
            else
                richTextBox1.Text = _message;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
