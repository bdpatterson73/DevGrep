using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using DevGrep.Controls.TreeMaps.TreeMapGen.AppLib;
using DevGrep.Controls.TreeMaps.TreeMapGen.GraphicsLib;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public class CenterCenterTextDrawer : TextDrawerBase
    {
        protected float m_fFontIncrementPt;
        protected float m_fFontMaxSizePt;
        protected float m_fFontMinSizePt;
        protected int m_iFontAlphaIncrementPerLevel;
        protected int m_iFontMaxAlpha;
        protected int m_iFontMinAlpha;
        protected Color m_oFontSolidColor;
        protected Color m_oSelectedFontColor;
        protected string m_sFontFamily;

        public CenterCenterTextDrawer(NodeLevelsWithText eNodeLevelsWithText, int iMinNodeLevelWithText,
                                      int iMaxNodeLevelWithText, string sFontFamily, float fFontMinSizePt,
                                      float fFontMaxSizePt, float fFontIncrementPt, Color oFontSolidColor,
                                      int iFontMinAlpha, int iFontMaxAlpha, int iFontAlphaIncrementPerLevel,
                                      Color oSelectedFontColor)
            : base(eNodeLevelsWithText, iMinNodeLevelWithText, iMaxNodeLevelWithText)
        {
            m_sFontFamily = sFontFamily;
            m_fFontMinSizePt = fFontMinSizePt;
            m_fFontMaxSizePt = fFontMaxSizePt;
            m_fFontIncrementPt = fFontIncrementPt;
            m_oFontSolidColor = oFontSolidColor;
            m_iFontMinAlpha = iFontMinAlpha;
            m_iFontMaxAlpha = iFontMaxAlpha;
            m_iFontAlphaIncrementPerLevel = iFontAlphaIncrementPerLevel;
            m_oSelectedFontColor = oSelectedFontColor;
            AssertValid();
        }

        public override void DrawTextForAllNodes(Graphics oGraphics, Rectangle oTreemapRectangle, Nodes oNodes)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oNodes != null);
            AssertValid();
            IFontMapper fontMapper = null;
            TransparentBrushMapper transparentBrushMapper = null;
            TextRenderingHint textRenderingHint = oGraphics.TextRenderingHint;
            try
            {
                fontMapper = CreateFontMapper(oGraphics);
                transparentBrushMapper = CreateTransparentBrushMapper();
                StringFormat oStringFormat = CreateStringFormat();
                DrawTextForNodes(oNodes, oGraphics, fontMapper, oStringFormat, transparentBrushMapper, 0);
            }
            finally
            {
                if (fontMapper != null)
                {
                    fontMapper.Dispose();
                }
                if (transparentBrushMapper != null)
                {
                    transparentBrushMapper.Dispose();
                }
                oGraphics.TextRenderingHint = (textRenderingHint);
            }
        }

        public override void DrawTextForSelectedNode(Graphics oGraphics, Node oSelectedNode)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oSelectedNode != null);
            AssertValid();
            IFontMapper fontMapper = null;
            Brush brush = null;
            TextRenderingHint textRenderingHint = 0;
            try
            {
                fontMapper = CreateFontMapper(oGraphics);
                brush = new SolidBrush(m_oSelectedFontColor);
                Font font;
                string text;
                if (fontMapper.NodeToFont(oSelectedNode, oSelectedNode.Level, oGraphics, out font, out text))
                {
                    textRenderingHint = base.SetTextRenderingHint(oGraphics, font);
                    StringFormat stringFormat = CreateStringFormat();
                    oGraphics.DrawString(text, font, brush, oSelectedNode.Rectangle, stringFormat);
                }
            }
            finally
            {
                if (fontMapper != null)
                {
                    fontMapper.Dispose();
                }
                if (brush != null)
                {
                    brush.Dispose();
                }
                oGraphics.TextRenderingHint = (textRenderingHint);
            }
        }

        protected void DrawTextForNodes(Nodes oNodes, Graphics oGraphics, IFontMapper oFontMapper,
                                        StringFormat oStringFormat, TransparentBrushMapper oTransparentBrushMapper,
                                        int iNodeLevel)
        {
            Debug.Assert(oNodes != null);
            Debug.Assert(oGraphics != null);
            Debug.Assert(oFontMapper != null);
            Debug.Assert(oStringFormat != null);
            Debug.Assert(oTransparentBrushMapper != null);
            Debug.Assert(iNodeLevel >= 0);
            foreach (Node current in oNodes)
            {
                RectangleF rectangle = current.Rectangle;
                if (!rectangle.IsEmpty)
                {
                    Font font;
                    string text;
                    if (base.TextShouldBeDrawnForNode(current, iNodeLevel) &&
                        oFontMapper.NodeToFont(current, iNodeLevel, oGraphics, out font, out text))
                    {
                        base.SetTextRenderingHint(oGraphics, font);
                        Brush brush = oTransparentBrushMapper.LevelToTransparentBrush(iNodeLevel);
                        oGraphics.DrawString(text, font, brush, rectangle, oStringFormat);
                    }
                    DrawTextForNodes(current.Nodes, oGraphics, oFontMapper, oStringFormat, oTransparentBrushMapper,
                                     iNodeLevel + 1);
                }
            }
        }

        protected IFontMapper CreateFontMapper(Graphics oGraphics)
        {
            Debug.Assert(oGraphics != null);
            AssertValid();
            return new MaximizingFontMapper(m_sFontFamily, m_fFontMinSizePt, m_fFontMaxSizePt, m_fFontIncrementPt,
                                            oGraphics);
        }

        protected TransparentBrushMapper CreateTransparentBrushMapper()
        {
            var transparentBrushMapper = new TransparentBrushMapper();
            transparentBrushMapper.Initialize(m_oFontSolidColor, m_iFontMinAlpha, m_iFontMaxAlpha,
                                              m_iFontAlphaIncrementPerLevel);
            return transparentBrushMapper;
        }

        protected StringFormat CreateStringFormat()
        {
            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            return stringFormat;
        }

        public override void AssertValid()
        {
            base.AssertValid();
            StringUtil.AssertNotEmpty(m_sFontFamily);
            Debug.Assert(m_fFontMinSizePt > 0f);
            Debug.Assert(m_fFontMaxSizePt > 0f);
            Debug.Assert(m_fFontMaxSizePt >= m_fFontMinSizePt);
            Debug.Assert(m_fFontIncrementPt > 0f);
            Debug.Assert(m_oFontSolidColor.A == 255);
            Debug.Assert(m_iFontMinAlpha >= 0 && m_iFontMinAlpha <= 255);
            Debug.Assert(m_iFontMaxAlpha >= 0 && m_iFontMaxAlpha <= 255);
            Debug.Assert(m_iFontMaxAlpha >= m_iFontMinAlpha);
            Debug.Assert(m_iFontAlphaIncrementPerLevel > 0);
        }
    }
}