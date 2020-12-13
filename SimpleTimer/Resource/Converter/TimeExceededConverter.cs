using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using SimpleTimer.HelperClass;

namespace SimpleTimer.Resource
{ 
    class TimeExceededConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string HoursLimitProp = (string)value;

            TimeSpan res;
            var result = TimeSpan.TryParseExact(HoursLimitProp, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out res);

            TimeSpan RingTime = HelperClass.ParseToTimeSpanRingTime();

            if (res > RingTime)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.Green;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }
}
