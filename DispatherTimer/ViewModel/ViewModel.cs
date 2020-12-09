using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace SimpleTimer
{
    public class ViewModel : BaseViewModel
    {
        public static readonly int StartFromSecConfProp = 50;

        private static Action _timerAction;
        private static int SecondsBetweenRun;
        private TimeSpan BackgroundWorkTimerInterval { get; set; }
        private TimeSpan LabelTimerInterval { get; set; }
    //ExecutableProcess executableProcess = new ExecutableProcess();
    ExecutableProcess newProcess;

    public ICommand PauseTimerCommand => new RelayCommand(param => PauseTimer());

        public ViewModel()
        {
            HoursLimitProp = (string)Properties.Appsettings.Default["TimeSetting"]; // (string)Properties.Appsettings.Default["TimeSetting"]

            //StartWorkingTimeTodayTimer();

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

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

        DateTime SecondsAlreadyPassed = DateTime.Now.AddSeconds(StartFromSecConfProp * -1);

        private void LabelTimer()
        {
            this.SecondsAlreadyPassed = SecondsAlreadyPassed.AddSeconds(this.LabelTimerInterval.Seconds);
            CurrentTime = SecondsAlreadyPassed.ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()

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
                _dailyTimer.Stop();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Debug.Print("I am unlocked: " + DateTime.Now);
                _dailyTimer.Start();
            }
        }

        private DispatcherTimer _dailyTimer;
        public void StartWorkingTimeTodayTimer()
        {
            DateTime RingTime = HelperClass.DateTimeConverter(HoursLimitProp);

            DateTime SecondsAlreadyPassed = DateTime.Now.AddSeconds(StartFromSecConfProp * -1);

            _dailyTimer = new DispatcherTimer(DispatcherPriority.Render);
            _dailyTimer.Interval = TimeSpan.FromSeconds(1);
            _dailyTimer.Tick += (sender, e) => { DailyTimer_Tick(sender, e, SecondsAlreadyPassed, RingTime); }; ;
            _dailyTimer.Start();
        }

        private void DailyTimer_Tick(object sender, EventArgs e, DateTime SecondsAlreadyPassed, DateTime RingTime)
        {
            CurrentTime = (DateTime.Now - SecondsAlreadyPassed).ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()

            RingTime = HelperClass.DateTimeConverter(HoursLimitProp);

            //Debug.WriteLine("Current time: " + CurrentTime);
            //Debug.WriteLine("Ring time: " + RingTime.ToLongTimeString());

            if (CurrentTime == RingTime.ToLongTimeString())
            {
                PlaySound();
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
