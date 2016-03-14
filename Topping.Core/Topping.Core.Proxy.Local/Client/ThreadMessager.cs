namespace Topping.Core.Proxy.Local.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using TopMachine.Topping.Dto;

    public class ThreadMessager
    {
        public static ThreadMessager Instance;
        public bool isLoading = false;
        public bool isSuspended = false;
        private Queue<MessageDto> Messages = new Queue<MessageDto>();
        private bool stop = false;
        private bool suspended = false;
        private static object syncRoot = new object();
        private static Thread thread;
        public Timer timer = null;

        public static void InitializeInstance()
        {
        }

        public void LoadTimer()
        {
        }

        public void Resume()
        {
            this.suspended = false;
        }

        public void StopTimer()
        {
            this.stop = true;
        }

        public void Suspend()
        {
            this.suspended = true;
        }

        private void TimerRoomsProc(object state)
        {
            if (!this.stop)
            {
                try
                {
                    if (!this.suspended)
                    {
                        MessageProxy.Proxy.GetMessages();
                    }
                }
                finally
                {
                }
                if (!this.stop)
                {
                    this.timer.Change(0x1388, 0);
                }
            }
            else
            {
                this.timer.Dispose();
            }
        }
    }
}

