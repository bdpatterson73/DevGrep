using System;
using System.Diagnostics;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.GraphicsLib
{
    internal class ToolTipTrackerEventArgs : EventArgs
    {
        private readonly object m_oObject;

        public ToolTipTrackerEventArgs(object oObject)
        {
            m_oObject = oObject;
        }

        public object Object
        {
            get
            {
                AssertValid();
                return m_oObject;
            }
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oObject != null);
        }
    }
}