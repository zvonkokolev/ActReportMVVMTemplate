using System.Windows;
using System.Windows.Controls;
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

        private void TbxFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if(((TextBox)sender).Text.Length < 3)
            {
                MessageBox.Show("Name soll mindestens 3 Buchstaben lang sein!");
            }
        }

        private void TbxLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length < 3)
            {
                MessageBox.Show("Nachname soll mindestens 3 Buchstaben lang sein!");
            }
        }
    }
}
