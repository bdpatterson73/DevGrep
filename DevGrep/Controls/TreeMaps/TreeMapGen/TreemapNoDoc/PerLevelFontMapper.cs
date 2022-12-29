using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.AppLib;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public class PerLevelFontMapper : IFontMapper, IDisposable
    {
        protected bool m_bDisposed;
        private ArrayList m_oFontForRectangles;

        protected internal PerLevelFontMapper(string sFamily, Rectangle oTreemapRectangle,
                                              float fTreemapRectangleDivisor, float fPerLevelDivisor,
                                              float fMinimumFontSize, Graphics oGraphics)
        {
            StringUtil.AssertNotEmpty(sFamily);
            Debug.Assert(fTreemapRectangleDivisor > 0f);
            Debug.Assert(fPerLevelDivisor > 0f);
            Debug.Assert(fMinimumFontSize > 0f);
            Debug.Assert(oGraphics != null);
            float num = oTreemapRectangle.Height/fTreemapRectangleDivisor;
            m_oFontForRectangles = new ArrayList();
            while (num > fMinimumFontSize)
            {
                var fontForRectangle = new FontForRectangle(sFamily, num, oGraphics);
                m_oFontForRectangles.Add(fontForRectangle);
                num /= fPerLevelDivisor;
            }
            m_bDisposed = false;
            AssertValid();
        }

        public bool NodeToFont(Node oNode, int iNodeLevel, Graphics oGraphics, out Font oFont, out string sTextToDraw)
        {
            Debug.Assert(oNode != null);
            Debug.Assert(iNodeLevel >= 0);
            Debug.Assert(oGraphics != null);
            AssertValid();
            bool result;
            if (iNodeLevel < m_oFontForRectangles.Count)
            {
                var fontForRectangle = (FontForRectangle) m_oFontForRectangles[iNodeLevel];
                string text = oNode.Text;
                if (fontForRectangle.CanFitInRectangle(text, oNode.Rectangle, oGraphics))
                {
                    oFont = fontForRectangle.Font;
                    sTextToDraw = text;
                    result = true;
                    return result;
                }
            }
            oFont = null;
            sTextToDraw = null;
            result = false;
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PerLevelFontMapper()
        {
            Dispose(false);
        }

        protected void Dispose(bool bDisposing)
        {
            if (!m_bDisposed && bDisposing)
            {
                if (m_oFontForRectangles != null)
                {
                    IEnumerator enumerator = m_oFontForRectangles.GetEnumerator();
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            var fontForRectangle = (FontForRectangle) enumerator.Current;
                            fontForRectangle.Dispose();
                        }
                    }
                    finally
                    {
                        var disposable = enumerator as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
                m_oFontForRectangles = null;
            }
            m_bDisposed = true;
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oFontForRectangles != null);
        }
    }
}