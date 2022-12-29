using System.Diagnostics;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public class SquarifiedLayoutEngine : LayoutEngineBase
    {
        protected bool m_bBottomWeighted;

        protected internal SquarifiedLayoutEngine(bool bBottomWeighted)
        {
            m_bBottomWeighted = bBottomWeighted;
        }

        public override void CalculateNodeRectangles(Nodes oNodes, RectangleF oParentRectangle, Node oParentNode,
                                                     EmptySpaceLocation eEmptySpaceLocation)
        {
            Debug.Assert(oNodes != null);
            Debug.Assert(oParentRectangle.Width > 0f);
            Debug.Assert(oParentRectangle.Height > 0f);
            if (oNodes.Count != 0)
            {
                Node[] array = oNodes.ToArraySortedBySizeMetric();
                double areaPerSizeMetric = GetAreaPerSizeMetric(oNodes, oParentRectangle, oParentNode);
                if (areaPerSizeMetric == 0.0)
                {
                    base.SetNodeRectanglesToEmpty(oNodes, true);
                }
                else
                {
                    if (eEmptySpaceLocation == EmptySpaceLocation.Top && oNodes.EmptySpace.SizeMetric > 0f)
                    {
                        double num = areaPerSizeMetric*oNodes.EmptySpace.SizeMetric;
                        Debug.Assert(oParentRectangle.Width > 0f);
                        double num2 = num/oParentRectangle.Width;
                        oParentRectangle = RectangleF.FromLTRB(oParentRectangle.Left,
                                                               oParentRectangle.Top + (float) num2,
                                                               oParentRectangle.Right, oParentRectangle.Bottom);
                        if (oParentRectangle.Height <= 0f)
                        {
                            base.SetNodeRectanglesToEmpty(oNodes, true);
                            return;
                        }
                    }
                    CalculateSquarifiedNodeRectangles(array, oParentRectangle, areaPerSizeMetric);
                    Node[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        Node node = array2[i];
                        RectangleF rectangle = node.Rectangle;
                        Debug.Assert(rectangle.Width >= 0f);
                        Debug.Assert(rectangle.Height >= 0f);
                    }
                }
            }
        }

        protected void CalculateSquarifiedNodeRectangles(Node[] aoSortedNodes, RectangleF oParentRectangle,
                                                         double dAreaPerSizeMetric)
        {
            Debug.Assert(aoSortedNodes != null);
            Debug.Assert(oParentRectangle.Width > 0f);
            Debug.Assert(oParentRectangle.Height > 0f);
            Debug.Assert(dAreaPerSizeMetric >= 0.0);
            int num = aoSortedNodes.Length;
            int num2 = -1;
            int num3 = -1;
            int i = 0;
            double num4 = 0.0;
            double num5 = 1.7976931348623157E+308;
            while (i < num)
            {
                if (oParentRectangle.IsEmpty)
                {
                    base.SetNodeRectanglesToEmpty(aoSortedNodes, i, num - 1);
                    break;
                }
                Node node = aoSortedNodes[i];
                double num6 = node.SizeMetric;
                if (num6 == 0.0)
                {
                    base.SetNodeRectanglesToEmpty(aoSortedNodes, i, i);
                    i++;
                }
                else
                {
                    bool flag = num2 != -1;
                    if (flag)
                    {
                        SaveInsertedRectangles(aoSortedNodes, num2, num3);
                    }
                    num4 += num6;
                    InsertNodesInRectangle(aoSortedNodes, oParentRectangle, flag ? num2 : i, i, num4, dAreaPerSizeMetric);
                    double aspectRatio = node.AspectRatio;
                    if (aspectRatio <= num5)
                    {
                        if (flag)
                        {
                            num3++;
                        }
                        else
                        {
                            num3 = (num2 = i);
                        }
                        i++;
                        num5 = aspectRatio;
                    }
                    else
                    {
                        if (flag)
                        {
                            RestoreInsertedRectangles(aoSortedNodes, num2, num3);
                        }
                        oParentRectangle = GetRemainingEmptySpace(aoSortedNodes, oParentRectangle, num2, num3);
                        num2 = -1;
                        num3 = -1;
                        num4 = 0.0;
                        num5 = 1.7976931348623157E+308;
                    }
                }
            }
        }

        protected void InsertNodesInRectangle(Node[] aoSortedNodes, RectangleF oParentRectangle,
                                              int iIndexOfFirstNodeToInsert, int iIndexOfLastNodeToInsert,
                                              double dSizeMetricSum, double dAreaPerSizeMetric)
        {
            Debug.Assert(aoSortedNodes != null);
            Debug.Assert(oParentRectangle.Width > 0f);
            Debug.Assert(oParentRectangle.Height > 0f);
            Debug.Assert(iIndexOfFirstNodeToInsert >= 0);
            Debug.Assert(iIndexOfLastNodeToInsert >= 0);
            Debug.Assert(iIndexOfLastNodeToInsert >= iIndexOfFirstNodeToInsert);
            Debug.Assert(iIndexOfLastNodeToInsert < aoSortedNodes.Length);
            Debug.Assert(dSizeMetricSum > 0.0);
            Debug.Assert(dAreaPerSizeMetric >= 0.0);
            bool flag = oParentRectangle.Width >= oParentRectangle.Height;
            double num = (flag ? oParentRectangle.Height : oParentRectangle.Width);
            Debug.Assert(num != 0.0);
            double num2 = dAreaPerSizeMetric*dSizeMetricSum;
            double num3 = num2/num;
            double num4;
            double num5;
            double num7;
            double num6;
            if (flag)
            {
                num4 = oParentRectangle.Left;
                num5 = num4 + num3;
                num6 = (num7 = (m_bBottomWeighted ? oParentRectangle.Bottom : oParentRectangle.Top));
            }
            else
            {
                if (m_bBottomWeighted)
                {
                    num6 = oParentRectangle.Bottom;
                    num7 = num6 - num3;
                }
                else
                {
                    num7 = oParentRectangle.Top;
                    num6 = num7 + num3;
                }
                num5 = (num4 = oParentRectangle.Left);
            }
            for (int i = iIndexOfFirstNodeToInsert; i <= iIndexOfLastNodeToInsert; i++)
            {
                Node node = aoSortedNodes[i];
                Debug.Assert(dSizeMetricSum != 0.0);
                double num8 = num*(node.SizeMetric/dSizeMetricSum);
                if (flag)
                {
                    if (m_bBottomWeighted)
                    {
                        num7 = num6 - num8;
                    }
                    else
                    {
                        num6 = num7 + num8;
                    }
                }
                else
                {
                    num5 = num4 + num8;
                }
                node.Rectangle = RectangleF.FromLTRB((float) num4, (float) num7, (float) num5, (float) num6);
                Debug.Assert(node.Rectangle.Width >= 0f);
                Debug.Assert(node.Rectangle.Height >= 0f);
                if (flag)
                {
                    if (m_bBottomWeighted)
                    {
                        num6 = num7;
                    }
                    else
                    {
                        num7 = num6;
                    }
                }
                else
                {
                    num4 = num5;
                }
            }
        }

        protected void SaveInsertedRectangles(Node[] aoNodes, int iIndexOfFirstInsertedNode,
                                              int iIndexOfLastInsertedNode)
        {
            Debug.Assert(aoNodes != null);
            Debug.Assert(iIndexOfFirstInsertedNode >= 0);
            Debug.Assert(iIndexOfLastInsertedNode >= 0);
            Debug.Assert(iIndexOfLastInsertedNode >= iIndexOfFirstInsertedNode);
            Debug.Assert(iIndexOfLastInsertedNode < aoNodes.Length);
            for (int i = iIndexOfFirstInsertedNode; i <= iIndexOfLastInsertedNode; i++)
            {
                aoNodes[i].SaveRectangle();
            }
        }

        protected void RestoreInsertedRectangles(Node[] aoNodes, int iIndexOfFirstInsertedNode,
                                                 int iIndexOfLastInsertedNode)
        {
            Debug.Assert(aoNodes != null);
            Debug.Assert(iIndexOfFirstInsertedNode >= 0);
            Debug.Assert(iIndexOfLastInsertedNode >= 0);
            Debug.Assert(iIndexOfLastInsertedNode >= iIndexOfFirstInsertedNode);
            Debug.Assert(iIndexOfLastInsertedNode < aoNodes.Length);
            for (int i = iIndexOfFirstInsertedNode; i <= iIndexOfLastInsertedNode; i++)
            {
                aoNodes[i].RestoreRectangle();
            }
        }

        protected double GetAreaPerSizeMetric(Nodes oNodes, RectangleF oParentRectangle, Node oParentNode)
        {
            Debug.Assert(oNodes != null);
            Debug.Assert(oParentRectangle.Width > 0f);
            Debug.Assert(oParentRectangle.Height > 0f);
            double num = (oParentRectangle.Width*oParentRectangle.Height);
            double num2 = 0.0;
            foreach (Node current in oNodes)
            {
                num2 += current.SizeMetric;
            }
            num2 += oNodes.EmptySpace.SizeMetric;
            double result;
            if (num2 == 0.0)
            {
                result = 0.0;
            }
            else
            {
                Debug.Assert(num2 != 0.0);
                result = num/num2;
            }
            return result;
        }

        protected RectangleF GetRemainingEmptySpace(Node[] aoNodes, RectangleF oParentRectangle,
                                                    int iIndexOfFirstInsertedNode, int iIndexOfLastInsertedNode)
        {
            Debug.Assert(aoNodes != null);
            Debug.Assert(oParentRectangle.Width > 0f);
            Debug.Assert(oParentRectangle.Height > 0f);
            Debug.Assert(iIndexOfFirstInsertedNode >= 0);
            Debug.Assert(iIndexOfLastInsertedNode >= 0);
            Debug.Assert(iIndexOfLastInsertedNode >= iIndexOfFirstInsertedNode);
            Debug.Assert(iIndexOfLastInsertedNode < aoNodes.Length);
            RectangleF rectangle = aoNodes[iIndexOfLastInsertedNode].Rectangle;
            RectangleF result;
            if (oParentRectangle.Width >= oParentRectangle.Height)
            {
                result = RectangleF.FromLTRB(rectangle.Right, oParentRectangle.Top, oParentRectangle.Right,
                                             oParentRectangle.Bottom);
            }
            else
            {
                if (m_bBottomWeighted)
                {
                    result = RectangleF.FromLTRB(oParentRectangle.Left, oParentRectangle.Top, oParentRectangle.Right,
                                                 rectangle.Top);
                }
                else
                {
                    result = RectangleF.FromLTRB(oParentRectangle.Left, rectangle.Bottom, oParentRectangle.Right,
                                                 oParentRectangle.Bottom);
                }
            }
            if (result.IsEmpty)
            {
                result = RectangleF.FromLTRB(0f, 0f, 0f, 0f);
            }
            return result;
        }
    }
}