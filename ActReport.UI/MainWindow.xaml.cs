using System.Windows;
using ViewModel;

namespace ActReport.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel();
        }
    }
}
