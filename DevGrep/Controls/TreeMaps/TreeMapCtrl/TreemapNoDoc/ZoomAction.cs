using System.Diagnostics;
using BLS.TreemapCtrl.TreemapNoDoc;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.TreemapNoDoc
{
    public abstract class ZoomAction
    {
        protected Node m_oParentOfZoomedNode;
        protected ZoomActionHistoryList m_oZoomActionHistoryList;
        protected Node m_oZoomedNode;

        protected ZoomAction(ZoomActionHistoryList oZoomActionHistoryList, Node oZoomedNode)
        {
            m_oZoomActionHistoryList = oZoomActionHistoryList;
            m_oZoomedNode = oZoomedNode;
            if (m_oZoomedNode != null)
            {
                m_oParentOfZoomedNode = oZoomedNode.Parent;
            }
            else
            {
                m_oParentOfZoomedNode = null;
            }
        }

        public Node ParentOfZoomedNode
        {
            get
            {
                AssertValid();
                return m_oParentOfZoomedNode;
            }
        }

        public abstract bool CanZoomOutFromZoomedNode();

        public virtual void Undo(TreemapGenerator oTreemapGenerator)
        {
            AssertValid();
            if (m_oParentOfZoomedNode != null)
            {
                Debug.Assert(oTreemapGenerator.Nodes.Count == 1);
                oTreemapGenerator.Nodes[0].PrivateSetParent(m_oParentOfZoomedNode);
            }
        }

        public void Redo(TreemapGenerator oTreemapGenerator)
        {
            AssertValid();
            Nodes nodes = oTreemapGenerator.Nodes;
            if (m_oZoomedNode == null)
            {
                m_oZoomActionHistoryList.RedoOriginalTopLevel(oTreemapGenerator);
            }
            else
            {
                oTreemapGenerator.Clear();
                nodes.Add(m_oZoomedNode);
            }
        }

        [Conditional("DEBUG")]
        public virtual void AssertValid()
        {
            Debug.Assert(m_oZoomActionHistoryList != null);
        }
    }
}