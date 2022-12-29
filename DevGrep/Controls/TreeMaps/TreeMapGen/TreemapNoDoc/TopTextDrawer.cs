using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using DevGrep.Controls.TreeMaps.TreeMapGen.AppLib;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public class TopTextDrawer : TextDrawerBase
    {
        protected const float TextHeightMultiplier = 1.1f;
        protected float m_fFontSizePt;
        protected int m_iMinimumTextHeight;
        protected Color m_oSelectedBackColor;
        protected Color m_oSelectedFontColor;
        protected Color m_oTextColor;
        protected string m_sFontFamily;

        public TopTextDrawer(NodeLevelsWithText eNodeLevelsWithText, int iMinNodeLevelWithText,
                             int iMaxNodeLevelWithText, string sFontFamily, float fFontSizePt, int iMinimumTextHeight,
                             Color oTextColor, Color oSelectedFontColor, Color oSelectedBackColor)
            : base(eNodeLevelsWithText, iMinNodeLevelWithText, iMaxNodeLevelWithText)
        {
            m_sFontFamily = sFontFamily;
            m_fFontSizePt = fFontSizePt;
            m_iMinimumTextHeight = iMinimumTextHeight;
            m_oTextColor = oTextColor;
            m_oSelectedFontColor = oSelectedFontColor;
            m_oSelectedBackColor = oSelectedBackColor;
            AssertValid();
        }

        public override void DrawTextForAllNodes(Graphics oGraphics, Rectangle oTreemapRectangle, Nodes oNodes)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oNodes != null);
            AssertValid();
            FontForRectangle fontForRectangle = null;
            SolidBrush solidBrush = null;
            TextRenderingHint textRenderingHint = 0;
            try
            {
                fontForRectangle = new FontForRectangle(m_sFontFamily, m_fFontSizePt, oGraphics);
                textRenderingHint = base.SetTextRenderingHint(oGraphics, fontForRectangle.Font);
                solidBrush = new SolidBrush(m_oTextColor);
                StringFormat oNonLeafStringFormat = CreateStringFormat(false);
                StringFormat oLeafStringFormat = CreateStringFormat(true);
                int textHeight = GetTextHeight(oGraphics, fontForRectangle.Font, m_iMinimumTextHeight);
                DrawTextForNodes(oNodes, oGraphics, fontForRectangle, textHeight, solidBrush, null, oNonLeafStringFormat,
                                 oLeafStringFormat, 0);
            }
            finally
            {
                if (solidBrush != null)
                {
                    solidBrush.Dispose();
                }
                if (fontForRectangle != null)
                {
                    fontForRectangle.Dispose();
                }
                oGraphics.TextRenderingHint = (textRenderingHint);
            }
        }

        public override void DrawTextForSelectedNode(Graphics oGraphics, Node oSelectedNode)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oSelectedNode != null);
            AssertValid();
            FontForRectangle fontForRectangle = null;
            SolidBrush solidBrush = null;
            SolidBrush solidBrush2 = null;
            TextRenderingHint textRenderingHint = 0;
            try
            {
                fontForRectangle = new FontForRectangle(m_sFontFamily, m_fFontSizePt, oGraphics);
                textRenderingHint = base.SetTextRenderingHint(oGraphics, fontForRectangle.Font);
                solidBrush = new SolidBrush(m_oSelectedFontColor);
                solidBrush2 = new SolidBrush(m_oSelectedBackColor);
                StringFormat oNonLeafStringFormat = CreateStringFormat(false);
                StringFormat oLeafStringFormat = CreateStringFormat(true);
                int textHeight = GetTextHeight(oGraphics, fontForRectangle.Font, m_iMinimumTextHeight);
                DrawTextForNode(oGraphics, oSelectedNode, fontForRectangle, textHeight, solidBrush, solidBrush2,
                                oNonLeafStringFormat, oLeafStringFormat);
            }
            finally
            {
                if (solidBrush != null)
                {
                    solidBrush.Dispose();
                }
                if (solidBrush2 != null)
                {
                    solidBrush2.Dispose();
                }
                if (fontForRectangle != null)
                {
                    fontForRectangle.Dispose();
                }
                oGraphics.TextRenderingHint = (textRenderingHint);
            }
        }

        public static int GetTextHeight(Graphics oGraphics, string sFontFamily, float fFontSizePt,
                                        int iMinimumTextHeight)
        {
            Debug.Assert(oGraphics != null);
            StringUtil.AssertNotEmpty(sFontFamily);
            Debug.Assert(fFontSizePt > 0f);
            Debug.Assert(iMinimumTextHeight >= 0);
            FontForRectangle fontForRectangle = null;
            int textHeight;
            try
            {
                fontForRectangle = new FontForRectangle(sFontFamily, fFontSizePt, oGraphics);
                textHeight = GetTextHeight(oGraphics, fontForRectangle.Font, iMinimumTextHeight);
            }
            finally
            {
                if (fontForRectangle != null)
                {
                    fontForRectangle.Dispose();
                }
            }
            return textHeight;
        }

        protected static int GetTextHeight(Graphics oGraphics, Font oFont, int iMinimumTextHeight)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oFont != null);
            Debug.Assert(iMinimumTextHeight >= 0);
            var num = (int) Math.Ceiling((1.1f*oFont.GetHeight(oGraphics)));
            return Math.Max(num, iMinimumTextHeight);
        }

        protected void DrawTextForNodes(Nodes oNodes, Graphics oGraphics, FontForRectangle oFontForRectangle,
                                        int iTextHeight, Brush oTextBrush, Brush oBackgroundBrush,
                                        StringFormat oNonLeafStringFormat, StringFormat oLeafStringFormat,
                                        int iNodeLevel)
        {
            Debug.Assert(oNodes != null);
            Debug.Assert(oGraphics != null);
            Debug.Assert(oFontForRectangle != null);
            Debug.Assert(iTextHeight >= 0);
            Debug.Assert(oTextBrush != null);
            Debug.Assert(oNonLeafStringFormat != null);
            Debug.Assert(oLeafStringFormat != null);
            Debug.Assert(iNodeLevel >= 0);
            foreach (Node current in oNodes)
            {
                if (!current.Rectangle.IsEmpty)
                {
                    if (base.TextShouldBeDrawnForNode(current, iNodeLevel))
                    {
                        DrawTextForNode(oGraphics, current, oFontForRectangle, iTextHeight, oTextBrush, oBackgroundBrush,
                                        oNonLeafStringFormat, oLeafStringFormat);
                    }
                    DrawTextForNodes(current.Nodes, oGraphics, oFontForRectangle, iTextHeight, oTextBrush,
                                     oBackgroundBrush, oNonLeafStringFormat, oLeafStringFormat, iNodeLevel + 1);
                }
            }
        }

        protected void DrawTextForNode(Graphics oGraphics, Node oNode, FontForRectangle oFontForRectangle,
                                       int iTextHeight, Brush oTextBrush, Brush oBackgroundBrush,
                                       StringFormat oNonLeafStringFormat, StringFormat oLeafStringFormat)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oNode != null);
            Debug.Assert(oFontForRectangle != null);
            Debug.Assert(iTextHeight >= 0);
            Debug.Assert(oTextBrush != null);
            Debug.Assert(oNonLeafStringFormat != null);
            Debug.Assert(oLeafStringFormat != null);
            AssertValid();
            bool flag = oNode.Nodes.Count == 0;
            Rectangle rectangleToDraw = oNode.RectangleToDraw;
            int penWidthPx = oNode.PenWidthPx;
            rectangleToDraw.Inflate(-penWidthPx, -penWidthPx);
            Rectangle rectangle;
            if (flag)
            {
                rectangle = rectangleToDraw;
            }
            else
            {
                rectangle = Rectangle.FromLTRB(rectangleToDraw.Left, rectangleToDraw.Top, rectangleToDraw.Right,
                                               rectangleToDraw.Top + iTextHeight);
            }
            int width = rectangle.Width;
            int height = rectangle.Height;
            if (width > 0 && height > 0 && height <= rectangleToDraw.Height)
            {
                if (oBackgroundBrush != null)
                {
                    oGraphics.FillRectangle(oBackgroundBrush, rectangle);
                }
                oGraphics.DrawString(oNode.Text, oFontForRectangle.Font, oTextBrush, rectangle,
                                     flag ? oLeafStringFormat : oNonLeafStringFormat);
            }
        }

        protected StringFormat CreateStringFormat(bool bLeafNode)
        {
            var stringFormat = new StringFormat();
            stringFormat.LineAlignment = (0);
            stringFormat.Alignment = (bLeafNode ? StringAlignment.Near : StringAlignment.Center);
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            return stringFormat;
        }

        public override void AssertValid()
        {
            base.AssertValid();
            StringUtil.AssertNotEmpty(m_sFontFamily);
            Debug.Assert(m_fFontSizePt > 0f);
            Debug.Assert(m_iMinimumTextHeight >= 0);
        }
    }
}