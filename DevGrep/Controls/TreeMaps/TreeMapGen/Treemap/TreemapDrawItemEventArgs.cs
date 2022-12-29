using System;
using System.Diagnostics;
using System.Drawing;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.Treemap
{
    public class TreemapDrawItemEventArgs : EventArgs
    {
        private readonly Node m_oNode;

        protected internal TreemapDrawItemEventArgs(Node oNode)
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

        public Rectangle Bounds
        {
            get
            {
                AssertValid();
                return m_oNode.RectangleToDraw;
            }
        }

        public int PenWidthPx
        {
            get
            {
                AssertValid();
                return m_oNode.PenWidthPx;
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