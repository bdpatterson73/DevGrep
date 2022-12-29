using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public interface ITextDrawer
    {
        void DrawTextForAllNodes(Graphics oGraphics, Rectangle oTreemapRectangle, Nodes oNodes);
        void DrawTextForSelectedNode(Graphics oGraphics, Node oSelectedNode);
    }
}