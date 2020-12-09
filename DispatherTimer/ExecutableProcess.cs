using System;
using System.Diagnostics;
using System.Threading;

namespace SimpleTimer
{
    public class ExecutableProcess
    {
        private Timer processTimer;
        private int delay;
        private CancellationTokenSource source;
        private CancellationToken token;
        private Action processToRun;
        private bool canStart = true;
        private bool IsPaused;

        public ExecutableProcess(int delaySeconds, Action process)
        {
            delay = delaySeconds;
            processToRun = process;
        }

        public void Start()
        {
            if (canStart)
            {
                canStart = false;
                source = new CancellationTokenSource();
                token = source.Token;
                processTimer = new Timer(TimedProcess, token, Timeout.Infinite, Timeout.Infinite);
                processTimer.Change(0, Timeout.Infinite);
                IsPaused = false;
            }

        }

        public void TogglePause()
        {
            if (IsPaused == true)
            {
                Debug.WriteLine("RESUMEEE");
                processTimer.Change(0, Timeout.Infinite);
                IsPaused = false; 
            }
            else
            {
                Debug.WriteLine("PAUSEEE");
                processTimer.Change(Timeout.Infinite, Timeout.Infinite);
                IsPaused = true;
            }
        }

        public void Stop()
        {
            source.Cancel();
        }

        public void TimedProcess(object state)
        {

            CancellationToken ct = (CancellationToken)state;
            if (ct.IsCancellationRequested)
            {
                processTimer.Dispose();
                canStart = true;
            }
            else
            {
                processToRun.Invoke();
                processTimer.Change(delay, Timeout.Infinite);
            }
        }

    }
}
