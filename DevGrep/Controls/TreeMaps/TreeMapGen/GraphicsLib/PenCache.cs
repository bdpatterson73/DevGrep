using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.GraphicsLib
{
    public class PenCache
    {
        private Color m_oPenColor;
        private Hashtable m_oPens;

        protected internal PenCache()
        {
            m_oPens = null;
        }

        public void Initialize(Color oPenColor)
        {
            if (m_oPens != null)
            {
                Dispose();
            }
            m_oPens = new Hashtable();
            m_oPenColor = oPenColor;
            AssertValid();
        }

        public Pen GetPen(int iWidthPx)
        {
            if (iWidthPx <= 0)
            {
                throw new ArgumentOutOfRangeException("iWidthPx", iWidthPx, "PenCache.GetPen(): iWidthPx must be > 0.");
            }
            var pen = (Pen) m_oPens[iWidthPx];
            if (pen == null)
            {
                pen = new Pen(m_oPenColor, iWidthPx);
                pen.Alignment = PenAlignment.Inset;
                m_oPens.Add(iWidthPx, pen); //BDP this.m_oPens.set_Item(iWidthPx, pen);
            }
            return pen;
        }

        public void Dispose()
        {
            AssertValid();
            if (m_oPens != null)
            {
                IDictionaryEnumerator enumerator = m_oPens.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var dictionaryEntry = (DictionaryEntry) enumerator.Current;
                        ((Pen) dictionaryEntry.Value).Dispose();
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
                m_oPens = null;
            }
        }

        [Conditional("DEBUG")]
        protected internal void AssertValid()
        {
            Debug.Assert(m_oPens != null);
        }
    }
}