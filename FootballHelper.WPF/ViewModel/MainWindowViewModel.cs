using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballHelper.DataBase;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FootballHelper.WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase content;
        private IRepository repo;

        private ViewModelBase table;
        private ViewModelBase matches;

        public MainWindowViewModel(IRepository repository)
        {
            repo = repository;
        }

        public ViewModelBase Content
        {
            get => content;
            set
            {
                if (content == value) return;
                content = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowTable
        {
            get
            {
                return new RelayCommand(
                   async (_) => await ShowTableImplAsync());
            }
        }

        private async Task ShowTableImplAsync()
        {
            var table = new TableViewModel(repo, this);
            await table.InitializeAsync();
            this.Content = table;
        }

        public ICommand ShowMatches
        {
            get
            {
                return new RelayCommand(
                    async (_) => await ShowMatchesImplAsync());
            }
        }

        private async Task ShowMatchesImplAsync()
        {
            var matches = new MatchViewModel(repo, this);
            await matches.InitializeAsync();
            this.Content = matches;       
        }
    }
}
