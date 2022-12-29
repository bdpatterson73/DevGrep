using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace DevGrep.Controls.TreeMaps.TreeMapGen.GraphicsLib
{
    public class TransparentBrushMapper
    {
        private int m_iTransparentBrushes;
        private ArrayList m_oTransparentBrushes;

        protected internal TransparentBrushMapper()
        {
            m_oTransparentBrushes = null;
            m_iTransparentBrushes = 0;
        }

        public void Initialize(Color oSolidColor, int iMinAlpha, int iMaxAlpha, int iAlphaIncrementPerLevel)
        {
            if (oSolidColor.A != 255)
            {
                throw new ArgumentOutOfRangeException("oSolidColor", oSolidColor,
                                                      "TransparentBrushMapper.Initialize(): oSolidColor must not be transparent.");
            }
            ValidateAlphaRange(iMinAlpha, iMaxAlpha, iAlphaIncrementPerLevel, "TransparentBrushMapper.Initialize()");
            m_oTransparentBrushes = new ArrayList();
            for (int i = iMinAlpha; i <= iMaxAlpha; i += iAlphaIncrementPerLevel)
            {
                Color color = Color.FromArgb(i, oSolidColor);
                Brush brush = new SolidBrush(color);
                m_oTransparentBrushes.Add(brush);
            }
            m_iTransparentBrushes = m_oTransparentBrushes.Count;
            AssertValid();
        }

        public Brush LevelToTransparentBrush(int iLevel)
        {
            AssertValid();
            if (iLevel < 0)
            {
                throw new ArgumentOutOfRangeException("iLevel", iLevel,
                                                      "TransparentBrushMapper.LevelToTransparentBrush: iLevel must be >= 0.");
            }
            if (iLevel >= m_iTransparentBrushes)
            {
                iLevel = m_iTransparentBrushes - 1;
            }
            return (Brush) m_oTransparentBrushes[iLevel];
        }

        public void Dispose()
        {
            AssertValid();
            if (m_oTransparentBrushes != null)
            {
                IEnumerator enumerator = m_oTransparentBrushes.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var brush = (Brush) enumerator.Current;
                        brush.Dispose();
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
        }

        protected internal static void ValidateAlphaRange(int iMinAlpha, int iMaxAlpha, int iIncrementPerLevel,
                                                          string sCaller)
        {
            if (iMinAlpha < 0 || iMinAlpha > 255)
            {
                throw new ArgumentOutOfRangeException("iMinAlpha", iMinAlpha,
                                                      sCaller + ": iMinAlpha must be between 0 and 255.");
            }
            if (iMaxAlpha < 0 || iMaxAlpha > 255)
            {
                throw new ArgumentOutOfRangeException("iMaxAlpha", iMaxAlpha,
                                                      sCaller + ": iMaxAlpha must be between 0 and 255.");
            }
            if (iMaxAlpha < iMinAlpha)
            {
                throw new ArgumentOutOfRangeException("iMaxAlpha", iMaxAlpha,
                                                      sCaller + ": iMaxAlpha must be >= iMinAlpha.");
            }
            if (iIncrementPerLevel <= 0)
            {
                throw new ArgumentOutOfRangeException("iIncrementPerLevel", iIncrementPerLevel,
                                                      sCaller + ": iIncrementPerLevel must be > 0.");
            }
        }

        [Conditional("DEBUG")]
        protected internal void AssertValid()
        {
            Debug.Assert(m_oTransparentBrushes != null);
            Debug.Assert(m_iTransparentBrushes != 0);
        }
    }
}