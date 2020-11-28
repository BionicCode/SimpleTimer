using System;
using System.Windows.Threading;

namespace DispatherTimer
{
    class ViewModel : BaseViewModel
    {
        public ViewModel()
        {
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

        private static DateTime StartTimeWholeDay;
        private DispatcherTimer _dailyTimer;
        public void StartWorkingTimeTodayTimer()
        {
            StartTimeWholeDay = DateTime.Now;
            DateTime x30MinsLater = StartTimeWholeDay.AddSeconds(-50); // -50 to start timer from 00:00:50

            _dailyTimer = new DispatcherTimer(DispatcherPriority.Render);
            _dailyTimer.Interval = TimeSpan.FromSeconds(1);
            _dailyTimer.Tick += (sender, args) =>
            {
                CurrentTime = (DateTime.Now - x30MinsLater).ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()
            };
            _dailyTimer.Start();
        }
    }
}
