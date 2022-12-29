using System;
using System.Diagnostics;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.GraphicsLib;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.Treemap
{
    public class Node : IComparable
    {
        protected bool m_bRectangleSaved;
        protected bool m_bRectangleSet;
        protected float m_fSizeMetric;
        protected int m_iPenWidthPx;
        private NodeColor m_oNodeColor;
        protected Nodes m_oNodes;
        protected Node m_oParentNode;
        protected RectangleF m_oRectangle;
        protected RectangleF m_oSavedRectangle;
        protected object m_oTag;
        protected TreemapGenerator m_oTreemapGenerator;
        protected string m_sText;
        protected string m_sToolTip;

        public Node(string text, float sizeMetric, float colorMetric)
        {
            InitializeWithValidation(text, sizeMetric, colorMetric);
            AssertValid();
        }

        public Node(string text, float sizeMetric, Color absoluteColor)
        {
            InitializeWithValidation(text, sizeMetric, 0f);
            m_oNodeColor.AbsoluteColor = absoluteColor;
            AssertValid();
        }

        public Node(string text, float sizeMetric, float colorMetric, object tag)
        {
            InitializeWithValidation(text, sizeMetric, colorMetric);
            m_oTag = tag;
            AssertValid();
        }

        public Node(string text, float sizeMetric, float colorMetric, object tag, string toolTip)
        {
            InitializeWithValidation(text, sizeMetric, colorMetric);
            m_oTag = tag;
            m_sToolTip = toolTip;
            AssertValid();
        }

        public string Text
        {
            get
            {
                AssertValid();
                return m_sText;
            }
            set
            {
                if (m_sText != value)
                {
                    m_sText = value;
                    FireRedrawRequired();
                }
                AssertValid();
            }
        }

        public float SizeMetric
        {
            get
            {
                AssertValid();
                return m_fSizeMetric;
            }
            set
            {
                ValidateSizeMetric(value, "Node.SizeMetric");
                if (m_fSizeMetric != value)
                {
                    m_fSizeMetric = value;
                    FireRedrawRequired();
                }
                AssertValid();
            }
        }

        public float ColorMetric
        {
            get
            {
                AssertValid();
                return m_oNodeColor.ColorMetric;
            }
            set
            {
                ValidateColorMetric(value, "Node.ColorMetric");
                if (m_oNodeColor.ColorMetric != value)
                {
                    m_oNodeColor.ColorMetric = value;
                    FireRedrawRequired();
                }
                AssertValid();
            }
        }

        public Color AbsoluteColor
        {
            get
            {
                AssertValid();
                return m_oNodeColor.AbsoluteColor;
            }
            set
            {
                if (m_oNodeColor.AbsoluteColor != value)
                {
                    m_oNodeColor.AbsoluteColor = value;
                    FireRedrawRequired();
                }
                AssertValid();
            }
        }

        public object Tag
        {
            get
            {
                AssertValid();
                return m_oTag;
            }
            set
            {
                m_oTag = value;
                AssertValid();
            }
        }

        public string ToolTip
        {
            get
            {
                AssertValid();
                return m_sToolTip;
            }
            set
            {
                m_sToolTip = value;
                AssertValid();
            }
        }

        public Nodes Nodes
        {
            get
            {
                AssertValid();
                return m_oNodes;
            }
        }

        public Node Parent
        {
            get
            {
                AssertValid();
                return m_oParentNode;
            }
        }

        public int Level
        {
            get
            {
                AssertValid();
                Node parent = Parent;
                int num = 0;
                while (parent != null)
                {
                    parent = parent.Parent;
                    num++;
                }
                return num;
            }
        }

        protected internal bool HasBeenDrawn
        {
            get
            {
                AssertValid();
                bool result;
                if (!m_oRectangle.IsEmpty)
                {
                    Debug.Assert(m_bRectangleSet);
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
        }

        protected internal RectangleF Rectangle
        {
            get
            {
                AssertValid();
                Debug.Assert(m_bRectangleSet);
                return m_oRectangle;
            }
            set
            {
                m_oRectangle = value;
                m_bRectangleSet = true;
                AssertValid();
            }
        }

        protected internal Rectangle RectangleToDraw
        {
            get
            {
                AssertValid();
                return GraphicsUtil.RectangleFToRectangle(Rectangle, PenWidthPx);
            }
        }

        protected internal double AspectRatio
        {
            get
            {
                AssertValid();
                Debug.Assert(m_bRectangleSet);
                float width = m_oRectangle.Width;
                float height = m_oRectangle.Height;
                double result;
                if (width > height)
                {
                    if (height == 0f)
                    {
                        result = 1.7976931348623157E+308;
                    }
                    else
                    {
                        result = (width/height);
                    }
                }
                else
                {
                    if (width == 0f)
                    {
                        result = 1.7976931348623157E+308;
                    }
                    else
                    {
                        result = (height/width);
                    }
                }
                return result;
            }
        }

        protected internal int PenWidthPx
        {
            get
            {
                AssertValid();
                Debug.Assert(m_iPenWidthPx != -1);
                return m_iPenWidthPx;
            }
            set
            {
                m_iPenWidthPx = value;
                AssertValid();
            }
        }

        protected internal TreemapGenerator TreemapGenerator
        {
            set
            {
                m_oTreemapGenerator = value;
                m_oNodes.TreemapGenerator = value;
                AssertValid();
            }
        }

        public int CompareTo(object otherNode)
        {
            AssertValid();
            return -m_fSizeMetric.CompareTo(((Node) otherNode).m_fSizeMetric);
        }

        public override string ToString()
        {
            AssertValid();
            return
                string.Format(
                    "Node object: Text=\"{0}\",  SizeMetric={1}, Tag={2}, Rectangle={{L={3}, R={4}, T={5}, B={6},  W={7}, H={8}}}, Size={9}",
                    new object[]
                        {
                            m_sText,
                            m_fSizeMetric,
                            (m_oTag == null) ? "null" : m_oTag.ToString(),
                            m_oRectangle.Left,
                            m_oRectangle.Right,
                            m_oRectangle.Top,
                            m_oRectangle.Bottom,
                            m_oRectangle.Width,
                            m_oRectangle.Height,
                            m_oRectangle.Width*m_oRectangle.Height
                        });
        }

        public void PrivateSetParent(Node oParentNode)
        {
            SetParent(oParentNode);
            AssertValid();
        }

        protected void InitializeWithValidation(string sText, float fSizeMetric, float fColorMetric)
        {
            ValidateSizeMetric(fSizeMetric, "Node");
            ValidateColorMetric(fColorMetric, "Node");
            m_oTreemapGenerator = null;
            m_oParentNode = null;
            m_sText = sText;
            m_fSizeMetric = fSizeMetric;
            m_oNodeColor = new NodeColor(fColorMetric);
            m_oTag = null;
            m_sToolTip = null;
            m_oNodes = new Nodes(this);
            m_iPenWidthPx = -1;
            m_bRectangleSet = false;
            m_bRectangleSaved = false;
        }

        protected internal void SetParent(Node oParentNode)
        {
            Debug.Assert(oParentNode != this);
            m_oParentNode = oParentNode;
            AssertValid();
        }

        protected internal bool GetNodeFromPoint(PointF oPointF, out Node oNode)
        {
            AssertValid();
            bool result;
            if (m_oRectangle.Contains(oPointF))
            {
                if (!m_oNodes.GetNodeFromPoint(oPointF, out oNode))
                {
                    oNode = this;
                }
                result = true;
            }
            else
            {
                oNode = null;
                result = false;
            }
            return result;
        }

        protected internal static void ValidateSizeMetric(float fValue, string sCaller)
        {
            if (fValue < 0f)
            {
                throw new ArgumentOutOfRangeException(sCaller, fValue, sCaller + ": SizeMetric must be >= 0.");
            }
        }

        protected internal static void ValidateColorMetric(float fValue, string sCaller)
        {
            if (float.IsNaN(fValue))
            {
                throw new ArgumentOutOfRangeException(sCaller, fValue, sCaller + ": ColorMetric can't be NaN.");
            }
        }

        protected internal void SaveRectangle()
        {
            m_oSavedRectangle = m_oRectangle;
            m_bRectangleSaved = true;
            AssertValid();
        }

        protected internal void RestoreRectangle()
        {
            Debug.Assert(m_bRectangleSaved);
            m_oRectangle = m_oSavedRectangle;
            m_bRectangleSaved = false;
            AssertValid();
        }

        protected void FireRedrawRequired()
        {
            AssertValid();
            if (m_oTreemapGenerator != null)
            {
                m_oTreemapGenerator.FireRedrawRequired();
            }
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_fSizeMetric >= 0f);
            m_oNodeColor.AssertValid();
            Debug.Assert(m_oNodes != null);
            m_oNodes.AssertValid();
            Debug.Assert(m_iPenWidthPx == -1 || m_iPenWidthPx >= 0);
        }
    }
}