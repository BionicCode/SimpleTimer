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

        private TimerViewModel TimerViewModel { get; }

        private void Run(object sender, StartupEventArgs e)
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            var mainWindow = new MainWindow() {
                DataContext = new ViewModel(new GeneralDataProvider())
            };

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
