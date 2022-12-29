using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.Treemap
{
    public class Nodes : IEnumerable
    {
        protected EmptySpace m_oEmptySpace;
        protected ArrayList m_oNodes;
        protected Node m_oParentNode;
        protected TreemapGenerator m_oTreemapGenerator;

        protected internal Nodes(Node oParentNode)
        {
            Initialize(oParentNode);
        }

        public int Count
        {
            get
            {
                AssertValid();
                return m_oNodes.Count;
            }
        }

        public int RecursiveCount
        {
            get
            {
                int num = 0;
                IEnumerator enumerator = m_oNodes.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var node = (Node) enumerator.Current;
                        num += 1 + node.Nodes.RecursiveCount;
                    }
                }
                finally
                {
                    var disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                return num;
            }
        }

        public Node this[int zeroBasedIndex]
        {
            get
            {
                AssertValid();
                int count = m_oNodes.Count;
                if (count == 0)
                {
                    throw new InvalidOperationException("Nodes[]: There are no nodes in the collection.");
                }
                if (zeroBasedIndex < 0 || zeroBasedIndex >= count)
                {
                    throw new ArgumentOutOfRangeException("zeroBasedIndex", zeroBasedIndex,
                                                          "Nodes[]: zeroBasedIndex must be between 0 and Nodes.Count-1.");
                }
                return (Node) m_oNodes[zeroBasedIndex];
            }
        }

        public EmptySpace EmptySpace
        {
            get
            {
                AssertValid();
                return m_oEmptySpace;
            }
        }

        protected internal TreemapGenerator TreemapGenerator
        {
            set
            {
                m_oTreemapGenerator = value;
                IEnumerator enumerator = m_oNodes.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var node = (Node) enumerator.Current;
                        node.TreemapGenerator = value;
                    }
                }
                finally
                {
                    var disposable = enumerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                m_oEmptySpace.TreemapGenerator = value;
                AssertValid();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            AssertValid();
            return new NodesEnumerator(this);
        }

        public Node Add(string text, float sizeMetric, float colorMetric)
        {
            Node.ValidateSizeMetric(sizeMetric, "Nodes.Add()");
            Node.ValidateColorMetric(colorMetric, "Nodes.Add()");
            return Add(new Node(text, sizeMetric, colorMetric));
        }

        public Node Add(string text, float sizeMetric, Color absoluteColor)
        {
            Node.ValidateSizeMetric(sizeMetric, "Nodes.Add()");
            return Add(new Node(text, sizeMetric, absoluteColor));
        }

        public Node Add(string text, float sizeMetric, float colorMetric, object tag)
        {
            Node node = Add(text, sizeMetric, colorMetric);
            node.Tag = tag;
            AssertValid();
            return node;
        }

        public Node Add(string text, float sizeMetric, float colorMetric, object tag, string toolTip)
        {
            Node node = Add(text, sizeMetric, colorMetric);
            node.Tag = tag;
            node.ToolTip = toolTip;
            AssertValid();
            return node;
        }

        public Node Add(Node node)
        {
            m_oNodes.Add(node);
            node.SetParent(m_oParentNode);
            if (m_oTreemapGenerator != null)
            {
                node.TreemapGenerator = m_oTreemapGenerator;
            }
            FireRedrawRequired();
            AssertValid();
            return node;
        }

        public Node[] ToArray()
        {
            var array = new Node[m_oNodes.Count];
            m_oNodes.CopyTo(array);
            return array;
        }

        public NodesEnumerator GetEnumerator()
        {
            AssertValid();
            return new NodesEnumerator(this);
        }

        protected void Initialize(Node oParentNode)
        {
            m_oTreemapGenerator = null;
            m_oParentNode = oParentNode;
            m_oNodes = new ArrayList();
            m_oEmptySpace = new EmptySpace();
        }

        protected internal void Clear()
        {
            AssertValid();
            m_oNodes.Clear();
            m_oEmptySpace = new EmptySpace();
        }

        protected internal bool GetNodeFromPoint(PointF oPointF, out Node oNode)
        {
            IEnumerator enumerator = m_oNodes.GetEnumerator();
            bool result;
            try
            {
                while (enumerator.MoveNext())
                {
                    var node = (Node) enumerator.Current;
                    if (node.GetNodeFromPoint(oPointF, out oNode))
                    {
                        result = true;
                        return result;
                    }
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            oNode = null;
            result = false;
            return result;
        }

        protected internal Node[] ToArraySortedBySizeMetric()
        {
            var array = new Node[m_oNodes.Count];
            m_oNodes.CopyTo(array);
            Array.Sort(array);
            return array;
        }

        protected void FireRedrawRequired()
        {
            if (m_oTreemapGenerator != null)
            {
                m_oTreemapGenerator.FireRedrawRequired();
            }
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oNodes != null);
            Debug.Assert(m_oEmptySpace != null);
            m_oEmptySpace.AssertValid();
        }
    }
}