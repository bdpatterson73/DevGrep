using System;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public interface IFontMapper : IDisposable
    {
        bool NodeToFont(Node oNode, int iNodeLevel, Graphics oGraphics, out Font oFont, out string sTextToDraw);
    }
}