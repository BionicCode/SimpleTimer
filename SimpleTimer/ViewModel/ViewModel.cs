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
        // BUG::Good to use a property (don't use public fields!). But you don't want to mutate the value of this property (prevent unwanted side effects). Therefore make the sett private (as this property is set internally or via constructor). If this property is not intended to ever change after initialization, make it read-only by removing the set method. If this property is not meant to accessed outside this class make it private too:
        // public TimerViewModel TimerViewModel { get; set; }
        public TimerViewModel TimerViewModel { get; }

    // Set the timer offset to 50 seconds
    private static readonly TimeSpan StartFromSecConfProp = TimeSpan.FromSeconds(50);

        // BUG::Turn into property and move initialization to constructor (optional but recommended)
        private TimeSpan SecondsAlreadyPassed { get; set; }

        public ICommand PauseTimerCommand => new RelayCommand(param => PauseTimer());

        public ICommand UpdateTimeLimitCommand => new RelayCommand(param => UpdateTimeLimit());

        private TimeSpan BackgroundWorkTimerInterval { get; set; }
        private TimeSpan LabelTimerInterval { get; set; }

        private IGeneralDataProvider GeneralDataProvider { get; }

        public void UpdateTimeLimit() => this.GeneralDataProvider.SetTimeLimit(this.HoursLimitProp);

        // INFO::Alternative version (my preference): inject all the dependencies. But don't mix it: either pass all dependencies to the constructor (variant 1) or instantiate all dependencies inside this class using 'new' (variant 2). The alternative constructor and TimerViewModel initialization could look like this:
        public ViewModel(IGeneralDataProvider generalDataProvider, TimerViewModel timerViewModel)
        {
            this.GeneralDataProvider = generalDataProvider;

            // Alternative version (variant 1)
            this.TimerViewModel = timerViewModel;
      
            // Original version (variant 2)
            //this.TimerViewModel = new TimerViewModel();

            this.SecondsAlreadyPassed = TimeSpan.Zero;

      // Initialize the current time elapsed field by adding the offset
      this.SecondsAlreadyPassed = this.SecondsAlreadyPassed.Add(ViewModel.StartFromSecConfProp);

            // We specify this method to be executed every 1 srcond // BACKGROUND WORK
            this.BackgroundWorkTimerInterval = TimeSpan.FromSeconds(1);
            var process = new ExecutableProcess(this.BackgroundWorkTimerInterval, ViewModel.MyProcessToExecute);
            process.Start();

            // We specify this method to be executed every 1 srcond // DISPLAYED IN LABEL
            this.LabelTimerInterval = TimeSpan.FromSeconds(1);
            TimerViewModel.NewProcess = new ExecutableProcess(this.LabelTimerInterval, LabelTimer);
            TimerViewModel.NewProcess.Start();

            Initialize();
        }

        private void Initialize()
        {
            this.HoursLimitProp = this.GeneralDataProvider.GetTimeLimit();
        }

        private void PauseTimer()
        {
            this.TimerViewModel.NewProcess.TogglePause();
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
