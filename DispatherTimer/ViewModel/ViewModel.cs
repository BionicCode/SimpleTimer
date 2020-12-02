using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        private static DateTime StartTimeWholeDay;
        private DispatcherTimer _dailyTimer;
        public void StartWorkingTimeTodayTimer()
        {
            int AlreadyWorkedTime = 0;
            DateTime RingTime = DateTime.Today.AddDays(10);

            if (!string.IsNullOrEmpty(HoursLimitProp))
            {
                List<int> TimeSplit = HoursLimitProp.Split(':').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();

                var dateNow = DateTime.Now;
                RingTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, TimeSplit[0], TimeSplit[1], 00);

                AlreadyWorkedTime = StartFromSecConfProp * -1;
            }

            StartTimeWholeDay = DateTime.Now;

            DateTime SecondsAlreadyPassed = StartTimeWholeDay.AddSeconds(AlreadyWorkedTime);

            _dailyTimer = new DispatcherTimer(DispatcherPriority.Render);
            _dailyTimer.Interval = TimeSpan.FromSeconds(1);
            _dailyTimer.Tick += (sender, e) => { DailyTimer_Tick(sender, e, SecondsAlreadyPassed, RingTime); }; ;
            _dailyTimer.Start();
        }

        private void DailyTimer_Tick(object sender, EventArgs e, DateTime SecondsAlreadyPassed, DateTime RingTime)
        {
            Debug.WriteLine("Current time: " + CurrentTime);
            Debug.WriteLine("Ring time: " + RingTime.ToLongTimeString());

            if (CurrentTime == RingTime.ToLongTimeString())
            {
                PlaySound();
            }

            CurrentTime = (DateTime.Now - SecondsAlreadyPassed).ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()
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
