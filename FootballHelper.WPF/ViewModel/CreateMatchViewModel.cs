using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FootballHelper.WPF.ViewModel
{
    public class CreateMatchViewModel : ViewModelBase
    {
        private IRepository repo;
        private MainWindowViewModel mvVM;
        private ObservableCollection<ClubShortViewModel> clubs;
        private ClubShortViewModel selectedHomeClub;
        private ClubShortViewModel selectedAwayClub;
        private DateTime dateOfTheMatch;

        private Brush buttonTextColor;


        public CreateMatchViewModel(IRepository repo, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.mvVM = mvVM;
            dateOfTheMatch = DateTime.Now;

            ButtonTextColor = Brushes.Black;
        }

        public async Task InitializeAsync()
        {
            IEnumerable<Club> clubList = await repo.GetClubsAsync();
            IEnumerable<ClubShortViewModel> clubsVMs = clubList.Select(club => new ClubShortViewModel(club, repo));
            clubs = new ObservableCollection<ClubShortViewModel>(clubsVMs);
        }

        public DateTime DateOfTheMatch
        {
            get => dateOfTheMatch;
            set
            {
                if (dateOfTheMatch == value) return;
                dateOfTheMatch = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ClubShortViewModel> Clubs
        {
            get => clubs;
            set
            {
                clubs = value;
            }
        }

        public ClubShortViewModel SelectedHomeClub
        {
            get => selectedHomeClub;
            set
            {
                if (selectedHomeClub == value) return;
                selectedHomeClub = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));

            }
        }

        public ClubShortViewModel SelectedAwayClub
        {
            get => selectedAwayClub;
            set
            {
                if (selectedAwayClub == value) return;
                selectedAwayClub = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand ComfirmCreate
        {
            get
            {
                return new RelayCommand(
                    (_) => ComfirmCreateImpl(),
                    (_) => CanShowButton());
            }
        }

        public void ComfirmCreateImpl()
        {
            Club homeClub = selectedHomeClub.Club;
            Club awayClub = selectedAwayClub.Club;

            if ((homeClub.ID != awayClub.ID)) 
            {
                Match match = new Match(-1, DateOnly.FromDateTime(dateOfTheMatch), homeClub, awayClub);

                ClubStatisticsPerMatch homeclst = new ClubStatisticsPerMatch(-1, homeClub, match, 0, 0, 0);
                ClubStatisticsPerMatch awayclst = new ClubStatisticsPerMatch(-1, awayClub, match, 0, 0, 0);
                List<ClubStatisticsPerMatch> workList = new List<ClubStatisticsPerMatch> { homeclst, awayclst };

                mvVM.Content = new MatchChangeStatsViewModel(repo, match, workList, mvVM);
            }
            else
            {
                ButtonTextColor = Brushes.Red;
            }
        }

        public ICommand ToMatches
            => new RelayCommand(async (_) => await ToMatchesImplAsync());

        private async Task ToMatchesImplAsync()
        {
            var matches = new MatchViewModel(repo, mvVM);
            await matches.InitializeAsync();
            mvVM.Content = matches;
        }

        public bool IsVisible => CanShowButton();
        private bool CanShowButton()
        {
            return (selectedHomeClub != null && selectedAwayClub != null);
        }


        public Brush ButtonTextColor
        {
            get => buttonTextColor;
            set
            {
                if (buttonTextColor != value)
                {
                    buttonTextColor = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
