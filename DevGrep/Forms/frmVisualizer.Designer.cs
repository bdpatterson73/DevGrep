namespace DevGrep.Forms
{
    partial class frmVisualizer
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
            this.treemapControl1 = new DevGrep.Controls.TreeMaps.TreeMapCtrl.Treemap.TreemapControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treemapControl1
            // 
            this.treemapControl1.AllowDrag = false;
            this.treemapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treemapControl1.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.treemapControl1.DiscreteNegativeColors = 20;
            this.treemapControl1.DiscretePositiveColors = 20;
            this.treemapControl1.EmptySpaceLocation = DevGrep.Controls.TreeMaps.TreeMapGen.Treemap.EmptySpaceLocation.DeterminedByLayoutAlgorithm;
            this.treemapControl1.FontFamily = "Arial";
            this.treemapControl1.FontSolidColor = System.Drawing.SystemColors.WindowText;
            this.treemapControl1.IsZoomable = false;
            this.treemapControl1.LayoutAlgorithm = DevGrep.Controls.TreeMaps.TreeMapGen.Treemap.LayoutAlgorithm.BottomWeightedSquarified;
            this.treemapControl1.Location = new System.Drawing.Point(12, 41);
            this.treemapControl1.MaxColor = System.Drawing.Color.Green;
            this.treemapControl1.MaxColorMetric = 100F;
            this.treemapControl1.MinColor = System.Drawing.Color.Red;
            this.treemapControl1.MinColorMetric = -100F;
            this.treemapControl1.Name = "treemapControl1";
            this.treemapControl1.NodeColorAlgorithm = DevGrep.Controls.TreeMaps.TreeMapGen.Treemap.NodeColorAlgorithm.UseColorMetric;
            this.treemapControl1.NodeLevelsWithText = DevGrep.Controls.TreeMaps.TreeMapGen.Treemap.NodeLevelsWithText.All;
            this.treemapControl1.PaddingDecrementPerLevelPx = 1;
            this.treemapControl1.PaddingPx = 5;
            this.treemapControl1.PenWidthDecrementPerLevelPx = 1;
            this.treemapControl1.PenWidthPx = 3;
            this.treemapControl1.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.treemapControl1.SelectedFontColor = System.Drawing.SystemColors.HighlightText;
            this.treemapControl1.ShowToolTips = true;
            this.treemapControl1.Size = new System.Drawing.Size(590, 362);
            this.treemapControl1.TabIndex = 0;
            this.treemapControl1.TextLocation = DevGrep.Controls.TreeMaps.TreeMapGen.Treemap.TextLocation.Top;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "<--";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(93, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "-->";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(197, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(339, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Begin";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 415);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treemapControl1);
            this.Name = "frmVisualizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Result Visualizer";
            this.Load += new System.EventHandler(this.frmVisualizer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TreeMaps.TreeMapCtrl.Treemap.TreemapControl treemapControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
    }
}