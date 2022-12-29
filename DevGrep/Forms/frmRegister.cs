using System.ComponentModel;
using System.Windows.Forms;

namespace DevGrep.Forms
{
    /// <summary>
    /// Summary description for frmRegister.
    /// </summary>
    public class frmRegister : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public frmRegister()
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
            // 
            // frmRegister
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(418, 232);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Register";

        }
        #endregion
    }
}