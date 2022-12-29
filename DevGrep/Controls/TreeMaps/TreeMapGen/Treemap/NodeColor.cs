using System.Diagnostics;
using System.Drawing;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.Treemap
{
    //[StructLayout(LayoutKind.Explicit)]
    internal struct NodeColor
    {
        //BDP [FieldOffset(0)]
        private float m_fColorMetric;
        //BDP [FieldOffset(0)]
        private Color m_oAbsoluteColor;

        public NodeColor(float fColorMetric)
        {
            m_oAbsoluteColor = Color.Black;
            m_fColorMetric = fColorMetric;
            AssertValid();
        }

        public NodeColor(Color oAbsoluteColor)
        {
            m_fColorMetric = 0f;
            m_oAbsoluteColor = oAbsoluteColor;
        }

        public float ColorMetric
        {
            get
            {
                AssertValid();
                float result;
                if (float.IsNaN(m_fColorMetric))
                {
                    result = 0f;
                }
                else
                {
                    result = m_fColorMetric;
                }
                return result;
            }
            set
            {
                Debug.Assert(!float.IsNaN(value));
                m_fColorMetric = value;
                AssertValid();
            }
        }

        public Color AbsoluteColor
        {
            get
            {
                AssertValid();
                return m_oAbsoluteColor;
            }
            set
            {
                m_oAbsoluteColor = value;
                AssertValid();
            }
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
        }
    }
}