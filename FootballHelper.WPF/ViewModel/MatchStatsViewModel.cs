using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using FootballHelper.Logic;
using static System.Formats.Asn1.AsnWriter;
using static System.Reflection.Metadata.BlobBuilder;

namespace FootballHelper.WPF.ViewModel
{
    public class MatchStatsViewModel : ViewModelBase
    {
        private IRepository repo;
        private MainWindowViewModel mvVM;

        private Match match;
        private int[] goals;
        // [0] - home ; [1] - away
        private List<ClubStatisticsPerMatch> clubsStats;

        private ObservableCollection<PlayerStatsShortViewModel> homePlayersStats;
        private ObservableCollection<PlayerStatsShortViewModel> awayPlayersStats;

        private PlayerStatsShortViewModel selectedPlayerHome;
        private PlayerStatsShortViewModel selectedPlayerAway;

        public MatchStatsViewModel(IRepository repo, Match match, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.match = match;
            this.mvVM = mvVM;

        }

        public async Task InitializeAsync()
        {
            clubsStats = await repo.GetClubsStatsForMatchAsync(match);
            var statsPerMatch = await repo.GetGoalsForMatchAsync(match);
            goals = match.CalculateFinalScore(statsPerMatch);

            List<PlayerStatisticsPerMatch> alPlStForMatch = await repo.GetAllPlayerStatisticsForMatchAsync(match);

            IEnumerable<PlayerStatsShortViewModel> homePlayersStatsVM =
                from plStatHome in match.GetPlayerStatsForHomeClub(alPlStForMatch)
                select new PlayerStatsShortViewModel(repo, plStatHome, match);

            IEnumerable<PlayerStatsShortViewModel> awayPlayersStatsVM =
                from plStatAway in match.GetPlayerStatsForAwayClub(alPlStForMatch)
                select new PlayerStatsShortViewModel(repo, plStatAway, match);

            homePlayersStats = new ObservableCollection<PlayerStatsShortViewModel>(homePlayersStatsVM);
            awayPlayersStats = new ObservableCollection<PlayerStatsShortViewModel>(awayPlayersStatsVM);
        }

        public List<ClubStatisticsPerMatch> ClubsStats
        {
            get => clubsStats;
            set
            {
                clubsStats = value;
            }
        }
    public ObservableCollection<PlayerStatsShortViewModel> HomePlayersStats
        {
            get => homePlayersStats;
            set
            {
                homePlayersStats = value;
            }
        }

        public ObservableCollection<PlayerStatsShortViewModel> AwayPlayersStats
        {
            get => awayPlayersStats;
            set
            {
                awayPlayersStats = value;
            }
        }

        public PlayerStatsShortViewModel SelectedPlayerHome
        {
            get => selectedPlayerHome;
            set
            {
                if (selectedPlayerHome == value) return;
                selectedPlayerHome = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisibleH));
            }
        }

        public PlayerStatsShortViewModel SelectedPlayerAway
        {
            get => selectedPlayerAway;
            set
            {
                if (selectedPlayerAway == value) return;
                selectedPlayerAway = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisibleA));
            }
        }

        public string HomeClubName
        {
            get => $"{match.HomeClub.Name}";
        }
        public string HomeClubPossesion
        {
            get => $"Владение: {clubsStats[0].Possession}";
        }
        public string HomeClubCorners
        {
            get => $"Угловые: {clubsStats[0].Corners}";
        }
        public string HomeClubOffsides
        {
            get => $"Оффсайды: {clubsStats[0].Offsides}";
        }
        public string HomeGoals
        {
            get => $"{goals[0]}";
        }

        public string AwayClubName
        {
            get => $"{match.AwayClub.Name}";
        }
        public string AwayClubPossesion
        {
            get => $"Владение: {clubsStats[1].Possession}";
        }
        public string AwayClubCorners
        {
            get => $"Угловые: {clubsStats[1].Corners}";
        }
        public string AwayClubOffsides
        {
            get => $"Оффсайды: {clubsStats[1].Offsides}";
        }
        public string AwayGoals
        {
            get => $"{goals[1]}";
        }

        public ICommand AddStatHome
        {
            get
            {
                return new RelayCommand(
                    async (_) => await AddStatHomeImplAsync());
            }
        }

        public async Task AddStatHomeImplAsync()
        {
            Player mockPlayer = new Player(-1, "null", "null", new DateOnly(0001, 1, 1), "null");
            PlayerStatisticsPerMatch plStat = new PlayerStatisticsPerMatch(-1, mockPlayer, match, 0, 0, 0, 0, false, false);

            var playerAdd = new PlayerAddStatsViewModel(repo, plStat, match, match.HomeClub, mvVM);
            await playerAdd.InitializeAsync();
            mvVM.Content = playerAdd;
        }

        public ICommand AddStatAway
        {
            get
            {
                return new RelayCommand(
                    async (_) => await AddStatAwayImplAsync());
            }
        }

        public async Task AddStatAwayImplAsync()
        {
            Player mockPlayer = new Player(-1, "null", "null", new DateOnly(0001, 1, 1), "null");
            PlayerStatisticsPerMatch plStat = new PlayerStatisticsPerMatch(-1, mockPlayer, match, 0, 0, 0, 0, false, false);

            var playerAdd = new PlayerAddStatsViewModel(repo, plStat, match, match.AwayClub, mvVM);
            await playerAdd.InitializeAsync();
            mvVM.Content = playerAdd;
        }

        public ICommand ChangeStatHome
        {
            get
            {
                return new RelayCommand(
                    (_) => ChangeHomePlayerImpl(),
                    (_) => CanShowHomeChange());
            }
        }
        public void ChangeHomePlayerImpl()
        {
            PlayerStatisticsPerMatch plStat = selectedPlayerHome.PlayerStatistic;
            mvVM.Content = new PlayerChangeStatsViewModel(repo, plStat, match, mvVM);
        }

        public ICommand ChangeMatchStat
            => new RelayCommand((_) => ChangeMatchStatImpl());

        private void ChangeMatchStatImpl()
        {
            mvVM.Content = new MatchChangeStatsViewModel(repo, match, clubsStats, mvVM);
        }

        public ICommand ChangeStatAway
        {
            get
            {
                return new RelayCommand(
                    (_) => ChangeAwayPlayerImpl(),
                    (_) => CanShowAwayChange());
            }
        }
        public void ChangeAwayPlayerImpl()
        {
            PlayerStatisticsPerMatch plStat = selectedPlayerAway.PlayerStatistic;
            mvVM.Content = new PlayerChangeStatsViewModel(repo, plStat, match, mvVM);
        }

        private bool CanShowHomeChange()
        {
            return selectedPlayerHome != null;
        }

        private bool CanShowAwayChange()
        {
            return selectedPlayerAway != null;
        }

        public bool IsVisibleH => CanShowHomeChange();
        public bool IsVisibleA => CanShowAwayChange();

    }
}
