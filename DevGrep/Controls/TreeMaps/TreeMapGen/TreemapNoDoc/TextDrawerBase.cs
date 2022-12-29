using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public abstract class TextDrawerBase : ITextDrawer
    {
        protected NodeLevelsWithText m_eNodeLevelsWithText;
        protected int m_iMaxNodeLevelWithText;
        protected int m_iMinNodeLevelWithText;

        public TextDrawerBase(NodeLevelsWithText eNodeLevelsWithText, int iMinNodeLevelWithText,
                              int iMaxNodeLevelWithText)
        {
            m_eNodeLevelsWithText = eNodeLevelsWithText;
            m_iMinNodeLevelWithText = iMinNodeLevelWithText;
            m_iMaxNodeLevelWithText = iMaxNodeLevelWithText;
        }

        public abstract void DrawTextForAllNodes(Graphics oGraphics, Rectangle oTreemapRectangle, Nodes oNodes);
        public abstract void DrawTextForSelectedNode(Graphics oGraphics, Node oSelectedNode);

        protected TextRenderingHint SetTextRenderingHint(Graphics oGraphics, Font oFont)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oFont != null);
            AssertValid();
            TextRenderingHint textRenderingHint = oGraphics.TextRenderingHint;
            oGraphics.TextRenderingHint = ((oFont.Size < 3.1f)
                                               ? TextRenderingHint.AntiAlias
                                               : TextRenderingHint.SystemDefault);
            return textRenderingHint;
        }

        protected bool TextShouldBeDrawnForNode(Node oNode, int iNodeLevel)
        {
            Debug.Assert(oNode != null);
            Debug.Assert(iNodeLevel >= 0);
            AssertValid();
            bool result;
            switch (m_eNodeLevelsWithText)
            {
                case NodeLevelsWithText.All:
                    result = true;
                    break;
                case NodeLevelsWithText.None:
                    result = false;
                    break;
                case NodeLevelsWithText.Leaves:
                    result = (oNode.Nodes.Count == 0);
                    break;
                case NodeLevelsWithText.Range:
                    result = (iNodeLevel >= m_iMinNodeLevelWithText && iNodeLevel <= m_iMaxNodeLevelWithText);
                    break;
                default:
                    Debug.Assert(false);
                    result = false;
                    break;
            }
            return result;
        }

        [Conditional("DEBUG")]
        public virtual void AssertValid()
        {
            Debug.Assert(Enum.IsDefined(typeof (NodeLevelsWithText), m_eNodeLevelsWithText));
            Debug.Assert(m_iMinNodeLevelWithText >= 0);
            Debug.Assert(m_iMaxNodeLevelWithText >= 0);
            Debug.Assert(m_iMaxNodeLevelWithText >= m_iMinNodeLevelWithText);
        }
    }
}