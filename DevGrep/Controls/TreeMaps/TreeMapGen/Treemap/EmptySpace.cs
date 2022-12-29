using System.Diagnostics;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.Treemap
{
    public class EmptySpace
    {
        protected float m_fSizeMetric;
        protected TreemapGenerator m_oTreemapGenerator;

        protected internal EmptySpace()
        {
            m_oTreemapGenerator = null;
            m_fSizeMetric = 0f;
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
                Node.ValidateSizeMetric(value, "EmptySpace.SizeMetric");
                if (m_fSizeMetric != value)
                {
                    m_fSizeMetric = value;
                    FireRedrawRequired();
                }
            }
        }

        protected internal TreemapGenerator TreemapGenerator
        {
            set
            {
                m_oTreemapGenerator = value;
                AssertValid();
            }
        }

        protected void FireRedrawRequired()
        {
            if (m_oTreemapGenerator != null)
            {
                m_oTreemapGenerator.FireRedrawRequired();
            }
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Node.ValidateSizeMetric(m_fSizeMetric, "EmptySpace.m_fSizeMetric");
        }
    }
}