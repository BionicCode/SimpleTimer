using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace DispatherTimer
{
    class ViewModel : BaseViewModel
    {

        public static string HoursLimitConfProp = "00:05";
        public static readonly int StartFromSecConfProp = 50;

        public ViewModel()
        {
            HoursLimitProp = HoursLimitConfProp;

            StartWorkingTimeTodayTimer();

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

            Debug.WriteLine("Current time: " + CurrentTime);
            Debug.WriteLine("Ring time: " + RingTime.ToLongTimeString());

            if (CurrentTime == RingTime.ToLongTimeString())
            {
                PlaySound();
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
