using SimpleTimer.Models;
using SimpleTimer.Models.HelperClasses;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SimpleTimer.ViewModels
{
    public class ViewModel : IViewModel
    {
        //private ExecutableProcess ExecutableProcess { get; set; }

        // Set the timer offset to 50 seconds
        private static readonly TimeSpan StartFromSecConfProp = TimeSpan.FromSeconds(50);
        private TimeSpan SecondsAlreadyPassed = TimeSpan.Zero;

        private static Action _timerAction;
        private static TimeSpan SecondsBetweenRun;

        // ExecutableProcess executableProcess = new ExecutableProcess();
        public ExecutableProcess newProcess = new ExecutableProcess(ViewModel.SecondsBetweenRun, ViewModel._timerAction);

        public ICommand PauseTimerCommand => new RelayCommand(param => PauseTimer());

        public ICommand UpdateTimeLimitCommand => new RelayCommand(param => UpdateTimeLimit());

        private TimeSpan BackgroundWorkTimerInterval { get; set; }
        private TimeSpan LabelTimerInterval { get; set; }

        private IGeneralDataProvider GeneralDataProvider { get; }

        public void UpdateTimeLimit() => this.GeneralDataProvider.SetTimeLimit(this.HoursLimitProp);

        public ViewModel(IGeneralDataProvider generalDataProvider)
        {
            this.GeneralDataProvider = generalDataProvider;

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

            Initialize();
        }

        private void Initialize()
        {
            this.HoursLimitProp = this.GeneralDataProvider.GetTimeLimit();
        }

        private void PauseTimer()
        {
            this.newProcess.TogglePause();
        }

        private void LabelTimer()
        {
            this.SecondsAlreadyPassed = this.SecondsAlreadyPassed.Add(this.LabelTimerInterval);
            this.CurrentTime = this.SecondsAlreadyPassed.ToString(@"hh\:mm\:ss"); // DateTime.Now.ToLongTimeString()

            // BUG::Wrong namespace. After refactoring (moving types to new projects/folders) you forgot to adjust the namespaces, 
            // so the type DateTimeConverter could not be resolved.
            // Old namespace: SimpleTimer.HelperClass. Fixed namespace:SimpleTimer.Model.HelperClass. I lived up to the occasion 
            // to rename the namespace/folder from ..Calsses to ..Classes (fixed typo)
            DateTime RingTime = HelperClass.DateTimeConverter(this.HoursLimitProp);

            //Debug.WriteLine("Current time: " + CurrentTime);
            //Debug.WriteLine("Ring time: " + RingTime.ToLongTimeString());

            if (this.CurrentTime == RingTime.ToLongTimeString())
            {
                // BUG::Not implemented.
                // FIX::Created method. Implementation pending.
                HelperClass.PlaySound();
            }
        }

        private static void MyProcessToExecute()
        {
            Debug.Write($"Running {DateTime.Now}" + "\n");
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

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Implementation of INotifyPropertyChanged
    }
}
