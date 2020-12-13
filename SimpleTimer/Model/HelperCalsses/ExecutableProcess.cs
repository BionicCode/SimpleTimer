using System;
using System.Diagnostics;
using System.Threading;

namespace SimpleTimer.HelperClass
{
    public class ExecutableProcess : IDisposable
    {
        private Timer processTimer;
        private TimeSpan Interval { get; set; }
        private Action processToRun;
        private bool canStart;
        private bool IsPaused;
        public ExecutableProcess(TimeSpan intervalSeconds, Action process)
        {
            this.Interval = intervalSeconds;
            processToRun = process;
            processTimer = new Timer(TimedProcess);
            this.canStart = true;
        }
        public void Start()
        {
            if (canStart)
            {
                canStart = false;
                IsPaused = false;
                processTimer.Change(0, (int)this.Interval.TotalMilliseconds);
            }
        }
        public void TogglePause()
        {
            if (IsPaused)
            {
                Debug.WriteLine("RESUMEEE");
                Start();
            }
            else
            {
                Debug.WriteLine("PAUSEEE");
                Stop();
                IsPaused = true;
            }
        }
        public void Stop()
        {
            processTimer.Change(Timeout.Infinite, Timeout.Infinite);
            this.canStart = true;
        }
        public void TimedProcess(object state)
        {
            processToRun?.Invoke();
        }
        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.processTimer?.Dispose();
            }
        }
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}