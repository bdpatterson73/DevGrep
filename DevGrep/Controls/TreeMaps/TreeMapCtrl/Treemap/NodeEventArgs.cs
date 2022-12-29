using System;
using System.Diagnostics;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.Treemap
{
    public class NodeEventArgs : EventArgs
    {
        private readonly Node m_oNode;

        protected internal NodeEventArgs(Node oNode)
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