using System.Diagnostics;
using BLS.TreemapCtrl.TreemapNoDoc;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.TreemapNoDoc
{
    public class ZoomedFromTopLevelAction : ZoomAction
    {
        public ZoomedFromTopLevelAction(ZoomActionHistoryList oZoomActionHistoryList, Node oZoomedNode,
                                        Nodes oOriginalTopLevelNodes) : base(oZoomActionHistoryList, oZoomedNode)
        {
            Debug.Assert(oZoomedNode != null);
            Debug.Assert(oOriginalTopLevelNodes != null);
            oZoomActionHistoryList.SetOriginalTopLevelInfo(oOriginalTopLevelNodes.ToArray(),
                                                           oOriginalTopLevelNodes.EmptySpace.SizeMetric);
            AssertValid();
        }

        public override bool CanZoomOutFromZoomedNode()
        {
            AssertValid();
            return true;
        }

        public override void Undo(TreemapGenerator oTreemapGenerator)
        {
            AssertValid();
            base.Undo(oTreemapGenerator);
            m_oZoomActionHistoryList.RedoOriginalTopLevel(oTreemapGenerator);
        }

        public override void AssertValid()
        {
            base.AssertValid();
            Node[] originalTopLevelNodes = m_oZoomActionHistoryList.OriginalTopLevelNodes;
            float originalTopLevelEmptySpaceSizeMetric = m_oZoomActionHistoryList.OriginalTopLevelEmptySpaceSizeMetric;
            Debug.Assert(originalTopLevelNodes != null);
            Debug.Assert(originalTopLevelNodes.Length > 0);
            Debug.Assert(originalTopLevelEmptySpaceSizeMetric != -3.40282347E+38f);
        }
    }
}