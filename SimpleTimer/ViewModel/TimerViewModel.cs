using Microsoft.Win32;
using SimpleTimer.Models.HelperClasses;
using System;
using System.Diagnostics;

namespace SimpleTimer.ViewModels
{
    public class TimerViewModel
    {
        //private static Action _timerAction;
        //private static TimeSpan SecondsBetweenRun;

        //// ExecutableProcess executableProcess = new ExecutableProcess();
        //public ExecutableProcess newProcess = new ExecutableProcess(TimerViewModel.SecondsBetweenRun, TimerViewModel._timerAction);

        //public void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        //{
        //    if (e.Reason == SessionSwitchReason.SessionLock)
        //    {
        //        Debug.Print("I am locked: " + DateTime.Now);
        //        this.newProcess.TogglePause();
        //    }
        //    else if (e.Reason == SessionSwitchReason.SessionUnlock)
        //    {
        //        Debug.Print("I am unlocked: " + DateTime.Now);
        //        this.newProcess.TogglePause();
        //    }
        //}
        //public void PauseTimer()
        //{
        //    this.newProcess.TogglePause();
        //}
    }
}
