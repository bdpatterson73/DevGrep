using System;
using System.Diagnostics;
using System.Drawing;
using DevGrep.Controls.TreeMaps.TreeMapGen.AppLib;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.TreemapNoDoc
{
    public class FontForRectangle : IDisposable
    {
        protected const int MinTruncatableTextLength = 4;
        protected bool m_bDisposed;
        protected Font m_oFont;

        protected internal FontForRectangle(string sFamily, float fEmSize, Graphics oGraphics)
        {
            StringUtil.AssertNotEmpty(sFamily);
            Debug.Assert(fEmSize > 0f);
            Debug.Assert(oGraphics != null);
            m_oFont = new Font(sFamily, fEmSize);
            m_bDisposed = false;
            AssertValid();
        }

        public Font Font
        {
            get
            {
                AssertValid();
                return m_oFont;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FontForRectangle()
        {
            Dispose(false);
        }

        public bool CanFitInRectangle(string sText, RectangleF oRectangle, Graphics oGraphics)
        {
            Debug.Assert(sText != null);
            Debug.Assert(oGraphics != null);
            AssertValid();
            SizeF sizeF = oGraphics.MeasureString(sText, Font);
            return sizeF.Width < oRectangle.Width && sizeF.Height < oRectangle.Height;
        }

        public bool CanFitInRectangleTruncate(ref string sText, RectangleF oRectangle, Graphics oGraphics)
        {
            Debug.Assert(sText != null);
            Debug.Assert(oGraphics != null);
            AssertValid();
            bool result;
            if (CanFitInRectangle(sText, oRectangle, oGraphics))
            {
                result = true;
            }
            else
            {
                string text;
                if (TruncateText(sText, out text) && CanFitInRectangle(text, oRectangle, oGraphics))
                {
                    sText = text;
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        protected bool TruncateText(string sText, out string sTruncatedText)
        {
            Debug.Assert(sText != null);
            AssertValid();
            bool result;
            if (sText.Length < 4)
            {
                sTruncatedText = null;
                result = false;
            }
            else
            {
                sTruncatedText = sText.Substring(0, 3) + "...";
                result = true;
            }
            return result;
        }

        protected void Dispose(bool bDisposing)
        {
            if (!m_bDisposed && bDisposing && m_oFont != null)
            {
                m_oFont.Dispose();
                m_oFont = null;
            }
            m_bDisposed = true;
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oFont != null);
        }
    }
}