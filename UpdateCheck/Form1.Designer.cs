namespace UpdateCheck
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnUpdateNow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLater = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdateNow
            // 
            this.btnUpdateNow.Location = new System.Drawing.Point(295, 137);
            this.btnUpdateNow.Name = "btnUpdateNow";
            this.btnUpdateNow.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateNow.TabIndex = 0;
            this.btnUpdateNow.Text = "Update Now";
            this.btnUpdateNow.UseVisualStyleBackColor = true;
            this.btnUpdateNow.Click += new System.EventHandler(this.btnUpdateNow_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 125);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnLater
            // 
            this.btnLater.Location = new System.Drawing.Point(214, 137);
            this.btnLater.Name = "btnLater";
            this.btnLater.Size = new System.Drawing.Size(75, 23);
            this.btnLater.TabIndex = 2;
            this.btnLater.Text = "Later";
            this.btnLater.UseVisualStyleBackColor = true;
            this.btnLater.Click += new System.EventHandler(this.btnLater_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 172);
            this.Controls.Add(this.btnLater);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdateNow);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Check for Updates";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateNow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLater;
    }
}

