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
        //ViewModel VM = new ViewModel();
        private ViewModel ViewModel { get; }

        public App()
        {
        }

        private void Run(object sender, StartupEventArgs e)
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            var mainWindow = new SettingsExampleMainView() {DataContext = new SettingsExampleViewModel(new UserDataProvider())};
            mainWindow.Show();
        }

        public void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                Debug.Print("I am locked: " + DateTime.Now);
                ViewModel.newProcess.TogglePause();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Debug.Print("I am unlocked: " + DateTime.Now);
                ViewModel.newProcess.TogglePause();
            }
        }
    }
}
