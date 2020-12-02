﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace DispatherTimer
{
    class TimeExceededConverter : IValueConverter
    {
        #region IValueConverter Members

        //private ViewModel ViewModel { get; }

        //public TimeExceededConverter(ViewModel viewModel)
        //{
        //    this.ViewModel = viewModel;
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = (string)value;

            DateTime RingTime = DateTime.Today;
            var dateNow = DateTime.Now;

            var SVM = new ViewModel();

            if (!string.IsNullOrEmpty(SVM.HoursLimitProp))
            {
                List<int> TimeSplit = SVM.HoursLimitProp.Split(':').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                RingTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, TimeSplit[0], TimeSplit[1], 00);
            }

            TimeSpan res;
            var result = TimeSpan.TryParseExact(text, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out res);

            if (res < RingTime.TimeOfDay)
            {
                return Brushes.Green;
            }

            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }
}
