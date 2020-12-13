using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SimpleTimer.HelperClass
{
    public static class HelperClass
    {
        public static DateTime DateTimeConverter(string TimeAsString)
        {
            DateTime dt;
            if (!DateTime.TryParseExact(TimeAsString, "HH:mm", CultureInfo.InvariantCulture,
                                                          DateTimeStyles.None, out dt))
            {
                // handle validation error
            }
            TimeSpan time = dt.TimeOfDay;

            return dt;
        }

        public static TimeSpan ParseToTimeSpan(string TimeAsString)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 0);

            if (!string.IsNullOrEmpty(TimeAsString))
            {
                List<int> TimeSplit = TimeAsString.Split(':').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                timeSpan = new TimeSpan(0, TimeSplit[0], TimeSplit[1], 0, 0);
            }

            return timeSpan;
        }

        public static TimeSpan ParseToTimeSpanRingTime()
        {
            string TimeAsString = Model.Properties.Appsettings.Default["TimeSetting"];

            TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 0);

            if (!string.IsNullOrEmpty(TimeAsString))
            {
                List<int> TimeSplit = TimeAsString.Split(':').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                timeSpan = new TimeSpan(0, TimeSplit[0], TimeSplit[1], 0, 0);
            }

            //Debug.WriteLine("Ring time: " + timeSpan);

            return timeSpan;
        }

        private static void PlaySound()
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
