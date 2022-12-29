using System.Diagnostics;
using BLS.TreemapCtrl.TreemapNoDoc;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.TreemapNoDoc
{
    public class ZoomedFromInnerNodeAction : ZoomAction
    {
        protected Node m_oInnerNode;

        public ZoomedFromInnerNodeAction(ZoomActionHistoryList oZoomActionHistoryList, Node oZoomedNode, Node oInnerNode)
            : base(oZoomActionHistoryList, oZoomedNode)
        {
            m_oInnerNode = oInnerNode;
            AssertValid();
        }

        public override bool CanZoomOutFromZoomedNode()
        {
            AssertValid();
            return m_oParentOfZoomedNode != null || m_oZoomActionHistoryList.OriginalTopLevelNodes.Length != 1;
        }

        public override void Undo(TreemapGenerator oTreemapGenerator)
        {
            AssertValid();
            base.Undo(oTreemapGenerator);
            oTreemapGenerator.Clear();
            oTreemapGenerator.Nodes.Add(m_oInnerNode);
        }

        public override void AssertValid()
        {
            base.AssertValid();
            Debug.Assert(m_oInnerNode != null);
        }
    }
}