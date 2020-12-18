using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using SimpleTimer.Models.HelperClasses;

namespace SimpleTimer.Resources.Converter
{ 
    // HINT::2:Class access modifier is internal. In order to use this class outside this assembly e.g. in Main this class must be public
    // HINT::2.1: Make class public
    // class TimeExceededConverter : IValueConverter
    public class TimeExceededConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string HoursLimitProp = (string)value;

            TimeSpan res;
            var result = TimeSpan.TryParseExact(HoursLimitProp, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out res);

      // HINT::Missing import.
      // HINT::Add using SimpleTimer.Model.HelperClasses
      TimeSpan RingTime = HelperClass.ParseToTimeSpanRingTime();

            if (res > RingTime)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.LightGreen;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }
}
