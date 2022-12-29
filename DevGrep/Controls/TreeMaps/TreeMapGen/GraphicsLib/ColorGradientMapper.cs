using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.GraphicsLib
{
    public class ColorGradientMapper
    {
        protected Brush[] m_aoDiscreteBrushes;
        protected Color[] m_aoDiscreteColors;
        protected float m_fColorMetricsPerDivision;
        protected float m_fMaxColorMetric;
        protected float m_fMinColorMetric;
        protected int m_iDiscreteColorCount;

        protected internal ColorGradientMapper()
        {
            m_aoDiscreteColors = null;
            m_aoDiscreteBrushes = null;
            m_iDiscreteColorCount = 0;
            m_fMinColorMetric = 0f;
            m_fMaxColorMetric = 0f;
            m_fColorMetricsPerDivision = 0f;
        }

        public void Initialize(Graphics oGraphics, float fMinColorMetric, float fMaxColorMetric, Color oMinColor,
                               Color oMaxColor, int iDiscreteColorCount, bool bCreateBrushes)
        {
            Debug.Assert(oGraphics != null);
            if (fMaxColorMetric <= fMinColorMetric)
            {
                throw new ArgumentOutOfRangeException("fMaxColorMetric", fMaxColorMetric,
                                                      "ColorGradientMapper.Initialize: fMaxColorMetric must be > fMinColorMetric.");
            }
            if (iDiscreteColorCount < 2 || iDiscreteColorCount > 256)
            {
                throw new ArgumentOutOfRangeException("iDiscreteColorCount", iDiscreteColorCount,
                                                      "ColorGradientMapper.Initialize: iDiscreteColorCount must be between 2 and 256.");
            }
            m_fMinColorMetric = fMinColorMetric;
            m_fMaxColorMetric = fMaxColorMetric;
            m_iDiscreteColorCount = iDiscreteColorCount;
            m_fColorMetricsPerDivision = (m_fMaxColorMetric - m_fMinColorMetric)/m_iDiscreteColorCount;
            m_aoDiscreteColors = CreateDiscreteColors(oGraphics, oMinColor, oMaxColor, iDiscreteColorCount);
            if (bCreateBrushes)
            {
                m_aoDiscreteBrushes = CreateDiscreteBrushes(m_aoDiscreteColors);
            }
        }

        public Color ColorMetricToColor(float fColorMetric)
        {
            if (m_iDiscreteColorCount == 0)
            {
                throw new InvalidOperationException(
                    "ColorGradientMapper.ColorMetricToColor: Must call Initialize() first.");
            }
            int num = ColorMetricToArrayIndex(fColorMetric);
            return m_aoDiscreteColors[num];
        }

        public Brush ColorMetricToBrush(float fColorMetric)
        {
            if (m_iDiscreteColorCount == 0)
            {
                throw new InvalidOperationException(
                    "ColorGradientMapper.ColorMetricToBrush: Must call Initialize() first.");
            }
            if (m_aoDiscreteBrushes == null)
            {
                throw new InvalidOperationException(
                    "ColorGradientMapper.ColorMetricToBrush: Must specify bCreateBrushes=true in Initialize() call.");
            }
            int num = ColorMetricToArrayIndex(fColorMetric);
            return m_aoDiscreteBrushes[num];
        }

        public void Dispose()
        {
            if (m_aoDiscreteBrushes != null)
            {
                Brush[] aoDiscreteBrushes = m_aoDiscreteBrushes;
                for (int i = 0; i < aoDiscreteBrushes.Length; i++)
                {
                    Brush brush = aoDiscreteBrushes[i];
                    brush.Dispose();
                }
            }
        }

        protected Color[] CreateDiscreteColors(Graphics oGraphics, Color oMinColor, Color oMaxColor,
                                               int iDiscreteColorCount)
        {
            Debug.Assert(oGraphics != null);
            Debug.Assert(iDiscreteColorCount > 1);
            var array = new Color[iDiscreteColorCount];
            var bitmap = new Bitmap(1, iDiscreteColorCount, oGraphics);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rectangle = Rectangle.FromLTRB(0, 0, 1, iDiscreteColorCount - 1);
            var linearGradientBrush = new LinearGradientBrush(rectangle, oMinColor, oMaxColor, 1);
            graphics.FillRectangle(linearGradientBrush, new Rectangle(Point.Empty, bitmap.Size));
            linearGradientBrush.Dispose();
            int i;
            for (i = 0; i < iDiscreteColorCount - 1; i++)
            {
                array[i] = bitmap.GetPixel(0, i);
            }
            array[i] = oMaxColor;
            bitmap.Dispose();
            return array;
        }

        protected Brush[] CreateDiscreteBrushes(Color[] aoDiscreteColors)
        {
            int num = aoDiscreteColors.Length;
            var array = new Brush[num];
            for (int i = 0; i < num; i++)
            {
                array[i] = new SolidBrush(aoDiscreteColors[i]);
            }
            return array;
        }

        protected int ColorMetricToArrayIndex(float fColorMetric)
        {
            Debug.Assert(m_iDiscreteColorCount != 0);
            int num;
            if (fColorMetric <= m_fMinColorMetric)
            {
                num = 0;
            }
            else
            {
                if (fColorMetric >= m_fMaxColorMetric)
                {
                    num = m_iDiscreteColorCount - 1;
                }
                else
                {
                    num = (int) ((fColorMetric - m_fMinColorMetric)/m_fColorMetricsPerDivision);
                }
            }
            Debug.Assert(num >= 0);
            Debug.Assert(num < m_iDiscreteColorCount);
            return num;
        }
    }
}