using FootballHelper.DataBase;
using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace FootballHelper.WPF.ViewModel
{
    class TableViewModel : ViewModelBase
    {
        private IRepository repo;
        private MainWindowViewModel mvVM;
        private ObservableCollection<ClubShortViewModel> clubs;
        private ClubShortViewModel selectedClub;

        public TableViewModel(IRepository repo, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.mvVM = mvVM;
        }

        public async Task InitializeAsync()
        {
            IEnumerable<Club> clubList = await repo.GetClubsAsync();
            IEnumerable<ClubShortViewModel> clubsVMs = clubList.Select(club => new ClubShortViewModel(club, repo));
            clubs = new ObservableCollection<ClubShortViewModel>(clubsVMs);
        }

        public ObservableCollection<ClubShortViewModel> Clubs
        {
            get => clubs;
            set
            {
                clubs = value;
            }
        }

        public ClubShortViewModel SelectedClub
        {
            get => selectedClub;
            set
            {
                if (selectedClub == value) return;
                selectedClub = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand ShowClub
        {
            get
            {
                return new RelayCommand(
                    (_) => ShowClubImpl(),
                    (_) => CanShowClub());
            }
        }

        private void ShowClubImpl()
        {
            Club club = selectedClub.Club;
            mvVM.Content = new ClubViewModel(repo, club, mvVM);
        }

        private bool CanShowClub()
        {
            return selectedClub != null;
        }

        public ICommand ShowStats
        {
            get
            {
                return new RelayCommand(
                    async (_) => await ShowStatsImplAsync());
            }
        }

        private async Task ShowStatsImplAsync()
        {
            var clubStats = new ClubStatsViewModel(repo, selectedClub.Club, mvVM);
            await clubStats.InitializeAsync();
            mvVM.Content = clubStats;
        }

        public ICommand AddClub
        {
            get
            {
                return new RelayCommand(
                    (_) => ToAddClubImpl());
            }
        }
        private void ToAddClubImpl()
        {
            mvVM.Content = new CreateClubViewModel(repo, mvVM);
        }

        public bool IsVisible => CanShowClub();
    }
}
