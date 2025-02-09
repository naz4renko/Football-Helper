using FootballHelper.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using static System.Reflection.Metadata.BlobBuilder;

namespace FootballHelper.WPF.ViewModel
{
    internal class PlayerAddStatsViewModel : ViewModelBase
    {
        private IRepository repo;
        private Match match;
        private Club club;
        private readonly MainWindowViewModel mvVM;
        private PlayerStatisticsPerMatch plStat;

        private string _selectedYellowCardOption;
        private string _selectedRedCardOption;

        public ObservableCollection<string> YellowCardOptions { get; }
        public ObservableCollection<string> RedCardOptions { get; }

        private ObservableCollection<PlayerShortViewModel> players;
        private PlayerShortViewModel selectedPlayer;

        private int goals;
        private int assists;
        private int shoots;
        private int tackling;

        private bool _isButtonEnabled = true;
        private string _buttonText = "Подтвердить";
        private Brush buttonTextColor;

        public PlayerAddStatsViewModel(IRepository repo, PlayerStatisticsPerMatch plStat, Match match, Club club, MainWindowViewModel mvVM)
        {
            this.repo = repo;
            this.plStat = plStat;
            this.match = match;
            this.club = club;
            this.mvVM = mvVM;

            YellowCardOptions = new ObservableCollection<string> { "Да", "Нет" };
            RedCardOptions = new ObservableCollection<string> { "Да", "Нет" };

            SelectedYellowCardOption = "Нет";
            SelectedRedCardOption = "Нет";

            goals = plStat.Goals;
            assists = plStat.Assists;
            shoots = plStat.Shoots;
            tackling = plStat.Tackling;

            ButtonTextColor = Brushes.Black;
        }

        public async Task InitializeAsync()
        {
            IEnumerable<Player> playersList = await repo.GetPlayersWithoutStatisticsInMatchForClubAsync(match, club);
            IEnumerable<PlayerShortViewModel> playersVM = playersList.Select(player => new PlayerShortViewModel(player));
            players = new ObservableCollection<PlayerShortViewModel>(playersVM);
        }

        public ObservableCollection<PlayerShortViewModel> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged(nameof(Players));
            }
        }

        public PlayerShortViewModel SelectedPlayer
        {
            get => selectedPlayer;
            set
            {
                if (selectedPlayer == value) return;
                selectedPlayer = value;
                OnPropertyChanged();
            }
        }

        public int Goals
        {
            get => goals;
            set
            {
                if (goals == value) return;
                goals = value;
                OnPropertyChanged();
            }
        }

        public int Assists
        {
            get => assists;
            set
            {
                if (assists == value) return;
                assists = value;
                OnPropertyChanged();
            }
        }

        public int Shoots
        {
            get => shoots;
            set
            {
                if (shoots == value) return;
                shoots = value;
                OnPropertyChanged();
            }
        }

        public int Tackling
        {
            get => tackling;
            set
            {
                if (tackling == value) return;
                tackling = value;
                OnPropertyChanged();
            }
        }

        public string SelectedYellowCardOption
        {
            get => _selectedYellowCardOption;
            set
            {
                if (_selectedYellowCardOption != value)
                {
                    _selectedYellowCardOption = value;
                    OnPropertyChanged(nameof(SelectedYellowCardOption));
                    OnPropertyChanged(nameof(YellowCard));
                }
            }
        }

        public string SelectedRedCardOption
        {
            get => _selectedRedCardOption;
            set
            {
                if (_selectedRedCardOption != value)
                {
                    _selectedRedCardOption = value;
                    OnPropertyChanged(nameof(SelectedRedCardOption));
                    OnPropertyChanged(nameof(RedCard));
                }
            }
        }
        public bool YellowCard
        {
            get => SelectedYellowCardOption == "Да";
            set
            {
                SelectedYellowCardOption = value ? "Да" : "Нет";
                OnPropertyChanged(nameof(SelectedYellowCardOption));
            }
        }

        public bool RedCard
        {
            get => SelectedRedCardOption == "Да";
            set
            {
                SelectedRedCardOption = value ? "Да" : "Нет";
                OnPropertyChanged(nameof(SelectedRedCardOption));
            }
        }

        public ICommand AddStats
                => new RelayCommand(
                    (_) => AddStatsImpl(),
                    (_) => CanShowButton());

        private void AddStatsImpl()
        {
            
            PlayerStatisticsPerMatch newPlStat = new PlayerStatisticsPerMatch(-1, selectedPlayer.Player, match, Goals, Assists, Shoots, Tackling, YellowCard, RedCard);
            if (CheckStats(newPlStat))
            {
                repo.InsertPlayerStatistic(newPlStat);
                ButtonTextColor = Brushes.Green;
                ButtonText = "Успешно!";
                IsButtonEnabled = false;
            }
            else
            {
                ButtonTextColor = Brushes.Red;
                ButtonText = "Ошибка!!";
            }

        }

        private bool CanShowButton()
        {
            if (selectedPlayer != null)
                return true;
            return false;
        }

        public ICommand ToMatch
            => new RelayCommand(async (_) => await ToMatchImplAsync());

        private async Task ToMatchImplAsync()
        {
            var workMatch = new MatchStatsViewModel(repo, match, mvVM);
            await workMatch.InitializeAsync();
            mvVM.Content = workMatch;
        }

        private bool CheckStats(PlayerStatisticsPerMatch plStats)
        {
            bool result = true;
            if (plStats.Assists < 0 || plStats.Shoots < 0 || plStats.Goals < 0 || plStats.Tackling < 0)
            {
                return false;

            }
            return result;
        }


        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                if (_isButtonEnabled != value)
                {
                    _isButtonEnabled = value;
                    OnPropertyChanged(nameof(IsButtonEnabled));
                }
            }
        }
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged(nameof(ButtonText));
                }
            }
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
