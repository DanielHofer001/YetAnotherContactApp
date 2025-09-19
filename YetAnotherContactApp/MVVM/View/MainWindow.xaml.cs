using YetAnotherContactApp.MVVM.ViewModel;
using System.Windows;

namespace YetAnotherContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ContactApi.InitializeClient();
            DataContext = new MainViewModel();
        }

    }
}
