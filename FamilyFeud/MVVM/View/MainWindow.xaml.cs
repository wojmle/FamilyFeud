using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FamilyFeud.MVVM.Model;
using FamilyFeud.MVVM.ViewModel;
using Wpf.Ui.Controls;

namespace FamilyFeud.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow
    {
        public MainWindow()
        {
            var game = new Game("TEAM1", "TEAM2");
            var viewModel = new MainWindowViewModel(game);
            ChildWindow childWindow = new ChildWindow();
            childWindow.DataContext = viewModel;
            childWindow.Show();
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
