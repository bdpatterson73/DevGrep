using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public interface ILayoutEngine
    {
        void CalculateNodeRectangles(Nodes oNodes, RectangleF oParentRectangle, Node oParentNode,
                                     EmptySpaceLocation eEmptySpaceLocation);

        void SetNodeRectanglesToEmpty(Node oNode);
        void SetNodeRectanglesToEmpty(Nodes oNodes, bool bRecursive);
    }
}