using FootballHelper.DataBase;
using FootballHelper.Logic;
using FootballHelper.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FootballHelper.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IRepository repository = new DataAccess();
            MainWindow win = new MainWindow();
            win.DataContext = new MainWindowViewModel(repository);
            win.Show();
        }
    }
}
