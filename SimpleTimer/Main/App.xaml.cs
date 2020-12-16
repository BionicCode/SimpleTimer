using Microsoft.Win32;
using SimpleTimer.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using SimpleTimer.Models;

namespace SimpleTimer.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        // BUG::Since this property is initialized from the constructor it can't be read-only and needs a set method
        // private TimerViewModel TimerViewModel { get; }
        private TimerViewModel TimerViewModel { get; set; }

        private void Run(object sender, StartupEventArgs e)
        {
            // INFO::No need to define the handler delegate explicitly
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            // Use the short version instead
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;

            // BUG::Make sure the TimerViewModel instance used by ViewModel and by App are the same. Please take a look at the constructor of ViewModel. In the comments (tagged with INFO) I suggest two variants of type construction. Please decide for one and chose the instantiation (below) accordingly.


            /**************************** Variant 1  */
            var sharedTimerViewModel = new TimerViewModel();
            var mainWindow = new MainWindow()
            {
                DataContext = new ViewModel(new GeneralDataProvider(), sharedTimerViewModel)
            };
            this.TimerViewModel = sharedTimerViewModel;


            /**************************** End of variant 1  */

            /**************************** Variant 2  */
            var viewModel = new ViewModel();
            var mainWindow = new MainWindow()
            {
                DataContext = viewModel
            };

            // Get instance via public property
            this.TimerViewModel = viewModel.TimerViewModel;

            /**************************** End of variant 2  */

            // INFO::First window shown will be automatically assigned to Application.MainWindow.
            // Otherwise you would have to assign: this.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                Debug.Print("I am locked: " + DateTime.Now);
                TimerViewModel.newProcess.TogglePause();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Debug.Print("I am unlocked: " + DateTime.Now);
                TimerViewModel.newProcess.TogglePause();
            }
        }
    }
}
