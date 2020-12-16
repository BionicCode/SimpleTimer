using SimpleTimer.Models.HelperClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTimer.ViewModels
{
    public class TimerViewModel
    {
    // BUG::Not really a bug. Avoid static members. Better use a private maybe read-only auto-property. According to the context (the way you are using this members) simply remove them
        //private static Action _timerAction;
        //private static TimeSpan SecondsBetweenRun;

        // ExecutableProcess executableProcess = new ExecutableProcess();

        // BUG::1:Do not have public fields. Field must be a property (don't forget to fix usages after turning this field into a property which also requires to changes the name to PascalCase.
        // BUG::2:Initialize from constructor using Variant 1 or Variant 2 (see ViewModel class constructor) or Variant 3: property initialization (which is what you are currently using, so lets stick to it). 
        //public ExecutableProcess newProcess = new ExecutableProcess(TimerViewModel.SecondsBetweenRun, _timerAction);
        public ExecutableProcess NewProcess { get; set; }

        public void PauseTimer()
        {
            this.NewProcess.TogglePause();
        }
    }
}
