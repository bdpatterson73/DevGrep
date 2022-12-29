namespace DevGrep
{
    partial class formLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLicense));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRegisteredUser = new System.Windows.Forms.TextBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLicenseCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLicenseDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(268, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Registered User:";
            // 
            // txtRegisteredUser
            // 
            this.txtRegisteredUser.Location = new System.Drawing.Point(104, 6);
            this.txtRegisteredUser.Name = "txtRegisteredUser";
            this.txtRegisteredUser.ReadOnly = true;
            this.txtRegisteredUser.Size = new System.Drawing.Size(226, 20);
            this.txtRegisteredUser.TabIndex = 2;
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(104, 32);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(226, 20);
            this.txtCompany.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Company:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(104, 58);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(226, 20);
            this.txtEmail.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email:";
            // 
            // txtLicenseCount
            // 
            this.txtLicenseCount.Location = new System.Drawing.Point(104, 84);
            this.txtLicenseCount.Name = "txtLicenseCount";
            this.txtLicenseCount.ReadOnly = true;
            this.txtLicenseCount.Size = new System.Drawing.Size(226, 20);
            this.txtLicenseCount.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "License Count:";
            // 
            // txtLicenseDate
            // 
            this.txtLicenseDate.Location = new System.Drawing.Point(104, 110);
            this.txtLicenseDate.Name = "txtLicenseDate";
            this.txtLicenseDate.ReadOnly = true;
            this.txtLicenseDate.Size = new System.Drawing.Size(226, 20);
            this.txtLicenseDate.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Licensed On:";
            // 
            // formLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 208);
            this.Controls.Add(this.txtLicenseDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLicenseCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRegisteredUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DevGrep License Information";
            this.Load += new System.EventHandler(this.formLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegisteredUser;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLicenseCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLicenseDate;
        private System.Windows.Forms.Label label5;
    }
}