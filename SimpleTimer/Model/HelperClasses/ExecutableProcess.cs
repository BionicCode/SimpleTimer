using System;
using System.Diagnostics;
using System.Threading;

namespace SimpleTimer.Models.HelperClasses
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
            this.processToRun = process;
            this.processTimer = new Timer(TimedProcess);
            this.canStart = true;
        }
        public void Start()
        {
            if (this.canStart)
            {
                this.canStart = false;
                this.IsPaused = false;
                this.processTimer.Change(0, (int)this.Interval.TotalMilliseconds);
            }
        }
        public void TogglePause()
        {
            if (this.IsPaused)
            {
                Debug.WriteLine("RESUMEEE");
                Start();
            }
            else
            {
                Debug.WriteLine("PAUSEEE");
                Stop();
                this.IsPaused = true;
            }
        }
        public void Stop()
        {
            this.processTimer.Change(Timeout.Infinite, Timeout.Infinite);
            this.canStart = true;
        }
        public void TimedProcess(object state)
        {
            this.processToRun?.Invoke();
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