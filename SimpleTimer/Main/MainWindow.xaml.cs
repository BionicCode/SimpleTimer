using System.Windows;
using System.Windows.Input;
using SimpleTimer.ViewModels;

namespace SimpleTimer.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();

            // BUG::Missing import. ADDITIONALLY type ambiguity: type name (ViewModel) equals namespace (ViewModel).
            // FIX::Add using SimpleTimer.ViewModel inside namespace or FQN type to emphasize type name: new ViewModel.ViewModel() or rename namespace/type: use plural for namespace: Models, ViewModels. I recommend renaming the namespace (I did it for you)
            //this.DataContext = new ViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateTimeLimit();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
