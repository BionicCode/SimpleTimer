﻿using SimpleTimer.Models.HelperClasses;
using System;

namespace SimpleTimer.ViewModels
{
    public class TimerViewModel
    {
        private static Action _timerAction;
        private static TimeSpan SecondsBetweenRun;

        // ExecutableProcess executableProcess = new ExecutableProcess();
        public ExecutableProcess newProcess = new ExecutableProcess(TimerViewModel.SecondsBetweenRun, TimerViewModel._timerAction);

        public void PauseTimer()
        {
            this.newProcess.TogglePause();
        }
    }
}
