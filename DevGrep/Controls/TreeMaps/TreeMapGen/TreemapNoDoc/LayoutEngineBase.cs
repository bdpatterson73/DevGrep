using System.Diagnostics;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public abstract class LayoutEngineBase : ILayoutEngine
    {
        public abstract void CalculateNodeRectangles(Nodes oNodes, RectangleF oParentRectangle, Node oParentNode,
                                                     EmptySpaceLocation eEmptySpaceLocation);

        public void SetNodeRectanglesToEmpty(Node oNode)
        {
            Debug.Assert(oNode != null);
            SetNodeRectangleToEmpty(oNode);
            SetNodeRectanglesToEmpty(oNode.Nodes, true);
        }

        public void SetNodeRectanglesToEmpty(Nodes oNodes, bool bRecursive)
        {
            Debug.Assert(oNodes != null);
            foreach (Node current in oNodes)
            {
                SetNodeRectangleToEmpty(current);
                if (bRecursive)
                {
                    SetNodeRectanglesToEmpty(current.Nodes, true);
                }
            }
        }

        protected void SetNodeRectanglesToEmpty(Node[] aoNodes, int iIndexOfFirstNodeToSet, int iIndexOfLastNodeToSet)
        {
            Debug.Assert(aoNodes != null);
            Debug.Assert(iIndexOfFirstNodeToSet >= 0);
            Debug.Assert(iIndexOfFirstNodeToSet < aoNodes.Length);
            Debug.Assert(iIndexOfLastNodeToSet >= 0);
            Debug.Assert(iIndexOfLastNodeToSet < aoNodes.Length);
            for (int i = iIndexOfFirstNodeToSet; i <= iIndexOfLastNodeToSet; i++)
            {
                Node node = aoNodes[i];
                SetNodeRectangleToEmpty(node);
                SetNodeRectanglesToEmpty(node.Nodes, true);
            }
        }

        protected void SetNodeRectangleToEmpty(Node oNode)
        {
            Debug.Assert(oNode != null);
            oNode.Rectangle = RectangleF.FromLTRB(0f, 0f, 0f, 0f);
        }
    }
}