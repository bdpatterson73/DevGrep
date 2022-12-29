using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DevGrep.Forms
{
	/// <summary>
	/// Summary description for frmDBSeartchAssist.
	/// </summary>
	public class frmDBSeartchAssist : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button button2;
        private Patterson.Windows.Forms.Controls.DBTreeBrowser dbTreeBrowser1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDBSeartchAssist()
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dbTreeBrowser1 = new Patterson.Windows.Forms.Controls.DBTreeBrowser();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(8, 256);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(72, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Remove";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dbTreeBrowser1
            // 
            this.dbTreeBrowser1.ConnectionStrings = null;
            this.dbTreeBrowser1.Location = new System.Drawing.Point(8, 8);
            this.dbTreeBrowser1.Name = "dbTreeBrowser1";
            this.dbTreeBrowser1.Size = new System.Drawing.Size(216, 240);
            this.dbTreeBrowser1.TabIndex = 3;
            // 
            // frmDBSeartchAssist
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(402, 304);
            this.Controls.Add(this.dbTreeBrowser1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDBSeartchAssist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Search Assistant";
            this.ResumeLayout(false);

        }
		#endregion

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            frmSQLLogin fsl = new frmSQLLogin() ;
            fsl.ShowDialog(); 
            if (fsl.DialogResult == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                dbTreeBrowser1.ConnectionStrings = new string[]{fsl.ConnectionString} ;
                dbTreeBrowser1.RefreshValues() ;
                this.Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            dbTreeBrowser1.CheckedNodes();
        }
	}
}
