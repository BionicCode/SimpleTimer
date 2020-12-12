using System.Windows;
using SimpleTimer.ViewModel;

namespace SimpleTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Appsettings.Default["TimeSetting"] = HoursLimitBox.Text;
            //Properties.Appsettings.Default.Save();
        }
    }
}
