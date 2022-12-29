using System.Diagnostics;
using System.Windows.Forms;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.Treemap
{
    public class NodeMouseEventArgs : MouseEventArgs
    {
        private readonly Node m_oNode;

        protected internal NodeMouseEventArgs(MouseEventArgs oMouseEventArgs, Node oNode)
            : base(
                oMouseEventArgs.Button, oMouseEventArgs.Clicks, oMouseEventArgs.X, oMouseEventArgs.Y,
                oMouseEventArgs.Delta)
        {
            m_oNode = oNode;
            AssertValid();
        }

        public Node Node
        {
            get
            {
                AssertValid();
                return m_oNode;
            }
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oNode != null);
            m_oNode.AssertValid();
        }
    }
}