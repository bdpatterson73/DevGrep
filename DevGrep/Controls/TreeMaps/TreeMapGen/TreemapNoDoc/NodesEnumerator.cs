using System.Collections;
using System.Diagnostics;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public class NodesEnumerator : IEnumerator
    {
        protected int m_iZeroBasedIndex;
        protected Nodes m_oNodes;

        public NodesEnumerator(Nodes nodes)
        {
            nodes.AssertValid();
            m_iZeroBasedIndex = -1;
            m_oNodes = nodes;
        }

        public Node Current
        {
            get
            {
                AssertValid();
                return m_oNodes[m_iZeroBasedIndex];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                AssertValid();
                return m_oNodes[m_iZeroBasedIndex];
            }
        }

        public bool MoveNext()
        {
            AssertValid();
            bool result;
            if (m_iZeroBasedIndex < m_oNodes.Count - 1)
            {
                m_iZeroBasedIndex++;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void Reset()
        {
            AssertValid();
            m_iZeroBasedIndex = -1;
        }

        [Conditional("DEBUG")]
        protected internal void AssertValid()
        {
            Debug.Assert(m_oNodes != null);
            m_oNodes.AssertValid();
            Debug.Assert(m_iZeroBasedIndex >= -1);
            Debug.Assert(m_iZeroBasedIndex <= m_oNodes.Count);
        }
    }
}