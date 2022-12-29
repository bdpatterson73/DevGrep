using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BLS.TreemapCtrl.TreemapNoDoc;
using DevGrep.Controls.TreeMaps.TreeMapCtrl.AppLib;
using DevGrep.Controls.TreeMaps.TreeMapCtrl.ControlLib;
using DevGrep.Controls.TreeMaps.TreeMapCtrl.GraphicsLib;
using DevGrep.Controls.TreeMaps.TreeMapCtrl.TreemapNoDoc;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.Treemap
{
    public class TreemapControl : Panel, ITreemapComponent
    {
        public delegate void NodeEventHandler(object sender, NodeEventArgs nodeEventArgs);

        public delegate void NodeMouseEventHandler(object sender, NodeMouseEventArgs nodeMouseEventArgs);

        private readonly Container components = null;
        private NodeMouseEventHandler _NodeMouseDown;
        //private TreemapControl.NodeMouseEventHandler _NodeMouseUp;
        //private TreemapControl.NodeEventHandler _NodeMouseHover;
        //private TreemapControl.NodeEventHandler _NodeDoubleClick;
        //private EventHandler _ZoomStateChanged;
        //private EventHandler _SelectedNodeChanged;
        protected bool m_bAllowDrag;
        protected bool m_bIsZoomable;
        protected bool m_bShowToolTips;
        protected Bitmap m_oBitmap;
        protected Point m_oLastDraggableMouseDownPoint;
        protected Point m_oLastMouseMovePoint;
        private ToolTipTracker m_oToolTipTracker;
        protected TreemapGenerator m_oTreemapGenerator;
        protected ZoomActionHistoryList m_oZoomActionHistoryList;
        private PictureBox picPictureBox;
        private ToolTipPanel pnlToolTip;

        public TreemapControl()
        {
            InitializeComponent();
            Controls.Add(picPictureBox);
            Controls.Add(pnlToolTip);
            pnlToolTip.BringToFront();
            m_oTreemapGenerator = new TreemapGenerator();
            m_oTreemapGenerator.RedrawRequired += TreemapGenerator_RedrawRequired;
            m_oBitmap = null;
            m_bShowToolTips = true;
            m_bAllowDrag = false;
            m_bIsZoomable = false;
            m_oZoomActionHistoryList = null;
            m_oToolTipTracker = new ToolTipTracker();
            m_oToolTipTracker.ShowToolTip += oToolTipTracker_ShowToolTip;
            m_oToolTipTracker.HideToolTip += oToolTipTracker_HideToolTip;
            m_oLastMouseMovePoint = new Point(-1, -1);
            m_oLastDraggableMouseDownPoint = new Point(-1, -1);
            ResizeRedraw = true;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public Node SelectedNode
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.SelectedNode;
            }
        }

        [Category("Node Text")]
        [Description("Indicates whether tooltips should be shown.")]
        public bool ShowToolTips
        {
            get
            {
                AssertValid();
                return m_bShowToolTips;
            }
            set
            {
                if (!value)
                    m_oToolTipTracker.Reset();
                m_bShowToolTips = value;
                AssertValid();
            }
        }

        [Description("Indicates whether a node can be dragged out of the control.")]
        [Category("Behavior")]
        public bool AllowDrag
        {
            get
            {
                AssertValid();
                return m_bAllowDrag;
            }
            set
            {
                m_bAllowDrag = value;
                AssertValid();
            }
        }

        [Category("Zooming")]
        [Description("Indicates whether the treemap can be zoomed.")]
        public bool IsZoomable
        {
            get
            {
                AssertValid();
                return m_bIsZoomable;
            }
            set
            {
                if (value != m_bIsZoomable)
                {
                    m_bIsZoomable = value;
                    if (m_bIsZoomable)
                    {
                        Debug.Assert(m_oZoomActionHistoryList == null);
                        m_oZoomActionHistoryList = new ZoomActionHistoryList();
                        m_oZoomActionHistoryList.Change += ZoomActionHistoryList_Change;
                    }
                    else
                    {
                        Debug.Assert(m_oZoomActionHistoryList != null);
                        m_oZoomActionHistoryList.Change -= ZoomActionHistoryList_Change;
                        m_oZoomActionHistoryList = null;
                    }
                }
                AssertValid();
            }
        }

        [ReadOnly(true)]
        [Browsable(false)]
        public Bitmap Bitmap
        {
            get
            {
                AssertValid();
                return m_oBitmap;
            }
        }

        [ReadOnly(true)]
        [Browsable(false)]
        public Nodes Nodes
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.Nodes;
            }
        }

        [ReadOnly(true)]
        [Browsable(false)]
        public string NodesXml
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.NodesXml;
            }
            set
            {
                m_oTreemapGenerator.NodesXml = value;
                AssertValid();
            }
        }

        [Description("The algorithm used to lay out the treemap's rectangles.")]
        [Category("Layout")]
        public LayoutAlgorithm LayoutAlgorithm
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.LayoutAlgorithm;
            }
            set
            {
                m_oTreemapGenerator.LayoutAlgorithm = value;
                AssertValid();
            }
        }

        [Category("Node Borders")]
        [Description("The padding that is added to the rectangles for top-level nodes.")]
        public int PaddingPx
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.PaddingPx;
            }
            set
            {
                m_oTreemapGenerator.PaddingPx = value;
                AssertValid();
            }
        }

        [Description("The number of pixels that is subtracted from the padding at each node level.")]
        [Category("Node Borders")]
        public int PaddingDecrementPerLevelPx
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.PaddingDecrementPerLevelPx;
            }
            set
            {
                m_oTreemapGenerator.PaddingDecrementPerLevelPx = value;
                AssertValid();
            }
        }

        [Category("Node Borders")]
        [Description("The pen width that is used to draw the rectangles for the top-level nodes.")]
        public int PenWidthPx
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.PenWidthPx;
            }
            set
            {
                m_oTreemapGenerator.PenWidthPx = value;
                AssertValid();
            }
        }

        [Description("The number of pixels that is subtracted from the pen width at each node level.")]
        [Category("Node Borders")]
        public int PenWidthDecrementPerLevelPx
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.PenWidthDecrementPerLevelPx;
            }
            set
            {
                m_oTreemapGenerator.PenWidthDecrementPerLevelPx = value;
                AssertValid();
            }
        }

        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = m_oTreemapGenerator.BackColor = value;
                AssertValid();
            }
        }

        [Description("The color of rectangle borders.")]
        [Category("Node Borders")]
        public Color BorderColor
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.BorderColor;
            }
            set
            {
                m_oTreemapGenerator.BorderColor = value;
                AssertValid();
            }
        }

        [Category("Node Fill Colors")]
        [Description("The algorithm used to color the treemap's rectangles.")]
        public NodeColorAlgorithm NodeColorAlgorithm
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.NodeColorAlgorithm;
            }
            set
            {
                m_oTreemapGenerator.NodeColorAlgorithm = value;
                AssertValid();
            }
        }

        [Category("Node Fill Colors")]
        [Description("The maximum negative fill color.")]
        public Color MinColor
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.MinColor;
            }
            set
            {
                m_oTreemapGenerator.MinColor = value;
                AssertValid();
            }
        }

        [Description("The maximum positive fill color.")]
        [Category("Node Fill Colors")]
        public Color MaxColor
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.MaxColor;
            }
            set
            {
                m_oTreemapGenerator.MaxColor = value;
                AssertValid();
            }
        }

        [Category("Node Fill Colors")]
        [Description("The Node.ColorMetric value to map to MinColor.")]
        public float MinColorMetric
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.MinColorMetric;
            }
            set
            {
                m_oTreemapGenerator.MinColorMetric = value;
                AssertValid();
            }
        }

        [Category("Node Fill Colors")]
        [Description("The Node.ColorMetric value to map to MaxColor.")]
        public float MaxColorMetric
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.MaxColorMetric;
            }
            set
            {
                m_oTreemapGenerator.MaxColorMetric = value;
                AssertValid();
            }
        }

        [Category("Node Fill Colors")]
        [Description("The number of discrete fill colors to use in the negative color range.")]
        public int DiscreteNegativeColors
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.DiscreteNegativeColors;
            }
            set
            {
                m_oTreemapGenerator.DiscreteNegativeColors = value;
                AssertValid();
            }
        }

        [Description("The number of discrete fill colors to use in the positive color range.")]
        [Category("Node Fill Colors")]
        public int DiscretePositiveColors
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.DiscretePositiveColors;
            }
            set
            {
                m_oTreemapGenerator.DiscretePositiveColors = value;
                AssertValid();
            }
        }

        [Description("The font family to use for drawing node text.")]
        [Category("Node Text")]
        public string FontFamily
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.FontFamily;
            }
            set
            {
                m_oTreemapGenerator.FontFamily = value;
                AssertValid();
            }
        }

        [Description("The solid color to use for unselected node text.")]
        [Category("Node Text")]
        public Color FontSolidColor
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.FontSolidColor;
            }
            set
            {
                m_oTreemapGenerator.FontSolidColor = value;
                AssertValid();
            }
        }

        [Category("Node Text")]
        [Description("The color to use for selected node text.")]
        public Color SelectedFontColor
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.SelectedFontColor;
            }
            set
            {
                m_oTreemapGenerator.SelectedFontColor = value;
                AssertValid();
            }
        }

        [Description("The color to use to highlight the selected node.")]
        [Category("Node Text")]
        public Color SelectedBackColor
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.SelectedBackColor;
            }
            set
            {
                m_oTreemapGenerator.SelectedBackColor = value;
                AssertValid();
            }
        }

        [Description("The node levels to show text for.")]
        [Category("Node Text")]
        public NodeLevelsWithText NodeLevelsWithText
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.NodeLevelsWithText;
            }
            set
            {
                m_oTreemapGenerator.NodeLevelsWithText = value;
                AssertValid();
            }
        }

        [Category("Node Text")]
        [Description("The location within a node's rectangle where the node's text is shown.")]
        public TextLocation TextLocation
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.TextLocation;
            }
            set
            {
                m_oTreemapGenerator.TextLocation = value;
                AssertValid();
            }
        }

        [Description("The location within a node's rectangle where the node's empty space is shown.")]
        [Category("Layout")]
        public EmptySpaceLocation EmptySpaceLocation
        {
            get
            {
                AssertValid();
                return m_oTreemapGenerator.EmptySpaceLocation;
            }
            set
            {
                m_oTreemapGenerator.EmptySpaceLocation = value;
                AssertValid();
            }
        }

        public void GetNodeLevelsWithTextRange(out int minLevel, out int maxLevel)
        {
            AssertValid();
            m_oTreemapGenerator.GetNodeLevelsWithTextRange(out minLevel, out maxLevel);
        }

        public void SetNodeLevelsWithTextRange(int minLevel, int maxLevel)
        {
            m_oTreemapGenerator.SetNodeLevelsWithTextRange(minLevel, maxLevel);
            AssertValid();
        }

        public void GetFontSizeRange(out float minSizePt, out float maxSizePt, out float incrementPt)
        {
            AssertValid();
            m_oTreemapGenerator.GetFontSizeRange(out minSizePt, out maxSizePt, out incrementPt);
        }

        public void SetFontSizeRange(float minSizePt, float maxSizePt, float incrementPt)
        {
            m_oTreemapGenerator.SetFontSizeRange(minSizePt, maxSizePt, incrementPt);
        }

        public void GetFontAlphaRange(out int minAlpha, out int maxAlpha, out int incrementPerLevel)
        {
            AssertValid();
            m_oTreemapGenerator.GetFontAlphaRange(out minAlpha, out maxAlpha, out incrementPerLevel);
        }

        public void SetFontAlphaRange(int minAlpha, int maxAlpha, int incrementPerLevel)
        {
            m_oTreemapGenerator.SetFontAlphaRange(minAlpha, maxAlpha, incrementPerLevel);
            AssertValid();
        }

        public void BeginUpdate()
        {
            AssertValid();
            m_oTreemapGenerator.BeginUpdate();
        }

        public void EndUpdate()
        {
            AssertValid();
            m_oTreemapGenerator.EndUpdate();
        }

        public void Clear()
        {
            AssertValid();
            Node selectedNode = SelectedNode;
            m_oTreemapGenerator.Clear();
            if (m_bIsZoomable)
            {
                Debug.Assert(m_oZoomActionHistoryList != null);
                m_oZoomActionHistoryList.Reset();
            }
            if (selectedNode == null)
                return;
            FireSelectedNodeChanged();
        }

        [Category("Mouse")]
        public event NodeMouseEventHandler NodeMouseDown;

        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    add
        //    {
        //        this._NodeMouseDown += value;
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    remove
        //    {
        //        this._NodeMouseDown -=  value;
        //    }
        //}

        [Category("Mouse")]
        public event NodeMouseEventHandler NodeMouseUp;

        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    add
        //    {
        //        this.NodeMouseUp += value;
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    remove
        //    {
        //        this.NodeMouseUp -= value;
        //    }
        //}

        [Category("Mouse")]
        public event NodeEventHandler NodeMouseHover;

        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    add
        //    {
        //        this.NodeMouseHover += value;
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    remove
        //    {
        //        this.NodeMouseHover -= value;
        //    }
        //}

        [Category("Action")]
        public event NodeEventHandler NodeDoubleClick;

        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    add
        //    {
        //        this.NodeDoubleClick += value;
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    remove
        //    {
        //        this.NodeDoubleClick -= value;
        //    }
        //}

        [Category("Action")]
        public event EventHandler ZoomStateChanged;

        //{
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    add
        //    {
        //        this.ZoomStateChanged += value;
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized)]
        //    remove
        //    {
        //        this.ZoomStateChanged -= value;
        //    }
        //}

        [Category("Action")]
        public event EventHandler SelectedNodeChanged;

        public bool CanZoomIn(Node node)
        {
            AssertValid();
            VerifyIsZoomable("CanZoomIn");
            Nodes nodes = Nodes;
            switch (nodes.Count)
            {
                case 0:
                    return false;
                case 1:
                    if (node == nodes[0])
                        return false;
                    else
                        break;
            }
            return true;
        }

        public bool CanZoomOut()
        {
            AssertValid();
            VerifyIsZoomable("CanZoomOut");
            if (!m_oZoomActionHistoryList.HasCurrentState)
                return false;
            else
                return m_oZoomActionHistoryList.PeekCurrentState.CanZoomOutFromZoomedNode();
        }

        public bool CanMoveBack()
        {
            AssertValid();
            VerifyIsZoomable("CanMoveBack");
            Debug.Assert(m_oZoomActionHistoryList != null);
            return m_oZoomActionHistoryList.HasCurrentState;
        }

        public bool CanMoveForward()
        {
            AssertValid();
            VerifyIsZoomable("CanMoveForward");
            Debug.Assert(m_oZoomActionHistoryList != null);
            return m_oZoomActionHistoryList.HasNextState;
        }

        public void ZoomIn(Node node)
        {
            Debug.Assert(node != null);
            AssertValid();
            VerifyIsZoomable("ZoomIn");
            if (!CanZoomIn(node))
                throw new InvalidOperationException(
                    "TreemapControl.ZoomIn: Can't zoom in to node.  Check the CanZoomIn property first.");
            Nodes nodes = Nodes;
            Debug.Assert(nodes.Count > 0);
            ZoomAction zoomAction;
            if (nodes.Count > 1 || !m_oZoomActionHistoryList.HasCurrentState)
            {
                zoomAction = new ZoomedFromTopLevelAction(m_oZoomActionHistoryList, node, nodes);
            }
            else
            {
                Debug.Assert(nodes.Count == 1);
                Debug.Assert(m_oZoomActionHistoryList.HasCurrentState);
                Node node1 = nodes[0];
                ZoomAction peekCurrentState = m_oZoomActionHistoryList.PeekCurrentState;
                if (peekCurrentState.ParentOfZoomedNode == null)
                {
                    zoomAction = m_oZoomActionHistoryList.OriginalTopLevelNodes.Length <= 1
                                     ? new ZoomedFromTopLevelAction(m_oZoomActionHistoryList, node, nodes)
                                     : (ZoomAction)
                                       new ZoomedFromOneTopLevelNodeAction(m_oZoomActionHistoryList, node, node1);
                }
                else
                {
                    Debug.Assert(peekCurrentState.ParentOfZoomedNode != null);
                    node1.PrivateSetParent(peekCurrentState.ParentOfZoomedNode);
                    zoomAction = new ZoomedFromInnerNodeAction(m_oZoomActionHistoryList, node, node1);
                }
            }
            m_oZoomActionHistoryList.InsertState(zoomAction);
            Node selectedNode = SelectedNode;
            m_oTreemapGenerator.Clear();
            nodes.Add(node);
            if (selectedNode == null)
                return;
            FireSelectedNodeChanged();
        }

        public void ZoomOut()
        {
            AssertValid();
            VerifyIsZoomable("ZoomOut");
            if (!CanZoomOut())
                throw new InvalidOperationException(
                    "TreemapControl.ZoomOut: Can't zoom out.  Check the CanZoomOut property first.");
            Nodes nodes = Nodes;
            Debug.Assert(nodes.Count == 1);
            Node node1 = nodes[0];
            Debug.Assert(m_oZoomActionHistoryList.HasCurrentState);
            ZoomAction peekCurrentState = m_oZoomActionHistoryList.PeekCurrentState;
            Node[] nodeArray;
            float num;
            ZoomAction zoomAction;
            if (peekCurrentState.ParentOfZoomedNode == null)
            {
                nodeArray = m_oZoomActionHistoryList.OriginalTopLevelNodes;
                num = m_oZoomActionHistoryList.OriginalTopLevelEmptySpaceSizeMetric;
                zoomAction = new ZoomedFromOneTopLevelNodeAction(m_oZoomActionHistoryList, null, node1);
            }
            else
            {
                Node parentOfZoomedNode = peekCurrentState.ParentOfZoomedNode;
                nodeArray = new Node[1]
                    {
                        parentOfZoomedNode
                    };
                num = 0.0f;
                Debug.Assert(parentOfZoomedNode != null);
                node1.PrivateSetParent(parentOfZoomedNode);
                zoomAction = new ZoomedFromInnerNodeAction(m_oZoomActionHistoryList, parentOfZoomedNode, node1);
            }
            m_oZoomActionHistoryList.InsertState(zoomAction);
            Node selectedNode = SelectedNode;
            m_oTreemapGenerator.Clear();
            BeginUpdate();
            foreach (Node node2 in nodeArray)
                nodes.Add(node2);
            nodes.EmptySpace.SizeMetric = num;
            EndUpdate();
            if (selectedNode == null)
                return;
            FireSelectedNodeChanged();
        }

        public void MoveBack()
        {
            AssertValid();
            VerifyIsZoomable("MoveBack");
            if (!CanMoveBack())
                throw new InvalidOperationException(
                    "TreemapControl.MoveBack: Can't move back.  Check the CanMoveBack property first.");
            Debug.Assert(m_oZoomActionHistoryList.HasCurrentState);
            ZoomAction currentState = m_oZoomActionHistoryList.CurrentState;
            Node selectedNode = SelectedNode;
            currentState.Undo(m_oTreemapGenerator);
            if (selectedNode == null)
                return;
            FireSelectedNodeChanged();
        }

        public void MoveForward()
        {
            AssertValid();
            VerifyIsZoomable("MoveForward");
            if (!CanMoveForward())
                throw new InvalidOperationException(
                    "TreemapControl.MoveForward: Can't move forward.  Check the CanMoveForward property first.");
            Debug.Assert(m_oZoomActionHistoryList.HasNextState);
            var zoomAction = (ZoomAction) m_oZoomActionHistoryList.NextState;
            Node selectedNode = SelectedNode;
            zoomAction.Redo(m_oTreemapGenerator);
            if (selectedNode == null)
                return;
            FireSelectedNodeChanged();
        }

        public void Draw(Graphics graphics, Rectangle treemapRectangle)
        {
            Debug.Assert(graphics != null);
            Debug.Assert(!treemapRectangle.IsEmpty);
            AssertValid();
            m_oTreemapGenerator.Draw(graphics, treemapRectangle);
            Invalidate();
        }

        public void SelectNode(Node node)
        {
            AssertValid();
            if (node == SelectedNode)
                return;
            m_oTreemapGenerator.SelectNode(node, m_oBitmap);
            if (m_oBitmap != null)
                picPictureBox.Image = m_oBitmap;
            FireSelectedNodeChanged();
        }

        protected void Draw(bool bRetainSelection)
        {
            AssertValid();
            m_oToolTipTracker.Reset();
            m_oLastMouseMovePoint = new Point(-1, -1);
            Size bitmapSizeToDraw = GetBitmapSizeToDraw();
            if (bitmapSizeToDraw.Width == 0 || bitmapSizeToDraw.Height == 0)
                return;
            if (m_oBitmap != null)
                m_oBitmap.Dispose();
            using (Graphics graphics = CreateGraphics())
            {
                try
                {
                    m_oBitmap = new Bitmap(bitmapSizeToDraw.Width, bitmapSizeToDraw.Height, graphics);
                }
                catch (ArgumentException ex)
                {
                    m_oBitmap = null;
                    throw new InvalidOperationException(
                        "The treemap image could not be created.  It may be too large.  (Its size is " +
                        bitmapSizeToDraw + ".)", ex);
                }
            }
            m_oTreemapGenerator.Draw(m_oBitmap, bRetainSelection);
            picPictureBox.Size = bitmapSizeToDraw;
            picPictureBox.Location = new Point(0);
            picPictureBox.Image = m_oBitmap;
        }

        protected PointF GetClientMousePosition()
        {
            return ControlUtil.GetClientMousePosition(this);
        }

        protected Size GetBitmapSizeToDraw()
        {
            bool autoScroll = AutoScroll;
            AutoScroll = false;
            Size clientSize = ClientSize;
            AutoScroll = autoScroll;
            return AutoScroll ? AutoScrollMinSize : clientSize;
        }

        protected override void OnPaintBackground(PaintEventArgs oPaintEventArgs)
        {
        }

        protected override void OnPaint(PaintEventArgs oPaintEventArgs)
        {
            AssertValid();
            Debug.Assert(oPaintEventArgs != null);
            Draw(true);
        }

        protected void ShowToolTip(Node oNode)
        {
            Debug.Assert(m_bShowToolTips);
            Debug.Assert(oNode != null);
            string toolTip = oNode.ToolTip;
            if (toolTip == null || toolTip == "")
                return;
            pnlToolTip.ShowToolTip(toolTip, this);
        }

        protected void HideToolTip()
        {
            pnlToolTip.HideToolTip();
            AssertValid();
        }

        protected void VerifyIsZoomable(string sMethodName)
        {
            Debug.Assert(sMethodName != null);
            Debug.Assert(sMethodName.Length > 0);
            if (!IsZoomable)
                throw new InvalidOperationException(
                    string.Format("TreemapControl.{0}: This can't be used if the IsZoomable property is false.",
                                  sMethodName));
        }

        protected void FireSelectedNodeChanged()
        {
            AssertValid();
            if (SelectedNodeChanged == null)
                return;
            SelectedNodeChanged(this, EventArgs.Empty);
        }

        protected override void Dispose(bool bDisposing)
        {
            if (bDisposing)
            {
                if (components != null)
                    components.Dispose();
                if (m_oBitmap != null)
                {
                    m_oBitmap.Dispose();
                    m_oBitmap = null;
                }
                if (m_oToolTipTracker != null)
                {
                    m_oToolTipTracker.Dispose();
                    m_oToolTipTracker = null;
                }
            }
            base.Dispose(bDisposing);
        }

        protected void picPictureBox_MouseDown(object oSource, MouseEventArgs oMouseEventArgs)
        {
            Node node;
            if (!m_oTreemapGenerator.GetNodeFromPoint(oMouseEventArgs.X, oMouseEventArgs.Y, out node))
                return;
            SelectNode(node);
            if (NodeMouseDown != null)
                NodeMouseDown(this, new NodeMouseEventArgs(oMouseEventArgs, node));
            if (oMouseEventArgs.Clicks == 2)
            {
                if (NodeDoubleClick == null)
                    return;
                NodeDoubleClick(this, new NodeEventArgs(node));
            }
            else
            {
                if (!m_bAllowDrag || (oMouseEventArgs.Button & MouseButtons.Left) != MouseButtons.Left)
                    return;
                m_oLastDraggableMouseDownPoint = new Point(oMouseEventArgs.X, oMouseEventArgs.Y);
            }
        }

        protected void picPictureBox_MouseUp(object oSource, MouseEventArgs oMouseEventArgs)
        {
            Node node;
            if (m_oTreemapGenerator.GetNodeFromPoint(oMouseEventArgs.X, oMouseEventArgs.Y, out node) &&
                NodeMouseUp != null)
                NodeMouseUp(this, new NodeMouseEventArgs(oMouseEventArgs, node));
            m_oLastDraggableMouseDownPoint = new Point(-1, -1);
        }

        private void picPictureBox_MouseMove(object oSource, MouseEventArgs oMouseEventArgs)
        {
            if (oMouseEventArgs.X == m_oLastMouseMovePoint.X && oMouseEventArgs.Y == m_oLastMouseMovePoint.Y)
                return;
            m_oLastMouseMovePoint = new Point(oMouseEventArgs.X, oMouseEventArgs.Y);
            if (pnlToolTip.Visible &&
                new Rectangle(pnlToolTip.Location, pnlToolTip.Size).Contains(m_oLastMouseMovePoint))
                return;
            Node node;
            m_oTreemapGenerator.GetNodeFromPoint(oMouseEventArgs.X, oMouseEventArgs.Y, out node);
            m_oToolTipTracker.OnMouseMoveOverObject(node);
            if (!(m_oLastDraggableMouseDownPoint != new Point(-1, -1)))
                return;
            int x = m_oLastDraggableMouseDownPoint.X;
            int y = m_oLastDraggableMouseDownPoint.Y;
            if (Math.Abs(oMouseEventArgs.X - x) < SystemInformation.DragSize.Width/2 &&
                Math.Abs(oMouseEventArgs.Y - y) < SystemInformation.DragSize.Height/2)
                return;
            Debug.Assert(m_oTreemapGenerator.GetNodeFromPoint(x, y, out node));
            Debug.Assert(node != null);
            var num = (int) DoDragDrop(node.Tag ?? string.Empty, DragDropEffects.Copy);
        }

        private void picPictureBox_MouseLeave(object oSource, EventArgs oEventArgs)
        {
            m_oToolTipTracker.OnMouseMoveOverObject(null);
            m_oLastDraggableMouseDownPoint = new Point(-1, -1);
        }

        private void oToolTipTracker_ShowToolTip(object oSource, ToolTipTrackerEventArgs oToolTipTrackerEventArgs)
        {
            var oNode = (Node) oToolTipTrackerEventArgs.Object;
            Debug.Assert(oNode != null);
            if (m_bShowToolTips)
                ShowToolTip(oNode);
            if (NodeMouseHover == null)
                return;
            NodeMouseHover(this, new NodeEventArgs(oNode));
        }

        private void oToolTipTracker_HideToolTip(object oSource, ToolTipTrackerEventArgs oToolTipTrackerEventArgs)
        {
            if (!m_bShowToolTips)
                return;
            HideToolTip();
        }

        private void ZoomActionHistoryList_Change(object oSender, EventArgs oEventArgs)
        {
            if (ZoomStateChanged == null)
                return;
            ZoomStateChanged(this, EventArgs.Empty);
        }

        protected void TreemapGenerator_RedrawRequired(object oSender, EventArgs oEventArgs)
        {
            Invalidate();
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oTreemapGenerator != null);
            m_oTreemapGenerator.AssertValid();
            if (m_bIsZoomable)
                Debug.Assert(m_oZoomActionHistoryList != null);
            else
                Debug.Assert(m_oZoomActionHistoryList == null);
            Debug.Assert(m_oToolTipTracker != null);
        }

        private void InitializeComponent()
        {
            picPictureBox = new PictureBox();
            pnlToolTip = new ToolTipPanel();
            picPictureBox.Location = new Point(126, 17);
            picPictureBox.Name = "picPictureBox";
            picPictureBox.TabIndex = 0;
            picPictureBox.TabStop = false;
            picPictureBox.MouseUp += picPictureBox_MouseUp;
            picPictureBox.MouseMove += picPictureBox_MouseMove;
            picPictureBox.MouseLeave += picPictureBox_MouseLeave;
            picPictureBox.MouseDown += picPictureBox_MouseDown;
            pnlToolTip.BackColor = SystemColors.Window;
            pnlToolTip.BorderStyle = BorderStyle.FixedSingle;
            pnlToolTip.Name = "pnlToolTip";
            pnlToolTip.TabIndex = 0;
        }
    }
}