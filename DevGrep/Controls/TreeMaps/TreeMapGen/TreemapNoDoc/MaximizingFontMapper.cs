using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.AppLib;
using DevGrep.Controls.TreeMaps.TreeMapGen.Treemap;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public class MaximizingFontMapper : IFontMapper, IDisposable
    {
        protected bool m_bDisposed;
        private ArrayList m_oFontForRectangles;

        protected internal MaximizingFontMapper(string sFamily, float fMinSizePt, float fMaxSizePt, float fIncrementPt,
                                                Graphics oGraphics)
        {
            StringUtil.AssertNotEmpty(sFamily);
            Debug.Assert(oGraphics != null);
            ValidateSizeRange(fMinSizePt, fMaxSizePt, fIncrementPt, "MaximizingFontMapper.Initialize()");
            m_oFontForRectangles = new ArrayList();
            for (float num = fMinSizePt; num <= fMaxSizePt; num += fIncrementPt)
            {
                var fontForRectangle = new FontForRectangle(sFamily, num, oGraphics);
                m_oFontForRectangles.Insert(0, fontForRectangle);
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
            string text = oNode.Text;
            RectangleF rectangle = oNode.Rectangle;
            IEnumerator enumerator = m_oFontForRectangles.GetEnumerator();
            bool result;
            try
            {
                while (enumerator.MoveNext())
                {
                    var fontForRectangle = (FontForRectangle) enumerator.Current;
                    if (fontForRectangle.CanFitInRectangle(text, rectangle, oGraphics))
                    {
                        oFont = fontForRectangle.Font;
                        sTextToDraw = text;
                        result = true;
                        return result;
                    }
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

        ~MaximizingFontMapper()
        {
            Dispose(false);
        }

        protected internal static void ValidateSizeRange(float fMinSizePt, float fMaxSizePt, float fIncrementPt,
                                                         string sCaller)
        {
            if (fMinSizePt <= 0f)
            {
                throw new ArgumentOutOfRangeException("fMinSizePt", fMinSizePt, sCaller + ": fMinSizePt must be > 0.");
            }
            if (fMaxSizePt <= 0f)
            {
                throw new ArgumentOutOfRangeException("fMaxSizePt", fMaxSizePt, sCaller + ": fMaxSizePt must be > 0.");
            }
            if (fMaxSizePt < fMinSizePt)
            {
                throw new ArgumentOutOfRangeException("fMaxSizePt", fMaxSizePt,
                                                      sCaller + ": fMaxSizePt must be >= fMinSizePt.");
            }
            if (fIncrementPt <= 0f)
            {
                throw new ArgumentOutOfRangeException("fIncrementPt", fIncrementPt,
                                                      sCaller + ": fIncrementPt must be > 0.");
            }
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