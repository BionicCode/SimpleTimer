namespace SimpleTimer.Models.HelperClasses
{
    class SessionSwitchListener
    {

        //// Set the timer offset to 50 seconds
        //private static readonly TimeSpan StartFromSecConfProp = TimeSpan.FromSeconds(50);
        //private TimeSpan SecondsAlreadyPassed = TimeSpan.Zero;

        //private static Action _timerAction;
        //private static TimeSpan SecondsBetweenRun;

        ////ExecutableProcess executableProcess = new ExecutableProcess();
        //ExecutableProcess newProcess = new ExecutableProcess(SecondsBetweenRun, _timerAction);

        //private TimeSpan BackgroundWorkTimerInterval { get; set; }
        //private TimeSpan LabelTimerInterval { get; set; }

        //public SessionSwitchListener()
        //{
        //    // We specify this method to be executed every 1 srcond // BACKGROUND WORK
        //    this.BackgroundWorkTimerInterval = TimeSpan.FromSeconds(1);
        //    var process = new ExecutableProcess(this.BackgroundWorkTimerInterval, MyProcessToExecute);
        //    process.Start();

        //    // We specify this method to be executed every 1 srcond // DISPLAYED IN LABEL
        //    this.LabelTimerInterval = TimeSpan.FromSeconds(1);
        //    //newProcess = new ExecutableProcess(this.LabelTimerInterval, LabelTimer);
        //    newProcess.Start();
        //}

        //private static void MyProcessToExecute()
        //{
        //    Debug.Write($"Running {DateTime.Now}" + "\n");
        //}

        //void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        //{
        //    if (e.Reason == SessionSwitchReason.SessionLock)
        //    {
        //        Debug.Print("I am locked: " + DateTime.Now);
        //        newProcess.TogglePause();
        //    }
        //    else if (e.Reason == SessionSwitchReason.SessionUnlock)
        //    {
        //        Debug.Print("I am unlocked: " + DateTime.Now);
        //        newProcess.TogglePause();
        //    }
        //}

        //private void PauseTimer()
        //{
        //    newProcess.TogglePause();
        //}

        //private void PlaySound()
        //{
        //    try
        //    {
        //        System.Media.SoundPlayer player = new System.Media.SoundPlayer(string.Concat(System.IO.Directory.GetCurrentDirectory(),
        //                @"\Miscellaneous\Sounds\bell.wav"));
        //        player.Play();

        //        Debug.WriteLine("+++++++++ bell ring +++++++++");

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
