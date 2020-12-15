using System;
using System.ComponentModel;

namespace SimpleTimer.ViewModels
{
  public interface ISettingsExampleViewModel : INotifyPropertyChanged
  {
    DateTime LastLogin { get; set; }
    string UserAlias { get; set; }
    string UserEmail { get; set; }
  }
}