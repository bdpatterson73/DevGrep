using System;
using System.Collections;
using System.Diagnostics;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.AppLib
{
    public class HistoryList
    {
        public delegate void ChangeEventHandler(object oSource, EventArgs oEventArgs);

        protected int m_iCurrentObjectIndex;
        protected ArrayList m_oStateList;

        public HistoryList()
        {
            m_oStateList = new ArrayList();
            m_iCurrentObjectIndex = -1;
            AssertValid();
        }

        //{
        //    [MethodImpl(32)]
        //    add
        //    {
        //        this.Change = (HistoryList.ChangeEventHandler)Delegate.Combine(this.Change, value);
        //    }
        //    [MethodImpl(32)]
        //    remove
        //    {
        //        this.Change = (HistoryList.ChangeEventHandler)Delegate.Remove(this.Change, value);
        //    }
        //}
        public object NextState
        {
            get
            {
                AssertValid();
                if (!HasNextState)
                {
                    throw new InvalidOperationException(
                        "HistoryList.NextState: There is no next state.  Check HasNextState before calling this.");
                }
                object result = m_oStateList[m_iCurrentObjectIndex + 1];
                m_iCurrentObjectIndex++;
                AssertValid();
                FireChangeEvent();
                return result;
            }
        }

        public object PreviousState
        {
            get
            {
                AssertValid();
                if (!HasPreviousState)
                {
                    throw new InvalidOperationException(
                        "HistoryList.PreviousState: There is no previous state.  Check HasPreviousState before calling this.");
                }
                object result = m_oStateList[m_iCurrentObjectIndex - 1];
                m_iCurrentObjectIndex--;
                AssertValid();
                FireChangeEvent();
                return result;
            }
        }

        public bool HasNextState
        {
            get
            {
                AssertValid();
                return m_iCurrentObjectIndex < m_oStateList.Count - 1;
            }
        }

        public bool HasPreviousState
        {
            get
            {
                AssertValid();
                return m_iCurrentObjectIndex > 0;
            }
        }

        public event ChangeEventHandler Change;

        public object InsertState(object oState)
        {
            Debug.Assert(oState != null);
            AssertValid();
            m_oStateList.RemoveRange(m_iCurrentObjectIndex + 1, m_oStateList.Count - m_iCurrentObjectIndex - 1);
            m_oStateList.Add(oState);
            m_iCurrentObjectIndex++;
            AssertValid();
            Debug.Assert(m_iCurrentObjectIndex == m_oStateList.Count - 1);
            FireChangeEvent();
            return oState;
        }

        public void Reset()
        {
            m_oStateList.Clear();
            m_iCurrentObjectIndex = -1;
            FireChangeEvent();
            AssertValid();
        }

        protected void FireChangeEvent()
        {
            if (Change != null)
            {
                var oEventArgs = new EventArgs();
                Change(this, oEventArgs);
            }
        }

        public override string ToString()
        {
            AssertValid();
            return string.Concat(new object[]
                {
                    "HistoryList object: Number of state objects: ",
                    m_oStateList.Count,
                    ".  Current object:",
                    m_iCurrentObjectIndex,
                    "."
                });
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_oStateList != null);
            Debug.Assert(m_iCurrentObjectIndex >= -1);
            if (m_iCurrentObjectIndex >= 0)
            {
                Debug.Assert(m_iCurrentObjectIndex < m_oStateList.Count);
            }
        }
    }
}