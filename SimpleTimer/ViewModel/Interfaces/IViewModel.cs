using System.ComponentModel;

namespace SimpleTimer.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string HoursLimitProp { get; set; }
    }
}
