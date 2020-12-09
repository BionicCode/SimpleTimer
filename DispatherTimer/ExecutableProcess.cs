using System;
using System.Diagnostics;
using System.Threading;

namespace SimpleTimer
{
  public class ExecutableProcess : IDisposable
  {
    private Timer processTimer;
    private TimeSpan interval;
    private Action processToRun;
    private bool canStart = true;
    private bool IsPaused;

    public ExecutableProcess(TimeSpan intervalSeconds, Action process)
    {
      this.interval = intervalSeconds;
      processToRun = process;
    }

    public void Start()
    {
      if (canStart)
      {
        canStart = false;
        IsPaused = false;
        processTimer = new Timer(TimedProcess);
        processTimer.Change(0, (int) this.interval.TotalMilliseconds);
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
      processToRun.Invoke();
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