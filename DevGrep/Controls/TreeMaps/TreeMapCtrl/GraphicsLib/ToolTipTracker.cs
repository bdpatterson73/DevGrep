using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DevGrep.Controls.TreeMaps.TreeMapCtrl.GraphicsLib
{
    internal class ToolTipTracker : IDisposable
    {
        public delegate void ToolTipTrackerEvent(object oSource, ToolTipTrackerEventArgs oToolTipTrackerEventArgs);

        public const int MinDelayMs = 1;
        public const int MaxDelayMs = 10000;
        protected const int DefaultShowDelayMs = 500;
        protected const int DefaultHideDelayMs = 5000;
        protected const int DefaultReshowDelayMs = 50;
        protected bool m_bDisposed;
        protected int m_iHideDelayMs;
        protected int m_iReshowDelayMs;
        protected int m_iShowDelayMs;
        protected State m_iState;
        protected object m_oObjectBeingTracked;
        protected Timer m_oTimer;

        public ToolTipTracker()
        {
            m_iShowDelayMs = 500;
            m_iHideDelayMs = 5000;
            m_iReshowDelayMs = 50;
            m_iState = State.NotDoingAnything;
            m_oObjectBeingTracked = null;
            m_bDisposed = false;
            m_oTimer = new Timer();
            m_oTimer.Tick += (TimerTick);
        }

        //{
        //    [MethodImpl(32)]
        //    add
        //    {
        //        this.HideToolTip = (ToolTipTracker.ToolTipTrackerEvent)Delegate.Combine(this.HideToolTip, value);
        //    }
        //    [MethodImpl(32)]
        //    remove
        //    {
        //        this.HideToolTip = (ToolTipTracker.ToolTipTrackerEvent)Delegate.Remove(this.HideToolTip, value);
        //    }
        //}
        public int ShowDelayMs
        {
            get
            {
                AssertValid();
                return m_iShowDelayMs;
            }
            set
            {
                ValidateDelayProperty(value, "ShowDelayMs");
                m_iShowDelayMs = value;
            }
        }

        public int HideDelayMs
        {
            get
            {
                AssertValid();
                return m_iHideDelayMs;
            }
            set
            {
                ValidateDelayProperty(value, "HideDelayMs");
                m_iHideDelayMs = value;
            }
        }

        public int ReshowDelayMs
        {
            get
            {
                AssertValid();
                return m_iReshowDelayMs;
            }
            set
            {
                ValidateDelayProperty(value, "ReshowDelayMs");
                m_iReshowDelayMs = value;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public event ToolTipTrackerEvent ShowToolTip;
        //{
        //    [MethodImpl(32)]
        //    add
        //    {
        //        this.ShowToolTip = (ToolTipTracker.ToolTipTrackerEvent)Delegate.Combine(this.ShowToolTip, value);
        //    }
        //    [MethodImpl(32)]
        //    remove
        //    {
        //        this.ShowToolTip = (ToolTipTracker.ToolTipTrackerEvent)Delegate.Remove(this.ShowToolTip, value);
        //    }
        //}
        public event ToolTipTrackerEvent HideToolTip;

        ~ToolTipTracker()
        {
            Dispose(false);
        }

        public void OnMouseMoveOverObject(object oObjectToTrack)
        {
            AssertValid();
            switch (m_iState)
            {
                case State.NotDoingAnything:
                    if (oObjectToTrack != null)
                    {
                        ChangeState(State.WaitingForShowTimeout, oObjectToTrack);
                    }
                    break;
                case State.WaitingForShowTimeout:
                    if (oObjectToTrack == null)
                    {
                        ChangeState(State.NotDoingAnything, null);
                    }
                    else
                    {
                        if (oObjectToTrack != m_oObjectBeingTracked)
                        {
                            ChangeState(State.WaitingForShowTimeout, oObjectToTrack);
                        }
                    }
                    break;
                case State.WaitingForHideTimeout:
                    if (oObjectToTrack == null)
                    {
                        FireHideToolTipEvent(m_oObjectBeingTracked);
                        ChangeState(State.WaitingForReshowTimeout, null);
                    }
                    else
                    {
                        if (oObjectToTrack == m_oObjectBeingTracked)
                        {
                            ChangeState(State.WaitingForHideTimeout, oObjectToTrack);
                        }
                        else
                        {
                            FireHideToolTipEvent(m_oObjectBeingTracked);
                            FireShowToolTipEvent(oObjectToTrack);
                            ChangeState(State.WaitingForHideTimeout, oObjectToTrack);
                        }
                    }
                    break;
                case State.WaitingForReshowTimeout:
                    if (oObjectToTrack != null)
                    {
                        FireShowToolTipEvent(oObjectToTrack);
                        ChangeState(State.WaitingForHideTimeout, oObjectToTrack);
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public void Reset()
        {
            AssertValid();
            m_oTimer.Stop();
            if (m_iState == State.WaitingForHideTimeout)
            {
                FireHideToolTipEvent(m_oObjectBeingTracked);
            }
            ChangeState(State.NotDoingAnything, null);
        }

        protected void ValidateDelayProperty(int iValue, string sPropertyName)
        {
            if (iValue < 1 || iValue > 10000)
            {
                throw new ArgumentOutOfRangeException(sPropertyName, iValue, string.Concat(new object[]
                    {
                        "ToolTipTracker.",
                        sPropertyName,
                        ": Must be between ",
                        1,
                        " and ",
                        10000,
                        "."
                    }));
            }
        }

        protected void ChangeState(State iState, object oObjectToTrack)
        {
            AssertValid();
            m_oTimer.Stop();
            m_iState = iState;
            m_oObjectBeingTracked = oObjectToTrack;
            int num;
            switch (iState)
            {
                case State.NotDoingAnything:
                    num = -1;
                    break;
                case State.WaitingForShowTimeout:
                    num = m_iShowDelayMs;
                    break;
                case State.WaitingForHideTimeout:
                    num = m_iHideDelayMs;
                    break;
                case State.WaitingForReshowTimeout:
                    num = m_iReshowDelayMs;
                    break;
                default:
                    Debug.Assert(false);
                    num = -1;
                    break;
            }
            if (num != -1)
            {
                m_oTimer.Interval = (num);
                m_oTimer.Start();
            }
            AssertValid();
        }

        protected void FireShowToolTipEvent(object oObject)
        {
            Debug.Assert(oObject != null);
            if (ShowToolTip != null)
            {
                ShowToolTip(this, new ToolTipTrackerEventArgs(oObject));
            }
        }

        protected void FireHideToolTipEvent(object oObject)
        {
            Debug.Assert(oObject != null);
            if (HideToolTip != null)
            {
                HideToolTip(this, new ToolTipTrackerEventArgs(oObject));
            }
        }

        protected void TimerTick(object oSource, EventArgs oEventArgs)
        {
            AssertValid();
            m_oTimer.Stop();
            switch (m_iState)
            {
                case State.NotDoingAnything:
                    Debug.Assert(false);
                    break;
                case State.WaitingForShowTimeout:
                    FireShowToolTipEvent(m_oObjectBeingTracked);
                    ChangeState(State.WaitingForHideTimeout, m_oObjectBeingTracked);
                    break;
                case State.WaitingForHideTimeout:
                    FireHideToolTipEvent(m_oObjectBeingTracked);
                    ChangeState(State.WaitingForReshowTimeout, null);
                    break;
                case State.WaitingForReshowTimeout:
                    ChangeState(State.NotDoingAnything, null);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        protected void Dispose(bool bDisposing)
        {
            if (!m_bDisposed && bDisposing)
            {
                m_oTimer.Stop();
                m_oTimer.Dispose();
            }
            m_bDisposed = true;
        }

        [Conditional("DEBUG")]
        public void AssertValid()
        {
            Debug.Assert(m_iShowDelayMs >= 1);
            Debug.Assert(m_iShowDelayMs <= 10000);
            Debug.Assert(m_iHideDelayMs >= 1);
            Debug.Assert(m_iHideDelayMs <= 10000);
            Debug.Assert(m_iReshowDelayMs >= 1);
            Debug.Assert(m_iReshowDelayMs <= 10000);
            switch (m_iState)
            {
                case State.NotDoingAnything:
                    Debug.Assert(m_oObjectBeingTracked == null);
                    break;
                case State.WaitingForShowTimeout:
                    Debug.Assert(m_oObjectBeingTracked != null);
                    break;
                case State.WaitingForHideTimeout:
                    Debug.Assert(m_oObjectBeingTracked != null);
                    break;
                case State.WaitingForReshowTimeout:
                    Debug.Assert(m_oObjectBeingTracked == null);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        protected enum State
        {
            NotDoingAnything,
            WaitingForShowTimeout,
            WaitingForHideTimeout,
            WaitingForReshowTimeout
        }
    }
}