using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Threading;

namespace DispatherTimer
{
    class ViewModel : BaseViewModel
    {

        public static readonly string HoursLimitConfProp = "00:01";
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
            DateTime dt = HelperClass.DateTimeConverter(HoursLimitProp);

            DateTime SecondsAlreadyPassed = DateTime.Now.AddSeconds(StartFromSecConfProp * -1);

            _dailyTimer = new DispatcherTimer(DispatcherPriority.Render);
            _dailyTimer.Interval = TimeSpan.FromSeconds(1);
            _dailyTimer.Tick += (sender, e) => { DailyTimer_Tick(sender, e, SecondsAlreadyPassed, dt); }; ;
            _dailyTimer.Start();
        }

        private void DailyTimer_Tick(object sender, EventArgs e, DateTime SecondsAlreadyPassed, DateTime RingTime)
        {
            CurrentTime = (DateTime.Now - SecondsAlreadyPassed).ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()

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
