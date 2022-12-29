using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevGrep.Controls.TreeMaps.TreeMapGen.AppLib;
using DevGrep.Controls.TreeMaps.TreeMapGen.GraphicsLib;
using DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.Treemap
{
    public class TreemapGenerator : ITreemapComponent
    {
        public delegate void TreemapDrawItemEventHandler(
            object sender, TreemapDrawItemEventArgs treemapDrawItemEventArgs);

        public const int MinPaddingPx = 1;
        public const int MaxPaddingPx = 100;
        public const int MinPaddingDecrementPerLevelPx = 0;
        public const int MaxPaddingDecrementPerLevelPx = 99;
        public const int MinPenWidthPx = 1;
        public const int MaxPenWidthPx = 100;
        public const int MinPenWidthDecrementPerLevelPx = 0;
        public const int MaxPenWidthDecrementPerLevelPx = 99;
        public const int MinDiscreteColors = 2;
        public const int MaxDiscreteColors = 50;
        protected const float MinRectangleWidthOrHeightPx = 1f;
        protected bool m_bInBeginUpdate;
        protected EmptySpaceLocation m_eEmptySpaceLocation;
        protected LayoutAlgorithm m_eLayoutAlgorithm;
        protected NodeColorAlgorithm m_eNodeColorAlgorithm;
        protected TextLocation m_eTextLocation;
        protected float m_fFontIncrementPt;
        protected float m_fFontMaxSizePt;
        protected float m_fFontMinSizePt;
        protected float m_fMaxColorMetric;
        protected float m_fMinColorMetric;
        protected int m_iDiscreteNegativeColors;
        protected int m_iDiscretePositiveColors;
        protected int m_iFontAlphaIncrementPerLevel;
        protected int m_iFontMaxAlpha;
        protected int m_iFontMinAlpha;
        protected int m_iMaxNodeLevelWithText;
        protected int m_iMinNodeLevelWithText;
        protected NodeLevelsWithText m_iNodeLevelsWithText;
        protected int m_iPaddingDecrementPerLevelPx;
        protected int m_iPaddingPx;
        protected int m_iPenWidthDecrementPerLevelPx;
        protected int m_iPenWidthPx;
        protected Color m_oBackColor;
        protected Color m_oBorderColor;
        protected Color m_oFontSolidColor;
        protected Color m_oMaxColor;
        protected Color m_oMinColor;
        protected Nodes m_oNodes;
        protected Bitmap m_oSavedSelectedNodeBitmap;
        protected Color m_oSelectedBackColor;
        protected Color m_oSelectedFontColor;
        protected Node m_oSelectedNode;
        protected string m_sFontFamily;

        public TreemapGenerator()
        {
            m_oNodes = new Nodes(null);
            m_oNodes.TreemapGenerator = this;
            m_iPaddingPx = 5;
            m_iPaddingDecrementPerLevelPx = 1;
            m_iPenWidthPx = 3;
            m_iPenWidthDecrementPerLevelPx = 1;
            m_oBackColor = SystemColors.Window;
            m_oBorderColor = SystemColors.WindowFrame;
            m_eNodeColorAlgorithm = NodeColorAlgorithm.UseColorMetric;
            m_oMinColor = Color.Red;
            m_oMaxColor = Color.Green;
            m_fMinColorMetric = -100f;
            m_fMaxColorMetric = 100f;
            m_iDiscretePositiveColors = 20;
            m_iDiscreteNegativeColors = 20;
            m_sFontFamily = "Arial";
            m_fFontMinSizePt = 8f;
            m_fFontMaxSizePt = 100f;
            m_fFontIncrementPt = 2f;
            m_oFontSolidColor = SystemColors.WindowText;
            m_iFontMinAlpha = 105;
            m_iFontMaxAlpha = 255;
            m_iFontAlphaIncrementPerLevel = 50;
            m_oSelectedFontColor = SystemColors.HighlightText;
            m_oSelectedBackColor = SystemColors.Highlight;
            m_iNodeLevelsWithText = NodeLevelsWithText.All;
            m_iMinNodeLevelWithText = 0;
            m_iMaxNodeLevelWithText = 999;
            m_eTextLocation = TextLocation.Top;
            m_eEmptySpaceLocation = EmptySpaceLocation.DeterminedByLayoutAlgorithm;
            m_oSelectedNode = null;
            m_oSavedSelectedNodeBitmap = null;
            m_bInBeginUpdate = false;
            m_eLayoutAlgorithm = LayoutAlgorithm.BottomWeightedSquarified;
        }

        [Browsable(false)]
        public Node SelectedNode
        {
            get
            {
                AssertValid();
                return m_oSelectedNode;
            }
        }

        //{
        //    [MethodImpl(32)]
        //    add
        //    {
        //        this.RedrawRequired = (EventHandler)Delegate.Combine(this.RedrawRequired, value);
        //    }
        //    [MethodImpl(32)]
        //    remove
        //    {
        //        this.RedrawRequired = (EventHandler)Delegate.Remove(this.RedrawRequired, value);
        //    }
        //}
        [Browsable(false)]
        public Nodes Nodes
        {
            get
            {
                AssertValid();
                return m_oNodes;
            }
        }

        [Browsable(false)]
        public string NodesXml
        {
            get
            {
                AssertValid();
                var nodesXmlSerializer = new NodesXmlSerializer();
                return nodesXmlSerializer.SerializeToString(m_oNodes, this);
            }
            set
            {
                CancelSelectedNode();
                var nodesXmlSerializer = new NodesXmlSerializer();
                m_oNodes = nodesXmlSerializer.DeserializeFromString(value, this);
                m_oNodes.TreemapGenerator = this;
                FireRedrawRequired();
                AssertValid();
            }
        }

        public LayoutAlgorithm LayoutAlgorithm
        {
            get
            {
                AssertValid();
                return m_eLayoutAlgorithm;
            }
            set
            {
                if (m_eLayoutAlgorithm != value)
                {
                    m_eLayoutAlgorithm = value;
                    FireRedrawRequired();
                }
            }
        }

        public int PaddingPx
        {
            get
            {
                AssertValid();
                return m_iPaddingPx;
            }
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("PaddingPx", value, string.Concat(new object[]
                        {
                            "TreemapGenerator.PaddingPx: Must be between ",
                            1,
                            " and ",
                            100,
                            "."
                        }));
                }
                if (m_iPaddingPx != value)
                {
                    m_iPaddingPx = value;
                    FireRedrawRequired();
                }
            }
        }

        public int PaddingDecrementPerLevelPx
        {
            get
            {
                AssertValid();
                return m_iPaddingDecrementPerLevelPx;
            }
            set
            {
                if (value < 0 || value > 99)
                {
                    throw new ArgumentOutOfRangeException("PaddingDecrementPerLevelPx", value,
                                                          string.Concat(new object[]
                                                              {
                                                                  "TreemapGenerator.PaddingDecrementPerLevelPx: Must be between "
                                                                  ,
                                                                  0,
                                                                  " and ",
                                                                  99,
                                                                  "."
                                                              }));
                }
                if (m_iPaddingDecrementPerLevelPx != value)
                {
                    m_iPaddingDecrementPerLevelPx = value;
                    FireRedrawRequired();
                }
            }
        }

        public int PenWidthPx
        {
            get
            {
                AssertValid();
                return m_iPenWidthPx;
            }
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("PenWidthPx", value, string.Concat(new object[]
                        {
                            "TreemapGenerator.PenWidthPx: Must be between ",
                            1,
                            " and ",
                            100,
                            "."
                        }));
                }
                if (m_iPenWidthPx != value)
                {
                    m_iPenWidthPx = value;
                    FireRedrawRequired();
                }
            }
        }

        public int PenWidthDecrementPerLevelPx
        {
            get
            {
                AssertValid();
                return m_iPenWidthDecrementPerLevelPx;
            }
            set
            {
                if (value < 0 || value > 99)
                {
                    throw new ArgumentOutOfRangeException("PenWidthDecrementPerLevelPx", value,
                                                          string.Concat(new object[]
                                                              {
                                                                  "TreemapGenerator.PenWidthDecrementPerLevelPx: Must be between "
                                                                  ,
                                                                  0,
                                                                  " and ",
                                                                  99,
                                                                  "."
                                                              }));
                }
                if (m_iPenWidthDecrementPerLevelPx != value)
                {
                    m_iPenWidthDecrementPerLevelPx = value;
                    FireRedrawRequired();
                }
            }
        }

        public Color BackColor
        {
            get
            {
                AssertValid();
                return m_oBackColor;
            }
            set
            {
                if (m_oBackColor != value)
                {
                    m_oBackColor = value;
                    FireRedrawRequired();
                }
            }
        }

        public Color BorderColor
        {
            get
            {
                AssertValid();
                return m_oBorderColor;
            }
            set
            {
                if (m_oBorderColor != value)
                {
                    m_oBorderColor = value;
                    FireRedrawRequired();
                }
            }
        }

        public NodeColorAlgorithm NodeColorAlgorithm
        {
            get
            {
                AssertValid();
                return m_eNodeColorAlgorithm;
            }
            set
            {
                if (m_eNodeColorAlgorithm != value)
                {
                    m_eNodeColorAlgorithm = value;
                    FireRedrawRequired();
                }
            }
        }

        public Color MinColor
        {
            get
            {
                AssertValid();
                return m_oMinColor;
            }
            set
            {
                if (m_oMinColor != value)
                {
                    m_oMinColor = value;
                    FireRedrawRequired();
                }
            }
        }

        public Color MaxColor
        {
            get
            {
                AssertValid();
                return m_oMaxColor;
            }
            set
            {
                if (m_oMaxColor != value)
                {
                    m_oMaxColor = value;
                    FireRedrawRequired();
                }
            }
        }

        public float MinColorMetric
        {
            get
            {
                AssertValid();
                return m_fMinColorMetric;
            }
            set
            {
                if (value >= 0f)
                {
                    throw new ArgumentOutOfRangeException("MinColorMetric", value,
                                                          "TreemapGenerator.MinColorMetric: Must be negative.");
                }
                if (m_fMinColorMetric != value)
                {
                    m_fMinColorMetric = value;
                    FireRedrawRequired();
                }
            }
        }

        public float MaxColorMetric
        {
            get
            {
                AssertValid();
                return m_fMaxColorMetric;
            }
            set
            {
                if (value <= 0f)
                {
                    throw new ArgumentOutOfRangeException("MaxColorMetric", value,
                                                          "TreemapGenerator.MaxColorMetric: Must be positive.");
                }
                if (m_fMaxColorMetric != value)
                {
                    m_fMaxColorMetric = value;
                    FireRedrawRequired();
                }
            }
        }

        public int DiscreteNegativeColors
        {
            get
            {
                AssertValid();
                return m_iDiscreteNegativeColors;
            }
            set
            {
                if (value < 2 || value > 50)
                {
                    throw new ArgumentOutOfRangeException("DiscreteNegativeColors", value, string.Concat(new object[]
                        {
                            "TreemapGenerator.DiscreteNegativeColors: Must be between ",
                            2,
                            " and ",
                            50,
                            "."
                        }));
                }
                if (m_iDiscreteNegativeColors != value)
                {
                    m_iDiscreteNegativeColors = value;
                    FireRedrawRequired();
                }
            }
        }

        public int DiscretePositiveColors
        {
            get
            {
                AssertValid();
                return m_iDiscretePositiveColors;
            }
            set
            {
                if (value < 2 || value > 50)
                {
                    throw new ArgumentOutOfRangeException("DiscretePositiveColors", value, string.Concat(new object[]
                        {
                            "TreemapGenerator.DiscretePositiveColors: Must be between ",
                            2,
                            " and ",
                            50,
                            "."
                        }));
                }
                if (m_iDiscretePositiveColors != value)
                {
                    m_iDiscretePositiveColors = value;
                    FireRedrawRequired();
                }
            }
        }

        public string FontFamily
        {
            get
            {
                AssertValid();
                return m_sFontFamily;
            }
            set
            {
                var font = new Font(value, 8f);
                string name = font.FontFamily.Name;
                font.Dispose();
                if (name.ToLower() != value.ToLower())
                {
                    throw new ArgumentOutOfRangeException("FontFamily", value,
                                                          "TreemapGenerator.FontFamily: No such font.");
                }
                if (m_sFontFamily != value)
                {
                    m_sFontFamily = value;
                    FireRedrawRequired();
                }
            }
        }

        public Color FontSolidColor
        {
            get
            {
                AssertValid();
                return m_oFontSolidColor;
            }
            set
            {
                if (value.A != 255)
                {
                    throw new ArgumentOutOfRangeException("FontSolidColor", value,
                                                          "TreemapGenerator.FontSolidColor: Must not be transparent.");
                }
                if (m_oFontSolidColor != value)
                {
                    m_oFontSolidColor = value;
                    FireRedrawRequired();
                }
            }
        }

        public Color SelectedFontColor
        {
            get
            {
                AssertValid();
                return m_oSelectedFontColor;
            }
            set
            {
                if (m_oSelectedFontColor != value)
                {
                    m_oSelectedFontColor = value;
                    FireRedrawRequired();
                }
            }
        }

        public Color SelectedBackColor
        {
            get
            {
                AssertValid();
                return m_oSelectedBackColor;
            }
            set
            {
                if (m_oSelectedBackColor != value)
                {
                    m_oSelectedBackColor = value;
                    FireRedrawRequired();
                }
            }
        }

        public NodeLevelsWithText NodeLevelsWithText
        {
            get
            {
                AssertValid();
                return m_iNodeLevelsWithText;
            }
            set
            {
                if (m_iNodeLevelsWithText != value)
                {
                    m_iNodeLevelsWithText = value;
                    FireRedrawRequired();
                }
            }
        }

        public TextLocation TextLocation
        {
            get
            {
                AssertValid();
                return m_eTextLocation;
            }
            set
            {
                if (m_eTextLocation != value)
                {
                    m_eTextLocation = value;
                    FireRedrawRequired();
                }
            }
        }

        public EmptySpaceLocation EmptySpaceLocation
        {
            get
            {
                AssertValid();
                return m_eEmptySpaceLocation;
            }
            set
            {
                if (m_eEmptySpaceLocation != value)
                {
                    m_eEmptySpaceLocation = value;
                    FireRedrawRequired();
                }
            }
        }

        public void GetNodeLevelsWithTextRange(out int minLevel, out int maxLevel)
        {
            AssertValid();
            minLevel = m_iMinNodeLevelWithText;
            maxLevel = m_iMaxNodeLevelWithText;
        }

        public void SetNodeLevelsWithTextRange(int minLevel, int maxLevel)
        {
            if (minLevel < 0)
            {
                throw new ArgumentOutOfRangeException("minLevel", minLevel,
                                                      "TreemapGenerator.SetNodeLevelsWithTextRange: Must be >= 0.");
            }
            if (maxLevel < 0)
            {
                throw new ArgumentOutOfRangeException("maxLevel", maxLevel,
                                                      "TreemapGenerator.SetNodeLevelsWithTextRange: Must be >= 0.");
            }
            if (maxLevel < minLevel)
            {
                throw new ArgumentOutOfRangeException("maxLevel", maxLevel,
                                                      "TreemapGenerator.SetNodeLevelsWithTextRange: Must be >= minLevel.");
            }
            m_iMinNodeLevelWithText = minLevel;
            m_iMaxNodeLevelWithText = maxLevel;
            FireRedrawRequired();
            AssertValid();
        }

        public void GetFontSizeRange(out float minSizePt, out float maxSizePt, out float incrementPt)
        {
            AssertValid();
            minSizePt = m_fFontMinSizePt;
            maxSizePt = m_fFontMaxSizePt;
            incrementPt = m_fFontIncrementPt;
        }

        public void SetFontSizeRange(float minSizePt, float maxSizePt, float incrementPt)
        {
            MaximizingFontMapper.ValidateSizeRange(minSizePt, maxSizePt, incrementPt,
                                                   "TreemapGenerator.SetFontSizeRange()");
            m_fFontMinSizePt = minSizePt;
            m_fFontMaxSizePt = maxSizePt;
            m_fFontIncrementPt = incrementPt;
            FireRedrawRequired();
            AssertValid();
        }

        public void GetFontAlphaRange(out int minAlpha, out int maxAlpha, out int incrementPerLevel)
        {
            AssertValid();
            minAlpha = m_iFontMinAlpha;
            maxAlpha = m_iFontMaxAlpha;
            incrementPerLevel = m_iFontAlphaIncrementPerLevel;
        }

        public void SetFontAlphaRange(int minAlpha, int maxAlpha, int incrementPerLevel)
        {
            TransparentBrushMapper.ValidateAlphaRange(minAlpha, maxAlpha, incrementPerLevel,
                                                      "TreemapGenerator.SetFontAlphaRange");
            m_iFontMinAlpha = minAlpha;
            m_iFontMaxAlpha = maxAlpha;
            m_iFontAlphaIncrementPerLevel = incrementPerLevel;
            FireRedrawRequired();
            AssertValid();
        }

        public void BeginUpdate()
        {
            AssertValid();
            m_bInBeginUpdate = true;
        }

        public void EndUpdate()
        {
            AssertValid();
            m_bInBeginUpdate = false;
            FireRedrawRequired();
        }

        public void Clear()
        {
            m_oNodes.Clear();
            CancelSelectedNode();
            FireRedrawRequired();
            AssertValid();
        }

        public event TreemapDrawItemEventHandler DrawItem;
        //{
        //    [MethodImpl(32)]
        //    add
        //    {
        //        this.DrawItem = (TreemapGenerator.TreemapDrawItemEventHandler)Delegate.Combine(this.DrawItem, value);
        //    }
        //    [MethodImpl(32)]
        //    remove
        //    {
        //        this.DrawItem = (TreemapGenerator.TreemapDrawItemEventHandler)Delegate.Remove(this.DrawItem, value);
        //    }
        //}
        public event EventHandler RedrawRequired;

        public void Draw(Bitmap bitmap, bool drawSelection)
        {
            Debug.Assert(bitmap != null);
            AssertValid();
            Rectangle treemapRectangle = Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height);
            Draw(bitmap, drawSelection, treemapRectangle);
        }

        public void Draw(Bitmap bitmap, bool drawSelection, Rectangle treemapRectangle)
        {
            Debug.Assert(bitmap != null);
            AssertValid();
            if (!Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height).Contains(treemapRectangle))
            {
                throw new ArgumentException(
                    "TreemapGenerator.Draw(): treemapRectangle is not contained within the bitmap.");
            }
            Node oSelectedNode = m_oSelectedNode;
            CancelSelectedNode();
            Graphics graphics = Graphics.FromImage(bitmap);
            Draw(graphics, treemapRectangle);
            graphics.Dispose();
            if (drawSelection && oSelectedNode != null)
            {
                SelectNode(oSelectedNode, bitmap);
            }
        }

        public void Draw(Graphics graphics, Rectangle treemapRectangle)
        {
            Debug.Assert(graphics != null);
            Debug.Assert(!treemapRectangle.IsEmpty);
            AssertValid();
            CalculateAndDrawRectangles(graphics, treemapRectangle, m_oNodes, null);
            if (m_iNodeLevelsWithText != NodeLevelsWithText.None)
            {
                DrawText(graphics, treemapRectangle, m_oNodes);
            }
        }

        public void Draw(Rectangle treemapRectangle)
        {
            AssertValid();
            if (DrawItem == null)
            {
                throw new InvalidOperationException(
                    "TreemapGenerator.Draw: The Draw(Rectangle) method initiates owner draw, which requires that the DrawItem event be handled.  The DrawItem event is not being handled.");
            }
            ILayoutEngine oLayoutEngine = CreateLayoutEngine();
            CalculateRectangles(m_oNodes, treemapRectangle, null, m_iPaddingPx, m_iPaddingPx, m_iPenWidthPx,
                                oLayoutEngine);
            DrawNodesByOwnerDraw(m_oNodes);
        }

        public bool GetNodeFromPoint(PointF pointF, out Node node)
        {
            return m_oNodes.GetNodeFromPoint(pointF, out node);
        }

        public bool GetNodeFromPoint(int x, int y, out Node node)
        {
            return GetNodeFromPoint(new PointF(x, y), out node);
        }

        public void SelectNode(Node node, Bitmap bitmap)
        {
            if (node != null)
            {
                node.AssertValid();
                if (node == m_oSelectedNode)
                {
                    return;
                }
            }
            if (bitmap != null)
            {
                Graphics graphics = Graphics.FromImage(bitmap);
                if (m_oSelectedNode != null && m_oSavedSelectedNodeBitmap != null)
                {
                    m_oSelectedNode.AssertValid();
                    Debug.Assert(!m_oSelectedNode.Rectangle.IsEmpty);
                    Debug.Assert(m_oSavedSelectedNodeBitmap != null);
                    int penWidthPx = SetNodePenWidthForSelection(m_oSelectedNode);
                    Rectangle rectangleToDraw = m_oSelectedNode.RectangleToDraw;
                    graphics.DrawImage(m_oSavedSelectedNodeBitmap, rectangleToDraw.X, rectangleToDraw.Y);
                    m_oSelectedNode.PenWidthPx = penWidthPx;
                    CancelSelectedNode();
                }
                if (node != null && node.HasBeenDrawn)
                {
                    Rectangle rectangleToDraw2 = node.RectangleToDraw;
                    m_oSavedSelectedNodeBitmap =
                        bitmap.Clone(
                            Rectangle.FromLTRB(rectangleToDraw2.Left, rectangleToDraw2.Top,
                                               Math.Min(rectangleToDraw2.Right + 1, bitmap.Width),
                                               Math.Min(rectangleToDraw2.Bottom + 1, bitmap.Height)), bitmap.PixelFormat);
                    DrawNodeAsSelected(node, graphics, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                }
                graphics.Dispose();
            }
            m_oSelectedNode = node;
        }

        protected void CalculateAndDrawRectangles(Graphics oGraphics, RectangleF oTreemapRectangle, Nodes oNodes,
                                                  Node oParentNode)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(oNodes != null);
            AssertValid();
            Brush brush = new SolidBrush(m_oBackColor);
            oGraphics.FillRectangle(brush, oTreemapRectangle);
            brush.Dispose();
            ILayoutEngine oLayoutEngine = CreateLayoutEngine();
            CalculateRectangles(oNodes, oTreemapRectangle, oParentNode, GetTopLevelTopPaddingPx(oGraphics), m_iPaddingPx,
                                m_iPenWidthPx, oLayoutEngine);
            ColorGradientMapper colorGradientMapper = null;
            ColorGradientMapper colorGradientMapper2 = null;
            if (m_eNodeColorAlgorithm == NodeColorAlgorithm.UseColorMetric)
            {
                colorGradientMapper = new ColorGradientMapper();
                Debug.Assert(m_fMaxColorMetric > 0f);
                colorGradientMapper.Initialize(oGraphics, 0f, m_fMaxColorMetric, Color.White, m_oMaxColor,
                                               m_iDiscretePositiveColors, true);
                colorGradientMapper2 = new ColorGradientMapper();
                Debug.Assert(m_fMinColorMetric < 0f);
                colorGradientMapper2.Initialize(oGraphics, 0f, -m_fMinColorMetric, Color.White, m_oMinColor,
                                                m_iDiscreteNegativeColors, true);
            }
            var penCache = new PenCache();
            penCache.Initialize(m_oBorderColor);
            DrawRectangles(oNodes, 0, oGraphics, colorGradientMapper2, colorGradientMapper, penCache);
            if (colorGradientMapper2 != null)
            {
                colorGradientMapper2.Dispose();
            }
            if (colorGradientMapper != null)
            {
                colorGradientMapper.Dispose();
            }
            penCache.Dispose();
        }

        protected void CalculateRectangles(Nodes oNodes, RectangleF oParentRectangle, Node oParentNode,
                                           int iTopPaddingPx, int iLeftRightBottomPaddingPx, int iPenWidthPx,
                                           ILayoutEngine oLayoutEngine)
        {
            Debug.Assert(oNodes != null);
            Debug.Assert(iTopPaddingPx > 0);
            Debug.Assert(iLeftRightBottomPaddingPx > 0);
            Debug.Assert(iPenWidthPx > 0);
            Debug.Assert(oLayoutEngine != null);
            AssertValid();
            int num = iTopPaddingPx;
            if (oParentNode == null)
            {
                iTopPaddingPx = iLeftRightBottomPaddingPx;
            }
            if (!AddPaddingToParentRectangle(ref oParentRectangle, ref iTopPaddingPx, ref iLeftRightBottomPaddingPx))
            {
                oLayoutEngine.SetNodeRectanglesToEmpty(oNodes, true);
            }
            else
            {
                if (oParentNode == null)
                {
                    iTopPaddingPx = num;
                }
                oLayoutEngine.CalculateNodeRectangles(oNodes, oParentRectangle, oParentNode, m_eEmptySpaceLocation);
                int num2 = DecrementPadding(iLeftRightBottomPaddingPx);
                int iPenWidthPx2 = DecrementPenWidth(iPenWidthPx);
                int iTopPaddingPx2 = 0;
                switch (m_eTextLocation)
                {
                    case TextLocation.CenterCenter:
                        iTopPaddingPx2 = num2;
                        break;
                    case TextLocation.Top:
                        iTopPaddingPx2 = iTopPaddingPx;
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
                foreach (Node current in oNodes)
                {
                    if (!current.Rectangle.IsEmpty)
                    {
                        RectangleF rectangle = current.Rectangle;
                        if (!AddPaddingToChildRectangle(ref rectangle, oParentRectangle, iLeftRightBottomPaddingPx))
                        {
                            current.Rectangle = FixSmallRectangle(current.Rectangle);
                            current.PenWidthPx = 1;
                            oLayoutEngine.SetNodeRectanglesToEmpty(current.Nodes, true);
                        }
                        else
                        {
                            current.Rectangle = rectangle;
                            current.PenWidthPx = iPenWidthPx;
                            RectangleF oParentRectangle2 = RectangleF.Inflate(rectangle, (-(float) iPenWidthPx),
                                                                              (-(float) iPenWidthPx));
                            CalculateRectangles(current.Nodes, oParentRectangle2, current, iTopPaddingPx2, num2,
                                                iPenWidthPx2, oLayoutEngine);
                        }
                    }
                }
            }
        }

        protected void DrawRectangles(Nodes oNodes, int iNodeLevel, Graphics oGraphics,
                                      ColorGradientMapper oNegativeColorGradientMapper,
                                      ColorGradientMapper oPositiveColorGradientMapper, PenCache oPenCache)
        {
            Debug.Assert(oNodes != null);
            Debug.Assert(iNodeLevel >= 0);
            Debug.Assert(oGraphics != null);
            Debug.Assert(m_eNodeColorAlgorithm != NodeColorAlgorithm.UseColorMetric ||
                         oNegativeColorGradientMapper != null);
            Debug.Assert(m_eNodeColorAlgorithm != NodeColorAlgorithm.UseColorMetric ||
                         oPositiveColorGradientMapper != null);
            Debug.Assert(oPenCache != null);
            foreach (Node current in oNodes)
            {
                if (!current.Rectangle.IsEmpty)
                {
                    Pen pen = oPenCache.GetPen(current.PenWidthPx);
                    Brush brush = null;
                    bool flag = false;
                    switch (m_eNodeColorAlgorithm)
                    {
                        case NodeColorAlgorithm.UseColorMetric:
                            {
                                Debug.Assert(oNegativeColorGradientMapper != null);
                                Debug.Assert(oPositiveColorGradientMapper != null);
                                float colorMetric = current.ColorMetric;
                                if (colorMetric <= 0f)
                                {
                                    brush = oNegativeColorGradientMapper.ColorMetricToBrush(-colorMetric);
                                }
                                else
                                {
                                    brush = oPositiveColorGradientMapper.ColorMetricToBrush(colorMetric);
                                }
                                break;
                            }
                        case NodeColorAlgorithm.UseAbsoluteColor:
                            brush = new SolidBrush(current.AbsoluteColor);
                            flag = true;
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                    Debug.Assert(brush != null);
                    Rectangle rectangleToDraw = current.RectangleToDraw;
                    oGraphics.FillRectangle(brush, rectangleToDraw);
                    oGraphics.DrawRectangle(pen, rectangleToDraw);
                    if (flag)
                    {
                        brush.Dispose();
                    }
                    DrawRectangles(current.Nodes, iNodeLevel + 1, oGraphics, oNegativeColorGradientMapper,
                                   oPositiveColorGradientMapper, oPenCache);
                }
            }
        }

        protected void DrawText(Graphics oGraphics, Rectangle oTreemapRectangle, Nodes oNodes)
        {
            AssertValid();
            ITextDrawer textDrawer = CreateTextDrawer();
            textDrawer.DrawTextForAllNodes(oGraphics, oTreemapRectangle, oNodes);
        }

        protected void DrawNodesByOwnerDraw(Nodes oNodes)
        {
            Debug.Assert(oNodes != null);
            Debug.Assert(DrawItem != null);
            AssertValid();
            foreach (Node current in oNodes)
            {
                if (!current.Rectangle.IsEmpty)
                {
                    var treemapDrawItemEventArgs = new TreemapDrawItemEventArgs(current);
                    DrawItem(this, treemapDrawItemEventArgs);
                    DrawNodesByOwnerDraw(current.Nodes);
                }
            }
        }

        protected void DrawNodeAsSelected(Node oNode, Graphics oGraphics, Rectangle oTreemapRectangle)
        {
            Debug.Assert(oNode != null);
            oNode.AssertValid();
            Debug.Assert(oGraphics != null);
            int penWidthPx = SetNodePenWidthForSelection(oNode);
            Brush brush = new SolidBrush(m_oSelectedBackColor);
            var pen = new Pen(brush, oNode.PenWidthPx);
            pen.Alignment = PenAlignment.Inset;
            oGraphics.DrawRectangle(pen, oNode.RectangleToDraw);
            pen.Dispose();
            brush.Dispose();
            oNode.PenWidthPx = penWidthPx;
            ITextDrawer textDrawer = CreateTextDrawer();
            textDrawer.DrawTextForSelectedNode(oGraphics, oNode);
        }

        protected internal void FireRedrawRequired()
        {
            if (RedrawRequired != null && !m_bInBeginUpdate)
            {
                RedrawRequired.Invoke(this, EventArgs.Empty);
            }
        }

        protected void CancelSelectedNode()
        {
            m_oSelectedNode = null;
            if (m_oSavedSelectedNodeBitmap != null)
            {
                m_oSavedSelectedNodeBitmap.Dispose();
                m_oSavedSelectedNodeBitmap = null;
            }
        }

        protected ILayoutEngine CreateLayoutEngine()
        {
            ILayoutEngine result;
            switch (m_eLayoutAlgorithm)
            {
                case LayoutAlgorithm.BottomWeightedSquarified:
                    result = new BottomWeightedSquarifiedLayoutEngine();
                    break;
                case LayoutAlgorithm.TopWeightedSquarified:
                    result = new TopWeightedSquarifiedLayoutEngine();
                    break;
                default:
                    Debug.Assert(false);
                    result = null;
                    break;
            }
            return result;
        }

        protected ITextDrawer CreateTextDrawer()
        {
            AssertValid();
            ITextDrawer result;
            switch (m_eTextLocation)
            {
                case TextLocation.CenterCenter:
                    result = new CenterCenterTextDrawer(m_iNodeLevelsWithText, m_iMinNodeLevelWithText,
                                                        m_iMaxNodeLevelWithText, m_sFontFamily, m_fFontMinSizePt,
                                                        m_fFontMaxSizePt, m_fFontIncrementPt, m_oFontSolidColor,
                                                        m_iFontMinAlpha, m_iFontMaxAlpha, m_iFontAlphaIncrementPerLevel,
                                                        m_oSelectedFontColor);
                    break;
                case TextLocation.Top:
                    result = new TopTextDrawer(m_iNodeLevelsWithText, m_iMinNodeLevelWithText, m_iMaxNodeLevelWithText,
                                               m_sFontFamily, m_fFontMinSizePt, GetTopMinimumTextHeight(),
                                               m_oFontSolidColor, m_oSelectedFontColor, m_oSelectedBackColor);
                    break;
                default:
                    Debug.Assert(false);
                    result = null;
                    break;
            }
            return result;
        }

        protected bool AddPaddingToParentRectangle(ref RectangleF oParentRectangle, ref int iTopPaddingPx,
                                                   ref int iLeftRightBottomPaddingPx)
        {
            Debug.Assert(iTopPaddingPx >= 0);
            Debug.Assert(iLeftRightBottomPaddingPx >= 0);
            int num = iTopPaddingPx;
            int num2 = iLeftRightBottomPaddingPx;
            RectangleF rectangleF = AddPaddingToRectangle(oParentRectangle, num, num2);
            bool result;
            if (RectangleIsSmallerThanMin(rectangleF))
            {
                result = false;
            }
            else
            {
                oParentRectangle = rectangleF;
                iTopPaddingPx = num;
                iLeftRightBottomPaddingPx = num2;
                result = true;
            }
            return result;
        }

        protected RectangleF AddPaddingToRectangle(RectangleF oRectangle, int iTopPaddingPx,
                                                   int iLeftRightBottomPaddingPx)
        {
            Debug.Assert(iTopPaddingPx >= 0);
            Debug.Assert(iLeftRightBottomPaddingPx >= 0);
            return RectangleF.FromLTRB(oRectangle.Left + iLeftRightBottomPaddingPx, oRectangle.Top + iTopPaddingPx,
                                       oRectangle.Right - iLeftRightBottomPaddingPx,
                                       oRectangle.Bottom - iLeftRightBottomPaddingPx);
        }

        protected bool AddPaddingToChildRectangle(ref RectangleF oChildRectangle, RectangleF oParentRectangle,
                                                  int iPaddingPx)
        {
            RectangleF rectangleF = AddPaddingToChildRectangle(oChildRectangle, oParentRectangle, iPaddingPx);
            bool result;
            if (RectangleIsSmallerThanMin(rectangleF))
            {
                if (iPaddingPx > 1)
                {
                    rectangleF = AddPaddingToChildRectangle(oChildRectangle, oParentRectangle, 1);
                }
                if (RectangleIsSmallerThanMin(rectangleF))
                {
                    result = false;
                    return result;
                }
            }
            oChildRectangle = rectangleF;
            result = true;
            return result;
        }

        protected RectangleF AddPaddingToChildRectangle(RectangleF oChildRectangle, RectangleF oParentRectangle,
                                                        int iPaddingPx)
        {
            float num = oChildRectangle.Left;
            float num2 = oChildRectangle.Top;
            float num3 = oChildRectangle.Right;
            float num4 = oChildRectangle.Bottom;
            float num5 = (iPaddingPx + 1)/2f;
            if (Math.Round(oChildRectangle.Left) != Math.Round(oParentRectangle.Left))
            {
                num += num5;
            }
            if (Math.Round(oChildRectangle.Top) != Math.Round(oParentRectangle.Top))
            {
                num2 += num5;
            }
            if (Math.Round(oChildRectangle.Right) != Math.Round(oParentRectangle.Right))
            {
                num3 -= num5;
            }
            if (Math.Round(oChildRectangle.Bottom) != Math.Round(oParentRectangle.Bottom))
            {
                num4 -= num5;
            }
            return RectangleF.FromLTRB(num, num2, num3, num4);
        }

        protected int GetTopLevelTopPaddingPx(Graphics oGraphics)
        {
            AssertValid();
            Debug.Assert(oGraphics != null);
            int result;
            switch (m_eTextLocation)
            {
                case TextLocation.CenterCenter:
                    result = m_iPaddingPx;
                    break;
                case TextLocation.Top:
                    result = TopTextDrawer.GetTextHeight(oGraphics, m_sFontFamily, m_fFontMinSizePt,
                                                         GetTopMinimumTextHeight());
                    break;
                default:
                    Debug.Assert(false);
                    result = -1;
                    break;
            }
            return result;
        }

        protected int GetTopMinimumTextHeight()
        {
            AssertValid();
            return DecrementPadding(m_iPaddingPx);
        }

        protected int DecrementPadding(int iPaddingPx)
        {
            return Math.Max(iPaddingPx - m_iPaddingDecrementPerLevelPx, 1);
        }

        protected int DecrementPenWidth(int iPenWidthPx)
        {
            return Math.Max(iPenWidthPx - m_iPenWidthDecrementPerLevelPx, 1);
        }

        protected bool RectangleIsSmallerThanMin(RectangleF oRectangle)
        {
            return oRectangle.Width < 1f || oRectangle.Height < 1f;
        }

        protected RectangleF FixSmallRectangle(RectangleF oUnpaddedNodeRectangle)
        {
            float num = oUnpaddedNodeRectangle.Left;
            float num2 = oUnpaddedNodeRectangle.Top;
            float num3 = oUnpaddedNodeRectangle.Right;
            float num4 = oUnpaddedNodeRectangle.Bottom;
            float width = oUnpaddedNodeRectangle.Width;
            float height = oUnpaddedNodeRectangle.Height;
            float num5 = 0.5f;
            if (height < 1f)
            {
                num2 = (float) ((num2 + num4)/2.0 - num5);
                num4 = num2 + 1f;
            }
            if (width < 1f)
            {
                num = (num + num3)/2f - num5;
                num3 = num + 1f;
            }
            RectangleF result = RectangleF.FromLTRB(num, num2, num3, num4);
            if (height < 1f)
            {
                Debug.Assert(Math.Round((double) result.Height) == 1.0);
            }
            if (width < 1f)
            {
                Debug.Assert(Math.Round((double) result.Width) == 1.0);
            }
            return result;
        }

        protected int SetNodePenWidthForSelection(Node oNode)
        {
            Debug.Assert(oNode != null);
            AssertValid();
            int penWidthPx = oNode.PenWidthPx;
            int num = m_iPaddingPx;
            int level = oNode.Level;
            for (int i = 0; i < level + 1; i++)
            {
                num = DecrementPadding(num);
            }
            int num2 = penWidthPx + num;
            switch (m_eTextLocation)
            {
                case TextLocation.CenterCenter:
                    num2 = Math.Max(num2, 4);
                    break;
                case TextLocation.Top:
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            oNode.PenWidthPx = num2;
            return penWidthPx;
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oNodes != null);
            m_oNodes.AssertValid();
            Debug.Assert(m_iPaddingPx >= 1);
            Debug.Assert(m_iPaddingPx <= 100);
            Debug.Assert(m_iPaddingDecrementPerLevelPx >= 0);
            Debug.Assert(m_iPaddingDecrementPerLevelPx <= 99);
            Debug.Assert(m_iPenWidthPx >= 1);
            Debug.Assert(m_iPenWidthPx <= 100);
            Debug.Assert(m_iPenWidthDecrementPerLevelPx >= 0);
            Debug.Assert(m_iPenWidthDecrementPerLevelPx <= 99);
            Debug.Assert(Enum.IsDefined(typeof (NodeColorAlgorithm), m_eNodeColorAlgorithm));
            Debug.Assert(m_fMinColorMetric < 0f);
            Debug.Assert(m_fMaxColorMetric > 0f);
            Debug.Assert(m_iDiscretePositiveColors >= 2);
            Debug.Assert(m_iDiscretePositiveColors <= 50);
            Debug.Assert(m_iDiscreteNegativeColors >= 2);
            Debug.Assert(m_iDiscreteNegativeColors <= 50);
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
            Debug.Assert(Enum.IsDefined(typeof (NodeLevelsWithText), m_iNodeLevelsWithText));
            Debug.Assert(m_iMinNodeLevelWithText >= 0);
            Debug.Assert(m_iMaxNodeLevelWithText >= 0);
            Debug.Assert(m_iMaxNodeLevelWithText >= m_iMinNodeLevelWithText);
            Debug.Assert(Enum.IsDefined(typeof (TextLocation), m_eTextLocation));
            Debug.Assert(Enum.IsDefined(typeof (EmptySpaceLocation), m_eEmptySpaceLocation));
            if (m_oSelectedNode != null)
            {
                m_oSelectedNode.AssertValid();
            }
            Debug.Assert(Enum.IsDefined(typeof (LayoutAlgorithm), m_eLayoutAlgorithm));
        }
    }
}