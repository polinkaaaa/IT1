using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LinearListViewModel<string>(); // Измените тип, если необходимо
        }
    }
}
