using System.Windows.Controls;
using YetAnotherContactApp.MVVM.ViewModel;

namespace YetAnotherContactApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            ContactApi.InitializeClient();

            DataContext = new MainViewModel();
        }
    }
}
