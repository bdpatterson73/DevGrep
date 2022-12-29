using System.Diagnostics;
using BLS.TreemapCtrl.TreemapNoDoc;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.TreemapNoDoc
{
    public class ZoomedFromOneTopLevelNodeAction : ZoomAction
    {
        protected Node m_oOriginalTopLevelNode;

        public ZoomedFromOneTopLevelNodeAction(ZoomActionHistoryList oZoomActionHistoryList, Node oZoomedNode,
                                               Node oOriginalTopLevelNode) : base(oZoomActionHistoryList, oZoomedNode)
        {
            m_oOriginalTopLevelNode = oOriginalTopLevelNode;
            AssertValid();
        }

        public override bool CanZoomOutFromZoomedNode()
        {
            AssertValid();
            return m_oParentOfZoomedNode != null;
        }

        public override void Undo(TreemapGenerator oTreemapGenerator)
        {
            AssertValid();
            base.Undo(oTreemapGenerator);
            oTreemapGenerator.Clear();
            oTreemapGenerator.Nodes.Add(m_oOriginalTopLevelNode);
        }

        public override void AssertValid()
        {
            base.AssertValid();
            Debug.Assert(m_oOriginalTopLevelNode != null);
        }
    }
}