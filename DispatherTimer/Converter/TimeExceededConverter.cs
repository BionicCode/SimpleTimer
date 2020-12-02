using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DispatherTimer
{
    class TimeExceededConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string HoursLimitProp = (string)value;

            DateTime dt = HelperClass.DateTimeConverter(HoursLimitProp);

            TimeSpan timeSpan = HelperClass.ParseToTimeSpan(HoursLimitProp);

            TimeSpan time = dt.TimeOfDay;

            if (time < timeSpan)
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
