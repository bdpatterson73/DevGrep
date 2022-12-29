using System;
using System.Diagnostics;
using DevGrep.Controls.TreeMaps.TreeMapCtrl.AppLib;
using DevGrep.Controls.TreeMaps.TreeMapCtrl.TreemapNoDoc;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace BLS.TreemapCtrl.TreemapNoDoc
{
    public class ZoomActionHistoryList : HistoryList
    {
        protected Node[] m_aoOriginalTopLevelNodes;
        protected float m_fOriginalTopLevelEmptySpaceSizeMetric;

        public ZoomActionHistoryList()
        {
            m_aoOriginalTopLevelNodes = null;
            m_fOriginalTopLevelEmptySpaceSizeMetric = -3.40282347E+38f;
            AssertValid();
        }

        public bool HasCurrentState
        {
            get
            {
                AssertValid();
                return m_iCurrentObjectIndex >= 0;
            }
        }

        public ZoomAction PeekCurrentState
        {
            get
            {
                AssertValid();
                if (!HasCurrentState)
                {
                    throw new InvalidOperationException(
                        "ZoomActionHistoryList.PeekCurrentState: There is no current state.  Check HasCurrentState before calling this.");
                }
                //Debug.Assert(this.m_oStateList.get_Item(this.m_iCurrentObjectIndex) is ZoomAction);
                return (ZoomAction) m_oStateList[m_iCurrentObjectIndex];
            }
        }

        public ZoomAction CurrentState
        {
            get
            {
                AssertValid();
                if (!HasCurrentState)
                {
                    throw new InvalidOperationException(
                        "ZoomActionHistoryList.CurrentState: There is no current state.  Check HasCurrentState before calling this.");
                }
                object obj = m_oStateList[m_iCurrentObjectIndex];
                m_iCurrentObjectIndex--;
                AssertValid();
                base.FireChangeEvent();
                Debug.Assert(obj is ZoomAction);
                return (ZoomAction) obj;
            }
        }

        public Node[] OriginalTopLevelNodes
        {
            get
            {
                AssertValid();
                Debug.Assert(m_aoOriginalTopLevelNodes != null);
                Debug.Assert(m_aoOriginalTopLevelNodes.Length > 0);
                Debug.Assert(m_fOriginalTopLevelEmptySpaceSizeMetric != -3.40282347E+38f);
                return m_aoOriginalTopLevelNodes;
            }
        }

        public float OriginalTopLevelEmptySpaceSizeMetric
        {
            get
            {
                AssertValid();
                Debug.Assert(m_aoOriginalTopLevelNodes != null);
                Debug.Assert(m_aoOriginalTopLevelNodes.Length > 0);
                Debug.Assert(m_fOriginalTopLevelEmptySpaceSizeMetric != -3.40282347E+38f);
                return m_fOriginalTopLevelEmptySpaceSizeMetric;
            }
        }

        public void SetOriginalTopLevelInfo(Node[] aoOriginalTopLevelNodes, float fOriginalTopLevelEmptySpaceSizeMetric)
        {
            m_aoOriginalTopLevelNodes = aoOriginalTopLevelNodes;
            m_fOriginalTopLevelEmptySpaceSizeMetric = fOriginalTopLevelEmptySpaceSizeMetric;
            AssertValid();
        }

        public void RedoOriginalTopLevel(TreemapGenerator oTreemapGenerator)
        {
            Debug.Assert(oTreemapGenerator != null);
            AssertValid();
            oTreemapGenerator.Clear();
            Nodes nodes = oTreemapGenerator.Nodes;
            Debug.Assert(m_aoOriginalTopLevelNodes != null);
            Debug.Assert(m_aoOriginalTopLevelNodes.Length > 0);
            Debug.Assert(m_fOriginalTopLevelEmptySpaceSizeMetric != -3.40282347E+38f);
            oTreemapGenerator.BeginUpdate();
            Node[] aoOriginalTopLevelNodes = m_aoOriginalTopLevelNodes;
            for (int i = 0; i < aoOriginalTopLevelNodes.Length; i++)
            {
                Node node = aoOriginalTopLevelNodes[i];
                nodes.Add(node);
            }
            nodes.EmptySpace.SizeMetric = m_fOriginalTopLevelEmptySpaceSizeMetric;
            oTreemapGenerator.EndUpdate();
        }

        public void Reset()
        {
            base.Reset();
            m_aoOriginalTopLevelNodes = null;
            m_fOriginalTopLevelEmptySpaceSizeMetric = -3.40282347E+38f;
            AssertValid();
        }

        public void AssertValid()
        {
            base.AssertValid();
            if (m_aoOriginalTopLevelNodes != null)
            {
                Debug.Assert(m_aoOriginalTopLevelNodes.Length > 0);
                Debug.Assert(m_fOriginalTopLevelEmptySpaceSizeMetric != -3.40282347E+38f);
            }
        }
    }
}