using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FamilyFeud.MVVM.ViewModel;
using FamilyFeud.MVVM.View;

namespace FamilyFeud
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow() { DataContext = new MainWindowViewModel() };
            window.Show();

            base.OnStartup(e);
        }
    }
}
