using System.Windows;
using BirdMaker.ViewModels;

namespace BirdMaker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}
