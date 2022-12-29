using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevGrep.Classes;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Forms
{
    internal partial class frmVisualizer : Form
    {
        private SearchTaskCollection _stc;
        public frmVisualizer(SearchTaskCollection stc)
        {
            _stc = stc;
            InitializeComponent();
        }

        private void frmVisualizer_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            comboBox1.Items.Add("File/Count");
            comboBox1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Nodes oNodes = treemapControl1.Nodes;

            Node childNode = new Node("Search Results", 100F, 10F);
            oNodes.Add(childNode);
            Application.DoEvents();
           
           
            foreach (SearchTask s in _stc)
            {

                Node nF = new Node(Path.GetFileNameWithoutExtension(s.TargetFile) + " " + s.MatchesFound, (float)s.MatchesFound, (float)s.MatchesFound*4f, null, s.TargetFile + " - " + s.MatchesFound + " matches.");
                childNode.Nodes.Add(nF);
                Application.DoEvents();
            }
        }
    }
}
