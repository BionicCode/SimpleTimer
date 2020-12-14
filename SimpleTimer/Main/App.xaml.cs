using Microsoft.Win32;
using SimpleTimer.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;

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
            //StartWorkingTimeTodayTimer();
            // INFO::This is an application/system event. it should be handled at higher levels e.g. App.xaml.cs. App.xaml.cs also handles unhandled exceptions etc. It has a global/application scope which is more suited to listen to system event from an architectural point of view. If you are interested I can show you how to bootstrap your application manually. It's quite simple (I promise) and is a next step before trying Dependency Injection, which you can't avoid very much longer :) 
            // Time to approach new stages. You are ready in my opinion.
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
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
