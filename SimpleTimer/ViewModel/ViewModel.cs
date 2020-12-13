using System;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Win32;
using SimpleTimer.Models.HelperClasses;

namespace SimpleTimer.ViewModels
{
    public class ViewModel : BaseViewModel
    {
        //private ExecutableProcess ExecutableProcess { get; set; }

        // Set the timer offset to 50 seconds
        private static readonly TimeSpan StartFromSecConfProp = TimeSpan.FromSeconds(50);
        private TimeSpan SecondsAlreadyPassed = TimeSpan.Zero;

        private static Action _timerAction;
        private static TimeSpan SecondsBetweenRun;
        //ExecutableProcess executableProcess = new ExecutableProcess();
        ExecutableProcess newProcess = new ExecutableProcess(ViewModel.SecondsBetweenRun, ViewModel._timerAction);

        public ICommand PauseTimerCommand => new RelayCommand(param => PauseTimer());

        private TimeSpan BackgroundWorkTimerInterval { get; set; }
        private TimeSpan LabelTimerInterval { get; set; }

        public ViewModel()
        {
            //StartWorkingTimeTodayTimer();
            // INFO::This is an application/system event. it should be handled at higher levels e.g. App.xaml.cs. App.xaml.cs also handles unhandled exceptions etc. It has a global/application scope which is more suited to listen to system event from an architectural point of view. If you are interested I can show you how to bootstrap your application manually. It's quite simple (I promise) and is a next step before trying Dependency Injection, which you can't avoid very much longer :) 
            // Time to approach new stages. You are ready in my opinion.
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            // Initialize the current time elapsed field by adding the offset
            this.SecondsAlreadyPassed = this.SecondsAlreadyPassed.Add(ViewModel.StartFromSecConfProp);

            // We specify this method to be executed every 1 srcond // BACKGROUND WORK
            this.BackgroundWorkTimerInterval = TimeSpan.FromSeconds(1);
            var process = new ExecutableProcess(this.BackgroundWorkTimerInterval, ViewModel.MyProcessToExecute);
            process.Start();

            // We specify this method to be executed every 1 srcond // DISPLAYED IN LABEL
            this.LabelTimerInterval = TimeSpan.FromSeconds(1);
            this.newProcess = new ExecutableProcess(this.LabelTimerInterval, LabelTimer);
            this.newProcess.Start();
        }
        private void PauseTimer()
        {
            this.newProcess.TogglePause();
        }

        private void LabelTimer()
        {
            this.SecondsAlreadyPassed = this.SecondsAlreadyPassed.Add(this.LabelTimerInterval);
            this.CurrentTime = this.SecondsAlreadyPassed.ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()

      // BUG::Wrong namespace. After refactoring (moving types to new projects/folders) you forgot to adjust the namespaces, so the type DateTimeConverter could not be resolved.
      // Old namespace: SimpleTimer.HelperClass. Fixed namespace:SimpleTimer.Model.HelperClass. I lived up to the occasion to rename the namespace/folder from ..Calsses to ..Classes (fixed typo)
      DateTime RingTime = HelperClass.DateTimeConverter(this.HoursLimitProp);

            //Debug.WriteLine("Current time: " + CurrentTime);
            //Debug.WriteLine("Ring time: " + RingTime.ToLongTimeString());

            if (this.CurrentTime == RingTime.ToLongTimeString())
            {
                // BUG::Not implemented.
                // FIX::Created method. Implementation pending.
                PlaySound();
            }
        }

        private void PlaySound()
        {
            throw new NotImplementedException();
        }

        private static void MyProcessToExecute()
        {
            Debug.Write($"Running {DateTime.Now}" + "\n");
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                Debug.Print("I am locked: " + DateTime.Now);
                this.newProcess.TogglePause();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Debug.Print("I am unlocked: " + DateTime.Now);
                this.newProcess.TogglePause();
            }
        }

        /// <summary>
        /// Timer implementation, working time today
        /// <summary>
        private string _currentTime;

        public string CurrentTime
        {
            get
            {
                return this._currentTime;
            }
            set
            {
                // BUG::Use always curly braces to prevent difficult to find bugs!! I wonder StyleCop is not complaining...
                if (this._currentTime == value)
                    return;
                this._currentTime = value;
                OnPropertyChanged();
            }
        }

        private string _hoursLimit;

        public string HoursLimitProp
        {
            get { return this._hoursLimit; }
            set
            {
                if (this._hoursLimit != value)
                {
                    this._hoursLimit = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
