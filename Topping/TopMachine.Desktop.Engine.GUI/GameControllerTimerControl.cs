using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TopMachine.Topping.Engine.GUI
{
    class GameControllerTimer: IDisposable
    {

        public delegate void OnChronoUpdateEvent(double timeSpent, double timeLeft);
        public delegate void OnChronoEndEvent();

        public event OnChronoUpdateEvent OnChronoUpdate;
        public event OnChronoEndEvent OnChronoEnd;

        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        public TimeSpan TimeStart;
        public TimeSpan TimeCurrent;
        public TimeSpan TimeOld;
        public TimeSpan PauseTime; 
        public double MaxTime;
        public bool busy = false; 


        public GameControllerTimer(double seconds)
        {
            MaxTime = seconds;
        }

        public void StartTimer()
        {
            busy = false;

            if (tmr != null)
            {
                tmr = new System.Windows.Forms.Timer();
                TimeStart = new TimeSpan(DateTime.Now.Ticks);
                tmr.Interval = 100;
                tmr.Tick += new EventHandler(tmr_Tick);
            }

            tmr.Enabled = true;

        }

        void tmr_Tick(object sender, EventArgs e)
        {
            // IF STOP REQUEST then NOTHING TO DO 

            if (tmr == null || busy) return;

            busy = true;
            TimeCurrent = new TimeSpan(DateTime.Now.Ticks);


            TimeSpan tot = TimeCurrent.Subtract(TimeStart);

            if (TimeCurrent.Seconds != TimeOld.Seconds)
            {
                if (OnChronoUpdate != null)
                {
                    OnChronoUpdate(tot.TotalSeconds, (double)MaxTime - tot.TotalSeconds);
                }
            }

            TimeOld = new TimeSpan(DateTime.Now.Ticks);

            if (tot.TotalSeconds > MaxTime)
            {
                OnChronoEnd();
            }

            busy = false;
        }

        public float Stop()
        {
            if (tmr != null)
            {
                tmr.Enabled = false;
            }

            busy = true;
            TimeCurrent = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan tot = TimeCurrent.Subtract(TimeStart);
            return (float) tot.TotalSeconds;
            
        }

        public bool paused = false; 
        public bool Pause()
        {
            if (!paused)
            {
                paused = true;
                busy = true;
                tmr.Enabled = false; 
                TimeCurrent = new TimeSpan(DateTime.Now.Ticks);
                PauseTime = TimeCurrent.Subtract(TimeStart);
                return true;
            }
            else
            {
                paused = false;
                busy = false;

                TimeStart = new TimeSpan(DateTime.Now.Ticks).Subtract(PauseTime);
                TimeOld = new TimeSpan(DateTime.Now.Ticks);
                tmr.Enabled = true;
                return false;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (tmr != null)
            {
                tmr.Enabled = false; 
                tmr.Dispose();
                tmr = null; 
            }
        }

        #endregion
    }
}
