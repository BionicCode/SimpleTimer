using Microsoft.Win32;
using SimpleTimer.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using SimpleTimer.Models;
using log4net;
using System.Reflection;

namespace SimpleTimer.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILog Logger { get; } =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public App()
        {
        }

        public ViewModel ViewModel { get; set; }

        private void Run(object sender, StartupEventArgs e)
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(this.SystemEvents_SessionSwitch);
            
            this.ViewModel = new ViewModel(new GeneralDataProvider());

            var mainWindow = new MainWindow() {
                DataContext = ViewModel
            };

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
