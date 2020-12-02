using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DispatherTimer
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
    }
}
