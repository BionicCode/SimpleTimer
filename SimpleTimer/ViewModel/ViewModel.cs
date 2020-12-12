using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SimpleTimer.ViewModel
{
    public class ViewModel : BaseViewModel
    {
        // Set the timer offset to 50 seconds
        private static readonly TimeSpan StartFromSecConfProp = TimeSpan.FromSeconds(50);
        private TimeSpan SecondsAlreadyPassed = TimeSpan.Zero;

        private static Action _timerAction;
        private static TimeSpan SecondsBetweenRun;
        //ExecutableProcess executableProcess = new ExecutableProcess();
        ExecutableProcess newProcess = new ExecutableProcess(SecondsBetweenRun, _timerAction);

        public ICommand PauseTimerCommand => new RelayCommand(param => PauseTimer());

        private TimeSpan BackgroundWorkTimerInterval { get; set; }
        private TimeSpan LabelTimerInterval { get; set; }

        public ViewModel()
        {
            //StartWorkingTimeTodayTimer();
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            // Initialize the current time elapsed field by adding the offset
            this.SecondsAlreadyPassed = this.SecondsAlreadyPassed.Add(StartFromSecConfProp);

            // We specify this method to be executed every 1 srcond // BACKGROUND WORK
            this.BackgroundWorkTimerInterval = TimeSpan.FromSeconds(1);
            var process = new ExecutableProcess(this.BackgroundWorkTimerInterval, MyProcessToExecute);
            process.Start();

            // We specify this method to be executed every 1 srcond // DISPLAYED IN LABEL
            this.LabelTimerInterval = TimeSpan.FromSeconds(1);
            newProcess = new ExecutableProcess(this.LabelTimerInterval, LabelTimer);
            newProcess.Start();
        }
        private void PauseTimer()
        {
            newProcess.TogglePause();
        }

        private void LabelTimer()
        {
            this.SecondsAlreadyPassed = this.SecondsAlreadyPassed.Add(this.LabelTimerInterval);
            CurrentTime = this.SecondsAlreadyPassed.ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()

            DateTime RingTime = HelperClass.DateTimeConverter(HoursLimitProp);

            //Debug.WriteLine("Current time: " + CurrentTime);
            //Debug.WriteLine("Ring time: " + RingTime.ToLongTimeString());

            if (CurrentTime == RingTime.ToLongTimeString())
            {
                PlaySound();
            }
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
                newProcess.TogglePause();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Debug.Print("I am unlocked: " + DateTime.Now);
                newProcess.TogglePause();
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
                if (_currentTime == value)
                    return;
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        private string _hoursLimit;

        public string HoursLimitProp
        {
            get { return _hoursLimit; }
            set
            {
                if (_hoursLimit != value)
                {
                    _hoursLimit = value;
                    OnPropertyChanged();
                }
            }
        }

        private void PlaySound()
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(string.Concat(System.IO.Directory.GetCurrentDirectory(),
                        @"\Miscellaneous\Sounds\bell.wav"));
                player.Play();

                Debug.WriteLine("+++++++++ bell ring +++++++++");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
